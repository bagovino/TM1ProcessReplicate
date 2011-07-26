using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows.Forms;
using log4net;
using log4net.Config;

namespace ProcessReplicate
{
    class TM1App : IDisposable
    {
        private IntPtr UserHandle;
        private IntPtr PoolHandle;

        private string CAMNamespace;
        private string CAMUsername;
        private SecureString CAMPassword;

        private string TM1Username;
        private SecureString TM1Password;

        private StatusStrip appss;
        private ProgressBar apppb;
        private ToolStripStatusLabel appsslbl;

        private bool disposed = false;

        private static readonly ILog LOG = LogManager.GetLogger("ProcessReplicate");

        private struct TM1Chore
        {
            public string Name;
            public IntPtr Handle;
            public int Active;
            public Dictionary<string, int> choresecurity;
        }

        public TM1App(string adminserv, string sslcertid)
        {
            TM1API.SystemFunctions.TM1APIInitialize();
            this.UserHandle = TM1API.SystemFunctions.TM1SystemOpen();
            this.PoolHandle = TM1API.ValueFunctions.TM1ValPoolCreate(this.UserHandle);
            this.SetAdminServer(adminserv, sslcertid);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                }

                this.LogoutFromAllServers();
                TM1API.ValueFunctions.TM1ValPoolDestroy(this.PoolHandle);
                TM1API.SystemFunctions.TM1SystemClose(this.UserHandle);
                TM1API.SystemFunctions.TM1APIFinalize();

                disposed = true;
            }
        }

        private void SetAdminServer(string adminserv, string sslcertid)
        {
            TM1API.AdminHostFunctions.TM1SystemAdminHostSet(this.UserHandle, adminserv);
            //TM1.AdminHostFunctions.TM1SystemSetAdminSSLCertAuthority(this.UserHandle, sslcertid);
        }

        public void SetVisualControls(StatusStrip ss, ToolStripStatusLabel sslbl, ProgressBar pb)
        {
            this.appss = ss;
            this.appsslbl = sslbl;
            this.apppb = pb;
        }

        public void SetCAMLoginCredentials(string cns, string user, SecureString pass)
        {
            this.CAMUsername = user;
            this.CAMNamespace = cns;
            this.CAMPassword = pass;
        }

        public void SetTM1LoginCredentials(string un, SecureString up)
        {
            this.TM1Username = un;
            this.TM1Password = up;
        }

        public int GetAuthenticationType(string server)
        {
            IntPtr tempptr = IntPtr.Zero;
            IntPtr serverstr = IntPtr.Zero;

            StringBuilder servername;

            int numservers = 0;
            int logintype = 0;
            int count;

            try
            {
                numservers = TM1API.SystemFunctions.TM1SystemServerNof(this.UserHandle);

                for (count = 1; count <= numservers; count++)
                {
                    servername = TM1API.SystemFunctions.TM1SystemServerName(this.UserHandle, count);

                    if (server.Equals(servername.ToString()))
                    {
                        serverstr = TM1API.ValueFunctions.TM1ValString(this.PoolHandle, server, server.Length);
                        tempptr = TM1API.SystemFunctions.TM1SystemGetServerConfig(this.PoolHandle, serverstr);
                        tempptr = TM1API.ValueFunctions.TM1ValArrayGet(this.UserHandle, tempptr, 1);
                        logintype = TM1API.ValueFunctions.TM1ValIndexGet(this.UserHandle, tempptr);

                        LOG.Info("Retrieved authentication type used by instance " + server + ". Value: " + logintype); 
                        break;
                    }
                }

                serverstr = IntPtr.Zero;
                tempptr = IntPtr.Zero;
            }
            catch (AccessViolationException ex)
            {
                LOG.Error("Could not retrieve authentication type used by" + server);
            }

            return logintype;
        }

        public bool LoginToServer(string server, int logintype)
        {
            IntPtr hServer = IntPtr.Zero;
            IntPtr hPool = IntPtr.Zero;
            IntPtr hCamArgs = IntPtr.Zero;
            IntPtr args = IntPtr.Zero;
            IntPtr cns = IntPtr.Zero;
            IntPtr cun = IntPtr.Zero;
            IntPtr cpw = IntPtr.Zero;
            IntPtr lun = IntPtr.Zero;
            IntPtr lup = IntPtr.Zero;

            IntPtr tempptr = IntPtr.Zero;
            IntPtr serverstr = IntPtr.Zero;
            IntPtr[] camarray;

            hServer = TM1API.SystemFunctions.TM1SystemServerHandle(this.UserHandle, server);

            if (hServer == IntPtr.Zero)
            {
                switch (logintype)
                {
                    case 1:
                        lun = TM1API.ValueFunctions.TM1ValString(this.PoolHandle, this.TM1Username, this.TM1Username.Length);

                        // This hopefully keeps the unencrypted password in memory only long enough allow logging in.
                        tempptr = Marshal.SecureStringToBSTR(this.TM1Password);
                        lup = TM1API.ValueFunctions.TM1ValStringEncrypt(this.PoolHandle, Marshal.PtrToStringBSTR(tempptr), Marshal.PtrToStringBSTR(tempptr).Length);
                        Marshal.ZeroFreeBSTR(tempptr);

                        hServer = TM1API.SystemFunctions.TM1SystemServerConnect(this.PoolHandle, TM1API.ValueFunctions.TM1ValString(this.PoolHandle, server, server.Length), lun, lup);
                        
                        break;
                    case 4:
                        cns = TM1API.ValueFunctions.TM1ValString(this.PoolHandle, this.CAMNamespace, this.CAMNamespace.Length);
                        cun = TM1API.ValueFunctions.TM1ValString(this.PoolHandle, this.CAMUsername, this.CAMUsername.Length);

                        // This hopefully keeps the unencrypted password in memory only long enough allow logging in.
                        tempptr = Marshal.SecureStringToBSTR(this.CAMPassword);
                        cpw = TM1API.ValueFunctions.TM1ValStringEncrypt(this.PoolHandle, Marshal.PtrToStringBSTR(tempptr), Marshal.PtrToStringBSTR(tempptr).Length);
                        Marshal.ZeroFreeBSTR(tempptr);

                        camarray = new IntPtr[] { cns, cun, cpw };
                        hCamArgs = TM1API.ValueFunctions.TM1ValArray(this.PoolHandle, camarray, 3);

                        hServer = TM1API.SystemFunctions.TM1SystemServerConnectWithCAMNamespace(this.PoolHandle, TM1API.ValueFunctions.TM1ValString(this.PoolHandle, server, server.Length), hCamArgs);

                        break;
                    default:
                        return false;
                }

                if (TM1API.ValueFunctions.TM1ValType(this.UserHandle, hServer) == TM1API.ValueFunctions.TM1ValTypeError())
                {
                    MessageBox.Show("TM1 Server " + server + " login failed.  Please make sure your credentials are valid.", "TM1 server connect failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LOG.Error("Could not log into instance " + server);
                    return false;
                }
                else
                {
                    LOG.Info("Successfully logged into instance " + server);
                    return true;
                }
            }
            else
            {
                // Already have a handle to this server so in theory we should already be logged in
                LOG.Info("Already logged into instance " + server);
                return true;
            }
        }

        public bool CheckServerLogin(string serv)
        {
            IntPtr hServer = IntPtr.Zero;

            hServer = TM1API.SystemFunctions.TM1SystemServerHandle(this.UserHandle, serv);

            if (hServer == IntPtr.Zero)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void LogoutFromAllServers()
        {
            int servers;
            StringBuilder server = new StringBuilder();
            IntPtr hServer = IntPtr.Zero;
            IntPtr hPool = IntPtr.Zero;
            IntPtr results = IntPtr.Zero;

            servers = TM1API.SystemFunctions.TM1SystemServerNof(this.UserHandle);

            for (int count = 1; count <= servers; count++)
            {
                server = TM1API.SystemFunctions.TM1SystemServerName(this.UserHandle, count);
                hServer = TM1API.SystemFunctions.TM1SystemServerHandle(this.UserHandle, server.ToString());

                if (hServer != IntPtr.Zero)
                {
                    results = TM1API.SystemFunctions.TM1SystemServerDisconnect(this.PoolHandle, hServer);

                    if (TM1API.ValueFunctions.TM1ValType(this.UserHandle, results) == TM1API.ValueFunctions.TM1ValTypeError())
                    {
                        //TODO: Warn user that an error occured while attempting to disconnect from a server.
                        LOG.Warn("Could not disconnect from instance " + server);
                    }

                    LOG.Info("Disconnect from instance " + server + " successful");
                }
            }
        }

        public List<string> GetServers()
        {
            List<string> servs = new List<string>();
            string servername;

            int numservers = 0;
            int count;

            numservers = TM1API.SystemFunctions.TM1SystemServerNof(this.UserHandle);

            for (count = 1; count <= numservers; count++)
            {
                servername = TM1API.SystemFunctions.TM1SystemServerName(this.UserHandle, count).ToString();
                servs.Add(servername);
            }

            LOG.Info("Retrieved list of available servers");
            return servs;
        }

        public List<string> GetCubesOnServer(string serv)
        {
            List<string> cubes = new List<string>();

            IntPtr hServer = IntPtr.Zero;
            IntPtr hCube = IntPtr.Zero;
            IntPtr hResult = IntPtr.Zero;
            IntPtr hName = IntPtr.Zero;
            IntPtr hIndex = IntPtr.Zero;

            int numcubes = 0;
            StringBuilder cubename;

            if (!this.CheckServerLogin(serv))
            {
                if (!this.LoginToServer(serv, this.GetAuthenticationType(serv)))
                {
                    return null;
                }
            }

            hServer = TM1API.SystemFunctions.TM1SystemServerHandle(this.UserHandle, serv);

            if (hServer != IntPtr.Zero)
            {
                hResult = TM1API.ObjectFunctions.TM1ObjectListCountGet(this.PoolHandle, hServer, TM1API.CubeFunctions.TM1ServerCubes());
                numcubes = TM1API.ValueFunctions.TM1ValIndexGet(this.UserHandle, hResult);


                for (int count = 1; count <= numcubes; count++)
                {
                    hIndex = TM1API.ValueFunctions.TM1ValIndex(this.PoolHandle, count);

                    hCube = TM1API.ObjectFunctions.TM1ObjectListHandleByIndexGet(this.PoolHandle, hServer, TM1API.CubeFunctions.TM1ServerCubes(), hIndex);
                    hName = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hCube, TM1API.ObjectFunctions.TM1ObjectName());
                    cubename = TM1API.ValueFunctions.TM1ValStringGet(this.UserHandle, hName);
                    cubes.Add(cubename.ToString());
                }
            }

            LOG.Info("Retrieved list of cubes on instance " + serv);
            return cubes;
        }

        public List<string> GetProcessesOnServer(string serv)
        {
            List<string> procs = new List<string>();

            IntPtr hServer = IntPtr.Zero;
            IntPtr hProcess = IntPtr.Zero;
            IntPtr hResult = IntPtr.Zero;
            IntPtr hName = IntPtr.Zero;
            IntPtr hIndex = IntPtr.Zero;

            int numprocs = 0;
            StringBuilder procname;

            if (!this.CheckServerLogin(serv))
            {
                if (!this.LoginToServer(serv, this.GetAuthenticationType(serv)))
                {
                    return null;
                }
            }

            hServer = TM1API.SystemFunctions.TM1SystemServerHandle(this.UserHandle, serv);

            if (hServer != IntPtr.Zero)
            {
                hResult = TM1API.ObjectFunctions.TM1ObjectListCountGet(this.PoolHandle, hServer, TM1API.ProcessFunctions.TM1ServerProcesses());
                numprocs = TM1API.ValueFunctions.TM1ValIndexGet(this.UserHandle, hResult);


                for (int count = 1; count <= numprocs; count++)
                {
                    hIndex = TM1API.ValueFunctions.TM1ValIndex(this.PoolHandle, count);

                    hProcess = TM1API.ObjectFunctions.TM1ObjectListHandleByIndexGet(this.PoolHandle, hServer, TM1API.ProcessFunctions.TM1ServerProcesses(), hIndex);
                    hName = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hProcess, TM1API.ObjectFunctions.TM1ObjectName());
                    procname = TM1API.ValueFunctions.TM1ValStringGet(this.UserHandle, hName);
                    procs.Add(procname.ToString());
                }
            }

            LOG.Info("Retrieved list of processes on instance " + serv);
            return procs;
        }

        public List<string> GetChoresOnServer(string serv)
        {
            List<string> chores = new List<string>();

            IntPtr hServer = IntPtr.Zero;
            IntPtr hChore = IntPtr.Zero;
            IntPtr hResult = IntPtr.Zero;
            IntPtr hName = IntPtr.Zero;
            IntPtr hIndex = IntPtr.Zero;

            int numprocs = 0;
            StringBuilder chorename;

            if (!this.CheckServerLogin(serv))
            {
                if (!this.LoginToServer(serv, this.GetAuthenticationType(serv)))
                {
                    return null;
                }
            }

            hServer = TM1API.SystemFunctions.TM1SystemServerHandle(this.UserHandle, serv);

            if (hServer != IntPtr.Zero)
            {
                hResult = TM1API.ObjectFunctions.TM1ObjectListCountGet(this.PoolHandle, hServer, TM1API.ChoreFunctions.TM1ServerChores());
                numprocs = TM1API.ValueFunctions.TM1ValIndexGet(this.UserHandle, hResult);


                for (int count = 1; count <= numprocs; count++)
                {
                    hIndex = TM1API.ValueFunctions.TM1ValIndex(this.PoolHandle, count);

                    hChore = TM1API.ObjectFunctions.TM1ObjectListHandleByIndexGet(this.PoolHandle, hServer, TM1API.ChoreFunctions.TM1ServerChores(), hIndex);
                    hName = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hChore, TM1API.ObjectFunctions.TM1ObjectName());
                    chorename = TM1API.ValueFunctions.TM1ValStringGet(this.UserHandle, hName);
                    chores.Add(chorename.ToString());
                }
            }

            LOG.Info("Retrieved list of chores on instance " + serv);
            return chores;
        }

        private string SelectProcessName(string process, string serv)
        {
            string destprocess = null;
            bool namechosen = false;
            int temp;

            IntPtr hName = IntPtr.Zero;
            IntPtr hProcess = IntPtr.Zero;
            IntPtr hServer = IntPtr.Zero;

            DialogResult res;
            ProcessNameDialog pnd = new ProcessNameDialog(process);

            while (!namechosen)
            {
                pnd.ShowDialog();
                destprocess = pnd.ProcessName;

                hServer = TM1API.SystemFunctions.TM1SystemServerHandle(this.UserHandle, serv);
                hName = TM1API.ValueFunctions.TM1ValString(this.PoolHandle, destprocess, 0);

                hProcess = TM1API.ObjectFunctions.TM1ObjectListHandleByNameGet(this.PoolHandle, hServer, TM1API.ProcessFunctions.TM1ServerProcesses(), hName);
                temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hProcess);

                if (temp == (int)TM1API.TM1ValTypes.TM1Object)
                {
                    res = MessageBox.Show("Process already exists on the destination server.  Would you like to overwrite the existing process?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (res == DialogResult.Yes)
                    {
                        namechosen = true;
                    }
                }
                else
                {
                    namechosen = true;
                }
            }

            pnd.Dispose();
            return destprocess;
        }
        public bool ReplicateProcess(string source, string dest, string process, int securitytype, int destnametype)
        {
            IntPtr hSourceServer = IntPtr.Zero;
            IntPtr hDestServer = IntPtr.Zero;
            IntPtr hSourceName = IntPtr.Zero;
            IntPtr hDestName = IntPtr.Zero;
            IntPtr hSourceProcess = IntPtr.Zero;
            IntPtr hDestProcess = IntPtr.Zero;
            IntPtr hTempProcess = IntPtr.Zero;
            IntPtr hTemp = IntPtr.Zero;
            IntPtr hResult = IntPtr.Zero;
            IntPtr hChores = IntPtr.Zero;
            IntPtr hChoreName = IntPtr.Zero;
            IntPtr hChore = IntPtr.Zero;
            IntPtr hChoreSteps = IntPtr.Zero;
            IntPtr hChoreProcesses = IntPtr.Zero;
            IntPtr hChoreProcess = IntPtr.Zero;
            IntPtr hTempChore = IntPtr.Zero;
            IntPtr hGroup = IntPtr.Zero;
            IntPtr hIndex = IntPtr.Zero;
            IntPtr hName = IntPtr.Zero;
            IntPtr hRight = IntPtr.Zero;
            IntPtr hProcCube = IntPtr.Zero;
            IntPtr hCellRef = IntPtr.Zero;
            IntPtr hGroups = IntPtr.Zero;
            IntPtr hValue = IntPtr.Zero;

            TM1Chore ch;
            string processname;
            string groupname;
            string destprocess;
            int numgroups;
            int choresteps = 0;
            int active = 0;
            int right;
            int temp;
            List<string> chores;
            List<TM1Chore> foundchores = new List<TM1Chore>();
            Dictionary<string, int> processsecurity = new Dictionary<string, int>();

            LOG.Info("Beginning replication of process " + process + " from instance " + source + " to instance " + dest + ".");
            
            if (!this.CheckServerLogin(source))
            {
                if (!this.LoginToServer(source, this.GetAuthenticationType(source)))
                {
                    LOG.Error("Could not log into the source instance.");
                    return false;
                }
            }

            if (!this.CheckServerLogin(dest))
            {
                if (!this.LoginToServer(dest, this.GetAuthenticationType(dest)))
                {
                    LOG.Error("Could not log into the destination instance.");
                    return false;
                }
            }

            /* NOTE: It seems like the TM1 API likes to take its time with actually loading information into the handles it generates to hold the process information.
             * As a work around to force it to behave as expected I put a ValType check on each call that retrieves a part of the source process, otherwise when setting
             * the property on the destination process it was being treated as empty.
             */
            apppb.Value = 0;

            if (destnametype == 1)
            {
                destprocess = SelectProcessName(process, dest);
            }
            else
            {
                destprocess = process;
            }

            LOG.Info("Using " + destprocess + " as the process name on the desintation instance.");
            
            hSourceServer = TM1API.SystemFunctions.TM1SystemServerHandle(this.UserHandle, source);
            hDestServer = TM1API.SystemFunctions.TM1SystemServerHandle(this.UserHandle, dest);

            hSourceName = TM1API.ValueFunctions.TM1ValString(this.PoolHandle, process, 0);
            hDestName = TM1API.ValueFunctions.TM1ValString(this.PoolHandle, destprocess, 0);

            hSourceProcess = TM1API.ObjectFunctions.TM1ObjectListHandleByNameGet(this.PoolHandle, hSourceServer, TM1API.ProcessFunctions.TM1ServerProcesses(), hSourceName);
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hSourceProcess);

            hDestProcess = TM1API.ObjectFunctions.TM1ObjectListHandleByNameGet(this.PoolHandle, hDestServer, TM1API.ProcessFunctions.TM1ServerProcesses(), hDestName);
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hDestProcess);

            LOG.Info("Retreived all existing object handles.");

            appsslbl.Text = "Replicating process...";
            appss.Refresh();

            // Create an unregistered copy of the source process on the destination server. 
            hTempProcess = TM1API.ProcessFunctions.TM1ProcessCreateEmpty(this.PoolHandle, hDestServer);
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTempProcess);

            LOG.Info("Created unregistered process on " + dest + ".");
            LOG.Info("Beginning replication of all process components.");

            // Data Source tab
            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessUIData());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessUIData(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessUIData from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceType());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceType(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessDataSourceType from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceNameForClient());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceNameForClient(), hTemp);
           
            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessDataSourceNameForClient from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceNameForServer());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceNameForServer(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessDataSourceNameForServer from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceASCIIDelimiter());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceASCIIDelimiter(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessDataSourceASCIIDelimiter from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceASCIIQuoteCharacter());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceASCIIQuoteCharacter(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessDataSourceASCIIQuoteCharacter from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceASCIIHeaderRecords());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceASCIIHeaderRecords(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessDataSourceASCIIHeaderRecords from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceASCIIDecimalSeparator());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceASCIIDecimalSeparator(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessDataSourceASCIIDecimalSeparator from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceASCIIThousandSeparator());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceASCIIThousandSeparator(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessDataSourceASCIIThousandSeparator from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceCubeView());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceCubeView(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessDataSourceCubeView from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceDimensionSubset());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceDimensionSubset(), hTemp);
            
            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessDataSourceDimensionSubset from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceOleDbLocation());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceOleDbLocation(), hTemp);
            
            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessDataSourceOleDbLocation from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceOleDbMdp());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceOleDbMdp(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessDataSourceOleDbMdp from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceUserName());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceUserName(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessDataSourceUserName from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessDataSourcePassword());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessDataSourcePassword(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessDataSourcePassword from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceQuery());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessDataSourceQuery(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessDataSourceQuery from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessGrantSecurityAccess());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessGrantSecurityAccess(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessGrantSecurityAccess from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            // Variables Tab
            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessVariablesUIData());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessVariablesUIData(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessVariablesUIData from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessVariablesNames());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessVariablesNames(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessVariablesNames from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessVariablesTypes());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessVariablesTypes(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessVariablesTypes from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessVariablesPositions());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessVariablesPositions(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessVariablesPositions from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessVariablesStartingBytes());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessVariablesStartingBytes(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessVariablesStartingBytes from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessVariablesEndingBytes());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessVariablesEndingBytes(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessVariablesEndingBytes from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessParametersNames());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessParametersNames(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessParametersNames from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessParametersTypes());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessParametersTypes(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessParametersTypes from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessParametersDefaultValues());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessParametersDefaultValues(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessParametersDefaultValues from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessParametersPromptStrings());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessParametersPromptStrings(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessParametersPromptStrings from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessPrologProcedure());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessPrologProcedure(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessPrologProcedure from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessMetaDataProcedure());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessMetaDataProcedure(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessMetaDataProcedure from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessDataProcedure());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessDataProcedure(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessDataProcedure from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hSourceProcess, TM1API.ProcessFunctions.TM1ProcessEpilogProcedure());
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempProcess, TM1API.ProcessFunctions.TM1ProcessEpilogProcedure(), hTemp);

            if (!CheckHandleValueType(hResult))
            {
                LOG.Error("API error moving ProcessEpilogProcedure from " + source + " to " + dest + ".");
            }

            apppb.PerformStep();

            /* hTemp = TM1.ObjectFunctions.TM1ObjectAttributeValueGet(this.PoolHandle, hChore, TM1.ProcessFunctions.TM1ProcessChoresUsing());
            temp = TM1.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
            */

            hResult = TM1API.ProcessFunctions.TM1ProcessCheck(this.PoolHandle, hTempProcess);
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hResult);

            LOG.Info("Determining if " + destprocess + " is a part of any chores on " + dest + ".");

            appsslbl.Text = "Analyzing chores...";
            appss.Refresh();

            /* Look at all the chores on the destination server and determine if this process is a step in any chores. 
             * If it is, that chore must be backed up as an unregistered chore and then deleted.  Once all chores that the replicated
             * process is a step in have been backed up as unregistered chores and deleted the process can then be deleted and 
             * the unregistered copy of the updated process can then be registered.  Then all unregistered chores that were created 
             * as a part of the backup are re-registered.  This (hopefully) won't be noticed by any TM1 developers.
             */

            chores = this.GetChoresOnServer(dest);
            LOG.Info("Retrieved all chores on " + dest);

            // Create an un-registered copy of each chore that has the target process as a step.
            foreach (string chore in chores)
            {
                hChoreName = TM1API.ValueFunctions.TM1ValString(this.PoolHandle, chore, 0);
                hChore = TM1API.ObjectFunctions.TM1ObjectListHandleByNameGet(this.PoolHandle, hDestServer, TM1API.ChoreFunctions.TM1ServerChores(), hChoreName);
                hChoreSteps = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hChore, TM1API.ChoreFunctions.TM1ChoreSteps());
                choresteps = TM1API.ValueFunctions.TM1ValArrayMaxSize(this.UserHandle, hChoreSteps);

                /* ChoreSteps Array -  Array of the processes that make up a chore.  First array is just an array of arrays.
                * Second Array is the process information, which is as follows.
                * Index 1: (?) TM1 internal chore id
                * Index 2: Process 
                * Index 3: Array of Process Parameter Values of type Index or String
                */

                for (int count = 1; count <= choresteps; count++)
                {
                    hChoreProcesses = TM1API.ValueFunctions.TM1ValArrayGet(this.UserHandle, hChoreSteps, count);
                    hChoreProcess = TM1API.ValueFunctions.TM1ValArrayGet(this.UserHandle, hChoreProcesses, 2);

                    processname = TM1API.ValueFunctions.TM1ValStringGet(this.UserHandle, hChoreProcess).ToString();

                    if (processname.Equals(destprocess))
                    {
                        LOG.Info("Found process " + destprocess + " in chore " + chore + " on " + dest + ".");
                        LOG.Info("Creating reunregistered copy of " + chore + " and backing up all object properties and security settings.");

                        hTempChore = TM1API.ChoreFunctions.TM1ChoreCreateEmpty(this.PoolHandle, hDestServer);

                        hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hChore, TM1API.ChoreFunctions.TM1ChoreSteps());
                        temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
                        hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempChore, TM1API.ChoreFunctions.TM1ChoreSteps(), hTemp);

                        hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hChore, TM1API.ChoreFunctions.TM1ChoreStartTime());
                        temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
                        hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hChore, TM1API.ChoreFunctions.TM1ChoreStartTime(), hTemp);

                        hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hChore, TM1API.ChoreFunctions.TM1ChoreFrequency());
                        temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
                        hResult = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hTempChore, TM1API.ChoreFunctions.TM1ChoreFrequency(), hTemp);

                        // Chore activity can only be set on a registered chore, so it's explicitly preserved to be set later after the chore is re-registered.
                        hTemp = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hChore, TM1API.ChoreFunctions.TM1ChoreActive());
                        temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
                        active = TM1API.ValueFunctions.TM1ValBoolGet(this.UserHandle, hTemp);

                        ch = new TM1Chore();
                        ch.Name = chore;
                        ch.Handle = hTempChore;
                        ch.Active = active;

                        hGroups = TM1API.ObjectFunctions.TM1ObjectListCountGet(this.PoolHandle, hDestServer, TM1API.SystemFunctions.TM1ServerGroups());
                        numgroups = TM1API.ValueFunctions.TM1ValIndexGet(this.UserHandle, hGroups);

                        ch.choresecurity = new Dictionary<string, int>();
                        
                        for (int groupcount = 1; groupcount <= numgroups; groupcount++)
                        {
                            hIndex = TM1API.ValueFunctions.TM1ValIndex(this.PoolHandle, groupcount);
                            hGroup = TM1API.ObjectFunctions.TM1ObjectListHandleByIndexGet(this.PoolHandle, hDestServer, TM1API.SystemFunctions.TM1ServerGroups(), hIndex);
                            hName = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hGroup, TM1API.ObjectFunctions.TM1ObjectName());
                            groupname = TM1API.ValueFunctions.TM1ValStringGet(this.UserHandle, hName).ToString();

                            hResult = TM1API.ObjectFunctions.TM1ObjectSecurityRightGet(this.PoolHandle, hChore, hGroup);

                            // None = 1, Read = 2, Write = 3, Reserve = 4, Lock = 5, Admin = 6
                            if (TM1API.ValueFunctions.TM1ValType(this.UserHandle, hResult) == (int)TM1API.TM1ValTypes.TM1Index)
                            {
                               right = TM1API.ValueFunctions.TM1ValIndexGet(this.UserHandle, hResult);
                               ch.choresecurity.Add(groupname, right);
                            }
                        }

                        foundchores.Add(ch);

                        LOG.Info("Chore " + chore + " successfully backed up.");

                        hTemp = TM1API.ObjectFunctions.TM1ObjectDelete(this.PoolHandle, hChore);
                        temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);
                        break;
                    }
                }
            }

            // 1 overrides the security on the destination server with that of the source (depending on if the group exists)
            // 2 will preserve the security on the destination server
            // default is no security
            appsslbl.Text = "Analyzing security...";
            appss.Refresh();

            LOG.Info("Determing if security settings should be saved.");

            switch (securitytype)
            {
                case 1:
                    hGroups = TM1API.ObjectFunctions.TM1ObjectListCountGet(this.PoolHandle, hSourceServer, TM1API.SystemFunctions.TM1ServerGroups());
                    numgroups = TM1API.ValueFunctions.TM1ValIndexGet(this.UserHandle, hGroups);

                    for (int count = 1; count <= numgroups; count++)
                    {
                        hIndex = TM1API.ValueFunctions.TM1ValIndex(this.PoolHandle, count);
                        hGroup = TM1API.ObjectFunctions.TM1ObjectListHandleByIndexGet(this.PoolHandle, hSourceServer, TM1API.SystemFunctions.TM1ServerGroups(), hIndex);
                        hName = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hGroup, TM1API.ObjectFunctions.TM1ObjectName());
                        groupname = TM1API.ValueFunctions.TM1ValStringGet(this.UserHandle, hName).ToString();
 
                        hResult = TM1API.ObjectFunctions.TM1ObjectSecurityRightGet(this.PoolHandle, hSourceProcess, hGroup);

                        // None = 1, Read = 2, Write = 3, Reserve = 4, Lock = 5, Admin = 6
                        if (TM1API.ValueFunctions.TM1ValType(this.UserHandle, hResult) == (int) TM1API.TM1ValTypes.TM1Index)
                        {
                            right = TM1API.ValueFunctions.TM1ValIndexGet(this.UserHandle, hResult);
                            processsecurity.Add(groupname, right);
                        }
                    }

                    LOG.Info("Security information for " + process + "  on " + source + " has been saved and will be used after the new process has been registered.");
                    break;
                case 2:
                    hGroups = TM1API.ObjectFunctions.TM1ObjectListCountGet(this.PoolHandle, hDestServer, TM1API.SystemFunctions.TM1ServerGroups());
                    numgroups = TM1API.ValueFunctions.TM1ValIndexGet(this.UserHandle, hGroups);

                    for (int count = 1; count <= numgroups; count++)
                    {
                        hIndex = TM1API.ValueFunctions.TM1ValIndex(this.PoolHandle, count);
                        hGroup = TM1API.ObjectFunctions.TM1ObjectListHandleByIndexGet(this.PoolHandle, hDestServer, TM1API.SystemFunctions.TM1ServerGroups(), hIndex);
                        hName = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hGroup, TM1API.ObjectFunctions.TM1ObjectName());
                        groupname = TM1API.ValueFunctions.TM1ValStringGet(this.UserHandle, hName).ToString();

                        hResult = TM1API.ObjectFunctions.TM1ObjectSecurityRightGet(this.PoolHandle, hDestProcess, hGroup);

                        // None = 1, Read = 2, Write = 3, Reserve = 4, Lock = 5, Admin = 6
                        if (TM1API.ValueFunctions.TM1ValType(this.UserHandle, hResult) == (int)TM1API.TM1ValTypes.TM1Index)
                        {
                            right = TM1API.ValueFunctions.TM1ValIndexGet(this.UserHandle, hResult);
                            processsecurity.Add(groupname, right);
                        } 
                    }

                    LOG.Info("Security information for " + destprocess + " on " + dest + " has been saved and will be used after the new process has been registered.");
                    break;
                default:
                    LOG.Info("Process security information will not be saved off.");
                    break;
            }

            if (TM1API.ValueFunctions.TM1ValType(this.UserHandle, hDestProcess) != (int)TM1API.TM1ValTypes.TM1Error)
            {
                // Finally, delete the original process
                hResult = TM1API.ObjectFunctions.TM1ObjectDelete(this.PoolHandle, hDestProcess);
                temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hResult);
            }

            appsslbl.Text = "Creating process...";
            appss.Refresh();

            hDestProcess = TM1API.ObjectFunctions.TM1ObjectRegister(this.PoolHandle, hDestServer, hTempProcess, hDestName);
            temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hDestProcess);

            LOG.Info("Process " + process + " has been created and registered as process " + destprocess + " on " + dest + ".");

            appsslbl.Text = "Setting security...";
            appss.Refresh();

            // Set the security rights (if applicable)
            if (processsecurity.Count > 0)
            {
                hGroups = TM1API.ObjectFunctions.TM1ObjectListCountGet(this.PoolHandle, hDestServer, TM1API.SystemFunctions.TM1ServerGroups());
                numgroups = TM1API.ValueFunctions.TM1ValIndexGet(this.UserHandle, hGroups);

                for (int count = 1; count <= numgroups; count++)
                {
                    hIndex = TM1API.ValueFunctions.TM1ValIndex(this.PoolHandle, count);
                    hGroup = TM1API.ObjectFunctions.TM1ObjectListHandleByIndexGet(this.PoolHandle, hDestServer, TM1API.SystemFunctions.TM1ServerGroups(), hIndex);
                    hName = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hGroup, TM1API.ObjectFunctions.TM1ObjectName());
                    groupname = TM1API.ValueFunctions.TM1ValStringGet(this.UserHandle, hName).ToString();

                    if (processsecurity.ContainsKey(groupname))
                    {
                        hValue = TM1API.ValueFunctions.TM1ValIndex(this.PoolHandle,  processsecurity[groupname]);
                        hResult = TM1API.ObjectFunctions.TM1ObjectSecurityRightSet(this.PoolHandle, hDestProcess, hGroup, hValue);
                    }
                }

                LOG.Info("Used saved off security information and set security for all groups.");
            }

            appsslbl.Text = "Restoring chores...";
            appss.Refresh();

            LOG.Info("Restoring any chores that has to be preserved...");

            // Loop over each chore that was preserved as an un-registered copy of and re-register them all.
            foreach (TM1Chore chore in foundchores)
            {
                hTemp = TM1API.ValueFunctions.TM1ValString(this.PoolHandle, chore.Name, 0);
                temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);

                hChore = TM1API.ObjectFunctions.TM1ObjectRegister(this.PoolHandle, hDestServer, chore.Handle, hTemp);
                temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hChore);

                hTemp = TM1API.ValueFunctions.TM1ValBool(this.PoolHandle, chore.Active);
                temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);

                hTemp = TM1API.ObjectFunctions.TM1ObjectPropertySet(this.PoolHandle, hChore, TM1API.ChoreFunctions.TM1ChoreActive(), hTemp);
                temp = TM1API.ValueFunctions.TM1ValType(this.UserHandle, hTemp);

                if (chore.choresecurity.Count > 0)
                {
                    hGroups = TM1API.ObjectFunctions.TM1ObjectListCountGet(this.PoolHandle, hDestServer, TM1API.SystemFunctions.TM1ServerGroups());
                    numgroups = TM1API.ValueFunctions.TM1ValIndexGet(this.UserHandle, hGroups);

                    for (int count = 1; count <= numgroups; count++)
                    {
                        hIndex = TM1API.ValueFunctions.TM1ValIndex(this.PoolHandle, count);
                        hGroup = TM1API.ObjectFunctions.TM1ObjectListHandleByIndexGet(this.PoolHandle, hDestServer, TM1API.SystemFunctions.TM1ServerGroups(), hIndex);
                        hName = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hGroup, TM1API.ObjectFunctions.TM1ObjectName());
                        groupname = TM1API.ValueFunctions.TM1ValStringGet(this.UserHandle, hName).ToString();

                        if (chore.choresecurity.ContainsKey(groupname))
                        {
                            hValue = TM1API.ValueFunctions.TM1ValIndex(this.PoolHandle, chore.choresecurity[groupname]);
                            hResult = TM1API.ObjectFunctions.TM1ObjectSecurityRightSet(this.PoolHandle, hChore, hGroup, hValue);
                        }
                    }
                }

                LOG.Info("Chore " + chore + " has been restored.");
            }

            LOG.Info("Chore restoration completed.");
            LOG.Info("Replication process from " + source + " to " + dest + " completed.");
            apppb.Value = 100;
            return true;
        }

        private bool CheckHandleValueType(IntPtr handle)
        {
            int result;

            result = TM1API.ValueFunctions.TM1ValType(this.UserHandle, handle);

            if (result == (int)TM1API.TM1ValTypes.TM1Error)
            {
                return false;
            }

            return true;
        }

        public void ExecuteChore(string serv, string chore)
        {
            IntPtr hServer = IntPtr.Zero;
            IntPtr hChore = IntPtr.Zero;
            IntPtr hResult = IntPtr.Zero;

            int errorcode;
            string errorstring;
            string errorlogfile;

            hServer = TM1API.SystemFunctions.TM1SystemServerHandle(this.UserHandle, serv);

            if (hServer != IntPtr.Zero)
            {
                hChore = TM1API.ObjectFunctions.TM1ObjectListHandleByNameGet(this.PoolHandle, hServer, TM1API.ChoreFunctions.TM1ServerChores(), TM1API.ValueFunctions.TM1ValString(this.PoolHandle, chore, chore.Length));
                hResult = TM1API.ChoreFunctions.TM1ChoreExecute(this.PoolHandle, hChore);

                if (TM1API.ValueFunctions.TM1ValType(this.UserHandle, hResult) == TM1API.ValueFunctions.TM1ValTypeArray())
                {
                    CheckProcessResult(hResult, out errorcode, out errorstring, out errorlogfile);

                    if (errorcode > 0)
                    {
                        MessageBox.Show(errorcode.ToString() + ": " + errorstring, "Error in process execution", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Success!
                }
            }
            else
            {
                MessageBox.Show("An active connection to the server could not be found.", "Error in chore execution", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckProcessResult(IntPtr hResult, out int errorcode, out string errorstring, out string errorfile)
        {
            errorcode = 0;
            errorstring = null;
            errorfile = null;

            if (TM1API.ValueFunctions.TM1ValType(this.UserHandle, hResult) == TM1API.ValueFunctions.TM1ValTypeArray())
            {
                if (TM1API.ValueFunctions.TM1ValArrayMaxSize(this.UserHandle, hResult) == 2)
                {
                    IntPtr hErrorHandle = IntPtr.Zero;
                    IntPtr hErrorFile = IntPtr.Zero;

                    hErrorHandle = TM1API.ValueFunctions.TM1ValArrayGet(this.UserHandle, hResult, 1);
                    hErrorFile = TM1API.ValueFunctions.TM1ValArrayGet(this.UserHandle, hResult, 2);

                    errorcode = TM1API.ValueFunctions.TM1ValErrorCode(this.UserHandle, hErrorHandle);
                    errorstring = TM1API.ValueFunctions.TM1ValErrorString(this.UserHandle, hErrorHandle).ToString();
                    errorfile = TM1API.ValueFunctions.TM1ValStringGet(this.UserHandle, hErrorFile).ToString();
                }
            }
        }

        public void ExecuteProcess(string serv, string proc)
        {
            IntPtr hServer = IntPtr.Zero;
            IntPtr hProcess = IntPtr.Zero;
            IntPtr hVariableNames = IntPtr.Zero;
            IntPtr hVariableTypes = IntPtr.Zero;
            IntPtr hPromptStrings = IntPtr.Zero;
            IntPtr hDefaultValues = IntPtr.Zero;
            IntPtr hParamVariableName = IntPtr.Zero;
            IntPtr hParamVariableType = IntPtr.Zero;
            IntPtr hParamPromptString = IntPtr.Zero;
            IntPtr hParamDefaultValue = IntPtr.Zero;
            IntPtr hParamValue = IntPtr.Zero;
            IntPtr hParamArray = IntPtr.Zero;
            IntPtr hResult = IntPtr.Zero;
            IntPtr[] argsarray = null;

            int num_variables = 0;
            int errorcode;
            string errorstring;
            string errorlogfile;

            hServer = TM1API.SystemFunctions.TM1SystemServerHandle(this.UserHandle, serv);
            
            if (hServer != IntPtr.Zero)
            {
                ProcessParameter ppinfo;
                string variable;
                string prompt;
                string value = null;
                string type = null;
                int typeindex;

                var ppsinfo = new List<ProcessParameter>();

                ParameterDialog pdlg = null;

                // Get a handle to the process
                hProcess = TM1API.ObjectFunctions.TM1ObjectListHandleByNameGet(this.PoolHandle, hServer, TM1API.ProcessFunctions.TM1ServerProcesses(), TM1API.ValueFunctions.TM1ValString(this.PoolHandle, proc, proc.Length));
                hVariableNames = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hProcess, TM1API.ProcessFunctions.TM1ProcessParametersNames());
                hVariableTypes = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hProcess, TM1API.ProcessFunctions.TM1ProcessParametersTypes());
                hPromptStrings = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hProcess, TM1API.ProcessFunctions.TM1ProcessParametersPromptStrings());
                hDefaultValues = TM1API.ObjectFunctions.TM1ObjectPropertyGet(this.PoolHandle, hProcess, TM1API.ProcessFunctions.TM1ProcessParametersDefaultValues());

                num_variables = TM1API.ValueFunctions.TM1ValArrayMaxSize(this.UserHandle, hVariableNames);

                for (int count = 0; count < num_variables; count++)
                {
                    hParamVariableName = TM1API.ValueFunctions.TM1ValArrayGet(this.UserHandle, hVariableNames, count + 1);
                    hParamVariableType = TM1API.ValueFunctions.TM1ValArrayGet(this.UserHandle, hVariableTypes, count + 1);
                    hParamPromptString = TM1API.ValueFunctions.TM1ValArrayGet(this.UserHandle, hPromptStrings, count + 1);
                    hParamDefaultValue = TM1API.ValueFunctions.TM1ValArrayGet(this.UserHandle, hDefaultValues, count + 1);
                    
                    variable = TM1API.ValueFunctions.TM1ValStringGet(this.UserHandle, hParamVariableName).ToString();
                    typeindex = TM1API.ValueFunctions.TM1ValIndexGet(this.UserHandle, hParamVariableType);
                    prompt = TM1API.ValueFunctions.TM1ValStringGet(this.UserHandle, hParamPromptString).ToString();

                    if (typeindex == 32)
                    {
                        type = "String";
                        value = TM1API.ValueFunctions.TM1ValStringGet(this.UserHandle, hParamDefaultValue).ToString();
                    }
                    else if (typeindex == 33)
                    {
                        type = "Numeric";
                        value = TM1API.ValueFunctions.TM1ValRealGet(this.UserHandle, hParamDefaultValue).ToString();
                    }
             
                    ppinfo = new ProcessParameter(variable, prompt, type, value);
                    ppsinfo.Add(ppinfo);
                    ppinfo = null;
                }

                if (ppsinfo.Count > 0)
                {
                    pdlg = new ParameterDialog(ppsinfo, serv + " - " + proc);

                    if (pdlg.ShowDialog() == DialogResult.OK)
                    {
                        ppsinfo = pdlg.FinalParamValues;

                        argsarray = new IntPtr[ppsinfo.Count];

                        for (int argcount = 0; argcount < ppsinfo.Count; argcount++)
                        {
                            if (ppsinfo[argcount].Type.Equals("Numeric"))
                            {
                                hParamValue = TM1API.ValueFunctions.TM1ValReal(this.PoolHandle, Double.Parse(ppsinfo[argcount].Value));
                            }
                            else if (ppsinfo[argcount].Type.Equals("String"))
                            {
                                hParamValue = TM1API.ValueFunctions.TM1ValString(this.PoolHandle, ppsinfo[argcount].Value, ppsinfo[argcount].Value.Length);
                            }

                            argsarray[argcount] = hParamValue;
                        }

                        hParamArray = TM1API.ValueFunctions.TM1ValArray(this.PoolHandle, argsarray, ppsinfo.Count);
                    }
                }

                hResult = TM1API.ProcessFunctions.TM1ProcessExecuteEx(this.PoolHandle, hProcess, hParamArray);

                if (TM1API.ValueFunctions.TM1ValType(this.UserHandle, hResult) == TM1API.ValueFunctions.TM1ValTypeArray())
                {
                    CheckProcessResult(hResult, out errorcode, out errorstring, out errorlogfile);

                    if (errorcode > 0)
                    {
                        MessageBox.Show(errorcode.ToString() + ": " + errorstring, "Error in process execution", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Success!
                }
            }
            else
            {
                MessageBox.Show("An active connection to the server could not be found.", "Error in process execution", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
