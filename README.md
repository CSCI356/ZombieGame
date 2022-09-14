# ZombieShooterGame
Zombie shooter 3D mobile game for a University project.

## For adding new features to the repo (Workflow for version control):

1) ```git checkout -b "feature/the_feature_you_are_working_on"```
Will create a new branch and switch over to it. 

2) Complete whatever you are working on with git add and commit. 
```git add .``` Prepares your changes for a commit.
```git commit -m "your commit message"``` actually makes the commit (only locally at this point).

3) ```git push``` Will push your changes to the same branch you created on the remote repository. NOTE: will have to set --upstream at this point. See your terminals info.

4) Once you are happy with your changes and want to introduce them to the master branch. Create a pull request (on github). Fix any merge conflicts (or get help).

5) Someone, will review and approve and then successfully merge into master. Make sure to delete old branches that have been merged.

## Further info on version control (how and why we are doing it like this):
I have set up according to this methodology so it is most similar to a workplace team environment: https://dev.to/profydev/professional-git-workflow-github-setup-for-react-developers-pfj

## How to add a new weapon
Currently only a very basic weapon upgrade system. 
As you destroy more zombies, the players weapon automatically upgrades at certain checkpoints. 

To add a new weapon:
1) Create a new weapon gameobject in the scene and save as a prefab by dragging into the project window. (./Assets/Player/GunPrefabs)
2) Click player object in the scene and view inspector to see the Weapon Manager script.
3) Within this tab, increase the 'size' of the list for both 'weapons' and 'Required Scores'.
4) Drag your new weapon prefab into the 'weapons' empty array element.
5) Enter an integer into the required scores for the corresponding element. This will be how many kills are required to unlock your weapon.


## Resources:

For making 3D art assets suggestion:
-MagicaVoxel https://ephtracy.github.io/

For animating 3D characters:
-Mixamo from Adobe https://www.mixamo.com/#/

