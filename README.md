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

### Cue Sheet Binary (`*.csb`)

The `*.csb` format is part of the closed-source CriWare audio library; ADX. We have some specifications for this format, as well as CPK which contains the actual data, but it's yet to be implemented into Marathon.

### Proprietary Sonic Team texture container (`*.ddm`)

The `*.ddm` format is a basic container that stores the names of textures per index and all of the texture data that pertains to them in the DirectDraw Surface format. It was supported by Marathon at one point, but only had a reader that exported the textures in a hacky way.

### DirectDraw Surface (`*.dds`)

The `*.dds` format is for textures - there's no need for Marathon to support this, as there are plenty of image manipulation programs that support it already.

### Font Map (`*.ftm`)

The `*.ftm` format contains information pertaining to the Shift-JIS encoding standard and defines the characters used in the font textures. This format is a bit of a nightmare, so not much research has been done on it yet.

### DirectX Shader Bytecode (`*.fxo`)

The `*.fxo` format is for compiled DirectX shaders - there's no need for Marathon to support this, as there are already disassemblers and compilers for DirectX that are supported by Xbox 360.

### Havok Collision Data (`*.hkx`)

The `*.hkx` format is part of the closed-source Havok physics library. SONIC THE HEDGEHOG uses Havok 3.3.0-b2, which is conveniently a version of Havok that nobody has outside of development studios.

### Kynapse Big File (`*.kbf`)

The `*.kbf` format is part of the closed-source Kynapse AI library. This format contains data pertaining to AI behaviour, spatial graphs, `Astar` data, `FindNearest` data, `PathCost` data, path ways and meshes. You can probably see where it gets the name 'big file' from. There has only been minimal research done on this format, as the embedded formats will also have to be researched individually.

### Proprietary CriWare & Sonic Team property format (`*.mab`)

The `*.mab` format was originally believed to be closely related to particles, since the particles that used meshes were always in this format. However, after digging around the event data for cutscenes, it seems it's also used for timing with various elements (e.g. subtitles, models, etc).

### Motion Base Information (`*.mbi`)

The `*.mbi` format is a plaintext file containing node definitions for a skeleton. This format hasn't been researched, but the data it represents is all helpfully labelled by whatever internal tool from SEGA exported it.

### Font Proportion (`*.pfi`)

The `*.pfi` format defines the margin and padding per character in a given font map. It was supported by Marathon at one point, but only had a reader and some data wasn't fully researched.

### Ninja Raw (`*.xtm; *.xto; *.xtv`)

The `*.xtm`, `*.xto` and `*.xtv` formats are leftover plaintext representations of their compiled counterparts as auto-generated C code from a 3ds Max script. These plaintext versions are not supported and aren't used by the game, but they helped with research on the actual compiled Ninja formats, which are supported by Marathon.