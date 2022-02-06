using System.Runtime.InteropServices;

namespace InsomniacGamesPS3EngineEditor
{
	[StructLayout(LayoutKind.Sequential)]
	public struct AssetSectionHeader
	{
		[MarshalAs(UnmanagedType.U4, SizeConst = 4)]
		public AssetSectionIdentifier indentifier;

		[MarshalAs(UnmanagedType.U4, SizeConst = 4)]
		public uint offset;

		[MarshalAs(UnmanagedType.U4, SizeConst = 4)]
		public uint one;

		[MarshalAs(UnmanagedType.U4, SizeConst = 4)]
		public uint length;
	}
}