//branch fix-all-bugs-and-added-arrowkeys-navigation README
//created by iyujie
//on 17/10/2016
//individual
@UtilityFunctions.cs  - game resources are called here (change explosion, background etc) [Victor]
@GameResources.cs needs few tweaks such that it returns: return _Object[passed_value];, ways to do unsure, not sure if needed for above [Victor]

add mute button @Menu_Controller.cs [Timothy]

added key pressed navigation @Deployment_Controller.cs [Leslie][DONE]
highscore to display @HighScoreController.cs [Kevin][DONE] 

//gonna do
dont know if should rename GameLogic.cs (actually the main file) to GameMain
change coding standards


//changed
added minor code change to files @/Model/..
ship deployment error at DeploymentController for mouse Y (unable to rotate vertically in the first place) 
add a get property to place ship properly @Model/SeaGridAdapter.cs 
^instantalize new Tile in constrcutor@Model/SeaGrid.cs
^ISeaGrid returns the grid through get method instead of calling Item
check if two locations are equal OR not equal @Model/AIPlayer.cs
add documentary @Model/Ship.cs
add documentary for RandomizeDeployment() @Model/Player.cs

