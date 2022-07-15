# Target Vector
An Unreal Engine 5 Template utilizing EOS (Epic Online Services), Dedicated Server, Steam, Modular Gameplay, Common UI, Advanced Locomotion System Refactored XT, Lyra Modeling Tools and more.
This template also employs Test-Driven Development (TDD) and Continuous Integration (CI).

_This is currently a work in progress, and not production-ready_

![Target Vector](TargetVector_full.png)

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
- Test-Driven Development (TDD) and Continuous Integration (CI)

**Dependecies**

- OnlineSubsystemEOS
- OnlineServicesEOS
- OnlineSubsystemSteam
- Geometry Script

**Included Additional Plugins**

- AsyncMixin
- Common UI
- Common Gameplay
- Common User
- Game Features
- Modular Gameplay
- UIExtension
- TTToolbox
- SkeletalMeshMerger

# Basic Setup
Setup for EOS and Dedicated Server

**Requirements**

- Unreal Engine 5
- Visual Studio

## EOS Setup

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


# Advanced Setup

Setup for Test-Driven Development (TDD) and Continuous Integration (CI)

**EVERYTHING BELOW HERE IS IN THE PROCESS OF BEING INTEGRATED AND DOES NOT CURRENTLY WORK**
**Instructions below are currently being adapted from a depricated version and may not be accurate**

**Requirements**

- Unreal Engine 5 built from github source code
- Visual Studio
- Java
- Jenkins
- Git
- Github
- Domain Name or ngrok
- OpenCppCoverage


**The CI/TDD Development Process**

1.  Make local changes

2. Commit and push changes to git remote repository (or Pull Request) 

3. Github receives the push and uses it's webhook to notify Jenkins (via Domain Name, Dynamic DNS or ngrok).

4. Jenkins receives a notification that a repository included in a pipeline has received a push.

5. Jenkins pulls each change to the remote repository into the Jenkins workspace.

6. Jenkins starts the pipeline associated with that repository.

7. The Pipeline builds the project.

8. The Pipeline runs tests while doing code coverage.

9. The Pipeline displays build status and tests reports.

10. Jenkins notifies Github and Discord of the results of the pipeline build.

Test first, Implementation after testing.

### First Time Steps

0. Install all required programs.

1. Setup a Domain Name, Dynamic DNS or use ngrok access Jenkins via port (default is 8080).

2. Setup Jenkins

3. Setup Discord

4. Create Unreal Project

5. [Setup EOS](#EOS-Setup)

5. Create Jenkins Multibranch Pipeline project

6. Push project changes to remote git server to activate the build trigger in Jenkins

### Install Programs

1. Unreal Engine 5
2. Git
3. Java
4. Jenkins
5. OpenCppCoverage

### Jenkins Setup

In Jenkins Install these plugins:

- Blue Ocean plugin (there're plugins necessary with it and if you want a prettier Jenkins).
- GitHub plugin (for pull requests).
- HTTP request plugin (may no longer be necessary).
- Cobertura plugin (for code coverage).
- discordSend (if you want discord notifications).
      
#### **Jenkinsfile**

- I use node 'master' because I have only one pc for Jenkins.
- I use a custom workspace (and at the beginning of the disk) because the Unreal Build Tool has problems with long filenames.
- The environment variables are used as parameters, to avoid hardcoding.
- BuildWithoutCooking.bat it's used to build the project. I don't use cooking because I think that process should be made in continuous delivery.
- It's not necessary to create the Visual Studio files because we don't do anything with Visual Studio, we run the tests using the Unreal Automation Tool.
- TestRunnerAndCodeCoverage uses OpenCppCodeCoverage (which does the code coverage) attached to the Unreal Engine Editor (which does the tests run).
- TestRunnerAndCodeCoverage.bat assumes that you have a separate folder for tests (\Tests). This could be changed hardcoding it or adding another parameter to the batch file.
- The Tests Report is made in JSon so we need to parse it to XML to be readable by JUnit. So, thanks to Michael Delva for his test report parser method found in his [blogspot](https://www.emidee.net/ue4/2018/11/13/UE4-Unit-Tests-in-Jenkins.html), I modified it only a little.
- The CodeCoverageReport will be used by Cobertura to display the code coverage.
- In some places, discordSend is used to send messages to a Discord channel.
- I do a git hard reset and git clean to clean the workspace after everything has been done. This way, if the repository it's something big, only the changes are downloaded and thus, we save bandwidth.

It would be nice to add github checks to pull requests, but it's not possible with a free account (in private repositories).


### Discord Setup
discordSend is used to send messages to a Discord channel. The Webhook for Discord must be defined with the TVDISCORDWEBHOOK Environment Variable in Jenkins. 

- In Discord right click on the channel you want Jenkins to send messages to and click Edit Channel.
- Navigate to Integrations > Webhooks and click New Webhook
- Name it to whatever you want and click Copy Webhook 
- In Jenkins Navigate to Manage Jenkins > Configure System > Global Variables and check Environment Variables (if not already checked).
- Click the Add button
- In the new entry enter TVDISCORDWEBHOOK as Name
- Paste the Webhook copied from your Discord Channel Settings
- Click the Save and Apply buttons


### Automated Testing

Included are 2 simple C++ tests in the "Game." group that are already setup to run in the Jenkinsfile and a VoidWorld map.
Create new C++ tests in the /Source/Tests folder and add them to the "Game." group to automatically run Testing and Code Coverage with Jenkins builds.

