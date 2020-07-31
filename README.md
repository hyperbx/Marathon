<br>

<p align="center">
    <img src="https://github.com/HyperPolygon64/Marathon/blob/marathon-master/Marathon.Toolkit/Resources/Images/Logos/Main_Logo_Medium_Colour.png?raw=true" />
</p>

<h1 align="center">Marathon</h1>

<h3 align="center">A work-in-progress toolkit and library for SONIC THE HEDGEHOG (2006) file formats</h3>

# Releases

### GitHub Releases
This is where [stable builds](https://github.com/HyperPolygon64/Marathon/releases) of Marathon are published; if you're looking for the most reliable experience.

### AppVeyor
While it can be fun to live on the bleeding edge; [AppVeyor builds](https://ci.appveyor.com/project/HyperPolygon64/marathon) are published for each new commit, so changes can be unstable.

# Capabilities

### Marathon.IO
- Supported formats:
    - Archives
        - [Compressed U8 Archive (ARC)](https://github.com/HyperPolygon64/Marathon/blob/marathon-master/Marathon.IO/Formats/Archives/CompressedU8Archive.cs) reading and writing
    - Meshes
        - [Collision (BIN)](https://github.com/HyperPolygon64/Marathon/blob/marathon-master/Marathon.IO/Formats/Meshes/Collision.cs) reading and writing
        - [Reflection Zone (RAB)](https://github.com/HyperPolygon64/Marathon/blob/marathon-master/Marathon.IO/Formats/Meshes/ReflectionZone.cs) reading and writing
    - Miscellaneous
        - [Asset Package (PKG)](https://github.com/HyperPolygon64/Marathon/blob/marathon-master/Marathon.IO/Formats/Miscellaneous/AssetPackage.cs) reading and writing
        - [Object Property Database (PROP)](https://github.com/HyperPolygon64/Marathon/blob/marathon-master/Marathon.IO/Formats/Miscellaneous/ObjectPropertyDatabase.cs) reading and writing
        - [Path Package (PathObj.bin)](https://github.com/HyperPolygon64/Marathon/blob/marathon-master/Marathon.IO/Formats/Miscellaneous/PathPackage.cs) reading and writing
    - Particles
        - [Particle List Container (PLC)](https://github.com/HyperPolygon64/Marathon/blob/marathon-master/Marathon.IO/Formats/Particles/ParticleListContainer.cs) reading and writing
        - [Particle Texture Bank (PTB)](https://github.com/HyperPolygon64/Marathon/blob/marathon-master/Marathon.IO/Formats/Particles/ParticleTextureBank.cs) reading and writing
    - Placement
        - [Object Placement (SET)](https://github.com/HyperPolygon64/Marathon/blob/marathon-master/Marathon.IO/Formats/Placement/ObjectPlacement.cs) reading and writing
    - Sounds
        - [Sound Bank (SBK)](https://github.com/HyperPolygon64/Marathon/blob/marathon-master/Marathon.IO/Formats/Sound/SoundBank.cs) reading and writing
    - Text
        - [Picture Font (PFT)](https://github.com/HyperPolygon64/Marathon/blob/marathon-master/Marathon.IO/Formats/Text/PictureFont.cs) reading and writing
        - [Text (MST)](https://github.com/HyperPolygon64/Marathon/blob/marathon-master/Marathon.IO/Formats/Text/Text.cs) reading and writing

### Marathon.Toolkit
- This project is currently not ready for use.

### Marathon.Wiki
- The wiki can be found [here](https://github.com/HyperPolygon64/Marathon/wiki) and links to the available HTML pages included in the repository.