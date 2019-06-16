<p align="center">
    <a href="https://github.com/HyperPolygon64/Sonic-06-Toolkit/blob/master/">
        <img src="https://github.com/HyperPolygon64/Sonic-06-Toolkit/blob/master/logo_small.png" />
    </a>
</p>

<h1 align="center">Sonic '06 Toolkit</h1>

<p align="center">Sonic '06 Toolkit is an application developed to help make modding easier for SONIC THE HEDGEHOG (2006). Now being written in C#, Sonic '06 Toolkit has been made open-source to allow the community to contribute to the project.</p>

# Usage
### "I just want a download link."
See the [releases page](https://github.com/HyperPolygon64/Sonic-06-Toolkit/releases), it doesn't get any simpler than that.

### "I want to build it myself."
Sure! Use Git to clone the repository to your drive. The solution was created using Visual Studio 2019, but you can still build it using Visual Studio 2017 just fine.

### "Are there any dependencies that are required?"
Yes, see the [Requirements](https://github.com/HyperPolygon64/Sonic-06-Toolkit#requirements) section at the bottom of this document.

### "How do I mod this game, then?"
Refer to the [wiki](https://github.com/HyperPolygon64/Sonic-06-Toolkit/wiki) for more in-depth information.

# Features
### ADX Encoding and CSB Extracting
- Currently based off [Skyth](https://github.com/blueskythlikesclouds)'s [Sonic Audio Tools](https://github.com/blueskythlikesclouds/SonicAudioTools) for extracting CSB files and some tools from [CRI Middleware](https://www.criware.com/en/) for encoding ADX files.

### ARC Unpacking and Repacking
- Currently based off g0ldenlink, xose and [Shadow LAG](https://github.com/lllsondowlll)'s ARC unpacker/repacker.

### AT3 Encoding
- Currently based off AT3 Tool, developed by SONY Computer Entertainment Inc.

### DDS Converting
- Currently based off Microsoft DirectX. This may need more work as converting PNG to DDS results in a slightly darker image.

### LUB Decompiling
- Currently based off [Shadow LAG](https://github.com/lllsondowlll)'s Java-based Lua decompiler.

### MST Exporting and Importing
- Currently based off [GerbilSoft](https://github.com/GerbilSoft)'s [mst06](https://github.com/GerbilSoft/mst06) tool.

### SET Exporting and Importing (experimental)
- Currently based off [Radfordhound](https://github.com/Radfordhound)'s [HedgeLib](https://github.com/Radfordhound/HedgeLib) S06SetData class.

### Xbox 360 ISO Extracting
- Currently based off in's [extract-xiso](https://github.com/XboxDev/extract-xiso) tool.

### XMA Encoding
- Currently based off [Microsoft](https://www.microsoft.com/en-gb)'s xmaencode2008 tool and a patcher developed for Sonic '06 Toolkit to play the XMAs in-game.

### XNO Converting and XNM Pairing
- Currently based off [Skyth](https://github.com/blueskythlikesclouds) and [DarioSamo](https://github.com/DarioSamo)'s [XNO converter](https://github.com/blueskythlikesclouds/SkythTools/blob/master/Sonic%20'06/xno2dae.exe).

# Project Goals
### XNCP Decompiling
- <b>Help wanted!</b> We currently don't have a way to decompile XNCP files. These files potentially contain information and/or values for menu placement, colour and whatnot.

# Requirements
### ADX Encoder
- Currently requires both [x86](https://www.microsoft.com/de-de/download/details.aspx?id=8328) and [x64](https://www.microsoft.com/en-us/download/details.aspx?id=13523) versions of Microsoft Visual C++ 2010 SP1 to work.

### AT3 Encoder
- Currently requires the [x86 variant of Microsoft Visual C++ 2010](https://www.microsoft.com/en-us/download/details.aspx?id=5555) to work.

### LUB Decompiler
- Currently requires the [latest version of Java](https://www.java.com/en/download/) to work.

### XNO Converter
- Currently requires both [x86](https://www.microsoft.com/de-de/download/details.aspx?id=8328) and [x64](https://www.microsoft.com/en-us/download/details.aspx?id=13523) versions of Microsoft Visual C++ 2010 SP1 to work.
