<br>

<p align="center">
    <img src="https://raw.githubusercontent.com/hyperbx/Marathon/main/Marathon.Shared/Resources/Images/Logos/Marathon.png" width="238" height="178"/>
</p>

<h1 align="center">Marathon</h1>

<h3 align="center">A toolkit and library for SONIC THE HEDGEHOG file formats</h3>

<br>

# Building
See the [Building](https://github.com/hyperbx/Marathon/wiki/Building) page on the wiki.

# Capabilities
- AI
    - [Kynapse Big File (`*.kbf`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/AI/KynapseBigFile.cs) reading and writing
- Archive
    - [Arc File (`*.arc`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Archive/U8Archive.cs) reading and writing
- Audio
    - [Sound Bank (`*.sbk`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Audio/SoundBank.cs) reading and writing
- Event
    - [Event Playbook (`*.epb`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Event/EventPlaybook.cs) reading and writing
    - [Time Event (`*.tev`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Event/TimeEvent.cs) reading and writing
- Mesh
    - [Collision (`collision.bin`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Mesh/Collision.cs) reading and writing
    - [Ninja (`*.xna; *.xnd; *.xne; *.xnf; *.xng; *.xni; *.xnm; *.xno; *.xnv`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Mesh/Ninja/NinjaNext.cs) reading and writing
    - [Spline Path (`*.path`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Mesh/SplinePath.cs) reading and writing
    - [Reflection Area (`*.rab`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Mesh/ReflectionArea.cs) reading and writing
- Parameter
    - [Enemy Parameter List (`ScriptParameter.bin`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Package/EnemyParameterList.cs) reading and writing
    - [Enemy Shot Parameter List (`ShotParameter.bin`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Package/EnemyShotParameterList.cs) reading and writing
    - [Object Explosion Parameter List (`Explosion.bin`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Package/ObjectExplosionParameterList.cs) reading and writing
    - [Object Physics Parameter List (`Common.bin`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Package/ObjectPhysicsParameterList.cs) reading and writing
    - [Package (`*.pkg`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Parameter/Package.cs) reading and writing
    - [Path Object Parameter List (`PathObj.bin`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Package/PathObjParameterList.cs) reading and writing
- Particle
    - [Particle Container (`*.plc`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Particle/ParticleContainer.cs) reading and writing
    - [Particle Effect Bank (`*.peb`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Particle/ParticleEffectBank.cs) reading and writing
    - [Particle Global Settings (`*.pgs`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Particle/ParticleGlobalSettings.cs) reading and writing
    - [Particle Texture Bank (`*.ptb`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Particle/ParticleTextureBank.cs) reading and writing
- Placement
    - [Prop Library (`*.prop`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Placement/PropLibrary.cs) reading and writing
    - [Stage Set (`*.set`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Placement/StageSet.cs) reading and writing    
- Save
    - [Save Data (`SonicNextSaveData.bin`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Save/SonicNextSaveData.cs) reading and writing
- Script
    - [Lua Binary (`*.lub`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Script/Lua/LuaBinary.cs) reading and writing
- Text
    - [Text Book (`*.mst`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Text/TextBook.cs) reading and writing
    - [Text Font Picture (`*.pft`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Text/TextFontPicture.cs) reading and writing
	
# Unsupported

Marathon currently doesn't support *every* format, despite the above list already being pretty large.

### Adaptive Transform Acoustic Coding 3 (ATRAC3)
The `*.at3` format is Sony's proprietary audio format used for the PlayStation 3 version of the game. There are no plans for Marathon to support this, as there are many open-source ways to create these files.

### Cue Sheet Binary
The `*.csb` format is part of CRIWARE's ADX middleware. There are currently no plans for Marathon to support this, as there would be better maintained open-source alternatives that aren't specific to this game.

### Proprietary Sonic Team texture container
The `*.ddm` format is a basic container that stores the names of textures per index and all of the texture data that pertains to them in the DirectDraw Surface format. It was supported by Marathon at one point, but only had reading capabilities.

### DirectDraw Surface
The `*.dds` format is for textures. There are no plans for Marathon to support this, there is plenty of software that can create these files already.

### Text Font Map
The `*.ftm` format contains information about how the characters are mapped to the font textures. This format is a bit of a nightmare, so not much research has been done on it yet.

### PlayStation 3 Fragment Shader Bytecode
The `*.fpo` format is the compiled fragment shader format for the PlayStation 3 version of the game. There are no plans for Marathon to support this.

### DirectX Shader Bytecode
The `*.fxo` format is for compiled DirectX shaders. There are no plans for Marathon to support this, as there are already disassemblers and compilers for DirectX that are supported by the Xbox 360.

### Havok Binary
The `*.hkx` format is part of the Havok physics engine, specifically Havok 3.3.0-b2 for this game. There are no plans for Marathon to support this.

### Acroarts Binary
The `*.mab` format is part of CRIWARE's Acroarts middleware. It's used for miscellaneous event data for particle effects and timing with various cutscene elements (e.g. subtitles, models, etc).

### Motion Base Information
The `*.mbi` format is a plaintext file format containing node definitions for skeletons. This format hasn't been researched, but the data it stores is all clearly labelled by the internal tool that exported it.

### SoX Model Format
The `*.mdl` format is used by the PlayStation 3 version of the game in place of Ninja models used for terrain.

### Proprietary Sony video format
The `*.pam` format is Sony's proprietary video format used for the PlayStation 3 version of the game for pre-rendered events. Apparently, this format consists of `*.avi` and `*.at3` data, but the only encoder available is part of the PlayStation 3 SDK and doesn't allow you to embed audio data in the video. There are no plans for Marathon to support this.

### Text Font Proportion
The `*.pfi` format defines the margin and padding per character in a given font map. It was supported by Marathon at one point, but only had a reader and some data wasn't fully researched.

### PlayStation 3 Vertex Shader Bytecode
The `*.vpo` format is the compiled vertex shader format for the PlayStation 3 version of the game. There are no plans for Marathon to support this.

### Windows Media Video
The `*.wmv` format is Microsoft's proprietary Windows Media Video format used for the Xbox 360 version of the game for pre-rendered events. There are no plans for Marathon to support this, as these files can be encoded by pretty much anything, but the audio tracks will need to be kept intact for English and Japanese audio, otherwise the game will either crash or skip the video.

### Xbox Media Audio
The `*.xma` format is Microsoft's proprietary variant of the Windows Media Audio format used for the Xbox 360 version of the game. There are no plans for Marathon to support this.

### Ninja Morph
The Ninja format, particularly for `*.xno` files (such as `kdv_obj_cage02.xno`) have an `NXMT` chunk that is currently unsupported, preventing these files from being read correctly.

### Ninja Raw
The `*.xtm`, `*.xto` and `*.xtv` formats are leftover plaintext representations of their binary counterparts as auto-generated C code from a 3ds Max script. These plaintext versions are not supported and aren't used by the game, but they helped with research on the binary Ninja formats, which are supported by Marathon.