<br>

<p align="center">
    <img src="https://raw.githubusercontent.com/hyperbx/Marathon/main/Marathon.Shared/Resources/Images/Logos/Marathon.png" width="238" height="178"/>
</p>

<h1 align="center">Marathon</h1>

<h3 align="center">A toolkit and library for SONIC THE HEDGEHOG (2006) file formats</h3>

<br>

# Building
See the [Building](https://github.com/hyperbx/Marathon/wiki/Building) page on the wiki.

# Supported
- Archive
    - [Arc File (`*.arc`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Archive/U8Archive.cs)
    - [DirectDraw Map (`*.ddm`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Archive/DirectDrawMap.cs)
- Audio
    - [Sound Bank (`*.sbk`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Audio/SoundBank.cs)
- Event
    - [Event Playbook (`*.epb`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Event/EventPlaybook.cs)
    - [Time Event (`*.tev`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Event/TimeEvent.cs)
- Kynapse
    - [Kynapse Big File (`*.kbf`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Kynapse/KynapseBigFile.cs)
- Mesh
    - [Land Collision (`collision.bin`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Mesh/LandCollision.cs)
    - [Reflection Area (`*.rab`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Mesh/ReflectionArea.cs)
    - [Spline Path (`*.path`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Mesh/SplinePath.cs)
- Ninja
    - [Camera (`*.xnc`, `*.xnd`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Ninja/CameraChunk.cs)
    - [Camera Animation (`*.xnd`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Ninja/CameraMotionChunk.cs)
    - [Effect List (`*.xne`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Ninja/EffectListChunk.cs)
    - [Light (`*.xni`, `*.xnl`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Ninja/LightChunk.cs)
    - [Light Animation (`*.xni`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Ninja/LightMotionChunk.cs)
    - [Material Animation (`*.xnv`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Ninja/MaterialMotionChunk.cs)
    - [Morph Animation (`*.xnf`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Ninja/MorphMotionChunk.cs)
    - [Morph Target (`*.xng`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Ninja/MorphTargetChunk.cs)
    - [Node Animation (`*.xnm`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Ninja/MotionChunk.cs)
    - [Node Name (`*.xna`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Ninja/NodeNameChunk.cs)
    - [Object (`*.xno`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Ninja/ObjectChunk.cs)
    - [Texture List (`*.xnt`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Ninja/TextureListChunk.cs)
- Parameter
    - [Enemy Parameter List (`ScriptParameter.bin`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Package/EnemyParameterList.cs)
    - [Enemy Shot Parameter List (`ShotParameter.bin`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Package/EnemyShotParameterList.cs)
    - [Object Explosion Parameter List (`Explosion.bin`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Package/ObjectExplosionParameterList.cs)
    - [Object Physics Parameter List (`Common.bin`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Package/ObjectPhysicsParameterList.cs)
    - [Package (`*.pkg`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Parameter/Package.cs)
    - [Path Object Parameter List (`PathObj.bin`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Package/PathObjParameterList.cs)
- Particle
    - [Particle Container (`*.plc`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Particle/ParticleContainer.cs)
    - [Particle Effect Bank (`*.peb`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Particle/ParticleEffectBank.cs)
    - [Particle Global Settings (`*.pgs`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Particle/ParticleGlobalSettings.cs)
    - [Particle Texture Bank (`*.ptb`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Particle/ParticleTextureBank.cs)
- Placement
    - [Prop Library (`*.prop`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Placement/PropLibrary.cs)
    - [Stage Set (`*.set`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Placement/StageSet.cs)
- Save
    - [Save Data (`SonicNextSaveData.bin`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Save/SonicNextSaveData.cs)
- Script
    - [Lua Binary (`*.lub`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Script/Lua/LuaBinary.cs)
- Text
    - [Text Book (`*.mst`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Text/TextBook.cs)
    - [Text Font Map (`*.ftm`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Text/TextFontMap.cs)
    - [Text Font Picture (`*.pft`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Text/TextFontPicture.cs)
    - [Text Font Proportion (`*.pfi`)](https://github.com/hyperbx/Marathon/blob/main/Marathon/Formats/Text/TextFontProportion.cs)
	
# Unsupported
Marathon currently doesn't support *every* format, despite the above list already being pretty large.

### Adaptive Transform Acoustic Coding 3 (ATRAC3)
The `*.at3` format is Sony's proprietary audio format used for the PlayStation 3 version of the game. There are no plans for Marathon to support this, as there are many open-source ways to create these files.

### Cue Sheet Binary
The `*.csb` format is part of CRIWARE's ADX middleware. There are currently no plans for Marathon to support this, as there would be better maintained open-source alternatives that aren't specific to this game.

### DirectDraw Surface
The `*.dds` format is for textures. There are no plans for Marathon to support this, there is plenty of software that can create these files already.

### PlayStation 3 Fragment Shader Bytecode
The `*.fpo` format is the compiled fragment shader format for the PlayStation 3 version of the game. There are no plans for Marathon to support this.

### DirectX Shader Bytecode
The `*.fxo` format is for compiled DirectX shaders. There are no plans for Marathon to support this, as there are already disassemblers and compilers for DirectX that are supported by the Xbox 360.

### Havok Binary
The `*.hkx` format is part of the Havok physics engine, specifically Havok 3.3.0-b2 for this game. There are no plans for Marathon to support this.

### Acroarts Binary
The `*.mab` format is part of SEGA's Acroarts middleware. It's used for miscellaneous event data for particle effects and timing with various cutscene elements (e.g. subtitles, models, etc).

### Motion Base Information
The `*.mbi` format is a plaintext file format containing node definitions for skeletons. This format hasn't been researched, but the data it stores is all clearly labelled by the internal tool that exported it.

### SoX Model Format
The `*.mdl` format is used by the PlayStation 3 version of the game in place of Ninja models used for terrain.

### Proprietary Sony video format
The `*.pam` format is Sony's proprietary video format used for the PlayStation 3 version of the game for pre-rendered events. Apparently, this format consists of `*.avi` and `*.at3` data, but the only encoder available is part of the PlayStation 3 SDK and doesn't allow you to embed audio data in the video. There are no plans for Marathon to support this.

### PlayStation 3 Vertex Shader Bytecode
The `*.vpo` format is the compiled vertex shader format for the PlayStation 3 version of the game. There are no plans for Marathon to support this.

### Windows Media Video
The `*.wmv` format is Microsoft's proprietary Windows Media Video format used for the Xbox 360 version of the game for pre-rendered events. There are no plans for Marathon to support this, as these files can be encoded by pretty much anything, but the audio tracks will need to be kept intact for English and Japanese audio, otherwise the game will either crash or skip the video.

### Xbox Media Audio
The `*.xma` format is Microsoft's proprietary variant of the Windows Media Audio format used for the Xbox 360 version of the game. There are no plans for Marathon to support this.

### Ninja Raw
The `*.xtm`, `*.xto` and `*.xtv` formats are leftover plaintext representations of their binary counterparts as auto-generated C code from a 3ds Max script. These plaintext versions are not supported and aren't used by the game, but they helped with research on the binary Ninja formats, which are supported by Marathon.
