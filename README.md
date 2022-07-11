# Target Vector
An Unreal Engine 5 Template utilizing EOS (Epic Online Services), Steam, Modular Gameplay, Common UI, Advanced Locomotion System Refactored XT, Lyra Modeling Tools and more. _This is currently a work in progress, and not production-ready_

![License MIT](https://img.shields.io/github/license/Voidware-Prohibited/ALS-Refactored-EOS?style=flat-square)
![Checks Pending](https://img.shields.io/github/checks-status/Voidware-Prohibited/ALS-Refactored-EOS/main?style=flat-square)

**Features**

- The EOSGameInstance C++ class exposes basic EOS functionality such as Login, Friends, Invites, and Creating and Joining Sessions to Blueprint. EOSPlus allows for a secondary authentication system (Steam by default).
- Modular Gameplay ready with UIExtension to improve Mod/UGC Authoring workflows in actors and UIs.
- CommonUI based main menu system with Tabbed navigation, Input Routing, Loading Screen, Primary Layout and Async Mixin implementation (Push Widget).
- Server Browser (via helper functions (work in progress)). 
- Advanced Locomotion System Refactored: Expanded Character States, Improved Foot Print system, Sliding and Vaulting (XT) _Coming Soon_.
- Modeling Tools: The Lyra Procedural Level block out tools using Geometry Script adapted into a plugin.
- TTToolbox: Various editor tools to facilitate retargeting characters and animations to the ALS skeleton.
- SkeletalMeshMerger: Merge skeletal component at run-time.

**Dependecies**

- OnlineSubsystemEOS
- OnlineServicesEOS
- OnlineSubsystemSteam
- Common UI
- Common User
- Common Gameplay
- Game Features
- Modular Gameplay
- Geometry Script

**Included Additional Plugins**

- AsyncMixin
- UIExtension
- TTToolbox
- SkeletalMeshMerger

### Setup
This project is configured to read EOS Artifacts from EOSSettings.ini, which for securirity purposes, requires you to create it, and is set up to be ignored in the .gitignore file.

In the Config folder create a text file named EOSSettings.ini and copy and paste the following:

	[/Script/OnlineSubsystemEOS.EOSSettings] 
	+Artifacts=(ArtifactName="EOSArtifact",ClientId="",ClientSecret="",ProductId="",SandboxId="",DeploymentId="",EncryptionKey="")

Complete the required Artifacts fields in the ini file. These values can be found in Application Details in your Epic Dev Portal.
It is highly recommended to keep your Artifacts in this ini file instead of editing the values in the Editor.

Open the Project with the Editor and in Project Settings > Maps & Modes select the EOS_GameInstance class in the Game Instance Class drop-down menu.

### Code
Source code for the EOS Game Instance class can be found in the Source folder inside the Project folder. You are encouraged to expand, improve or sub-class the EOSGameInstance C++ class to meet the unique requirements of your project.

CommonUI functionality is implemented in the B_PlayerController, B_MainMenuController and B_HUD Classes.
Source code for ALS_Refactored_XT can be found in the Plugins folder inside the Project folder.

### Testing
To test the basic EOS functionality use the EOS_Testing.bat file. Edit the "ue5path" varible to reflect your Unreal Engine 5 installation directory. Currently, to test EOS functionality between multiple players you will need an Epic and Steam login for each player. 

- Developer Authentication Tool integration coming soon.- 
- Automated Testing and CI coming soon.

### Troubleshooting
When testing, the Epic Games Overlay and Steams Overlay should always appear. If not, check your log files and verify all the EOS Artefacts are correct. Incorrect or incomplete EOS setup may also result in crashes.
