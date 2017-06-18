#!/bin/sh

# MSys2 shell
# pacman -S zip unzip

name="PathConverter"
zipfile="$name.zip"

rm $zipfile 2> /dev/null

zip -j "$zipfile" "$name/bin/Release/$name.exe"
zip "$zipfile" README.md screenshot.png LICENSE
