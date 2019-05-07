<p align="center">
    <a href="https://github.com/HyperPolygon64/Sonic-06-Toolkit/blob/master/">
        <img src="https://github.com/HyperPolygon64/Sonic-06-Toolkit/blob/master/logo_small.png" />
    </a>
</p>

<h1 align="center">Sonic '06 Toolkit</h1>

<p align="center">Sonic '06 Toolkit is an application developed to help make modding easier for SONIC THE HEDGEHOG (2006). Now being written in C#, Sonic '06 Toolkit has been made open-source to allow the community to contribute to the project.</p>

# Features
### ARC Unpacking and Repacking (to be replaced)
- Currently based off [Shadow LAG](https://github.com/lllsondowlll) and [ModdingUnderground](https://www.youtube.com/user/ModdingUnderground)'s ARC unpacker/repacker. Once [RadfordHound](https://github.com/Radfordhound)'s [HedgeLib](https://github.com/Radfordhound/HedgeLib) ARC class is complete, we will be able to make use of that.

### ADX Encoding and CSB Extracting (done)
- Currently based off [Skyth](https://github.com/blueskythlikesclouds)'s [Sonic Audio Tools](https://github.com/blueskythlikesclouds/SonicAudioTools) for extracting CSB files and some tools from [CRI Middleware](https://www.criware.com/en/) for encoding ADX files.

### LUB Decompiling (to be replaced)
- Currently based off [Shadow LAG](https://github.com/lllsondowlll)'s Java-based Lua decompiler which is soon to be replaced.

### MST Decoding (experimental)
- Currently using [Natsumi](https://github.com/TheRetroSnake)'s BINA to string converter. We currently don't have a way to convert text back to the MST format. 

### SET Exporting and XML Importing (experimental)
- Currently based off [RadfordHound](https://github.com/Radfordhound)'s [HedgeLib](https://github.com/Radfordhound/HedgeLib) S06SetData class.

### XNO Converting and XNM Pairing (done)
- Currently based off [Skyth](https://github.com/blueskythlikesclouds) and [DarioSamo](https://github.com/DarioSamo)'s [XNO converter](https://github.com/blueskythlikesclouds/SkythTools/blob/master/Sonic%20'06/xno2dae.exe).

# Requirements
### ADX Encoder
- Currently requires both [x86](https://www.microsoft.com/de-de/download/details.aspx?id=8328) and [x64](https://www.microsoft.com/en-us/download/details.aspx?id=13523) versions of Microsoft Visual C++ 2010 SP1.

### LUB Decompiler
- Currently requires the [latest version of Java](https://www.java.com/en/download/) to work.

### XNO Converter
- Currently requires both [x86](https://www.microsoft.com/de-de/download/details.aspx?id=8328) and [x64](https://www.microsoft.com/en-us/download/details.aspx?id=13523) versions of Microsoft Visual C++ 2010 SP1.
