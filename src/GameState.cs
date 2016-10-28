using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
/// <summary>
/// The GameStates represent the state of the Battleships game play.
/// This is used to control the actions and view displayed to
/// the player.
/// </summary>

public enum GameState
{
	/// <summary>
	/// The player is viewing the main menu.
	/// </summary>
	ViewingMainMenu,

	/// <summary>
	/// The player is viewing the game menu
	/// </summary>
	ViewingGameMenu,

	/// <summary>
	/// The player is looking at the high scores
	/// </summary>
	ViewingHighScores,

	/// <summary>
	/// The player is altering the game settings
	/// </summary>
	AlteringSettings,

	/// <summary>
	/// Players are deploying their ships
	/// </summary>
	Deploying,

	/// <summary>
	/// Players are attempting to locate each others ships
	/// </summary>
	Discovering,

	/// <summary>
	/// One player has won, showing the victory screen
	/// </summary>
	EndingGame,

	/// <summary>
	/// The player is altering the game music
	/// </summary>
	AlteringMusic,

	/// <summary>
	/// The player is altering the game screen resolution
	/// </summary>
	AlteringResolution,

	/// <summary>
	/// The player has quit. Show ending credits and terminate the game
	/// </summary>
	Quitting
}

/// <summary>
/// The GameResolutions represent the state of the Battleships window resolution.
/// This is used to control the display size of the game elements.
/// </summary>
public enum GameResolution
{
	/// <summary>
	/// The game resolution is 800x600
	/// </summary>
	Res800x600,

	/// <summary>
	/// The game resolution is 1024x768
	/// </summary>
	Res1024x768,

	/// <summary>
	/// The game resolution is 1280x960
	/// </summary>
	Res1280x960
}