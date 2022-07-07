# Target Vector
An Unreal Engine 5 Template utilizing EOS (Epic Online Services), Steam, Modular Gameplay, Common UI and ALS (Advanced Locomotion System Refactored XT).

**Features**

- The EOSGameInstance C++ class exposes basic EOS functionality such as Login, Friends, Invites, and Creating and Joining Sessions to Blueprint. EOSPlus allows for a secondary authentication system (Steam by default).
- Modular Gameplay ready with UIExtension to improve Mod/UGC Authoring workflows in actors and UIs.
- CommonUI based main menu system with Tabbed navigation, Input Routing, Loading Screen, Primary Layout and Async Mixin implementation (Push Widget).
- A basic Server Browser (via helper functions (work in progress)). 
- Advanced Locomotion System Refactored with expanded Character States, Improved Foot Print system, Sliding and Vaulting (XT).
- TTToolbox provides various editor tools to facilitate retargeting characters and animations to the ALS skeleton.
- SkeletalMeshMerger for merging skeletal component at run-time.

### Setup
In Project Settings > Maps & Modes select the EOS_GameInstance class in the Game Instance Class drop-down menu.
In Project Settings > Plugins > Online Subsystem EOS, complete the required Artifacts. These values can be found in Application Details in your Epic Dev Portal.

### Code
Source code for the EOS Game Instance class can be found in the Source folder inside the Project folder. You are encouraged to expand, improve or sub-class the EOSGameInstance C++ class to meet the unique requirements of your project.
CommonUI functionality is implemented in the B_PlayerController, B_MainMenuController and B_HUD Classes.
Source code for ALS_Refactored_XT can be found in the Plugins folder inside the Project folder.

### Testing
To test the basic EOS functionality use the EOS_Testing.bat file. Edit the "ue5path" varible to reflect your Unreal Engine 5 installation directory.
Automated Testing coming soon.

### Troubleshooting
When testing, the Epic Games Overlay and Steams Overlay should always appear. If not, check your log files and verify all the EOS Artefacts are correct. Incorrect or incomplete EOS setup may also result in crashes.
