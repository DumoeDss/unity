# Setup a new project

- Create a new project in Unity
- In *Edit->Project Settings->Editor* set **Version Control** to ***Visible Meta Files***
- In *Edit->Project Settings->Editor* set **Asset Serialization Mode** to ***Force Text***
- In *Edit->Project Settings->Player* set **Use Direct3D 11** to ***Off***
- Import the following packages (which can be found at `visdata/unity/packages`):
    - Advanced Additive Scenes 1.6e
    - FullInspector 2.5, [Docs](http://jacobdufault.github.io/fullinspector/guide/)
    - InControl 
    - MiddleVR 1.6
    - HighlightingSystem
    - UnityVS
    - uIntellisSense (*optional*, make sure to also have the Visual Studio extension installed => double-click `uIntelliSense/VisualStudioExtension/uIntellisSense.VisualStudio`)
    - Zenject
- `cd` into the project directory
    - `git init`
    - Create a `.gitignore`-file with the following contents:
        ```bash
Assets/FullInspector/
Assets/InControl/
Assets/MiddleVR/
Assets/UnityVS/
Assets/Zenject/
Assets/Plugins/
[Ll]ibrary/
[Tt]emp/
[Oo]bj/
[Bb]uild/
# Autogenerated VS/MD solution and project files
/*.csproj
/*.unityproj
/*.sln
/*.suo
/*.user
/*.userprefs
/*.pidb
/*.booproj
#Unity3D Generated File On Crash Reports
sysinfo.txt
        ```

    - `git add .`
    - `git commit -m "Initial."`
- Add the UFZ scripts as a git submodule:
    - `git submodule add https://github.com/bilke/unity Assets/UFZ`
    - `git commit -m "Added submodule"`

Now you can start working. For organization please put nothing project specific into the *Assets/UFZ*-directory. You can backup your project on *visdata*:

```bash
git init --bare /Volumes/visdata/unity/repos/my-project-name.git
git remote add origin /Volumes/visdata/unity/repos/my-project-name.git
git push -u origin master
```
