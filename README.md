<br>

<p align="center">
    <img src="https://raw.githubusercontent.com/HyperBE32/Marathon/legacy-marathon-master/Marathon.Resources/Resources/Images/Logos/Toolkit_Medium_Colour.png" />
</p>

<h1 align="center">Marathon</h1>

<h3 align="center">A toolkit and library for SONIC THE HEDGEHOG file formats</h3>

<br>

# Releases

### GitHub Releases
This is where [stable builds](https://github.com/HyperBE32/Marathon/releases) of Marathon are published; if you're looking for the most reliable experience.

### GitHub Actions
While it can be fun to live on the bleeding edge; [GitHub Actions](https://github.com/Big-Endian-32/Marathon/actions) publishes new builds for each new commit, so changes can be unstable.

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