# Teamspeak 3 Plugin TeamCord

![alt text](https://github.com/Kleinrotti/TeamCord/blob/master/plugin.JPG)

## First to say

This plugin is in a very early state and could contain bugs or even cause a Teamspeak crash!
If you experience bugs or crashes please read the Issues section below for more information.

This plugin could be interpreted as [self-bot](https://support.discord.com/hc/en-us/articles/115002192352-Automated-user-accounts-self-bots-) by discord.
I wouldn't classify TeamCord as "self-bot", because no automated actions are made like a bot does. It is only a small discord client integrated in Teamspeak for cross voice communication.
But you use it at your own risk.

## What is this?

TeamCord is a Teamspeak plugin which brings Discord and Teamspeak together.
This means, you have the ability to create a connection/link between a Teamspeak and a Discord voice channel for cross voice communication.
The only thing you need is a discord account und this plugin installed for Teamspeak.

## Install requirements

- Windows 10 64Bit
- [.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48) runtime (should be automatically shipped with newer Windows 10 versions)
- Teamspeak 3 64Bit

## Installation

1. Close Teamspeak, otherwise you have to restart Teamspeak after installation.
2. Download the latest version of TeamCord [here](https://github.com/Kleinrotti/TeamCord/releases).
3. Start the downloaded installer which does everything needed automatically (TeamCord is only installed for the current logged in windows user).

## How to use

### General

- When opening Teamspeak, a new tray icon should appear which indicates the TeamCord connection status
- Firstly you have to login to discord. You can do this by opening the Addon settings.
  - Open Teamspeak -> Tools -> Options -> Addons -> TeamCord and click on "Settings".
- If you connect to a Teamspeak server, TeamCord will automatically log you into discord (you can disable this in the settings and login manually via -> Plugins -> TeamCord -> Login).
- Before you can connect to a Discord channel you have to be a member of that server first with the needed permissions!
- When a Teamspeak channel is linked with a discord channel, TeamCord is connection to that Discord channel too.

### Link a discord channel

- Right click on a Teamspeak channel -> Click on TeamCord -> Link to channel
- You can currently only link Discord channels of server where you are a member of
- Alternatively you can manually paste this json string into a Teamspeak channel description (replace the zeroes with the Discord channel ID):
  > {"Teamcord":{"Channel":000000000}}
- Only this json string should be in the channel description otherwise it couldn't be recognized by TeamCord

### Join a discord channel

- By default TeamCord asks you by joining a Teamspeak channel if a connection to the linked Discord channel should be established.
- You can also connect/disconnect to the Discord channel manually by right clicking on the Teamspeak channel -> TeamCord -> Join.
- To see who is currently connected with the discord channel, click on the Teamspeak channel -> a Userlist on the right Teamspeak panel is visible (above the description).
  - To refresh the Userlist you have re-click on the Teamspeak channel currently.

### Audio settings

- To change the volume of a discord user right click the TeamCord tray icon -> Volume control.
- Muting is done via the Teamspeak mute buttons or hotkeys
- Microphone settings are set by Teamspeak in the settings
- Audio output goes to your default speakers set in windows control center

## Issues

If you found a bug please append the Teamspeak log file to your Issue (%appdata%\TS3Client\logs).
For a better understanding you can enable Debug logging in the TeamCord settings which gives more detailed logs.

Trayicon made by Freepik from www.flaticon.com
