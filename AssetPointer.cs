using System.Runtime.InteropServices;

namespace InsomniacGamesPS3EngineEditor
{
	[StructLayout(LayoutKind.Sequential)]
	public struct AssetPointer
	{
		[MarshalAs(UnmanagedType.U8, SizeConst = 8)]
		public ulong identifier;

		[MarshalAs(UnmanagedType.U4, SizeConst = 4)]
		public uint offset;

		[MarshalAs(UnmanagedType.U4, SizeConst = 4)]
		public uint length;

		public AssetPointer(StreamHelper sh)
		{
			identifier = sh.ReadUInt64();
			offset   = sh.ReadUInt32();
			length   = sh.ReadUInt32();
		}
	}
}