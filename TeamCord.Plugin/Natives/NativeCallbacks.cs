using System;
using System.Runtime.InteropServices;
using TeamCord.Core;
using TeamCord.Plugin.Natives;

/// Return Type: unsigned int
///result: char**
public delegate uint TS3Functions_getClientLibVersion(ref IntPtr result);

/// Return Type: unsigned int
///result: uint64*
public delegate uint TS3Functions_getClientLibVersionNumber(ref ulong result);

/// Return Type: unsigned int
///port: int
///result: uint64*
public delegate uint TS3Functions_spawnNewServerConnectionHandler(int port, ref ulong result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
public delegate uint TS3Functions_destroyServerConnectionHandler(ulong serverConnectionHandlerID);

/// Return Type: unsigned int
///errorCode: unsigned int
///error: char**
public delegate uint TS3Functions_getErrorMessage(uint errorCode, ref IntPtr error);

/// Return Type: unsigned int
///pointer: void*
public delegate uint TS3Functions_freeMemory(IntPtr pointer);

/// Return Type: unsigned int
///logMessage: char*
///severity: LogLevel
///channel: char*
///logID: uint64->unsigned __int64
public delegate uint TS3Functions_logMessage([MarshalAs(UnmanagedType.LPStr)] string logMessage, LogLevel severity, [MarshalAs(UnmanagedType.LPStr)] string channel, ulong logID);

/// Return Type: unsigned int
///modeID: char*
///result: char****
public delegate uint TS3Functions_getPlaybackDeviceList([MarshalAs(UnmanagedType.LPStr)] string modeID, out IntPtr result);

/// Return Type: unsigned int
///result: char***
public delegate uint TS3Functions_getPlaybackModeList([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] ref string[] result);

/// Return Type: unsigned int
///modeID: char*
///result: char****
public delegate uint TS3Functions_getCaptureDeviceList([MarshalAs(UnmanagedType.LPStr)] string modeID, ref IntPtr result);

/// Return Type: unsigned int
///result: char***
public delegate uint TS3Functions_getCaptureModeList([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] ref string[] result);

/// Return Type: unsigned int
///modeID: char*
///result: char***
public delegate uint TS3Functions_getDefaultPlaybackDevice([MarshalAs(UnmanagedType.LPStr)] string modeID, [MarshalAs(UnmanagedType.LPArray, SizeConst = 2, ArraySubType = UnmanagedType.LPStr)] out string[] result);

/// Return Type: unsigned int
///result: char**
public delegate uint TS3Functions_getDefaultPlayBackMode([MarshalAs(UnmanagedType.LPStr)] out string result);

/// Return Type: unsigned int
///modeID: char*
///result: char***
public delegate uint TS3Functions_getDefaultCaptureDevice([MarshalAs(UnmanagedType.LPStr)] string modeID, [MarshalAs(UnmanagedType.LPArray, SizeConst = 2, ArraySubType = UnmanagedType.LPStr)] out string[] result);

/// Return Type: unsigned int
///result: char**
public delegate uint TS3Functions_getDefaultCaptureMode([MarshalAs(UnmanagedType.LPStr)] ref string result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///modeID: char*
///playbackDevice: char*
public delegate uint TS3Functions_openPlaybackDevice(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string modeID, [MarshalAs(UnmanagedType.LPStr)] string playbackDevice);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///modeID: char*
///captureDevice: char*
public delegate uint TS3Functions_openCaptureDevice(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string modeID, [MarshalAs(UnmanagedType.LPStr)] string captureDevice);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///result: char**
///isDefault: int*
public delegate uint TS3Functions_getCurrentPlaybackDeviceName(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] out string result, out bool isDefault);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///result: char**
public delegate uint TS3Functions_getCurrentPlayBackMode(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] out string result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///result: char**
///isDefault: int*
public delegate uint TS3Functions_getCurrentCaptureDeviceName(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] ref string result, ref bool isDefault);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///result: char**
public delegate uint TS3Functions_getCurrentCaptureMode(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] ref string result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
public delegate uint TS3Functions_initiateGracefulPlaybackShutdown(ulong serverConnectionHandlerID);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
public delegate uint TS3Functions_closePlaybackDevice(ulong serverConnectionHandlerID);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
public delegate uint TS3Functions_closeCaptureDevice(ulong serverConnectionHandlerID);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
public delegate uint TS3Functions_activateCaptureDevice(ulong serverConnectionHandlerID);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///path: char*
///loop: int
///waveHandle: uint64*
public delegate uint TS3Functions_playWaveFileHandle(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string path, int loop, ref ulong waveHandle);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///waveHandle: uint64->unsigned __int64
///pause: int
public delegate uint TS3Functions_pauseWaveFileHandle(ulong serverConnectionHandlerID, ulong waveHandle, int pause);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///waveHandle: uint64->unsigned __int64
public delegate uint TS3Functions_closeWaveFileHandle(ulong serverConnectionHandlerID, ulong waveHandle);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///path: char*
public delegate uint TS3Functions_playWaveFile(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string path);

/// Return Type: unsigned int
///deviceID: char*
///deviceDisplayName: char*
///capFrequency: int
///capChannels: int
///playFrequency: int
///playChannels: int
public delegate uint TS3Functions_registerCustomDevice([MarshalAs(UnmanagedType.LPStr)] string deviceID, [MarshalAs(UnmanagedType.LPStr)] string deviceDisplayName, int capFrequency, int capChannels, int playFrequency, int playChannels);

/// Return Type: unsigned int
///deviceID: char*
public delegate uint TS3Functions_unregisterCustomDevice([MarshalAs(UnmanagedType.LPStr)] string deviceID);

/// Return Type: unsigned int
///deviceName: char*
///buffer: short*
///samples: int
public delegate uint TS3Functions_processCustomCaptureData([MarshalAs(UnmanagedType.LPStr)] string deviceName, ref short[] buffer, int samples);

/// Return Type: unsigned int
///deviceName: char*
///buffer: short*
///samples: int
public delegate uint TS3Functions_acquireCustomPlaybackData([MarshalAs(UnmanagedType.LPStr)] string deviceName, ref short[] buffer, int samples);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///ident: char*
///result: float*
public delegate uint TS3Functions_getPreProcessorInfoValueFloat(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string ident, ref float result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///ident: char*
///result: char**
public delegate uint TS3Functions_getPreProcessorConfigValue(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string ident, ref IntPtr result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///ident: char*
///value: char*
public delegate uint TS3Functions_setPreProcessorConfigValue(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string ident, [MarshalAs(UnmanagedType.LPStr)] string value);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///ident: char*
///result: char**
public delegate uint TS3Functions_getEncodeConfigValue(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string ident, ref IntPtr result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///ident: char*
///result: float*
public delegate uint TS3Functions_getPlaybackConfigValueAsFloat(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string ident, ref float result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///ident: char*
///value: char*
public delegate uint TS3Functions_setPlaybackConfigValue(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string ident, [MarshalAs(UnmanagedType.LPStr)] string value);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///value: float
public delegate uint TS3Functions_setClientVolumeModifier(ulong serverConnectionHandlerID, ushort clientID, float value);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
public delegate uint TS3Functions_startVoiceRecording(ulong serverConnectionHandlerID);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
public delegate uint TS3Functions_stopVoiceRecording(ulong serverConnectionHandlerID);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///position: TS3_VECTOR*
///forward: TS3_VECTOR*
///up: TS3_VECTOR*
public delegate uint TS3Functions_systemset3DListenerAttributes(ulong serverConnectionHandlerID, ref TS3_VECTOR position, ref TS3_VECTOR forward, ref TS3_VECTOR up);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///waveHandle: uint64->unsigned __int64
///position: TS3_VECTOR*
public delegate uint TS3Functions_set3DWaveAttributes(ulong serverConnectionHandlerID, ulong waveHandle, ref TS3_VECTOR position);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///distanceFactor: float
///rolloffScale: float
public delegate uint TS3Functions_systemset3DSettings(ulong serverConnectionHandlerID, float distanceFactor, float rolloffScale);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///position: TS3_VECTOR*
public delegate uint TS3Functions_channelset3DAttributes(ulong serverConnectionHandlerID, ushort clientID, ref TS3_VECTOR position);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///identity: char*
///ip: char*
///port: unsigned int
///nickname: char*
///defaultChannelArray: char**
///defaultChannelPassword: char*
///serverPassword: char*
public delegate uint TS3Functions_startConnection(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string identity, [MarshalAs(UnmanagedType.LPStr)] string ip, uint port, [MarshalAs(UnmanagedType.LPStr)] string nickname, ref IntPtr defaultChannelArray, [MarshalAs(UnmanagedType.LPStr)] string defaultChannelPassword, [MarshalAs(UnmanagedType.LPStr)] string serverPassword);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///quitMessage: char*
public delegate uint TS3Functions_stopConnection(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string quitMessage);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///newChannelID: uint64->unsigned __int64
///password: char*
///returnCode: char*
public delegate uint TS3Functions_requestClientMove(ulong serverConnectionHandlerID, ushort clientID, ulong newChannelID, [MarshalAs(UnmanagedType.LPStr)] string password, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///returnCode: char*
public delegate uint TS3Functions_requestClientVariables(ulong serverConnectionHandlerID, ushort clientID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///kickReason: char*
///returnCode: char*
public delegate uint TS3Functions_requestClientKickFromChannel(ulong serverConnectionHandlerID, ushort clientID, [MarshalAs(UnmanagedType.LPStr)] string kickReason, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///kickReason: char*
///returnCode: char*
public delegate uint TS3Functions_requestClientKickFromServer(ulong serverConnectionHandlerID, ushort clientID, [MarshalAs(UnmanagedType.LPStr)] string kickReason, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///force: int
///returnCode: char*
public delegate uint TS3Functions_requestChannelDelete(ulong serverConnectionHandlerID, ulong channelID, int force, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///newChannelParentID: uint64->unsigned __int64
///newChannelOrder: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestChannelMove(ulong serverConnectionHandlerID, ulong channelID, ulong newChannelParentID, ulong newChannelOrder, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///message: char*
///targetClientID: anyID->unsigned short
///returnCode: char*
public delegate uint TS3Functions_requestSendPrivateTextMsg(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string message, ushort targetClientID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///message: char*
///targetChannelID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestSendChannelTextMsg(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string message, ulong targetChannelID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///message: char*
///returnCode: char*
public delegate uint TS3Functions_requestSendServerTextMsg(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string message, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///returnCode: char*
public delegate uint TS3Functions_requestConnectionInfo(ulong serverConnectionHandlerID, ushort clientID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///targetChannelIDArray: uint64*
///targetClientIDArray: anyID*
///returnCode: char*
public delegate uint TS3Functions_requestClientSetWhisperList(ulong serverConnectionHandlerID, ushort clientID, ref ulong targetChannelIDArray, ref ushort targetClientIDArray, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelIDArray: uint64*
///returnCode: char*
public delegate uint TS3Functions_requestChannelSubscribe(ulong serverConnectionHandlerID, ref ulong channelIDArray, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestChannelSubscribeAll(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelIDArray: uint64*
///returnCode: char*
public delegate uint TS3Functions_requestChannelUnsubscribe(ulong serverConnectionHandlerID, ref ulong channelIDArray, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestChannelUnsubscribeAll(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestChannelDescription(ulong serverConnectionHandlerID, ulong channelID, IntPtr returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientIDArray: anyID*
///returnCode: char*
public delegate uint TS3Functions_requestMuteClients(ulong serverConnectionHandlerID, ref ushort clientIDArray, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientIDArray: anyID*
///returnCode: char*
public delegate uint TS3Functions_requestUnmuteClients(ulong serverConnectionHandlerID, ref ushort clientIDArray, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///message: char*
///returnCode: char*
public delegate uint TS3Functions_requestClientPoke(ulong serverConnectionHandlerID, ushort clientID, [MarshalAs(UnmanagedType.LPStr)] string message, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientUniqueIdentifier: char*
///returnCode: char*
public delegate uint TS3Functions_requestClientIDs(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string clientUniqueIdentifier, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientUniqueIdentifier: char*
///clientID: anyID->unsigned short
///returnCode: char*
public delegate uint TS3Functions_clientChatClosed(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string clientUniqueIdentifier, ushort clientID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///returnCode: char*
public delegate uint TS3Functions_clientChatComposing(ulong serverConnectionHandlerID, ushort clientID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///password: char*
///description: char*
///duration: uint64->unsigned __int64
///targetChannelID: uint64->unsigned __int64
///targetChannelPW: char*
///returnCode: char*
public delegate uint TS3Functions_requestServerTemporaryPasswordAdd(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string password, [MarshalAs(UnmanagedType.LPStr)] string description, ulong duration, ulong targetChannelID, [MarshalAs(UnmanagedType.LPStr)] string targetChannelPW, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///password: char*
///returnCode: char*
public delegate uint TS3Functions_requestServerTemporaryPasswordDel(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string password, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestServerTemporaryPasswordList(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///result: anyID*
public delegate uint TS3Functions_getClientID(ulong serverConnectionHandlerID, ref ushort result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///flag: size_t->unsigned int
///result: int*
public delegate uint TS3Functions_getClientSelfVariableAsInt(ulong serverConnectionHandlerID, ClientProperties flag, ref int result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///flag: size_t->unsigned int
///result: char**
public delegate uint TS3Functions_getClientSelfVariableAsString(ulong serverConnectionHandlerID, ClientProperties flag, [MarshalAs(UnmanagedType.LPStr)] ref string result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///flag: size_t->unsigned int
///value: int
public delegate uint TS3Functions_setClientSelfVariableAsInt(ulong serverConnectionHandlerID, ClientProperties flag, int value);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///flag: size_t->unsigned int
///value: char*
public delegate uint TS3Functions_setClientSelfVariableAsString(ulong serverConnectionHandlerID, ClientProperties flag, [MarshalAs(UnmanagedType.LPStr)] string value);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_flushClientSelfUpdates(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///flag: size_t->unsigned int
///result: int*
public delegate uint TS3Functions_getClientVariableAsInt(ulong serverConnectionHandlerID, ushort clientID, ClientProperties flag, ref int result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///flag: size_t->unsigned int
///result: uint64*
public delegate uint TS3Functions_getClientVariableAsUInt64(ulong serverConnectionHandlerID, ushort clientID, ClientProperties flag, ref ulong result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///flag: size_t->unsigned int
///result: char**
public delegate uint TS3Functions_getClientVariableAsString(ulong serverConnectionHandlerID, ushort clientID, ClientProperties flag, [MarshalAs(UnmanagedType.LPStr)] ref string result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///result: anyID**
public delegate uint TS3Functions_getClientList(ulong serverConnectionHandlerID, ref IntPtr result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///result: uint64*
public delegate uint TS3Functions_getChannelOfClient(ulong serverConnectionHandlerID, ushort clientID, ref ulong result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///flag: size_t->unsigned int
///result: int*
public delegate uint TS3Functions_getChannelVariableAsInt(ulong serverConnectionHandlerID, ulong channelID, ChannelProperties flag, ref int result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///flag: size_t->unsigned int
///result: uint64*
public delegate uint TS3Functions_getChannelVariableAsUInt64(ulong serverConnectionHandlerID, ulong channelID, ChannelProperties flag, ref ulong result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///flag: size_t->unsigned int
///result: char**
public delegate uint TS3Functions_getChannelVariableAsString(ulong serverConnectionHandlerID, ulong channelID, ChannelProperties flag, [MarshalAs(UnmanagedType.LPStr)] out string result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelNameArray: char**
///result: uint64*
public delegate uint TS3Functions_getChannelIDFromChannelNames(ulong serverConnectionHandlerID, ref IntPtr channelNameArray, ref ulong result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///flag: size_t->unsigned int
///value: int
public delegate uint TS3Functions_setChannelVariableAsInt(ulong serverConnectionHandlerID, ulong channelID, ChannelProperties flag, int value);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///flag: size_t->unsigned int
///value: uint64->unsigned __int64
public delegate uint TS3Functions_setChannelVariableAsUInt64(ulong serverConnectionHandlerID, ulong channelID, ChannelProperties flag, ulong value);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///flag: size_t->unsigned int
///value: char*
public delegate uint TS3Functions_setChannelVariableAsString(ulong serverConnectionHandlerID, ulong channelID, ChannelProperties flag, [MarshalAs(UnmanagedType.LPStr)] string value);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_flushChannelUpdates(ulong serverConnectionHandlerID, ulong channelID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelParentID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_flushChannelCreation(ulong serverConnectionHandlerID, ulong channelParentID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///result: uint64**
public delegate uint TS3Functions_getChannelList(ulong serverConnectionHandlerID, ref IntPtr result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///result: anyID**
public delegate uint TS3Functions_getChannelClientList(ulong serverConnectionHandlerID, ulong channelID, ref IntPtr result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///result: uint64*
public delegate uint TS3Functions_getParentChannelOfChannel(ulong serverConnectionHandlerID, ulong channelID, ref ulong result);

/// Return Type: unsigned int
///result: uint64**
public delegate uint TS3Functions_getServerConnectionHandlerList(ref IntPtr result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///flag: size_t->unsigned int
///result: int*
public delegate uint TS3Functions_getServerVariableAsInt(ulong serverConnectionHandlerID, IntPtr flag, ref int result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///flag: size_t->unsigned int
///result: uint64*
public delegate uint TS3Functions_getServerVariableAsUInt64(ulong serverConnectionHandlerID, IntPtr flag, ref ulong result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///flag: size_t->unsigned int
///result: char**
public delegate uint TS3Functions_getServerVariableAsString(ulong serverConnectionHandlerID, uint flag, [MarshalAs(UnmanagedType.LPStr)] out string result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
public delegate uint TS3Functions_requestServerVariables(ulong serverConnectionHandlerID);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///result: int*
public delegate uint TS3Functions_getConnectionStatus(ulong serverConnectionHandlerID, ref int result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///flag: size_t->unsigned int
///result: uint64*
public delegate uint TS3Functions_getConnectionVariableAsUInt64(ulong serverConnectionHandlerID, ushort clientID, IntPtr flag, ref ulong result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///flag: size_t->unsigned int
///result: double*
public delegate uint TS3Functions_getConnectionVariableAsDouble(ulong serverConnectionHandlerID, ushort clientID, IntPtr flag, ref double result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///flag: size_t->unsigned int
///result: char**
public delegate uint TS3Functions_getConnectionVariableAsString(ulong serverConnectionHandlerID, ushort clientID, IntPtr flag, ref IntPtr result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
public delegate uint TS3Functions_cleanUpConnectionInfo(ulong serverConnectionHandlerID, ushort clientID);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientUniqueIdentifier: char*
///returnCode: char*
public delegate uint TS3Functions_requestClientDBIDfromUID(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string clientUniqueIdentifier, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientUniqueIdentifier: char*
///returnCode: char*
public delegate uint TS3Functions_requestClientNamefromUID(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string clientUniqueIdentifier, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientDatabaseID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestClientNamefromDBID(ulong serverConnectionHandlerID, ulong clientDatabaseID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///clientDescription: char*
///returnCode: char*
public delegate uint TS3Functions_requestClientEditDescription(ulong serverConnectionHandlerID, ushort clientID, [MarshalAs(UnmanagedType.LPStr)] string clientDescription, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///isTalker: int
///returnCode: char*
public delegate uint TS3Functions_requestClientSetIsTalker(ulong serverConnectionHandlerID, ushort clientID, int isTalker, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///isTalkerRequest: int
///isTalkerRequestMessage: char*
///returnCode: char*
public delegate uint TS3Functions_requestIsTalker(ulong serverConnectionHandlerID, int isTalkerRequest, [MarshalAs(UnmanagedType.LPStr)] string isTalkerRequestMessage, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///command: char*
///returnCode: char*
public delegate uint TS3Functions_requestSendClientQueryCommand(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string command, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///transferID: anyID->unsigned short
///result: char**
public delegate uint TS3Functions_getTransferFileName(ushort transferID, ref IntPtr result);

/// Return Type: unsigned int
///transferID: anyID->unsigned short
///result: char**
public delegate uint TS3Functions_getTransferFilePath(ushort transferID, ref IntPtr result);

/// Return Type: unsigned int
///transferID: anyID->unsigned short
///result: uint64*
public delegate uint TS3Functions_getTransferFileSize(ushort transferID, ref ulong result);

/// Return Type: unsigned int
///transferID: anyID->unsigned short
///result: uint64*
public delegate uint TS3Functions_getTransferFileSizeDone(ushort transferID, ref ulong result);

/// Return Type: unsigned int
///transferID: anyID->unsigned short
///result: int*
public delegate uint TS3Functions_isTransferSender(ushort transferID, ref int result);

/// Return Type: unsigned int
///transferID: anyID->unsigned short
///result: int*
public delegate uint TS3Functions_getTransferStatus(ushort transferID, ref int result);

/// Return Type: unsigned int
///transferID: anyID->unsigned short
///result: float*
public delegate uint TS3Functions_getCurrentTransferSpeed(ushort transferID, ref float result);

/// Return Type: unsigned int
///transferID: anyID->unsigned short
///result: float*
public delegate uint TS3Functions_getAverageTransferSpeed(ushort transferID, ref float result);

/// Return Type: unsigned int
///transferID: anyID->unsigned short
///result: uint64*
public delegate uint TS3Functions_getTransferRunTime(ushort transferID, ref ulong result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///channelPW: char*
///file: char*
///overwrite: int
///resume: int
///sourceDirectory: char*
///result: anyID*
///returnCode: char*
public delegate uint TS3Functions_sendFile(ulong serverConnectionHandlerID, ulong channelID, [MarshalAs(UnmanagedType.LPStr)] string channelPW, [MarshalAs(UnmanagedType.LPStr)] string file, int overwrite, int resume, [MarshalAs(UnmanagedType.LPStr)] string sourceDirectory, ref ushort result, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///channelPW: char*
///file: char*
///overwrite: int
///resume: int
///destinationDirectory: char*
///result: anyID*
///returnCode: char*
public delegate uint TS3Functions_requestFile(ulong serverConnectionHandlerID, ulong channelID, [MarshalAs(UnmanagedType.LPStr)] string channelPW, [MarshalAs(UnmanagedType.LPStr)] string file, int overwrite, int resume, [MarshalAs(UnmanagedType.LPStr)] string destinationDirectory, ref ushort result, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///transferID: anyID->unsigned short
///deleteUnfinishedFile: int
///returnCode: char*
public delegate uint TS3Functions_haltTransfer(ulong serverConnectionHandlerID, ushort transferID, int deleteUnfinishedFile, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///channelPW: char*
///path: char*
///returnCode: char*
public delegate uint TS3Functions_requestFileList(ulong serverConnectionHandlerID, ulong channelID, [MarshalAs(UnmanagedType.LPStr)] string channelPW, [MarshalAs(UnmanagedType.LPStr)] string path, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///channelPW: char*
///file: char*
///returnCode: char*
public delegate uint TS3Functions_requestFileInfo(ulong serverConnectionHandlerID, ulong channelID, [MarshalAs(UnmanagedType.LPStr)] string channelPW, [MarshalAs(UnmanagedType.LPStr)] string file, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///channelPW: char*
///file: char**
///returnCode: char*
public delegate uint TS3Functions_requestDeleteFile(ulong serverConnectionHandlerID, ulong channelID, [MarshalAs(UnmanagedType.LPStr)] string channelPW, ref IntPtr file, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///channelPW: char*
///directoryPath: char*
///returnCode: char*
public delegate uint TS3Functions_requestCreateDirectory(ulong serverConnectionHandlerID, ulong channelID, [MarshalAs(UnmanagedType.LPStr)] string channelPW, [MarshalAs(UnmanagedType.LPStr)] string directoryPath, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///fromChannelID: uint64->unsigned __int64
///channelPW: char*
///toChannelID: uint64->unsigned __int64
///toChannelPW: char*
///oldFile: char*
///newFile: char*
///returnCode: char*
public delegate uint TS3Functions_requestRenameFile(ulong serverConnectionHandlerID, ulong fromChannelID, [MarshalAs(UnmanagedType.LPStr)] string channelPW, ulong toChannelID, [MarshalAs(UnmanagedType.LPStr)] string toChannelPW, [MarshalAs(UnmanagedType.LPStr)] string oldFile, [MarshalAs(UnmanagedType.LPStr)] string newFile, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///toClientUID: char*
///subject: char*
///message: char*
///returnCode: char*
public delegate uint TS3Functions_requestMessageAdd(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string toClientUID, [MarshalAs(UnmanagedType.LPStr)] string subject, [MarshalAs(UnmanagedType.LPStr)] string message, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///messageID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestMessageDel(ulong serverConnectionHandlerID, ulong messageID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///messageID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestMessageGet(ulong serverConnectionHandlerID, ulong messageID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestMessageList(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///messageID: uint64->unsigned __int64
///flag: int
///returnCode: char*
public delegate uint TS3Functions_requestMessageUpdateFlag(ulong serverConnectionHandlerID, ulong messageID, int flag, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///serverPassword: char*
///returnCode: char*
public delegate uint TS3Functions_verifyServerPassword(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string serverPassword, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///channelPassword: char*
///returnCode: char*
public delegate uint TS3Functions_verifyChannelPassword(ulong serverConnectionHandlerID, ulong channelID, [MarshalAs(UnmanagedType.LPStr)] string channelPassword, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///timeInSeconds: uint64->unsigned __int64
///banReason: char*
///returnCode: char*
public delegate uint TS3Functions_banclient(ulong serverConnectionHandlerID, ushort clientID, ulong timeInSeconds, [MarshalAs(UnmanagedType.LPStr)] string banReason, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///ipRegExp: char*
///nameRegexp: char*
///uniqueIdentity: char*
///timeInSeconds: uint64->unsigned __int64
///banReason: char*
///returnCode: char*
public delegate uint TS3Functions_banadd(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string ipRegExp, [MarshalAs(UnmanagedType.LPStr)] string nameRegexp, [MarshalAs(UnmanagedType.LPStr)] string uniqueIdentity, ulong timeInSeconds, [MarshalAs(UnmanagedType.LPStr)] string banReason, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientDBID: uint64->unsigned __int64
///timeInSeconds: uint64->unsigned __int64
///banReason: char*
///returnCode: char*
public delegate uint TS3Functions_banclientdbid(ulong serverConnectionHandlerID, ulong clientDBID, ulong timeInSeconds, [MarshalAs(UnmanagedType.LPStr)] string banReason, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///banID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_bandel(ulong serverConnectionHandlerID, ulong banID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_bandelall(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestBanList(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///targetClientDatabaseID: uint64->unsigned __int64
///complainReason: char*
///returnCode: char*
public delegate uint TS3Functions_requestComplainAdd(ulong serverConnectionHandlerID, ulong targetClientDatabaseID, [MarshalAs(UnmanagedType.LPStr)] string complainReason, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///targetClientDatabaseID: uint64->unsigned __int64
///fromClientDatabaseID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestComplainDel(ulong serverConnectionHandlerID, ulong targetClientDatabaseID, ulong fromClientDatabaseID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///targetClientDatabaseID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestComplainDelAll(ulong serverConnectionHandlerID, ulong targetClientDatabaseID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///targetClientDatabaseID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestComplainList(ulong serverConnectionHandlerID, ulong targetClientDatabaseID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestServerGroupList(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///groupName: char*
///groupType: int
///returnCode: char*
public delegate uint TS3Functions_requestServerGroupAdd(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string groupName, int groupType, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///serverGroupID: uint64->unsigned __int64
///force: int
///returnCode: char*
public delegate uint TS3Functions_requestServerGroupDel(ulong serverConnectionHandlerID, ulong serverGroupID, int force, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///serverGroupID: uint64->unsigned __int64
///clientDatabaseID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestServerGroupAddClient(ulong serverConnectionHandlerID, ulong serverGroupID, ulong clientDatabaseID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///serverGroupID: uint64->unsigned __int64
///clientDatabaseID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestServerGroupDelClient(ulong serverConnectionHandlerID, ulong serverGroupID, ulong clientDatabaseID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientDatabaseID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestServerGroupsByClientID(ulong serverConnectionHandlerID, ulong clientDatabaseID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///serverGroupID: uint64->unsigned __int64
///continueonerror: int
///permissionIDArray: int*
///permissionValueArray: int*
///permissionNegatedArray: int*
///permissionSkipArray: int*
///arraySize: int
///returnCode: char*
public delegate uint TS3Functions_requestServerGroupAddPerm(ulong serverConnectionHandlerID, ulong serverGroupID, int continueonerror, ref uint permissionIDArray, ref int permissionValueArray, ref int permissionNegatedArray, ref int permissionSkipArray, int arraySize, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///serverGroupID: uint64->unsigned __int64
///continueOnError: int
///permissionIDArray: int*
///arraySize: int
///returnCode: char*
public delegate uint TS3Functions_requestServerGroupDelPerm(ulong serverConnectionHandlerID, ulong serverGroupID, int continueOnError, ref uint permissionIDArray, int arraySize, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///serverGroupID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestServerGroupPermList(ulong serverConnectionHandlerID, ulong serverGroupID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///serverGroupID: uint64->unsigned __int64
///withNames: int
///returnCode: char*
public delegate uint TS3Functions_requestServerGroupClientList(ulong serverConnectionHandlerID, ulong serverGroupID, int withNames, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestChannelGroupList(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///groupName: char*
///groupType: int
///returnCode: char*
public delegate uint TS3Functions_requestChannelGroupAdd(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string groupName, int groupType, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelGroupID: uint64->unsigned __int64
///force: int
///returnCode: char*
public delegate uint TS3Functions_requestChannelGroupDel(ulong serverConnectionHandlerID, ulong channelGroupID, int force, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelGroupID: uint64->unsigned __int64
///continueonerror: int
///permissionIDArray: int*
///permissionValueArray: int*
///arraySize: int
///returnCode: char*
public delegate uint TS3Functions_requestChannelGroupAddPerm(ulong serverConnectionHandlerID, ulong channelGroupID, int continueonerror, ref uint permissionIDArray, ref int permissionValueArray, int arraySize, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelGroupID: uint64->unsigned __int64
///continueOnError: int
///permissionIDArray: int*
///arraySize: int
///returnCode: char*
public delegate uint TS3Functions_requestChannelGroupDelPerm(ulong serverConnectionHandlerID, ulong channelGroupID, int continueOnError, ref uint permissionIDArray, int arraySize, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelGroupID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestChannelGroupPermList(ulong serverConnectionHandlerID, ulong channelGroupID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelGroupIDArray: uint64*
///channelIDArray: uint64*
///clientDatabaseIDArray: uint64*
///arraySize: int
///returnCode: char*
public delegate uint TS3Functions_requestSetClientChannelGroup(ulong serverConnectionHandlerID, ref ulong channelGroupIDArray, ref ulong channelIDArray, ref ulong clientDatabaseIDArray, int arraySize, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///permissionIDArray: int*
///permissionValueArray: int*
///arraySize: int
///returnCode: char*
public delegate uint TS3Functions_requestChannelAddPerm(ulong serverConnectionHandlerID, ulong channelID, ref uint permissionIDArray, ref int permissionValueArray, int arraySize, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///permissionIDArray: int*
///arraySize: int
///returnCode: char*
public delegate uint TS3Functions_requestChannelDelPerm(ulong serverConnectionHandlerID, ulong channelID, ref uint permissionIDArray, int arraySize, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestChannelPermList(ulong serverConnectionHandlerID, ulong channelID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientDatabaseID: uint64->unsigned __int64
///permissionIDArray: int*
///permissionValueArray: int*
///permissionSkipArray: int*
///arraySize: int
///returnCode: char*
public delegate uint TS3Functions_requestClientAddPerm(ulong serverConnectionHandlerID, ulong clientDatabaseID, ref uint permissionIDArray, ref int permissionValueArray, ref int permissionSkipArray, int arraySize, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientDatabaseID: uint64->unsigned __int64
///permissionIDArray: int*
///arraySize: int
///returnCode: char*
public delegate uint TS3Functions_requestClientDelPerm(ulong serverConnectionHandlerID, ulong clientDatabaseID, ref uint permissionIDArray, int arraySize, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///clientDatabaseID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestClientPermList(ulong serverConnectionHandlerID, ulong clientDatabaseID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///clientDatabaseID: uint64->unsigned __int64
///permissionIDArray: int*
///permissionValueArray: int*
///arraySize: int
///returnCode: char*
public delegate uint TS3Functions_requestChannelClientAddPerm(ulong serverConnectionHandlerID, ulong channelID, ulong clientDatabaseID, ref uint permissionIDArray, ref int permissionValueArray, int arraySize, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///clientDatabaseID: uint64->unsigned __int64
///permissionIDArray: int*
///arraySize: int
///returnCode: char*
public delegate uint TS3Functions_requestChannelClientDelPerm(ulong serverConnectionHandlerID, ulong channelID, ulong clientDatabaseID, ref uint permissionIDArray, int arraySize, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///clientDatabaseID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestChannelClientPermList(ulong serverConnectionHandlerID, ulong channelID, ulong clientDatabaseID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandler: uint64->unsigned __int64
///tokenKey: char*
///returnCode: char*
public delegate uint TS3Functions_privilegeKeyUse(ulong serverConnectionHandler, [MarshalAs(UnmanagedType.LPStr)] string tokenKey, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandler: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestPermissionList(ulong serverConnectionHandler, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///serverConnectionHandler: uint64->unsigned __int64
///clientDBID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///returnCode: char*
public delegate uint TS3Functions_requestPermissionOverview(ulong serverConnectionHandler, ulong clientDBID, ulong channelID, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: unsigned int
///clientPropertyString: char*
///resultFlag: size_t*
public delegate uint TS3Functions_clientPropertyStringToFlag([MarshalAs(UnmanagedType.LPStr)] string clientPropertyString, ref uint resultFlag);

/// Return Type: unsigned int
///channelPropertyString: char*
///resultFlag: size_t*
public delegate uint TS3Functions_channelPropertyStringToFlag([MarshalAs(UnmanagedType.LPStr)] string channelPropertyString, ref uint resultFlag);

/// Return Type: unsigned int
///serverPropertyString: char*
///resultFlag: size_t*
public delegate uint TS3Functions_serverPropertyStringToFlag([MarshalAs(UnmanagedType.LPStr)] string serverPropertyString, ref uint resultFlag);

/// Return Type: void
///path: char*
///maxLen: size_t->unsigned int
public delegate void TS3Functions_getAppPath(IntPtr path, uint maxLen);

/// Return Type: void
///path: char*
///maxLen: size_t->unsigned int
public delegate void TS3Functions_getResourcesPath(IntPtr path, uint maxLen);

/// Return Type: void
///path: char*
///maxLen: size_t->unsigned int
public delegate void TS3Functions_getConfigPath(IntPtr path, uint maxLen);

/// Return Type: void
///path: char*
///maxLen: size_t->unsigned int
public delegate void TS3Functions_getPluginPath(IntPtr path, uint maxLen, [MarshalAs(UnmanagedType.LPStr)] string pluginID);

/// Return Type: uint64->unsigned __int64
public delegate ulong TS3Functions_getCurrentServerConnectionHandlerID();

/// Return Type: void
///serverConnectionHandlerID: uint64->unsigned __int64
///message: char*
///messageTarget: PluginMessageTarget
public unsafe delegate void TS3Functions_printMessage(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string message, PluginMessageTarget messageTarget);

/// Return Type: void
///message: char*
public delegate void TS3Functions_printMessageToCurrentTab([MarshalAs(UnmanagedType.LPStr)] string message);

/// Return Type: void
///text: char*
///result: char*
///maxLen: size_t->unsigned int
public delegate void TS3Functions_urlsToBB([MarshalAs(UnmanagedType.LPStr)] string text, IntPtr result, IntPtr maxLen);

/// Return Type: void
///serverConnectionHandlerID: uint64->unsigned __int64
///pluginID: char*
///command: char*
///targetMode: int
///targetIDs: anyID*
///returnCode: char*
public delegate void TS3Functions_sendPluginCommand(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string pluginID, [MarshalAs(UnmanagedType.LPStr)] string command, int targetMode, ref ushort targetIDs, [MarshalAs(UnmanagedType.LPStr)] string returnCode);

/// Return Type: void
///path: char*
///result: char*
///maxLen: size_t->unsigned int
public delegate void TS3Functions_getDirectories([MarshalAs(UnmanagedType.LPStr)] string path, IntPtr result, IntPtr maxLen);

/// Return Type: unsigned int
///scHandlerID: uint64->unsigned __int64
///host: char*
///port: unsigned short*
///password: char*
///maxLen: size_t->unsigned int
public delegate uint TS3Functions_getServerConnectInfo(ulong scHandlerID, IntPtr host, ref ushort port, IntPtr password, IntPtr maxLen);

/// Return Type: unsigned int
///scHandlerID: uint64->unsigned __int64
///channelID: uint64->unsigned __int64
///path: char*
///password: char*
///maxLen: size_t->unsigned int
public unsafe delegate uint TS3Functions_getChannelConnectInfo(ulong scHandlerID, ulong channelID, IntPtr path, char* password, ulong maxLen);

/// Return Type: void
///pluginID: char*
///returnCode: char*
///maxLen: size_t->unsigned int
public delegate void TS3Functions_createReturnCode([MarshalAs(UnmanagedType.LPStr)] string pluginID, IntPtr returnCode, uint maxLen);

/// Return Type: unsigned int
///scHandlerID: uint64->unsigned __int64
///itemType: PluginItemType
///itemID: uint64->unsigned __int64
public delegate uint TS3Functions_requestInfoUpdate(ulong scHandlerID, PluginItemType itemType, ulong itemID);

/// Return Type: uint64->unsigned __int64
///scHandlerID: uint64->unsigned __int64
public delegate ulong TS3Functions_getServerVersion(ulong scHandlerID);

/// Return Type: unsigned int
///scHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///result: int*
public delegate uint TS3Functions_isWhispering(ulong scHandlerID, ushort clientID, ref int result);

/// Return Type: unsigned int
///scHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///result: int*
public delegate uint TS3Functions_isReceivingWhisper(ulong scHandlerID, ushort clientID, ref int result);

/// Return Type: unsigned int
///scHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///result: char*
///maxLen: size_t->unsigned int
public delegate uint TS3Functions_getAvatar(ulong scHandlerID, ushort clientID, IntPtr result, IntPtr maxLen);

/// Return Type: void
///pluginID: char*
///menuID: int
///enabled: int
public delegate void TS3Functions_setPluginMenuEnabled([MarshalAs(UnmanagedType.LPStr)] string pluginID, int menuID, bool enabled);

/// Return Type: void
public delegate void TS3Functions_showHotkeySetup();

/// Return Type: void
///pluginID: char*
///keyword: char*
///qParentWindow: void*
public delegate void TS3Functions_requestHotkeyInputDialog([MarshalAs(UnmanagedType.LPStr)] string pluginID, [MarshalAs(UnmanagedType.LPStr)] string keyword, IntPtr qParentWindow);

/// Return Type: unsigned int
///pluginID: char*
///keywords: char**
///hotkeys: char**
///arrayLen: size_t->unsigned int
///hotkeyBufSize: size_t->unsigned int
public delegate uint TS3Functions_getHotkeyFromKeyword([MarshalAs(UnmanagedType.LPStr)] string pluginID, ref IntPtr keywords, ref IntPtr hotkeys, IntPtr arrayLen, IntPtr hotkeyBufSize);

/// Return Type: unsigned int
///scHandlerID: uint64->unsigned __int64
///clientID: anyID->unsigned short
///result: char*
///maxLen: size_t->unsigned int
public delegate uint TS3Functions_getClientDisplayName(ulong scHandlerID, ushort clientID, IntPtr result, IntPtr maxLen);

/// Return Type: unsigned int
///list: PluginBookmarkList**
public delegate uint TS3Functions_getBookmarkList(ref IntPtr list);

/// Return Type: unsigned int
///profile: PluginGuiProfile
///defaultProfileIdx: int*
///result: char***
public delegate uint TS3Functions_getProfileList(PluginGuiProfile profile, ref int defaultProfileIdx, ref IntPtr result);

/// Return Type: unsigned int
///connectTab: PluginConnectTab
///serverLabel: char*
///serverAddress: char*
///serverPassword: char*
///nickname: char*
///channel: char*
///channelPassword: char*
///captureProfile: char*
///playbackProfile: char*
///hotkeyProfile: char*
///soundProfile: char*
///userIdentity: char*
///oneTimeKey: char*
///phoneticName: char*
///scHandlerID: uint64*
public delegate uint TS3Functions_guiConnect(PluginConnectTab connectTab, [MarshalAs(UnmanagedType.LPStr)] string serverLabel, [MarshalAs(UnmanagedType.LPStr)] string serverAddress, [MarshalAs(UnmanagedType.LPStr)] string serverPassword, [MarshalAs(UnmanagedType.LPStr)] string nickname, [MarshalAs(UnmanagedType.LPStr)] string channel, [MarshalAs(UnmanagedType.LPStr)] string channelPassword, [MarshalAs(UnmanagedType.LPStr)] string captureProfile, [MarshalAs(UnmanagedType.LPStr)] string playbackProfile, [MarshalAs(UnmanagedType.LPStr)] string hotkeyProfile, [MarshalAs(UnmanagedType.LPStr)] string soundProfile, [MarshalAs(UnmanagedType.LPStr)] string userIdentity, [MarshalAs(UnmanagedType.LPStr)] string oneTimeKey, [MarshalAs(UnmanagedType.LPStr)] string phoneticName, ref ulong scHandlerID);

/// Return Type: unsigned int
///connectTab: PluginConnectTab
///bookmarkuuid: char*
///scHandlerID: uint64*
public delegate uint TS3Functions_guiConnectBookmark(PluginConnectTab connectTab, [MarshalAs(UnmanagedType.LPStr)] string bookmarkuuid, ref ulong scHandlerID);

/// Return Type: unsigned int
///bookmarkuuid: char*
///serverLabel: char*
///serverAddress: char*
///serverPassword: char*
///nickname: char*
///channel: char*
///channelPassword: char*
///captureProfile: char*
///playbackProfile: char*
///hotkeyProfile: char*
///soundProfile: char*
///uniqueUserId: char*
///oneTimeKey: char*
///phoneticName: char*
public delegate uint TS3Functions_createBookmark([MarshalAs(UnmanagedType.LPStr)] string bookmarkuuid, [MarshalAs(UnmanagedType.LPStr)] string serverLabel, [MarshalAs(UnmanagedType.LPStr)] string serverAddress, [MarshalAs(UnmanagedType.LPStr)] string serverPassword, [MarshalAs(UnmanagedType.LPStr)] string nickname, [MarshalAs(UnmanagedType.LPStr)] string channel, [MarshalAs(UnmanagedType.LPStr)] string channelPassword, [MarshalAs(UnmanagedType.LPStr)] string captureProfile, [MarshalAs(UnmanagedType.LPStr)] string playbackProfile, [MarshalAs(UnmanagedType.LPStr)] string hotkeyProfile, [MarshalAs(UnmanagedType.LPStr)] string soundProfile, [MarshalAs(UnmanagedType.LPStr)] string uniqueUserId, [MarshalAs(UnmanagedType.LPStr)] string oneTimeKey, [MarshalAs(UnmanagedType.LPStr)] string phoneticName);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///permissionName: char*
///result: unsigned int*
public delegate uint TS3Functions_getPermissionIDByName(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string permissionName, ref uint result);

/// Return Type: unsigned int
///serverConnectionHandlerID: uint64->unsigned __int64
///permissionName: char*
///result: int*
public delegate uint TS3Functions_getClientNeededPermission(ulong serverConnectionHandlerID, [MarshalAs(UnmanagedType.LPStr)] string permissionName, ref int result);

[StructLayoutAttribute(LayoutKind.Sequential)]
public struct TS3Functions
{
    /// TS3Functions_getClientLibVersion
    public TS3Functions_getClientLibVersion getClientLibVersion;

    /// TS3Functions_getClientLibVersionNumber
    public TS3Functions_getClientLibVersionNumber getClientLibVersionNumber;

    /// TS3Functions_spawnNewServerConnectionHandler
    public TS3Functions_spawnNewServerConnectionHandler spawnNewServerConnectionHandler;

    /// TS3Functions_destroyServerConnectionHandler
    public TS3Functions_destroyServerConnectionHandler destroyServerConnectionHandler;

    /// TS3Functions_getErrorMessage
    public TS3Functions_getErrorMessage getErrorMessage;

    /// TS3Functions_freeMemory
    public TS3Functions_freeMemory freeMemory;

    /// TS3Functions_logMessage
    public TS3Functions_logMessage logMessage;

    /// TS3Functions_getPlaybackDeviceList
    public TS3Functions_getPlaybackDeviceList getPlaybackDeviceList;

    /// TS3Functions_getPlaybackModeList
    public TS3Functions_getPlaybackModeList getPlaybackModeList;

    /// TS3Functions_getCaptureDeviceList
    public TS3Functions_getCaptureDeviceList getCaptureDeviceList;

    /// TS3Functions_getCaptureModeList
    public TS3Functions_getCaptureModeList getCaptureModeList;

    /// TS3Functions_getDefaultPlaybackDevice
    public TS3Functions_getDefaultPlaybackDevice getDefaultPlaybackDevice;

    /// TS3Functions_getDefaultPlayBackMode
    public TS3Functions_getDefaultPlayBackMode getDefaultPlayBackMode;

    /// TS3Functions_getDefaultCaptureDevice
    public TS3Functions_getDefaultCaptureDevice getDefaultCaptureDevice;

    /// TS3Functions_getDefaultCaptureMode
    public TS3Functions_getDefaultCaptureMode getDefaultCaptureMode;

    /// TS3Functions_openPlaybackDevice
    public TS3Functions_openPlaybackDevice openPlaybackDevice;

    /// TS3Functions_openCaptureDevice
    public TS3Functions_openCaptureDevice openCaptureDevice;

    /// TS3Functions_getCurrentPlaybackDeviceName
    public TS3Functions_getCurrentPlaybackDeviceName getCurrentPlaybackDeviceName;

    /// TS3Functions_getCurrentPlayBackMode
    public TS3Functions_getCurrentPlayBackMode getCurrentPlayBackMode;

    /// TS3Functions_getCurrentCaptureDeviceName
    public TS3Functions_getCurrentCaptureDeviceName getCurrentCaptureDeviceName;

    /// TS3Functions_getCurrentCaptureMode
    public TS3Functions_getCurrentCaptureMode getCurrentCaptureMode;

    /// TS3Functions_initiateGracefulPlaybackShutdown
    public TS3Functions_initiateGracefulPlaybackShutdown initiateGracefulPlaybackShutdown;

    /// TS3Functions_closePlaybackDevice
    public TS3Functions_closePlaybackDevice closePlaybackDevice;

    /// TS3Functions_closeCaptureDevice
    public TS3Functions_closeCaptureDevice closeCaptureDevice;

    /// TS3Functions_activateCaptureDevice
    public TS3Functions_activateCaptureDevice activateCaptureDevice;

    /// TS3Functions_playWaveFileHandle
    public TS3Functions_playWaveFileHandle playWaveFileHandle;

    /// TS3Functions_pauseWaveFileHandle
    public TS3Functions_pauseWaveFileHandle pauseWaveFileHandle;

    /// TS3Functions_closeWaveFileHandle
    public TS3Functions_closeWaveFileHandle closeWaveFileHandle;

    /// TS3Functions_playWaveFile
    public TS3Functions_playWaveFile playWaveFile;

    /// TS3Functions_registerCustomDevice
    public TS3Functions_registerCustomDevice registerCustomDevice;

    /// TS3Functions_unregisterCustomDevice
    public TS3Functions_unregisterCustomDevice unregisterCustomDevice;

    /// TS3Functions_processCustomCaptureData
    public TS3Functions_processCustomCaptureData processCustomCaptureData;

    /// TS3Functions_acquireCustomPlaybackData
    public TS3Functions_acquireCustomPlaybackData acquireCustomPlaybackData;

    /// TS3Functions_getPreProcessorInfoValueFloat
    public TS3Functions_getPreProcessorInfoValueFloat getPreProcessorInfoValueFloat;

    /// TS3Functions_getPreProcessorConfigValue
    public TS3Functions_getPreProcessorConfigValue getPreProcessorConfigValue;

    /// TS3Functions_setPreProcessorConfigValue
    public TS3Functions_setPreProcessorConfigValue setPreProcessorConfigValue;

    /// TS3Functions_getEncodeConfigValue
    public TS3Functions_getEncodeConfigValue getEncodeConfigValue;

    /// TS3Functions_getPlaybackConfigValueAsFloat
    public TS3Functions_getPlaybackConfigValueAsFloat getPlaybackConfigValueAsFloat;

    /// TS3Functions_setPlaybackConfigValue
    public TS3Functions_setPlaybackConfigValue setPlaybackConfigValue;

    /// TS3Functions_setClientVolumeModifier
    public TS3Functions_setClientVolumeModifier setClientVolumeModifier;

    /// TS3Functions_startVoiceRecording
    public TS3Functions_startVoiceRecording startVoiceRecording;

    /// TS3Functions_stopVoiceRecording
    public TS3Functions_stopVoiceRecording stopVoiceRecording;

    /// TS3Functions_systemset3DListenerAttributes
    public TS3Functions_systemset3DListenerAttributes systemset3DListenerAttributes;

    /// TS3Functions_set3DWaveAttributes
    public TS3Functions_set3DWaveAttributes set3DWaveAttributes;

    /// TS3Functions_systemset3DSettings
    public TS3Functions_systemset3DSettings systemset3DSettings;

    /// TS3Functions_channelset3DAttributes
    public TS3Functions_channelset3DAttributes channelset3DAttributes;

    /// TS3Functions_startConnection
    public TS3Functions_startConnection startConnection;

    /// TS3Functions_stopConnection
    public TS3Functions_stopConnection stopConnection;

    /// TS3Functions_requestClientMove
    public TS3Functions_requestClientMove requestClientMove;

    /// TS3Functions_requestClientVariables
    public TS3Functions_requestClientVariables requestClientVariables;

    /// TS3Functions_requestClientKickFromChannel
    public TS3Functions_requestClientKickFromChannel requestClientKickFromChannel;

    /// TS3Functions_requestClientKickFromServer
    public TS3Functions_requestClientKickFromServer requestClientKickFromServer;

    /// TS3Functions_requestChannelDelete
    public TS3Functions_requestChannelDelete requestChannelDelete;

    /// TS3Functions_requestChannelMove
    public TS3Functions_requestChannelMove requestChannelMove;

    /// TS3Functions_requestSendPrivateTextMsg
    public TS3Functions_requestSendPrivateTextMsg requestSendPrivateTextMsg;

    /// TS3Functions_requestSendChannelTextMsg
    public TS3Functions_requestSendChannelTextMsg requestSendChannelTextMsg;

    /// TS3Functions_requestSendServerTextMsg
    public TS3Functions_requestSendServerTextMsg requestSendServerTextMsg;

    /// TS3Functions_requestConnectionInfo
    public TS3Functions_requestConnectionInfo requestConnectionInfo;

    /// TS3Functions_requestClientSetWhisperList
    public TS3Functions_requestClientSetWhisperList requestClientSetWhisperList;

    /// TS3Functions_requestChannelSubscribe
    public TS3Functions_requestChannelSubscribe requestChannelSubscribe;

    /// TS3Functions_requestChannelSubscribeAll
    public TS3Functions_requestChannelSubscribeAll requestChannelSubscribeAll;

    /// TS3Functions_requestChannelUnsubscribe
    public TS3Functions_requestChannelUnsubscribe requestChannelUnsubscribe;

    /// TS3Functions_requestChannelUnsubscribeAll
    public TS3Functions_requestChannelUnsubscribeAll requestChannelUnsubscribeAll;

    /// TS3Functions_requestChannelDescription
    public TS3Functions_requestChannelDescription requestChannelDescription;

    /// TS3Functions_requestMuteClients
    public TS3Functions_requestMuteClients requestMuteClients;

    /// TS3Functions_requestUnmuteClients
    public TS3Functions_requestUnmuteClients requestUnmuteClients;

    /// TS3Functions_requestClientPoke
    public TS3Functions_requestClientPoke requestClientPoke;

    /// TS3Functions_requestClientIDs
    public TS3Functions_requestClientIDs requestClientIDs;

    /// TS3Functions_clientChatClosed
    public TS3Functions_clientChatClosed clientChatClosed;

    /// TS3Functions_clientChatComposing
    public TS3Functions_clientChatComposing clientChatComposing;

    /// TS3Functions_requestServerTemporaryPasswordAdd
    public TS3Functions_requestServerTemporaryPasswordAdd requestServerTemporaryPasswordAdd;

    /// TS3Functions_requestServerTemporaryPasswordDel
    public TS3Functions_requestServerTemporaryPasswordDel requestServerTemporaryPasswordDel;

    /// TS3Functions_requestServerTemporaryPasswordList
    public TS3Functions_requestServerTemporaryPasswordList requestServerTemporaryPasswordList;

    /// TS3Functions_getClientID
    public TS3Functions_getClientID getClientID;

    /// TS3Functions_getClientSelfVariableAsInt
    public TS3Functions_getClientSelfVariableAsInt getClientSelfVariableAsInt;

    /// TS3Functions_getClientSelfVariableAsString
    public TS3Functions_getClientSelfVariableAsString getClientSelfVariableAsString;

    /// TS3Functions_setClientSelfVariableAsInt
    public TS3Functions_setClientSelfVariableAsInt setClientSelfVariableAsInt;

    /// TS3Functions_setClientSelfVariableAsString
    public TS3Functions_setClientSelfVariableAsString setClientSelfVariableAsString;

    /// TS3Functions_flushClientSelfUpdates
    public TS3Functions_flushClientSelfUpdates flushClientSelfUpdates;

    /// TS3Functions_getClientVariableAsInt
    public TS3Functions_getClientVariableAsInt getClientVariableAsInt;

    /// TS3Functions_getClientVariableAsUInt64
    public TS3Functions_getClientVariableAsUInt64 getClientVariableAsUInt64;

    /// TS3Functions_getClientVariableAsString
    public TS3Functions_getClientVariableAsString getClientVariableAsString;

    /// TS3Functions_getClientList
    public TS3Functions_getClientList getClientList;

    /// TS3Functions_getChannelOfClient
    public TS3Functions_getChannelOfClient getChannelOfClient;

    /// TS3Functions_getChannelVariableAsInt
    public TS3Functions_getChannelVariableAsInt getChannelVariableAsInt;

    /// TS3Functions_getChannelVariableAsUInt64
    public TS3Functions_getChannelVariableAsUInt64 getChannelVariableAsUInt64;

    /// TS3Functions_getChannelVariableAsString
    public TS3Functions_getChannelVariableAsString getChannelVariableAsString;

    /// TS3Functions_getChannelIDFromChannelNames
    public TS3Functions_getChannelIDFromChannelNames getChannelIDFromChannelNames;

    /// TS3Functions_setChannelVariableAsInt
    public TS3Functions_setChannelVariableAsInt setChannelVariableAsInt;

    /// TS3Functions_setChannelVariableAsUInt64
    public TS3Functions_setChannelVariableAsUInt64 setChannelVariableAsUInt64;

    /// TS3Functions_setChannelVariableAsString
    public TS3Functions_setChannelVariableAsString setChannelVariableAsString;

    /// TS3Functions_flushChannelUpdates
    public TS3Functions_flushChannelUpdates flushChannelUpdates;

    /// TS3Functions_flushChannelCreation
    public TS3Functions_flushChannelCreation flushChannelCreation;

    /// TS3Functions_getChannelList
    public TS3Functions_getChannelList getChannelList;

    /// TS3Functions_getChannelClientList
    public TS3Functions_getChannelClientList getChannelClientList;

    /// TS3Functions_getParentChannelOfChannel
    public TS3Functions_getParentChannelOfChannel getParentChannelOfChannel;

    /// TS3Functions_getServerConnectionHandlerList
    public TS3Functions_getServerConnectionHandlerList getServerConnectionHandlerList;

    /// TS3Functions_getServerVariableAsInt
    public TS3Functions_getServerVariableAsInt getServerVariableAsInt;

    /// TS3Functions_getServerVariableAsUInt64
    public TS3Functions_getServerVariableAsUInt64 getServerVariableAsUInt64;

    /// TS3Functions_getServerVariableAsString
    public TS3Functions_getServerVariableAsString getServerVariableAsString;

    /// TS3Functions_requestServerVariables
    public TS3Functions_requestServerVariables requestServerVariables;

    /// TS3Functions_getConnectionStatus
    public TS3Functions_getConnectionStatus getConnectionStatus;

    /// TS3Functions_getConnectionVariableAsUInt64
    public TS3Functions_getConnectionVariableAsUInt64 getConnectionVariableAsUInt64;

    /// TS3Functions_getConnectionVariableAsDouble
    public TS3Functions_getConnectionVariableAsDouble getConnectionVariableAsDouble;

    /// TS3Functions_getConnectionVariableAsString
    public TS3Functions_getConnectionVariableAsString getConnectionVariableAsString;

    /// TS3Functions_cleanUpConnectionInfo
    public TS3Functions_cleanUpConnectionInfo cleanUpConnectionInfo;

    /// TS3Functions_requestClientDBIDfromUID
    public TS3Functions_requestClientDBIDfromUID requestClientDBIDfromUID;

    /// TS3Functions_requestClientNamefromUID
    public TS3Functions_requestClientNamefromUID requestClientNamefromUID;

    /// TS3Functions_requestClientNamefromDBID
    public TS3Functions_requestClientNamefromDBID requestClientNamefromDBID;

    /// TS3Functions_requestClientEditDescription
    public TS3Functions_requestClientEditDescription requestClientEditDescription;

    /// TS3Functions_requestClientSetIsTalker
    public TS3Functions_requestClientSetIsTalker requestClientSetIsTalker;

    /// TS3Functions_requestIsTalker
    public TS3Functions_requestIsTalker requestIsTalker;

    /// TS3Functions_requestSendClientQueryCommand
    public TS3Functions_requestSendClientQueryCommand requestSendClientQueryCommand;

    /// TS3Functions_getTransferFileName
    public TS3Functions_getTransferFileName getTransferFileName;

    /// TS3Functions_getTransferFilePath
    public TS3Functions_getTransferFilePath getTransferFilePath;

    /// TS3Functions_getTransferFileSize
    public TS3Functions_getTransferFileSize getTransferFileSize;

    /// TS3Functions_getTransferFileSizeDone
    public TS3Functions_getTransferFileSizeDone getTransferFileSizeDone;

    /// TS3Functions_isTransferSender
    public TS3Functions_isTransferSender isTransferSender;

    /// TS3Functions_getTransferStatus
    public TS3Functions_getTransferStatus getTransferStatus;

    /// TS3Functions_getCurrentTransferSpeed
    public TS3Functions_getCurrentTransferSpeed getCurrentTransferSpeed;

    /// TS3Functions_getAverageTransferSpeed
    public TS3Functions_getAverageTransferSpeed getAverageTransferSpeed;

    /// TS3Functions_getTransferRunTime
    public TS3Functions_getTransferRunTime getTransferRunTime;

    /// TS3Functions_sendFile
    public TS3Functions_sendFile sendFile;

    /// TS3Functions_requestFile
    public TS3Functions_requestFile requestFile;

    /// TS3Functions_haltTransfer
    public TS3Functions_haltTransfer haltTransfer;

    /// TS3Functions_requestFileList
    public TS3Functions_requestFileList requestFileList;

    /// TS3Functions_requestFileInfo
    public TS3Functions_requestFileInfo requestFileInfo;

    /// TS3Functions_requestDeleteFile
    public TS3Functions_requestDeleteFile requestDeleteFile;

    /// TS3Functions_requestCreateDirectory
    public TS3Functions_requestCreateDirectory requestCreateDirectory;

    /// TS3Functions_requestRenameFile
    public TS3Functions_requestRenameFile requestRenameFile;

    /// TS3Functions_requestMessageAdd
    public TS3Functions_requestMessageAdd requestMessageAdd;

    /// TS3Functions_requestMessageDel
    public TS3Functions_requestMessageDel requestMessageDel;

    /// TS3Functions_requestMessageGet
    public TS3Functions_requestMessageGet requestMessageGet;

    /// TS3Functions_requestMessageList
    public TS3Functions_requestMessageList requestMessageList;

    /// TS3Functions_requestMessageUpdateFlag
    public TS3Functions_requestMessageUpdateFlag requestMessageUpdateFlag;

    /// TS3Functions_verifyServerPassword
    public TS3Functions_verifyServerPassword verifyServerPassword;

    /// TS3Functions_verifyChannelPassword
    public TS3Functions_verifyChannelPassword verifyChannelPassword;

    /// TS3Functions_banclient
    public TS3Functions_banclient banclient;

    /// TS3Functions_banadd
    public TS3Functions_banadd banadd;

    /// TS3Functions_banclientdbid
    public TS3Functions_banclientdbid banclientdbid;

    /// TS3Functions_bandel
    public TS3Functions_bandel bandel;

    /// TS3Functions_bandelall
    public TS3Functions_bandelall bandelall;

    /// TS3Functions_requestBanList
    public TS3Functions_requestBanList requestBanList;

    /// TS3Functions_requestComplainAdd
    public TS3Functions_requestComplainAdd requestComplainAdd;

    /// TS3Functions_requestComplainDel
    public TS3Functions_requestComplainDel requestComplainDel;

    /// TS3Functions_requestComplainDelAll
    public TS3Functions_requestComplainDelAll requestComplainDelAll;

    /// TS3Functions_requestComplainList
    public TS3Functions_requestComplainList requestComplainList;

    /// TS3Functions_requestServerGroupList
    public TS3Functions_requestServerGroupList requestServerGroupList;

    /// TS3Functions_requestServerGroupAdd
    public TS3Functions_requestServerGroupAdd requestServerGroupAdd;

    /// TS3Functions_requestServerGroupDel
    public TS3Functions_requestServerGroupDel requestServerGroupDel;

    /// TS3Functions_requestServerGroupAddClient
    public TS3Functions_requestServerGroupAddClient requestServerGroupAddClient;

    /// TS3Functions_requestServerGroupDelClient
    public TS3Functions_requestServerGroupDelClient requestServerGroupDelClient;

    /// TS3Functions_requestServerGroupsByClientID
    public TS3Functions_requestServerGroupsByClientID requestServerGroupsByClientID;

    /// TS3Functions_requestServerGroupAddPerm
    public TS3Functions_requestServerGroupAddPerm requestServerGroupAddPerm;

    /// TS3Functions_requestServerGroupDelPerm
    public TS3Functions_requestServerGroupDelPerm requestServerGroupDelPerm;

    /// TS3Functions_requestServerGroupPermList
    public TS3Functions_requestServerGroupPermList requestServerGroupPermList;

    /// TS3Functions_requestServerGroupClientList
    public TS3Functions_requestServerGroupClientList requestServerGroupClientList;

    /// TS3Functions_requestChannelGroupList
    public TS3Functions_requestChannelGroupList requestChannelGroupList;

    /// TS3Functions_requestChannelGroupAdd
    public TS3Functions_requestChannelGroupAdd requestChannelGroupAdd;

    /// TS3Functions_requestChannelGroupDel
    public TS3Functions_requestChannelGroupDel requestChannelGroupDel;

    /// TS3Functions_requestChannelGroupAddPerm
    public TS3Functions_requestChannelGroupAddPerm requestChannelGroupAddPerm;

    /// TS3Functions_requestChannelGroupDelPerm
    public TS3Functions_requestChannelGroupDelPerm requestChannelGroupDelPerm;

    /// TS3Functions_requestChannelGroupPermList
    public TS3Functions_requestChannelGroupPermList requestChannelGroupPermList;

    /// TS3Functions_requestSetClientChannelGroup
    public TS3Functions_requestSetClientChannelGroup requestSetClientChannelGroup;

    /// TS3Functions_requestChannelAddPerm
    public TS3Functions_requestChannelAddPerm requestChannelAddPerm;

    /// TS3Functions_requestChannelDelPerm
    public TS3Functions_requestChannelDelPerm requestChannelDelPerm;

    /// TS3Functions_requestChannelPermList
    public TS3Functions_requestChannelPermList requestChannelPermList;

    /// TS3Functions_requestClientAddPerm
    public TS3Functions_requestClientAddPerm requestClientAddPerm;

    /// TS3Functions_requestClientDelPerm
    public TS3Functions_requestClientDelPerm requestClientDelPerm;

    /// TS3Functions_requestClientPermList
    public TS3Functions_requestClientPermList requestClientPermList;

    /// TS3Functions_requestChannelClientAddPerm
    public TS3Functions_requestChannelClientAddPerm requestChannelClientAddPerm;

    /// TS3Functions_requestChannelClientDelPerm
    public TS3Functions_requestChannelClientDelPerm requestChannelClientDelPerm;

    /// TS3Functions_requestChannelClientPermList
    public TS3Functions_requestChannelClientPermList requestChannelClientPermList;

    /// TS3Functions_privilegeKeyUse
    public TS3Functions_privilegeKeyUse privilegeKeyUse;

    /// TS3Functions_requestPermissionList
    public TS3Functions_requestPermissionList requestPermissionList;

    /// TS3Functions_requestPermissionOverview
    public TS3Functions_requestPermissionOverview requestPermissionOverview;

    /// TS3Functions_clientPropertyStringToFlag
    public TS3Functions_clientPropertyStringToFlag clientPropertyStringToFlag;

    /// TS3Functions_channelPropertyStringToFlag
    public TS3Functions_channelPropertyStringToFlag channelPropertyStringToFlag;

    /// TS3Functions_serverPropertyStringToFlag
    public TS3Functions_serverPropertyStringToFlag serverPropertyStringToFlag;

    /// TS3Functions_getAppPath
    public TS3Functions_getAppPath getAppPath;

    /// TS3Functions_getResourcesPath
    public TS3Functions_getResourcesPath getResourcesPath;

    /// TS3Functions_getConfigPath
    public TS3Functions_getConfigPath getConfigPath;

    /// TS3Functions_getPluginPath
    public TS3Functions_getPluginPath getPluginPath;

    /// TS3Functions_getCurrentServerConnectionHandlerID
    public TS3Functions_getCurrentServerConnectionHandlerID getCurrentServerConnectionHandlerID;

    /// TS3Functions_printMessage
    public TS3Functions_printMessage printMessage;

    /// TS3Functions_printMessageToCurrentTab
    public TS3Functions_printMessageToCurrentTab printMessageToCurrentTab;

    /// TS3Functions_urlsToBB
    public TS3Functions_urlsToBB urlsToBB;

    /// TS3Functions_sendPluginCommand
    public TS3Functions_sendPluginCommand sendPluginCommand;

    /// TS3Functions_getDirectories
    public TS3Functions_getDirectories getDirectories;

    /// TS3Functions_getServerConnectInfo
    public TS3Functions_getServerConnectInfo getServerConnectInfo;

    /// TS3Functions_getChannelConnectInfo
    public TS3Functions_getChannelConnectInfo getChannelConnectInfo;

    /// TS3Functions_createReturnCode
    public TS3Functions_createReturnCode createReturnCode;

    /// TS3Functions_requestInfoUpdate
    public TS3Functions_requestInfoUpdate requestInfoUpdate;

    /// TS3Functions_getServerVersion
    public TS3Functions_getServerVersion getServerVersion;

    /// TS3Functions_isWhispering
    public TS3Functions_isWhispering isWhispering;

    /// TS3Functions_isReceivingWhisper
    public TS3Functions_isReceivingWhisper isReceivingWhisper;

    /// TS3Functions_getAvatar
    public TS3Functions_getAvatar getAvatar;

    /// TS3Functions_setPluginMenuEnabled
    public TS3Functions_setPluginMenuEnabled setPluginMenuEnabled;

    /// TS3Functions_showHotkeySetup
    public TS3Functions_showHotkeySetup showHotkeySetup;

    /// TS3Functions_requestHotkeyInputDialog
    public TS3Functions_requestHotkeyInputDialog requestHotkeyInputDialog;

    /// TS3Functions_getHotkeyFromKeyword
    public TS3Functions_getHotkeyFromKeyword getHotkeyFromKeyword;

    /// TS3Functions_getClientDisplayName
    public TS3Functions_getClientDisplayName getClientDisplayName;

    /// TS3Functions_getBookmarkList
    public TS3Functions_getBookmarkList getBookmarkList;

    /// TS3Functions_getProfileList
    public TS3Functions_getProfileList getProfileList;

    /// TS3Functions_guiConnect
    public TS3Functions_guiConnect guiConnect;

    /// TS3Functions_guiConnectBookmark
    public TS3Functions_guiConnectBookmark guiConnectBookmark;

    /// TS3Functions_createBookmark
    public TS3Functions_createBookmark createBookmark;

    /// TS3Functions_getPermissionIDByName
    public TS3Functions_getPermissionIDByName getPermissionIDByName;

    /// TS3Functions_getClientNeededPermission
    public TS3Functions_getClientNeededPermission getClientNeededPermission;
}