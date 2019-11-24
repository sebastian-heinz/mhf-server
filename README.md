Monster Hunter Frontier Z - Server
===
Server Emulator for the Online Game Monster Hunter Frontier Z.   
Please read through the whole file before asking questions.

## Table of contents
- [Disclaimer](#disclaimer)
- [Notice](#notice)
- [Setup](#setup)
  - [Visual Studio](#visual-studio)
  - [VS Code](#vs-code)
  - [IntelliJ Rider](#intellij-rider)
- [Hosts](#hosts)
- [Sever](#server)
- [Client](#client)
  - [Unpacking](#unpacking)
  - [GameGuard](#gameguard)
  - [Bugs](#bugs)
- [Guidelines](#guidelines)
- [Attribution](#attribution)
  - [Contributers](#contributers)
  - [3rd Parties and Libraries](#3rd-parties-and-libraries)

# Disclaimer
The project is intended for educational purpose only.

# Notice
This server requires that you own a copy of the game and assets.
These assets are not included in this repository to comply with copyrights.
In order to run this server you are required to provide a `wwww` folder.
At the moment no alternative as been developed, as soon as this happened this repository will be updated.

# Setup
## 1) Clone the repository  
`git clone https://github.com/sebastian-heinz/mhf-server.git`

## 2) Install .Net Core 3.0 SDK or later  
https://dotnet.microsoft.com/download

## 3) Use your IDE of choice:

## 3.1) Visual Studio
### Notice:
Minimum version of "Visual Studio 2019 v16.3" or later.

### Open Project:
Open the `MonsterHunterFrontierZ.sln`-file

## 3.2) VS Code
Download IDE: https://code.visualstudio.com/download  
C# Plugin: https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp  

### Open Project:
Open the Project Folder:  
`\mhf-server`

## 3.3) IntelliJ Rider
https://www.jetbrains.com/rider/

### Open Project:  
Open the `MonsterHunterFrontierZ.sln`-file

## 4) Debug the Project
Run the `Mhf.Cli`-Project

# Hosts
Add following entries to your hosts file to force the client to connect to the local instance.
```
127.0.0.1 cog-members.mhf-z.jp        # MHF Launcher       (web)
127.0.0.1 capcom-onlinegames.jp       # MHF Authentication (web)
127.0.0.1 www.capcom-onlinegames.jp   # MHF Authentication (web)
127.0.0.1 sign-mhf.capcom-networks.jp # MHF Authentication (auth/server)
127.0.0.1 srv-mhf.capcom-networks.jp  # MHF ServerList     (web) 
127.0.0.1 l0.mhf-g.jp                 # MHF File Checksum  (web) 
127.0.0.1 u0.mhf-g.jp                 # MHF File Host      (web) 
```

# Server
With default configuration the server will listen on following ports:
```
80 - http/launcher gui
433 - https/authentication
53312 - tcp/authentication
53310 - tcp/lobby
```
ensure that no other local services run on these.request

# Client
Following modifications are recommended when trying to use this server:

## Unpacking
mhf.exe, mhl.dll, mhfo.dll and mhfo-hd.dll are protected with AsProtect. 
A tool called `AsDecom` can unpack these files, but it must run on WindowsXP. 

## GameGuard
To disable GameGuard please delete or rename `gameguard.des`-file.
Additionally in `mhl.dll` the following byte need to be patched `000053C3:74->77`

## Bugs
`mhl.dll`-file contains a bug when performing HTTP requests.
```
00356352                    48 54 54 50 2F 31 2E 31 0A 43        HTTP/1.1.C
00356368  61 63 68 65 2D 43 6F 6E 74 72 6F 6C 3A 20 6E 6F  ache-Control: no
00356384  2D 63 61 63 68 65 0A                             -cache.
```
It only uses a single LF `0x0A` whereas the spec requires CR LF `0x0D 0x0A`
In order to obtain enough bytes we can change `cache-control`-header to `Expires`-headers like so:
```
Offset(d) 00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15

00356352                    48 54 54 50 2F 31 2E 31 0D 0A        HTTP/1.1..
00356368  45 78 70 69 72 65 73 3A 20 30 0D 0A              Expires: 0..
```
without this patch the C# web server will not process the request, and maybe other servers as well.

# Guidelines
## Git 
### Workflow
The work on this project should happen via `feature-branches`
   
Feature branches (or sometimes called topic branches) are used to develop new features for the upcoming or a distant future release. 
When starting development of a feature, the target release in which this feature will be incorporated may well be unknown at that point. 
The essence of a feature branch is that it exists as long as the feature is in development, 
but will eventually be merged back into develop (to definitely add the new feature to the upcoming release) or discarded (in case of a disappointing experiment).
   
1) Create a new `feature/feature-name` or `fix/bug-fix-name` branch from master
2) Push all your changes to that branch
3) Create a Pull Request to merge that branch into `master`

## Best Practise
- Do not use Console.WriteLine etc, use the specially designed logger.
- Own the Code: extract solutions, discard libraries.
- Annotate functions with documentation comments (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments).

## C# Coding Standards and Naming Conventions
| Object Name               | Notation    | Char Mask          | Underscores |
|:--------------------------|:------------|:-------------------|:------------|
| Class name                | PascalCase  | [A-z][0-9]         | No          |
| Constructor name          | PascalCase  | [A-z][0-9]         | No          |
| Method name               | PascalCase  | [A-z][0-9]         | No          |
| Method arguments          | camelCase   | [A-z][0-9]         | No          |
| Local variables           | camelCase   | [A-z][0-9]         | No          |
| Constants name            | PascalCase  | [A-z][0-9]         | No          |
| Field name                | _camelCase  | [A-z][0-9]         | Yes         |
| Properties name           | PascalCase  | [A-z][0-9]         | No          |
| Delegate name             | PascalCase  | [A-z]              | No          |
| Enum type name            | PascalCase  | [A-z]              | No          |

# Attribution
## Contributors
- Nothilvien [@sebastian-heinz](https://github.com/sebastian-heinz)

## 3rd Parties and Libraries
- System.Data.SQLite (https://system.data.sqlite.org/)
- MySqlConnector (https://www.nuget.org/packages/MySqlConnector)
- bcrypt.net (https://github.com/BcryptNet/bcrypt.net)
- AspNetCore (https://github.com/aspnet/AspNetCore)
- .NET Standard (https://github.com/dotnet/standard)
- Arrowgene.Services (https://github.com/Arrowgene/Arrowgene.Services)
- CRC32 Implementation by Damien Guard (https://github.com/damieng/DamienGKit/blob/master/CSharp/DamienG.Library/Security/Cryptography/Crc32.cs)


