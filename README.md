# terrariaPlayerEditor
A lite console Terraria player editor

## Introduction
This is a Terraria player save file (*.plr) editor realized by calling native Terraria methods. 

By calling native methods, this editor can theoretically support most versions of Terraria

This editor directly uses Terraria's localized texts so that less translation are needed.

## Usage

Two references , ReLogic.dll and Newtonsoft.Json.dll, are used in this editor, which can be downloaded in the release page.

To use this editor, the executable file and the two dlls should be put in the terraria folder where the Terraria.exe exists.

The editor will calls the native Terraria method that loads players from default path.
