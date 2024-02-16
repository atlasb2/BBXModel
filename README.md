# BBXModel
A research project exploring using Unity as an interface to the Black Box Experimental Studio's multimedia systems.

# Initial Setup
Follow the steps below to get a Unity project set up with this git repository.

## Project Creation and Settings
1. Create a new Unity project.
2. Go to File &rarr; Build Settings &rarr; Player Settings
3. In the left hand list of settings, click `Version Control`. If that doesn't exist, go to `Editor`.
4. Ensure that the Version Control Mode is set to `Visible Meta Files`.
5. On the left hand side, click `Editor` if you are not already there.
6. Ensure that the Asset Serialization Mode is set to `Force Text`.

## Cloning the Project
1. Close Unity.
2. Go into your local file system, and find the Unity Project on disk. Go into the folder.
3. Delete any of the following folders or files that you see from your local file system:
    - `Assets`
    - `Packages`
    - `ProjectSettings`
    - `.collabignore`
4. Open a terminal in the current folder. (ie inside the main project folder)
5. `git init`
6. `git remote add origin https://github.com/atlasb2/BBXModel.git`. <i>(To clone with SSH, run</i> `git remote add origin git@github.com:atlasb2/BBXModel.git` <i>instead.)</i>
7. `git fetch`
8. `git checkout main`
6. Open the Unity Project back up. You should be good to go.
