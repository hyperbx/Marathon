<br>

<p align="center">
    <img src="https://raw.githubusercontent.com/Big-Endian-32/Marathon/master/Marathon.Shared/Resources/Images/Logos/Marathon.png" width="238" height="178"/>
</p>

<h1 align="center">Marathon</h1>

<h3 align="center">A toolkit and library for SONIC THE HEDGEHOG file formats</h3>

<br>

# Releases

### GitHub Releases
This is where [stable builds](https://github.com/HyperBE32/Marathon/releases) of Marathon are published; if you're looking for the most reliable experience.

### GitHub Actions
While it can be fun to live on the bleeding edge, [GitHub Actions](https://github.com/Big-Endian-32/Marathon/actions) publishes new builds for each new commit, so changes can be unstable.

### Pre-requisites
Marathon works on Windows 7 SP1 and later, but ***requires*** .NET 6.0, please install both [x86](https://dotnet.microsoft.com/download/dotnet/thank-you/runtime-6.0.0-windows-x86-installer) and [x64](https://dotnet.microsoft.com/download/dotnet/thank-you/runtime-6.0.0-windows-x64-installer) versions of the runtime.

# Building

### Windows
- Windows 10 (x64) version 1909 or higher
- Visual Studio 2022
- .NET 6.0 runtime and SDK

Clone Marathon and open the solution in Visual Studio 2022 and build for the necessary configuration.

# Capabilities

- Archive
    - [U8 Archive (`*.arc`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Archive/U8Archive.cs) reading and writing
- Audio
    - [Sound Bank (`*.sbk`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Audio/SoundBank.cs) reading and writing
- Event
    - [Event Playbook (`*.epb`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Event/EventPlaybook.cs) reading and writing
    - [Time Event (`*.tev`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Event/TimeEvent.cs) reading and writing
- Mesh
    - [Collision (`collision.bin`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Mesh/Collision.cs) reading and writing
	- [Ninja (`*.xna; *.xnd; *.xne; *.xnf; *.xng; *.xni; *.xnm; *.xno; *.xnv`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Mesh/Ninja/NinjaNext.cs) reading and writing
    - [Path Spline (`*.path`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Mesh/PathSpline.cs) reading and writing
    - [Reflection Zone (`*.rab`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Mesh/ReflectionZone.cs) reading and writing
- Package
    - [Asset Package (`*.pkg`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Package/AssetPackage.cs) reading and writing
    - [Common Package (`Common.bin`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Package/CommonPackage.cs) reading and writing
    - [Explosion Package (`Explosion.bin`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Package/ExplosionPackage.cs) reading and writing
    - [Path Package (`PathObj.bin`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Package/PathPackage.cs) reading and writing
    - [Script Package (`ScriptParameter.bin`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Package/ScriptPackage.cs) reading and writing
    - [Shot Package (`ShotParameter.bin`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Package/ShotPackage.cs) reading and writing
- Particle
    - [Particle Container (`*.plc`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Particle/ParticleContainer.cs) reading and writing
    - [Particle Effect Bank (`*.peb`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Particle/ParticleEffectBank.cs) reading and writing
    - [Particle Generation System (`*.pgs`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Particle/ParticleGenerationSystem.cs) reading and writing
    - [Particle Texture Bank (`*.ptb`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Particle/ParticleTextureBank.cs) reading and writing
- Placement
    - [Object Placement (`*.set`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Placement/ObjectPlacement.cs) reading and writing    
    - [Object Property Database (`*.prop`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Placement/ObjectPropertyDatabase.cs) reading and writing
- Save
    - [Save Data (`SonicNextSaveData.bin`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Save/SonicNextSaveData.cs) reading and writing
- Script
    - [Lua Binary (`*.lub`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Script/Lua/LuaBinary.cs) reading and writing
- Text
    - [Message Table (`*.mst`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Text/MessageTable.cs) reading and writing
    - [Picture Font (`*.pft`)](https://github.com/Big-Endian-32/Marathon/blob/master/Marathon/Formats/Text/PictureFont.cs) reading and writing
	
# Unsupported

Marathon currently doesn't support *every* format, despite the above list already being pretty large.

### Adaptive Transform Acoustic Coding 3 (ATRAC3)

The `*.at3` format is SONY's proprietary audio format used for the PlayStation 3 version of the game. There are many open-source ways to encode these files.

### Cue Sheet Binary

The `*.csb` format is part of the closed-source CriWare audio library; ADX. We have some specifications for this format, as well as the `*.cpk` format which contains the actual data, but it's yet to be implemented into Marathon.

### Proprietary Sonic Team texture container

The `*.ddm` format is a basic container that stores the names of textures per index and all of the texture data that pertains to them in the DirectDraw Surface format. It was supported by Marathon at one point, but only had a reader that exported the textures in a hacky way.

### DirectDraw Surface

The `*.dds` format is for textures - there's no need for Marathon to support this, as there are plenty of image manipulation programs that support it already.

### Font Map

The `*.ftm` format contains information pertaining to the Shift-JIS encoding standard and defines the characters used in the font textures. This format is a bit of a nightmare, so not much research has been done on it yet.

### PlayStation 3 Fragment Shader Bytecode

The `*.fpo` format is the compiled fragment shader format for the PlayStation 3 version of the game. Unlike DirectX, each shader technique in this format is split up into separate files, rather than being in a single shader.

### DirectX Shader Bytecode

The `*.fxo` format is for compiled DirectX shaders - there's no need for Marathon to support this, as there are already disassemblers and compilers for DirectX that are supported by Xbox 360.

### Havok Collision Data

The `*.hkx` format is part of the closed-source Havok physics library. SONIC THE HEDGEHOG uses Havok 3.3.0-b2, which is conveniently a version of Havok that nobody has outside of development studios.

### Kynapse Big File

The `*.kbf` format is part of the closed-source Kynapse AI library. This format contains data pertaining to AI behaviour, spatial graphs, `Astar` data, `FindNearest` data, `PathCost` data, path ways and meshes. You can probably see where it gets the name 'big file' from. There has only been minimal research done on this format, as the embedded formats will also have to be researched individually.

### Proprietary CriWare and Sonic Team property format

The `*.mab` format was originally believed to be closely related to particles, since the particles that used meshes were always in this format. However, after digging around the event data for cutscenes, it seems it's also used for timing with various elements (e.g. subtitles, models, etc). The internal assertions for this format refer to it as [Acroarts](https://web.archive.org/web/20211030052512/https://www.cri-mw.co.jp/product/cs/acroarts/index.html), which is a development environment from CriWare.

### Motion Base Information

The `*.mbi` format is a plaintext file containing node definitions for a skeleton. This format hasn't been researched, but the data it represents is all helpfully labelled by whatever internal tool from SEGA exported it.

### PlayStation 3 Model Format

Stage models in the PlayStation 3 version of the game are usually split up into a unique `*.mdl` format, while preliminary research has been done on this format a lot of the extra data beyond the simple model construction is unknown.

### Proprietary SONY video format

The `*.pam` format is SONY's proprietary video format used for the PlayStation 3 version of the game for pre-rendered events. Apparently, this format consists of `*.avi` and `*.at3` data, but the only encoder available is part of the PlayStation 3 SDK and doesn't allow you to embed audio data in the video.

### Font Proportion

The `*.pfi` format defines the margin and padding per character in a given font map. It was supported by Marathon at one point, but only had a reader and some data wasn't fully researched.

### PlayStation 3 Vertex Shader Bytecode

The `*.vpo` format is the compiled vertex shader format for the PlayStation 3 version of the game. Unlike DirectX, each shader technique in this format is split up into separate files, rather than being in a single shader.

### Windows Media Video

The `*.wmv` format is Microsoft's proprietary Windows Media Video format used for the Xbox 360 version of the game for pre-rendered events. These files can be encoded by pretty much anything, but the audio tracks will need to be kept intact for English and Japanese audio, otherwise the game will either crash or skip the video.

### Xbox Media Audio

The `*.xma` format is Microsoft's proprietary variant of the Windows Media Audio format used for the Xbox 360 version of the game. You can use Xbox 360 SDK Build 3424 to encode these files properly for use with SONIC THE HEDGEHOG.

### Unknown Ninja Chunk

A few `*.xno` files (such as `kdv_obj_cage02.xno`) have a `NXMT` chunk (believed to stand for Ninja Morph Type) that is currently unsupported, preventing these files from being fully read by Marathon.

### Ninja Raw

The `*.xtm`, `*.xto` and `*.xtv` formats are leftover plaintext representations of their compiled counterparts as auto-generated C code from a 3ds Max script. These plaintext versions are not supported and aren't used by the game, but they helped with research on the actual compiled Ninja formats, which are supported by Marathon.
