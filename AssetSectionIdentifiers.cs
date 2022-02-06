namespace InsomniacGamesPS3EngineEditor
{
	public enum AssetSectionIdentifier : uint
	{
		Unknown				= 0x0001D000, 		//?? (Is the number 6)
		ShaderPointers		= 0x0001D100, 		//Shaders Asset Pointer Array (shaders.dat)
		ShaderOnes			= 0x0001D101, 		//ones
		TextureMetadata		= 0x0001D140, 		//Textures Metadata array
		TexturePointers		= 0x0001D180, 		//Textures Asset Pointer Array (textures.dat)
		TextureOnes			= 0x0001D181, 		//ones
		HighmipPointers		= 0x0001D1C0, 		//Highmips Asset Pointer Array (highmips.dat)
		CubemapPointers		= 0x0001D200, 		//Cubemaps Asset Pointer Array (cubemaps.dat)
		CubemapOnes			= 0x0001D201, 		//ones
		TiesPointers		= 0x0001D300, 		//Ties Asset Pointer Array (ties.dat)
		TiesOnes			= 0x0001D301, 		//ones
		MobyPointers		= 0x0001D600, 		//Mobys Asset Pointer Array (mobys.dat)
		MobyOnes			= 0x0001D601, 		//ones
		AnimsetPointers		= 0x0001D700, 		//Animsets Asset Pointer Array (animsets.dat)
		AnimsetOnes			= 0x0001D701, 		//ones
		ZonePointers		= 0x0001DA00, 		//Zones Asset Pointer Array (zones.dat)
		ZoneOnes			= 0x0001DA01, 		//ones
		LightingPointers	= 0x0001DB00, 		//Lighting Asset Pointer Array (lighting.dat)
		LightingOnes 		= 0x0001DB01  		//ones
	}
}