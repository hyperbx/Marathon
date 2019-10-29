<p align="center">
    <a href="https://github.com/HyperPolygon64/Sonic-06-Toolkit/blob/master/">
        <img src="https://github.com/HyperPolygon64/Sonic-06-Toolkit/blob/master/logo_small.png" />
    </a>
</p>

<h1 align="center">Sonic '06 Toolkit</h1>

<p align="center">Sonic '06 Toolkit is a C# application developed to help make modding easier for SONIC THE HEDGEHOG (2006)</p>

# Usage
### "I just want a download link."
See the [releases page](https://github.com/HyperPolygon64/Sonic-06-Toolkit/releases), it doesn't get any simpler than that.

### "I want to build it myself."
Sure! Use Git to clone the repository to your drive and use [Visual Studio](https://visualstudio.microsoft.com/) to compile it (this project was developed and built with Visual Studio 2019).

### "Are there any pre-requisites that are required?"
Yes, see the [Requirements](https://github.com/HyperPolygon64/Sonic-06-Toolkit#requirements) section at the bottom of this document.

### "How do I mod this game, then?"
Refer to the [wiki](https://github.com/HyperPolygon64/Sonic-06-Toolkit/wiki) for more in-depth information.

# Features
### ADX Encoding and CSB Extracting
- Currently based off [Skyth](https://github.com/blueskythlikesclouds)'s [SonicAudioLib](https://github.com/blueskythlikesclouds/SonicAudioTools) library for extracting CSB files and [Alex Barney](https://github.com/Thealexbarney)'s [VGAudio](https://github.com/Thealexbarney/VGAudio) library.

### ARC Extracting
- Currently based off g0ldenlink, xose and [Shadow LAG](https://github.com/lllsondowlll)'s ARC unpacker/repacker.

### AT3 Encoding
- Currently based off ATRAC3plus TOOL, developed by SONY Computer Entertainment Inc.

### BIN Encoding
- Currently based off DarioSamo's [S06Collision](https://github.com/DarioSamo/libgens-sonicglvl/blob/master/src/LibS06/S06Collision.cpp) class.

### DDS Converting
- Currently based off Microsoft DirectX 12 and some internal work for previewing textures.

### LUB Decompiling
- Currently based off [Shadow LAG](https://github.com/lllsondowlll)'s Java-based Lua decompiler.

### MST Encoding
- Currently based off [GerbilSoft](https://github.com/GerbilSoft)'s [mst06](https://github.com/GerbilSoft/mst06) tool.

### SET Encoding (experimental)
- Currently based off [Radfordhound](https://github.com/Radfordhound)'s [HedgeLib](https://github.com/Radfordhound/HedgeLib) S06SetData class.

### Xbox 360 ISO Extracting
- Currently based off [Swizzy](https://github.com/Swizzy)'s [XISOExtractorGUI](https://github.com/Swizzy/XISOExtractorGUI) code.

### XMA Encoding
- Currently based off [Microsoft](https://www.microsoft.com/en-gb)'s xmaencode2008 tool and a patcher developed for Sonic '06 Toolkit to play the XMAs in-game.

### XNO/XNM Decoding
- Currently based off [Skyth](https://github.com/blueskythlikesclouds) and [DaríoSamo](https://github.com/DarioSamo)'s [XNO converter](https://github.com/blueskythlikesclouds/SkythTools/blob/master/Sonic%20'06/xno2dae.exe).

# Project Goals
### XNCP Decoding
- <b>Help wanted!</b> We currently don't have a way to decompile XNCP files. These files potentially contain information and/or values for menu placement, colour and whatnot.

# Pre-requisites
### ATRAC3plus TOOL
- Currently requires the [x86 variant of Microsoft Visual C++ 2010](https://www.microsoft.com/en-us/download/details.aspx?id=5555) to work.

### s06collision
- Currently requires [Python](https://www.python.org/downloads/windows/) to work.

### unlub
- Currently requires the [latest version of Java](https://www.java.com/en/download/) to work.

### xno2dae
- Currently requires both [x86](https://www.microsoft.com/de-de/download/details.aspx?id=8328) and [x64](https://www.microsoft.com/en-us/download/details.aspx?id=13523) versions of Microsoft Visual C++ 2010 SP1 to work.
