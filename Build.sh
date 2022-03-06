#!/bin/bash

PROFILE_DATA=".profiles"
CONFIG="Release"

if [ $# -ne 0 ]
then
    while getopts ac:dhp: option
    do
        case $option in
            a)
                BUILD_ALL=1
                ;;
            c)
                CONFIG="${OPTARG}"
                ;;
            d)
                CLEAN=1
                ;;
            h)
                echo "Marathon Build Script"
                echo ""
                echo "All your platforms are belong to us."
                echo ""
                echo "Usage:"
                echo "-a - build Marathon with all available profiles."
                echo "-c [name] - build Marathon with a specific configuration."
                echo "-d - cleans the solution before building Marathon."
                echo "-h - display help."
                echo "-p [name] - build Marathon with a specific profile."
                exit
                ;;
            p)
                PROFILE="${OPTARG}"
                ;;
        esac
    done
fi

if [[ "${OSTYPE}" == "cygwin" ]] || [[ "${OSTYPE}" == "msys" ]] || [[ "${OSTYPE}" == "win32" ]] || [[ $(uname -r | sed -n 's/.*\( *Microsoft *\).*/\1/ip') ]]
then
    PLATFORM="Linux"
    IS_WINDOWS=1
elif [ "${OSTYPE}" == "linux-gnu"* ]
then
    PLATFORM="Linux"
elif [ "${OSTYPE}" == "darwin"* ]
then
    PLATFORM="macOS"
fi

# Check if the .NET SDK is installed.
if ! [ -x "$(command -v dotnet)" ]
then
    echo ".NET SDK is required to build Marathon."

    case "${PLATFORM}" in
        "Linux")
            echo "See the official .NET documentation on installing the SDK for Linux here: https://docs.microsoft.com/dotnet/core/install/linux"
            ;;
        "macOS")
            echo "You can install the required .NET SDK for macOS from here: https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-6.0.200-macos-x64-installer"
            ;;
    esac

    if [ IS_WINDOWS ]
    then
        echo "Please use the provided PowerShell script to build for Windows."
    fi

    exit
fi

if [ $CLEAN ]
then
    dotnet clean
fi

if [ -n "${PROFILE}" ]
then
    PROFILE_COUNT=$(wc -l < ${PROFILE_DATA})

    for (( i = 1; i <= $PROFILE_COUNT + 1; i++ ))
    do
        PROFILE_CURRENT=$(cut -f1 "${PROFILE_DATA}" | sed "${i}q;d")

        # Skip profile if it was already specified and built.
        if [[ $PROFILE_CURRENT == $PROFILE ]]
        then
            PROFILE_EXISTS=1
            break
        fi
    done

    if [ ! $PROFILE_EXISTS ]
    then
        echo "No such profile exists: ${PROFILE}"
        exit
    fi

    dotnet publish /p:Configuration="${CONFIG}" /p:PublishProfile="${PROFILE}"
    
    if [ ! $BUILD_ALL ]
    then
        exit
    fi
fi

if [ $BUILD_ALL ]
then
    PROFILE_COUNT=$(wc -l < ${PROFILE_DATA})

    for (( i = 1; i <= $PROFILE_COUNT + 1; i++ ))
    do
        PROFILE_CURRENT=$(cut -f1 "${PROFILE_DATA}" | sed "${i}q;d")

        # Skip profile if it was already specified and built.
        if [[ $PROFILE_CURRENT == $PROFILE ]]
        then
            continue
        fi

        dotnet publish /p:Configuration="${CONFIG}" /p:PublishProfile="${PROFILE_CURRENT}"
    done
else
    case "${PLATFORM}" in
        "Linux")
            dotnet publish /p:Configuration="${CONFIG}" /p:PublishProfile="linux-x64"
            ;;
        "macOS")
            dotnet publish /p:Configuration="${CONFIG}" /p:PublishProfile="osx-x64"
            ;;
    esac
fi