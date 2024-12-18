#!/bin/bash

# Define paths
SRC_DIR="Mod Manager"
SRC_FILE="GorillaTagMod.cs"
DST_DIR="BepInEx/plugins"
DST_FILE="GorillaTagMod.dll"
TEMP_FILE="GorillaTagModCopy.cs"

# Copy the source file
cp "$SRC_DIR/$SRC_FILE" "$TEMP_FILE"

# Compile the copied file into a DLL
mcs -target:library -out:$DST_FILE $TEMP_FILE

# Move the DLL to the destination directory
mv -f "$DST_FILE" "$DST_DIR"

# Clean up the temporary file
rm "$TEMP_FILE"

echo "Build and copy complete!"