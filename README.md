# Insomniac Games PS3 Engine Editor

A modding tool for the PS3 Ratchet and Clank games + maybe a couple resistance games idk

## Usage

* Extract your level_cached.psarc and level_uncached.psarc using a tool of your choice.
* Upon starting the program, you'll be asked to find an assetlookup.dat file, do so :)
* You may be prompted to find highmips.dat, this is usually in level_uncached.psarc

### Viewing/Editing Textures

* Once assetlookup.dat is loaded, click "Edit Textures".
* Feel free to scroll through the textures.
* To extract a texture, click the extract button when the desired texture is selected. Do note these textures are upside down as they're upside down in the files.
* To replace a texture, click the replace button when the desired texture is selected. You'll be prompted to select an input image, do so and happy modding. Do note that the program expects the input image to be upside down as that's how they are in the files.

## Features

| Game | Texture Editing |
|---|---|
| Resistance: Fall of Man | ❌ |
| Ratchet & Clank (Future): Tools of Destruction | ❌ |
| Ratchet & Clank (Future): Quest for Booty | ✅ |
| Resistance 2 | ✅ |
| Ratchet & Clank (Future): A Crack in Time | ✅ |
| Resistance 3 | ✅ |
| Ratchet & Clank: All 4 One | ✅ |
| Ratchet & Clank: Full Frontal Assault/Q-Force | ✅ |
| Ratchet & Clank: Into the Nexus/Nexus | ✅ |

## To Do

* Support ToD and Resistance 1
* Add back text viewing + maybe text editing
* Add other, cooler things like models and audio

## Credits
* NefariousTechSupport: Created this
* Nominom: Their [BCnEncoder](https://github.com/Nominom/BCnEncoder.NET) library was used for textures
* SixLabours: Their [ImageSharp](https://github.com/SixLabors/ImageSharp) library was used for textures
* AdventureT: StreamHelper.cs is a heavily modified version of [this](https://github.com/AdventureT/TrbMultiTool/blob/opengl/TrbMultiTool/TrbMultiTool/EndiannessAwareBinaryReader.cs)