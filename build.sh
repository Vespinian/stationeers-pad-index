#!/bin/bash
CONFIG="${1:-Debug}"
MODNAME="SatellitePadIndex"
dotnet build -c "$CONFIG"
MOD_DIR="/mnt/sofia/SteamLibrary/steamapps/compatdata/544550/pfx/drive_c/users/steamuser/Documents/My Games/Stationeers/mods/$MODNAME"
mkdir -p "$MOD_DIR"
echo "Copying dll to Stationeers directory"
cp "bin/$CONFIG/net48/SatellitePadIndex.dll" "$MOD_DIR/"
if [ "$CONFIG" = "Debug" ]; then
    echo "Copying pdb to Stationeers directory"
    cp "bin/$CONFIG/net48/$MODNAME.pdb" "$MOD_DIR/"
else
    rm -f "$MOD_DIR/$MODNAME.pdb"
fi
echo Copying GameData to Stationeers directory
cp -r GameData "$MOD_DIR/"
echo Copying About to Stationeers directory
cp -r About "$MOD_DIR/"
