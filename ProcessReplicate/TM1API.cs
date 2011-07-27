/* 
    Copyright (C) 2011 Brian Agovino

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ProcessReplicate
{
    /// <summary>
    /// Wrapper class for the C TM1 API.
    /// </summary>
    class TM1API
    {
        private const string TM1APIDLL = @"tm1api.dll";

        private struct TM1Object
        {
            UInt32 major_ix;
            UInt32 minor_ix;
            byte dbcon_ix;
            byte otype;
            byte[] reserved;
        }

        public enum TM1ValTypes
        {
            TM1Real = 1,
            TM1String = 2,
            TM1Index = 3,
            TM1Bool = 4,
            TM1Object = 5,
            TM1Error = 6,
            TM1Array = 7,
            TM1StringW = 14,
            TM1Binary = 15
        };

        public sealed class SystemFunctions
        {
            [DllImport(TM1APIDLL)]
            public static extern void TM1APIInitialize();

            [DllImport(TM1APIDLL)]
            public static extern void TM1APIFinalize();

            [DllImport(TM1APIDLL)]
            public static extern int TM1CancelClientJob(IntPtr hUser, IntPtr hServer);

            [DllImport(TM1APIDLL)]
            public static extern int TM1UserKill(IntPtr hUser, IntPtr hServer);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ServerGroups();

            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1SystemBuildNumber();

            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1SystemBuildNumberW();

            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1SystemBuildNumberUTF8();

            [DllImport(TM1APIDLL)]
            public static extern void TM1SystemClose(IntPtr hUser);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SystemGetServerConfig(IntPtr hPool, IntPtr hServer);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SystemOpen();

            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1SystemServerClientName(IntPtr hUser, int index);

            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1SystemServerClientNameW(IntPtr hUser, int index);

            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1SystemServerClientNameUTF8(IntPtr hUser, int index);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SystemServerConnect(IntPtr hPool, IntPtr vServerName, IntPtr vClientName, IntPtr vPassword);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SystemServerConnectWithCAMNamespace(IntPtr hPool, IntPtr vServerName, IntPtr vCAMArgs);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SystemServerConnectWithCAMPassport(IntPtr hPool, IntPtr vServerName, IntPtr vCAMArgs);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SystemServerConnectIntegratedLogin(IntPtr hPool, IntPtr sServerName);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SystemServerDisconnect(IntPtr hPool, IntPtr hServer);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SystemServerHandle(IntPtr hUser, [MarshalAs(UnmanagedType.LPStr)] string server);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SystemServerHandleW(IntPtr hUser, [MarshalAs(UnmanagedType.BStr)] string server);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SystemServerHandleUTF8(IntPtr hUser, [MarshalAs(UnmanagedType.LPStr)] string server);

            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1SystemServerName(IntPtr hUser, int index);

            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1SystemServerNameW(IntPtr hUser, int index);

            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1SystemServerNameUTF8(IntPtr hUser, int index);

            [DllImport(TM1APIDLL)]
            public static extern int TM1SystemServerNof(IntPtr hUser);

            [DllImport(TM1APIDLL)]
            public static extern void TM1SystemServerReload(IntPtr hUser);

            [DllImport(TM1APIDLL)]
            public static extern int TM1SystemServerStart(IntPtr hUser, [MarshalAs(UnmanagedType.LPStr)] string server, [MarshalAs(UnmanagedType.LPStr)] string datadirectory, [MarshalAs(UnmanagedType.LPStr)] string adminhost, [MarshalAs(UnmanagedType.LPStr)] string protocol, int port);

            [DllImport(TM1APIDLL)]
            public static extern int TM1SystemServerStartW(IntPtr hUser, [MarshalAs(UnmanagedType.BStr)] string server, [MarshalAs(UnmanagedType.BStr)] string datadirectory, [MarshalAs(UnmanagedType.BStr)] string adminhost, [MarshalAs(UnmanagedType.BStr)] string protocol, int port);

            [DllImport(TM1APIDLL)]
            public static extern int TM1SystemServerStartUTF8(IntPtr hUser, [MarshalAs(UnmanagedType.LPStr)] string server, [MarshalAs(UnmanagedType.LPStr)] string datadirectory, [MarshalAs(UnmanagedType.LPStr)] string adminhost, [MarshalAs(UnmanagedType.LPStr)] string protocol, int port);

            [DllImport(TM1APIDLL)]
            public static extern int TM1SystemServerStartEx(IntPtr hUsr, [MarshalAs(UnmanagedType.LPStr)] string cmdline);

            [DllImport(TM1APIDLL)]
            public static extern int TM1SystemServerStartExW(IntPtr hUsr, [MarshalAs(UnmanagedType.BStr)] string cmdline);

            [DllImport(TM1APIDLL)]
            public static extern int TM1SystemServerStartExUTF8(IntPtr hUsr, [MarshalAs(UnmanagedType.LPStr)] string cmdline);

            [DllImport(TM1APIDLL)]
            public static extern int TM1SystemServerStop(IntPtr hUsr, [MarshalAs(UnmanagedType.LPStr)] string server, int save);

            [DllImport(TM1APIDLL)]
            public static extern int TM1SystemServerStopW(IntPtr hUsr, [MarshalAs(UnmanagedType.BStr)] string server, int save);

            [DllImport(TM1APIDLL)]
            public static extern int TM1SystemServerStopUTF8(IntPtr hUsr, [MarshalAs(UnmanagedType.LPStr)] string server, int save);

            [DllImport(TM1APIDLL)]
            public static extern int TM1SystemVersionGet();
        }

        public sealed class AdminHostFunctions
        {
            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1SystemAdminHostGet(IntPtr hUser);

            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1SystemAdminHostGetW(IntPtr hUser);

            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1SystemAdminHostGetUTF8(IntPtr hUser);

            [DllImport(TM1APIDLL)]
            public static extern void TM1SystemAdminHostSet(IntPtr hUser, [MarshalAs(UnmanagedType.LPStr)] string adminsrv);

            [DllImport(TM1APIDLL)]
            public static extern void TM1SystemAdminHostSetW(IntPtr hUser, [MarshalAs(UnmanagedType.BStr)] string adminsrv);

            [DllImport(TM1APIDLL)]
            public static extern void TM1SystemAdminHostSetUTF8(IntPtr hUser, [MarshalAs(UnmanagedType.LPStr)] string adminsrv);

            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1SystemGetAdminSSLCertAuthority(IntPtr hUser);

            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1SystemGetAdminSSLCertAuthorityW(IntPtr hUser);

            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1SystemGetAdminSSLCertAuthorityUTF8(IntPtr hUser);

            [DllImport(TM1APIDLL)]
            public static extern void TM1SystemSetAdminSSLCertAuthority(IntPtr hUser, [MarshalAs(UnmanagedType.LPStr)] string sslcertid);

            [DllImport(TM1APIDLL)]
            public static extern void TM1SystemSetAdminSSLCertAuthorityW(IntPtr hUser, [MarshalAs(UnmanagedType.BStr)] string sslcertid);

            [DllImport(TM1APIDLL)]
            public static extern void TM1SystemSetAdminSSLCertAuthorityUTF8(IntPtr hUser, [MarshalAs(UnmanagedType.LPStr)] string sslcertid);

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValidateSSLConfig(IntPtr hUser);
        }

        public sealed class BlobFunctions
        {
            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1BlobClose(IntPtr hPool, IntPtr hBlob);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1BlobCreate(IntPtr hPool, IntPtr hServer, IntPtr sName);

            [DllImport(TM1APIDLL)]
            public static extern int TM1BlobGet(IntPtr hUser, IntPtr hBlob, int x, int n, [MarshalAs(UnmanagedType.LPStr)] string buf);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1BlobOpen(IntPtr hPool, IntPtr hBlob);

            [DllImport(TM1APIDLL)]
            public static extern int TM1BlobPut(IntPtr hUser, IntPtr hBlob, int x, int n, [MarshalAs(UnmanagedType.LPStr)] string buf);
        }

        public sealed class ClientFunctions
        {
            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ClientAdd(IntPtr hPool, IntPtr hServer, IntPtr sClientName);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ClientGroupAssign(IntPtr hPool, IntPtr hClient, IntPtr hGroup);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ClientGroupIsAssigned(IntPtr hPool, IntPtr hClient, IntPtr hGroup);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ClientGroupRemove(IntPtr hPool, IntPtr hClient, IntPtr hGroup);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ClientHasHold(IntPtr hPool, IntPtr hClient);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ClientPasswordAssign(IntPtr hPool, IntPtr hClient, IntPtr sPassword);
        }

        public sealed class ChoreFunctions
        {
            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ServerChores();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ChoreCreateEmpty(IntPtr hPool, IntPtr hServer);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ChoreExecute(IntPtr hPool, IntPtr hChore);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ChoreActive();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ChoreFrequency();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ChoreStartTime();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ChoreSteps();
        }

        public sealed class CubeFunctions
        {
            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ServerCubes();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellDrillListGet(IntPtr hPool, IntPtr hCube, IntPtr hArrayOfKeys);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellsDrillListGet(IntPtr hPool, IntPtr hCube, IntPtr hArrayofArraysOfKeys);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellDrillObjectBuild(IntPtr hPool, IntPtr hCube, IntPtr hArrayOfKeys, IntPtr sDrillProcessName);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellDrillStringGet(IntPtr hPool, IntPtr hCube, IntPtr hArrayOfElements);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellValueGet(IntPtr hPool, IntPtr hCube, IntPtr hArrayOfElements);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellPickListGet(IntPtr hPool, IntPtr hCube, IntPtr hArrayOfElements);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellsPickListGet(IntPtr hPool, IntPtr hCube, IntPtr hArrayOfCells);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellPickListExists(IntPtr hPool, IntPtr hCube, IntPtr hArrayOfElements);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellValueSet(IntPtr hPool, IntPtr hCube, IntPtr hArrayOfElements, IntPtr hValue);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellSpread(IntPtr hPool, IntPtr hServer, IntPtr vArrayOfCells, IntPtr vCellReference, IntPtr vSpreadData);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellSpreadStatusGet(IntPtr hPool, IntPtr hServer, IntPtr hCells, IntPtr hCellRange);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellSpreadViewArray(IntPtr hPool, IntPtr hView, IntPtr hCellRange, IntPtr hCellReference, IntPtr sControl);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellsValueGet(IntPtr hPool, IntPtr hCube, IntPtr vArrayOfCells);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellsValueSet(IntPtr hPool, IntPtr hCube, IntPtr vArrayOfCells, IntPtr vValues);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCreate(IntPtr hPool, IntPtr hServer, IntPtr hArrayOfDimensions);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeDimensionListGet(IntPtr hPool, IntPtr hCube);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubePerspectiveCreate(IntPtr hPool, IntPtr hCube, IntPtr hArrayOfElementTitles);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubePerspectiveDestroy(IntPtr hPool, IntPtr hCube, IntPtr hArrayOfElementTitles);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeShowsNulls(IntPtr hPool, IntPtr hCube);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeClearData(IntPtr hPool, IntPtr hCube);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeListByNamesGet(IntPtr hPool, IntPtr hServer, IntPtr vCubeNames);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeListGet(IntPtr hPool, IntPtr hServer, IntPtr vCubeNames);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeDimensions();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellSpreadFunctionOk();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellSpreadNumericSetOk();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellSpreadStringSetOk();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellSpreadStatusOk();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellSpreadStatusHeld();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellSpreadStatusHeldConsolidation();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeCellSpreadStatusWritable();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeLogChanges();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeMeasuresDimension();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubePerspectivesMaxMemory();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubePerspectivesMinTime();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeReplicationSyncRule();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeReplicationSyncViews();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeRule();
            
            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeTimeDimension();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeTimeLastInvalidated();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1CubeViews();

        }

        public sealed class DimensionFunctions
        {
            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ServerDimensions();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1DimensionAttributesGet(IntPtr hPool, IntPtr hDimension);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1DimensionCheck(IntPtr hPool, IntPtr hDimension);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1DimensionCreateEmpty(IntPtr hPool, IntPtr hServer);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1DimensionElementComponentAdd(IntPtr hPool, IntPtr hElement, IntPtr hComponent, IntPtr rWeight);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1DimensionElementComponentDelete(IntPtr hPool, IntPtr hCElement, IntPtr hElement);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1DimensionElementComponentWeightGet(IntPtr hPool, IntPtr hCElement, IntPtr hElement);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1DimensionElementDelete(IntPtr hPool, IntPtr hElement);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1DimensionElementInsert(IntPtr hPool, IntPtr hDimension, IntPtr hElementAfter, IntPtr sName, IntPtr vType);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1DimensionElementListByIndexGet(IntPtr hPool, IntPtr hDimension, IntPtr vElementNames, IntPtr iFlage);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1DimensionElementListByIndexGetEx(IntPtr hPool, IntPtr hDimension, IntPtr iBeginIndex, IntPtr iCount, IntPtr iFlag);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1DimensionRootElementsGet(IntPtr hPool, IntPtr hDimension);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1DimensionRootSubsetGet(IntPtr hPool, IntPtr hDimension, IntPtr iRight);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1DimensionElementParentsSubsetGet(IntPtr hPool, IntPtr hElement, IntPtr iRight);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1DimensionElementChildrenSubsetGet(IntPtr hPool, IntPtr hElement, IntPtr iRight);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1DimensionUpdate(IntPtr hPool, IntPtr hOldDimension, IntPtr hNewDimension);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1DimensionTopElement();

        }

        public sealed class ObjectFunctions
        {
            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectAttributeDelete(IntPtr hPool, IntPtr hObject, IntPtr hAttribute);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectAttributeInsert(IntPtr hPool, IntPtr hObject, IntPtr hAttribute);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectAttributeValueGet(IntPtr hPool, IntPtr hObject, IntPtr hAttributeBefore, IntPtr hName, IntPtr hType);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectAttributeValueSet(IntPtr hPool, IntPtr hObject, IntPtr hAttribute, IntPtr vValue); 

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectCopy(IntPtr hPool, IntPtr hSource, IntPtr hDestination);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectDelete(IntPtr hPool, IntPtr hObject);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectDestroy(IntPtr hPool, IntPtr hObject);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectFileDelete(IntPtr hPool, IntPtr hObject);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectFileLoad(IntPtr hPool, IntPtr hServer, IntPtr hParent, IntPtr iObjectType, IntPtr sObjectName); 

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectFileSave(IntPtr hPool, IntPtr hObject);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectListCountGet(IntPtr hPool, IntPtr hObject, IntPtr hProperty);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectListHandleByIndexGet(IntPtr hPool, IntPtr hObject, IntPtr hPropertyList, IntPtr iIndex);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectListHandleByNameGet(IntPtr hPool, IntPtr hObject, IntPtr hPropertyList, IntPtr vName);

            [DllImport(TM1APIDLL, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            public static extern IntPtr TM1ObjectPropertyGet(IntPtr hPool, IntPtr hObject, IntPtr hObjectProperty);

            [DllImport(TM1APIDLL, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, ExactSpelling = true)]
            public static extern IntPtr TM1ObjectPropertySet(IntPtr hPool, IntPtr hObject, IntPtr hObjectProperty, IntPtr hValue);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectSubPropertyByRangeGet(IntPtr hPool, IntPtr hRootObject, IntPtr vObjectType, IntPtr hPrivate, IntPtr vStartIndex, IntPtr vQuantity, IntPtr vProperty);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectPrivateDelete(IntPtr hPool, IntPtr hObject);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectPrivateListCountGet(IntPtr hPool, IntPtr hObject, IntPtr iPropertyList);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectPrivateListHandleByIndexGet(IntPtr hPool, IntPtr hObject, IntPtr iPropertyList, IntPtr iIndex);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectPrivateListHandleByNameGet(IntPtr hPool, IntPtr hObject, IntPtr iPropertyList, IntPtr sName);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectPrivatePublish(IntPtr hPool, IntPtr hObject, IntPtr vProperty);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectPrivateRegister(IntPtr hPool, IntPtr vParent, IntPtr hObject, IntPtr sName);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectRegister(IntPtr hPool, IntPtr hParent, IntPtr hObject, IntPtr hName);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectReplicate(IntPtr hPool, IntPtr hObject);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectSecurityRightGet(IntPtr hPool, IntPtr hObject, IntPtr hGroup);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectSecurityRightSet(IntPtr hPool, IntPtr hObject, IntPtr hGroup, IntPtr iRight);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectSecurityLock(IntPtr hPool, IntPtr hObject);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectSecurityRelease(IntPtr hPool, IntPtr hObject);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectSecurityReserve(IntPtr hPool, IntPtr hObject);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectSecurityUnlock(IntPtr hPool, IntPtr hObject);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectAttributes();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectChangedSinceLoaded();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectLastTimeUpdated();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectMemoryUsed();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectName();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectParent();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectPublic();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectPrivate();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectRegistration();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectReplication();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectReplicationConnection();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectReplicationSourceName();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectReplicationSourceObjectName();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectReplicationStatus();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectSecurityOwner();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectSecurityStatus();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectType();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ObjectUnregistered();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SecurityRightNone();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SecurityRightRead();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SecurityRightWrite();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SecurityRightReserve();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SecurityRightLock();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SecurityRightAdmin();
        }

        public sealed class ProcessFunctions
        {
            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ServerProcesses();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessCreateEmpty(IntPtr hPool, IntPtr hServer);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessCheck(IntPtr hPool, IntPtr hProcess);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessExecute(IntPtr hPool, IntPtr hProcess, IntPtr hParametersArray);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessExecuteEx(IntPtr hPool, IntPtr hProcess, IntPtr hParametersArray);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessExecuteSQLQuery(IntPtr hPool, IntPtr hProcess, IntPtr hDatabaseInfoArray);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessVariableNameIsValid(IntPtr hPool, IntPtr hProcess, IntPtr hVariableName);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessChoresUsing();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessDataProcedure();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessDataSourceASCIIDecimalSeparator();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessDataSourceASCIIDelimiter();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessDataSourceASCIIHeaderRecords();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessDataSourceASCIIQuoteCharacter();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessDataSourceASCIIThousandSeparator();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessDataSourceCubeView();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessDataSourceDimensionSubset();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessDataSourceNameForClient();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessDataSourceNameForServer();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessDataSourceOleDbLocation();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessDataSourceOleDbMdp();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessDataSourcePassword();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessDataSourceQuery();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessDataSourceType();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessDataSourceUserName();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessEpilogProcedure();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessGrantSecurityAccess();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessMetaDataProcedure();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessParametersDefaultValues();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessParametersNames();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessParametersPromptStrings();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessParametersTypes();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessPrologProcedure();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessUIData();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessVariablesEndingBytes();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessVariablesNames();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessVariablesPositions();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessVariablesStartingBytes();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessVariablesTypes();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessVariablesUIData();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ProcessComplete();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorProcessAborted();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorProcessCompletedWithMessages();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorProcessHasMinorErrors();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorProcessIsBeingUsedByChore();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorProcessParameterDefaultValueNotDefined();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorProcessParameterPromptStringNotDefined();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorProcessQuitCalled();

        }

        public sealed class RuleFunctions
        {
            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1RuleAttach(IntPtr hPool, IntPtr hRule);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1RuleCheck(IntPtr hPool, IntPtr hRule);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1RuleCreateEmpty(IntPtr hPool, IntPtr hCube, IntPtr hType);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1RuleDetach(IntPtr hPool, IntPtr hRule);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1RuleLineGet(IntPtr hPool, IntPtr hRule, IntPtr iPosition);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1RuleLineInsert(IntPtr hPool, IntPtr hRule, IntPtr iPosition, IntPtr sLine);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1RuleNofLines();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1RuleErrorLine();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1RuleErrorString();
        }

        public sealed class SubsetFunctions
        {
            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetAll(IntPtr hPool, IntPtr hSubset);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetCreateByExpression(IntPtr hPool, IntPtr hServer, IntPtr sExpression);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetCreateEmpty(IntPtr hPool, IntPtr hDimension);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetElementDisplay(IntPtr hPool, IntPtr hSubset, IntPtr iElement);

            [DllImport(TM1APIDLL)]
            public static extern int TM1SubsetElementDisplayEll(IntPtr hUser, IntPtr vString);

            [DllImport(TM1APIDLL)]
            public static extern int TM1SubsetElementDisplayLevel(IntPtr hUser, IntPtr vString);

            [DllImport(TM1APIDLL)]
            public static extern int TM1SubsetElementDisplayLine(IntPtr hUser, IntPtr vString, int iIndex);

             [DllImport(TM1APIDLL)]
            public static extern int TM1SubsetElementDisplayMinus(IntPtr hUser, IntPtr vString);

             [DllImport(TM1APIDLL)]
            public static extern int TM1SubsetElementDisplayPlus(IntPtr hUser, IntPtr vString);

             [DllImport(TM1APIDLL)]
            public static extern int TM1SubsetElementDisplaySelection(IntPtr hUser, IntPtr vString);

             [DllImport(TM1APIDLL)]
            public static extern int TM1SubsetElementDisplayTee(IntPtr hUser, IntPtr vString);

             [DllImport(TM1APIDLL)]
            public static extern int TM1SubsetElementDisplayWeight(IntPtr hUser, IntPtr vString);

             [DllImport(TM1APIDLL)]
             public static extern IntPtr TM1SubsetElementListByIndexGet(IntPtr hPool, IntPtr hSubset, IntPtr iBeginIndex, IntPtr iCount);

             [DllImport(TM1APIDLL)]
             public static extern IntPtr TM1SubsetElementListByIndexGetEx(IntPtr hPool, IntPtr hSubset, IntPtr iBeginIndex, IntPtr iCount, IntPtr vDimName);

             [DllImport(TM1APIDLL)]
             public static extern IntPtr TM1SubsetElementListByNamesGet(IntPtr hPool, IntPtr hSubset, IntPtr vElementNames);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetInsertElement(IntPtr hPool, IntPtr hSubset, IntPtr hElement, IntPtr iPosition);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetInsertSubset(IntPtr hPool, IntPtr hSubsetA, IntPtr hSubsetB, IntPtr iPosition);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetListGet(IntPtr hPool, IntPtr hDimension, IntPtr iFlag);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetListByNamesGet(IntPtr hPool, IntPtr hDimension, IntPtr vSubsetNames, IntPtr iFlag);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetRootElementsGet(IntPtr hPool, IntPtr hSubset);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetSelectByAttribute(IntPtr hPool, IntPtr hSubset, IntPtr hAlias, IntPtr sValueToMatch, IntPtr bSelection);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetSelectByIndex(IntPtr hPool, IntPtr hSubset, IntPtr iPosition, IntPtr bSelection);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetSelectByLevel(IntPtr hPool, IntPtr hSubset, IntPtr iLevel, IntPtr bSelection);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetSelectByPattern(IntPtr hPool, IntPtr hSubset, IntPtr sPattern, IntPtr bSelection);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetSelectionDelete(IntPtr hPool, IntPtr hSubset);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetSelectionInsertChildren(IntPtr hPool, IntPtr hSubset);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetSelectionInsertParents(IntPtr hPool, IntPtr hSubset);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetSelectionKeep(IntPtr hPool, IntPtr hSubset);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetSelectNone(IntPtr hPool, IntPtr hSubset);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetSort(IntPtr hPool, IntPtr hSubset, IntPtr bSortDown);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetSortByHierarchy(IntPtr hPool, IntPtr hSubset);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetSubtract(IntPtr hPool, IntPtr SubsetA, IntPtr SubsetB);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1SubsetUpdate(IntPtr hPool, IntPtr hOldSubset, IntPtr hNewSubset);         
        }

        public sealed class ViewFunctions
        {
            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1GetViewByHandle(IntPtr hPool, IntPtr hView, IntPtr iFlag);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1GetViewByName(IntPtr hPool, IntPtr hServer, IntPtr sCube, IntPtr sViewName, IntPtr bIsPrivate, IntPtr iFlag);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewArrayColumnsNof(IntPtr hPool, IntPtr hView);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewArrayConstruct(IntPtr hPool, IntPtr hView);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewArrayDestroy(IntPtr hPool, IntPtr hView);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewArrayRowsNof(IntPtr hPool, IntPtr hView);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewArrayValueByRangeGet(IntPtr hPool, IntPtr hView, IntPtr iRowStart, IntPtr iColStart, IntPtr iRowEnd, IntPtr iColEnd);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewArrayValueGet(IntPtr hPool, IntPtr hView, IntPtr iColumn, IntPtr iRow);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewArrayValuePickListGet(IntPtr hPool, IntPtr hView, IntPtr iColumn, IntPtr iRow);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewArrayValuePickListByRangeGet(IntPtr hPool, IntPtr hView, IntPtr iRowStart, IntPtr iColStart, IntPtr iRowEnd, IntPtr iColEnd);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewArrayValuePickListExists(IntPtr hPool, IntPtr hView, IntPtr iColumn, IntPtr iRow);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewCellsValueGet(IntPtr hPool, IntPtr hView, IntPtr vArrayOfCells);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewCreate(IntPtr hPool, IntPtr hCube, IntPtr hTitleSubsetArray, IntPtr hColumnSubsetArray, IntPtr hRowSubsetArray);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewExtractCreate(IntPtr hPool, IntPtr hView);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewExtractDestroy(IntPtr hPool, IntPtr hView);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewExtractGetNext(IntPtr hPool, IntPtr hView);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewListGet(IntPtr hPool, IntPtr hCube, IntPtr iFlag);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewListByNamesGet(IntPtr hPool, IntPtr hCube, IntPtr vViewNames, IntPtr iFlag);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewArrayCellOrdinal();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewArrayCellValue();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewArrayCellFormattedValue();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewArrayCellFormatString();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewArrayMemberName();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewArrayMemberType();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewArrayMemberDescription();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewExtractComparison();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewExtractComparisonNone();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewExtractComparisonEQ_A();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewExtractComparisonGE_A();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewExtractComparisonLE_A();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewExtractComparisonGT_A();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewExtractComparisonLT_A();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewExtractComparisonNE_A();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewExtractComparisonGE_A_LE_B();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewExtractComparisonGT_A_LT_B();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewExtractSkipConsolidatedValues();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewExtractSkipRuleValues();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewExtractSkipZeroes();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewExtractRealLimitA();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewExtractRealLimitB();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewExtractStringLimitA();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewExtractStringLimitB();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewFormat();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewPreConstruct();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewRowSubsets();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewSuppressZeroes();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewShowAutomatically();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewTitleElements();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ViewTitleSubsets();
        }

        public sealed class ValueFunctions
        {
            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeAttribute();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeAttributeAlias();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeAttributeNumeric();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeAttributeString();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeBlob();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeClient();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeChore();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeConnection();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeCube();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeDimension();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeElement();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeElementSimple();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeElementConsolidated();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeElementString();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeGroup();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeProcess();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeRule();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeRuleCalculation();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeRuleDrill();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeSQLNotSupported();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeSQLNumericColumn();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeSQLStringColumn();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeSQLTable();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeSubset();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeServer();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeView();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeVariableNumeric();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1TypeVariableString();

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ValArray(IntPtr hPool, IntPtr[] vInitArray, int iArraySize);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ValArrayGet(IntPtr hUser, IntPtr vArray, int iIndex);

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValArraySize(IntPtr hUser, IntPtr vArray);

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValArrayMaxSize(IntPtr hUser, IntPtr vArray);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ValArraySet(IntPtr vArray, IntPtr vValue, int iIndex);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ValArraySetSize(IntPtr vArray, int iSize);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ValBool(IntPtr hPool, int iBool);

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValBoolGet(IntPtr hUser, IntPtr vBool);

            [DllImport(TM1APIDLL)]
            public static extern void TM1ValBoolSet(IntPtr hBool, int vBool);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ValBytesGet(IntPtr hUser, IntPtr vBytes, int iSize);

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValErrorCode(IntPtr hUser, IntPtr vError);

            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1ValErrorString(IntPtr hUser, IntPtr vValue);

            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1ValErrorStringW(IntPtr hUser, IntPtr vValue);

            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1ValErrorStringUTF8(IntPtr hUser, IntPtr vValue);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ValIndex(IntPtr hUser, int iIndex);

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValIndexGet(IntPtr hUser, IntPtr vIndex);

            [DllImport(TM1APIDLL)]
            public static extern void TM1ValIndexSet(IntPtr hIndex, int vIndex);

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValIsUndefined(IntPtr hUser, IntPtr vValue);

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValIsUpdatable(IntPtr hUSer, IntPtr vValue);

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValIsChanged(IntPtr hUser, IntPtr vValue);

            //[DllImport(TM1APIDLL)]
            //public static extern IntPtr TM1ValObject(IntPtr hPool, IntPtr pObject);

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValObjectCanRead(IntPtr hUser, IntPtr hObject);

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValObjectCanWrite(IntPtr hUser, IntPtr hObject);

            //[DllImport(TM1APIDLL)]
            //public static extern IntPtr TM1ValObjectGet(IntPtr hUser, IntPtr vObject, IntPtr pObject);

            //[DllImport(TM1APIDLL)]
            //public static extern IntPtr TM1ValObjectSet(IntPtr hUser, IntPtr hObject);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ValObjectType(IntPtr hUser, IntPtr hObject);

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValPoolCount(IntPtr hPool);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ValPoolCreate(IntPtr hUser);

            [DllImport(TM1APIDLL)]
            public static extern void TM1ValPoolDestroy(IntPtr hPool);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ValPoolGet(IntPtr hPool, int iIndex);

            [DllImport(TM1APIDLL)]
            public static extern long TM1ValPoolMemory(IntPtr hPool);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ValReal(IntPtr hPool, double vReal);

            [DllImport(TM1APIDLL)]
            public static extern double TM1ValRealGet(IntPtr hUser, IntPtr vReal);

            [DllImport(TM1APIDLL)]
            public static extern void TM1ValRealSet(IntPtr hReal, double vReal);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ValString(IntPtr hPool, [MarshalAs(UnmanagedType.LPStr)] string value, int value_length);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ValStringW(IntPtr hPool, [MarshalAs(UnmanagedType.BStr)] string value, int value_length);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ValStringUTF8(IntPtr hPool, [MarshalAs(UnmanagedType.LPStr)] string value, int value_length);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ValStringEncrypt(IntPtr hPool, [MarshalAs(UnmanagedType.LPStr)] string value, int value_length);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ValStringEncryptW(IntPtr hPool, [MarshalAs(UnmanagedType.BStr)] string value, int value_length);

            [DllImport(TM1APIDLL)]
            public static extern IntPtr TM1ValStringEncryptUTF8(IntPtr hPool, [MarshalAs(UnmanagedType.LPStr)] string value, int value_length);

            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1ValStringGet(IntPtr hUser, IntPtr vString);

            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1ValStringGetW(IntPtr hUser, IntPtr vString);

            [DllImport(TM1APIDLL)]
            public static extern StringBuilder TM1ValStringGetUTF8(IntPtr hUser, IntPtr vString);

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValStringMaxSize(IntPtr hUser, IntPtr vString);

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValStringWMaxSize(IntPtr hUser, IntPtr vString);

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValStringUTF8MaxSize(IntPtr hUser, IntPtr vString);

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValTypeIsString(IntPtr hUser, IntPtr hValue);

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValTypeIsBinary(IntPtr hUser, IntPtr hValue);

            [DllImport(TM1APIDLL)]
            public static extern void TM1ValStringSet(IntPtr vString, [MarshalAs(UnmanagedType.LPStr)] string value);

            [DllImport(TM1APIDLL)]
            public static extern void TM1ValStringSetW(IntPtr vString, [MarshalAs(UnmanagedType.BStr)] string value);
            
            [DllImport(TM1APIDLL)]
            public static extern void TM1ValStringSetUTF8(IntPtr vString, [MarshalAs(UnmanagedType.LPStr)] string value);

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValType(IntPtr hUser, IntPtr hValue);

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValTypeEx(IntPtr hUser, IntPtr hValue);

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValTypeArray();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValTypeBinary();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValTypeBool();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValTypeError();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValTypeIndex();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValTypeObject();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValTypeReal();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValTypeString();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ValTypeStringW();
        }

        public sealed class ErrorFunctions
        {
            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorAuditLogResultSetDoesNotExist();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorAuditLogResultSetInvalidRange();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorAuditLogRecordDoesNotExist();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorAuditLogResultSetOutOfMemory();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorBlobCloseFailed();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorBlobCreateFailed();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorBlobGetFailed();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorBlobNotOpen();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorBlobOpenFailed();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorBlobPutFailed();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCAMDllLoadFailed();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCAMObjectCreateFailed();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorClientPasswordNotDefined();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorClientAddedWithErrors();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorClientAlreadyExists();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorControlAliasNotFound();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorControlAliasInvalidType();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorControlAliasInvalidValueType();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubeCellValueTypeMismatch();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubeCellWriteStatusCubeNoWriteAccess();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubeCellWriteStatusCubeLocked();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubeCellWriteStatusCubeReserved();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubeCellWriteStatusElementIsConsolidated();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubeCellWriteStatusElementLocked();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubeCellWriteStatusElementNoWriteAccess();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubeCellWriteStatusElementReserved();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubeCellWriteStatusRuleApplies();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubeCreationFailed();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubeDimensionInvalid();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubeDrillNotFound();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubeDrillInvalidStructure();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubeKeyInvalid();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubeNotEnoughDimensions();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubeNumberOfKeysInvalid();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubePerspectiveAllSimpleElements();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubePerspectiveCreationFailed();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubeNoTimeDimension();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubeMeasuresAndTimeDimensionSame();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorCubeTooManyDimensions();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorDataSpreadFailed();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorDimensionCouldNotBeCompiled();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorDimensionElementAlreadyExists();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorDimensionElementComponentAlreadyExists();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorDimensionElementComponentDoesNotExist();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorDimensionElementComponentNotNumeric();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorDimensionElementDoesNotExist();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorDimensionElementNotConsolidated();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorDimensionHasCircularReferences();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorDimensionHasNoElements();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorDimensionIsBeingUsedByCube();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorDimensionNotChecked();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorDimensionNotRegistered();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorDimensionUpdatedFailedInvalidHierarchies();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorExecutingAuditLogQuery();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorGroupAddedWithErrors();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorGroupAlreadyExists();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorGroupMaximunNumberExceeded();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorInvalidCapabilityFeature();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorInvalidCapabilityPermission();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorInvalidCapabilityPolicy();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectAttributeNotDefined();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectAttributeInvalidType();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectDeleted();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectDuplicationFailed();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectFileNotFound();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectFunctionDoesNotApply();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectHandleInvalid();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectHasNoParent();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectIndexInvalid();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectFileInvalid();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectInvalid();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectIncompatibleTypes();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectIsRegistered();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectIsUnregistered();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectListIsEmpty();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectNameExists();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectNameIsBlank();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectNameInvalid();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectNotFound();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectNotLoaded();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectPropertyIsList();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectPropertyNotDefined();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectPropertyNotList();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectRegistrationFailed();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectSecurityIsLocked();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectSecurityNoAdminRights();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectSecurityNoLockRights();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectSecurityNoReadRights();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectSecurityNoReserveRights();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorObjectSecurityNoWriteRights();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorRuleIsAttached();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorRuleIsNotChecked();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorRuleLineNotFound();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorSubsetIsBeingUsedByView();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorSystemFunctionObsolete();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorSystemServerClientAlreadyConnected();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorSystemServerClientNotConnected();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorSystemServerClientNotFound();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorSystemServerClientPasswordInvalid();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorSystemServerNotFound();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorSystemServerCAMSecurityRequired();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorSystemOutOfMemory();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorSystemUserHandleInvalid();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorSystemValueInvalid();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorSystemParameterTypeInvalid();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorUpdateNonLeafCellValueFailed();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorViewHasPrivateSubsets();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorViewNotConstructed();

            [DllImport(TM1APIDLL)]
            public static extern int TM1ErrorViewExpressionEmpty();
        }     
    }
}
