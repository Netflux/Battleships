using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using SwinGameSDK;

/// <summary>
/// The menu controller handles the drawing and user interactions
/// from the menus in the game. These include the main menu, game
/// menu and the settings m,enu.
/// </summary>

static class MenuController
{

	/// <summary>
	/// The menu structure for the game.
	/// </summary>
	/// <remarks>
	/// These are the text captions for the menu items.
	/// </remarks>
	private static readonly string[][] _menuStructure = {
		new string[] {
			"PLAY",
			"SETUP",
			"SCORES",
			"SOUND",
			"RESOLUTION",
			"QUIT"
		},
		new string[] {
			"RETURN",
			"SURRENDER",
			"QUIT"
		},
		new string[] {
			"EASY",
			"MEDIUM",
			"HARD"
		},
		new string[]{
			"MUTE",
			"TORTUGA",
			"DRUMS",
			"HORROR"
		},
		new string[]{
			"800x600",
			"1024x768",
			"1280x960"
		}
	};

	private const int MENU_TOP = 570;
	private const int MENU_LEFT = 40;
	private const int MENU_GAP = 0;
	private const int BUTTON_WIDTH = 120;
	private const int BUTTON_HEIGHT = 20;
	private const int BUTTON_SEP = BUTTON_WIDTH + MENU_GAP;

	private const int TEXT_OFFSET = 0;
	private const int MAIN_MENU = 0;
	private const int GAME_MENU = 1;

	private const int SETUP_MENU = 2;
	private const int SETUP_MENU_EASY_BUTTON = 0;
	private const int SETUP_MENU_MEDIUM_BUTTON = 1;
	private const int SETUP_MENU_HARD_BUTTON = 2;
	private const int SETUP_MENU_EXIT_BUTTON = 3;

	private const int MAIN_MENU_PLAY_BUTTON = 0;
	private const int MAIN_MENU_SETUP_BUTTON = 1;
	private const int MAIN_MENU_TOP_SCORES_BUTTON = 2;
	private const int MAIN_MENU_SOUND_BUTTON = 3;
	private const int MAIN_MENU_RESOLUTION_BUTTON = 4;
	private const int MAIN_MENU_QUIT_BUTTON = 5;

	private const int SOUND_MENU = 3;
	private const int SOUND_MENU_MUTE_BUTTON = 0;
	private const int SOUND_MENU_TORTUGA_BUTTON = 1;
	private const int SOUND_MENU_DRUMS_BUTTON = 2;
	private const int SOUND_MENU_HORROR_BUTTON = 3;
	private const int SOUND_MENU_EXIT_BUTTON = 4;

	private const int RESOLUTION_MENU = 4;
	private const int RESOLUTION_MENU_800x600_BUTTON = 0;
	private const int RESOLUTION_MENU_1024x768_BUTTON = 1;
	private const int RESOLUTION_MENU_1280x960_BUTTON = 2;
	private const int RESOLUTION_MENU_EXIT_BUTTON = 3;

	private const int GAME_MENU_RETURN_BUTTON = 0;
	private const int GAME_MENU_SURRENDER_BUTTON = 1;


	private const int GAME_MENU_QUIT_BUTTON = 2;
	private static readonly Color MENU_COLOR = SwinGame.RGBAColor(0, 0, 0, 0);

	private static readonly Color HIGHLIGHT_COLOR = SwinGame.RGBAColor(1, 57, 86, 255);
	/// <summary>
	/// Handles the processing of user input when the main menu is showing
	/// </summary>
	public static void HandleMainMenuInput()
	{
		HandleMenuInput(MAIN_MENU, 0, 0);
	}

	/// <summary>
	/// Handles the processing of user input when the main menu is showing
	/// </summary>
	public static void HandleSetupMenuInput()
	{
		bool handled = false;
		handled = HandleMenuInput(SETUP_MENU, 3, 0);

		if (!handled) {
			HandleMenuInput(MAIN_MENU, 0, 0);
		}
	}

	/// <summary>
	/// Handles the processing of user input when the sound menu is showing
	/// </summary>
	public static void HandleSoundMenuInput()
	{
		bool handled = false;
		handled = HandleMenuInput(SOUND_MENU, 3, 1);

		if (!handled) {
			HandleMenuInput(MAIN_MENU, 0, 0);
		}
	}

	/// <summary>
	/// Handles the processing of user input when the resolution menu is showing
	/// </summary>
	public static void HandleResolutionMenuInput()
	{
		bool handled = false;
		handled = HandleMenuInput(RESOLUTION_MENU, 3, 3);

		if (!handled) {
			HandleMenuInput(MAIN_MENU, 0, 0);
		}
	}

	/// <summary>
	/// Handle input in the game menu.
	/// </summary>
	/// <remarks>
	/// Player can return to the game, surrender, or quit entirely
	/// </remarks>
	public static void HandleGameMenuInput()
	{
		HandleMenuInput(GAME_MENU, 0, 0);
	}

	/// <summary>
	/// Handles input for the specified menu.
	/// </summary>
	/// <param name="menu">the identifier of the menu being processed</param>
	/// <param name="level">the vertical level of the menu</param>
	/// <param name="xOffset">the xoffset of the menu</param>
	/// <returns>false if a clicked missed the buttons. This can be used to check prior menus.</returns>
	private static bool HandleMenuInput(int menu, int level, int xOffset)
	{
		if (SwinGame.KeyTyped(KeyCode.vk_ESCAPE)) {
			GameController.EndCurrentState();
			return true;
		}

		if (SwinGame.MouseClicked(MouseButton.LeftButton)) {
			int i = 0;
			for (i = 0; i <= _menuStructure[menu].Length - 1; i++) {
				//IsMouseOver the i'th button of the menu
				if (IsMouseOverMenu(i, level, xOffset)) {
					PerformMenuAction(menu, i);
					return true;
				}
			}

			if (level > 0) {
				//none clicked - so end this sub menu
				GameController.EndCurrentState();
			}
		}

		return false;
	}

	/// <summary>
	/// Draws the main menu to the screen.
	/// </summary>
	public static void DrawMainMenu()
	{
		//Clears the Screen to Black
		//SwinGame.DrawText("Main Menu", Color.White, GameFont("ArialLarge"), 50, 50)

		DrawButtons(MAIN_MENU);
	}

	/// <summary>
	/// Draws the Game menu to the screen
	/// </summary>
	public static void DrawGameMenu()
	{
		//Clears the Screen to Black
		//SwinGame.DrawText("Paused", Color.White, GameFont("ArialLarge"), 50, 50)

		DrawButtons(GAME_MENU);
	}

	/// <summary>
	/// Draws the settings menu to the screen.
	/// </summary>
	/// <remarks>
	/// Also shows the main menu
	/// </remarks>
	public static void DrawSettings()
	{
		//Clears the Screen to Black
		//SwinGame.DrawText("Settings", Color.White, GameFont("ArialLarge"), 50, 50)

		if (GameController.Resolution == GameResolution.Res800x600) {
			SwinGame.DrawBitmap (GameResources.GameImage ("SetupMenu"), 59, 484);
		} else if (GameController.Resolution == GameResolution.Res1024x768) {
			SwinGame.DrawBitmap (GameResources.GameImage ("SetupMenu1024x768"), 58, 618);
		} else if (GameController.Resolution == GameResolution.Res1280x960) {
			SwinGame.DrawBitmap (GameResources.GameImage ("SetupMenu1280x960"), 58, 776);
		}

		DrawButtons(MAIN_MENU);
		DrawButtons(SETUP_MENU, 3, 0);
	}

	/// <summary>
	/// Draws the sound menu to the screen.
	/// </summary>
	/// <remarks>
	/// Also shows the main menu
	/// </remarks>
	public static void DrawSound()
	{
		if (GameController.Resolution == GameResolution.Res800x600) {
			SwinGame.DrawBitmap (GameResources.GameImage ("SoundMenu"), 171, 484);
		} else if (GameController.Resolution == GameResolution.Res1024x768) {
			SwinGame.DrawBitmap (GameResources.GameImage ("SoundMenu1024x768"), 200, 618);
		} else if (GameController.Resolution == GameResolution.Res1280x960) {
			SwinGame.DrawBitmap (GameResources.GameImage ("SoundMenu1280x960"), 235, 776);
		}

		DrawButtons(MAIN_MENU);
		DrawButtons(SOUND_MENU, 3, 1);
	}

	/// <summary>
	/// Draws the resolution menu to the screen.
	/// </summary>
	/// <remarks>
	/// Also shows the main menu
	/// </remarks>
	public static void DrawResolution()
	{
		if (GameController.Resolution == GameResolution.Res800x600) {
			SwinGame.DrawBitmap (GameResources.GameImage ("ResolutionMenu"), 389, 484);
		} else if (GameController.Resolution == GameResolution.Res1024x768) {
			SwinGame.DrawBitmap (GameResources.GameImage ("ResolutionMenu1024x768"), 481, 621);
		} else if (GameController.Resolution == GameResolution.Res1280x960) {
			SwinGame.DrawBitmap (GameResources.GameImage ("ResolutionMenu1280x960"), 584, 776);
		}

		DrawButtons(MAIN_MENU);
		DrawButtons(RESOLUTION_MENU, 3, 3);
	}

	/// <summary>
	/// Draw the buttons associated with a top level menu.
	/// </summary>
	/// <param name="menu">the index of the menu to draw</param>
	private static void DrawButtons(int menu)
	{
		DrawButtons(menu, 0, 0);
	}

	/// <summary>
	/// Draws the menu at the indicated level.
	/// </summary>
	/// <param name="menu">the menu to draw</param>
	/// <param name="level">the level (height) of the menu</param>
	/// <param name="xOffset">the offset of the menu</param>
	/// <remarks>
	/// The menu text comes from the _menuStructure field. The level indicates the height
	/// of the menu, to enable sub menus. The xOffset repositions the menu horizontally
	/// to allow the submenus to be positioned correctly.
	/// </remarks>
	private static void DrawButtons(int menu, int level, int xOffset)
	{
		int btnTop = 0;

		btnTop = MENU_TOP - (MENU_GAP + BUTTON_HEIGHT) * level;
		int i = 0;
		for (i = 0; i <= _menuStructure [menu].Length - 1; i++) {
			int btnLeft = 0;
			btnLeft = MENU_LEFT + BUTTON_SEP * (i + xOffset);
			//SwinGame.FillRectangle (Color.White, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
			SwinGame.DrawTextLines (_menuStructure [menu] [i], MENU_COLOR, Color.Transparent, GameResources.GameFont ("Liberatorm"), FontAlignment.AlignCenter, (int)(Math.Round ((btnLeft + TEXT_OFFSET) * GameController.ResolutionMultiplier)), (int)(Math.Round ((btnTop + TEXT_OFFSET) * GameController.ResolutionMultiplier)), BUTTON_WIDTH, BUTTON_HEIGHT);

			if (SwinGame.MouseDown (MouseButton.LeftButton) & IsMouseOverMenu (i, level, xOffset)) {
				SwinGame.DrawRectangle (HIGHLIGHT_COLOR, (int)(Math.Round ((btnLeft + TEXT_OFFSET) * GameController.ResolutionMultiplier)), (int)(Math.Round ((btnTop + TEXT_OFFSET) * GameController.ResolutionMultiplier)), BUTTON_WIDTH, BUTTON_HEIGHT);
			}
		}
	}

	/// <summary>
	/// Determined if the mouse is over one of the button in the main menu.
	/// </summary>
	/// <param name="button">the index of the button to check</param>
	/// <returns>true if the mouse is over that button</returns>
	private static bool IsMouseOverButton(int button)
	{
		return IsMouseOverMenu(button, 0, 0);
	}

	/// <summary>
	/// Checks if the mouse is over one of the buttons in a menu.
	/// </summary>
	/// <param name="button">the index of the button to check</param>
	/// <param name="level">the level of the menu</param>
	/// <param name="xOffset">the xOffset of the menu</param>
	/// <returns>true if the mouse is over the button</returns>
	private static bool IsMouseOverMenu(int button, int level, int xOffset)
	{
		int btnTop = MENU_TOP - (MENU_GAP + BUTTON_HEIGHT) * level;
		int btnLeft = MENU_LEFT + BUTTON_SEP * (button + xOffset);

		return UtilityFunctions.IsMouseInRectangle ((int)(Math.Round (btnLeft * GameController.ResolutionMultiplier)), (int)(Math.Round (btnTop * GameController.ResolutionMultiplier)), BUTTON_WIDTH, BUTTON_HEIGHT);
	}

	/// <summary>
	/// A button has been clicked, perform the associated action.
	/// </summary>
	/// <param name="menu">the menu that has been clicked</param>
	/// <param name="button">the index of the button that was clicked</param>
	public static void PerformMenuAction(int menu, int button)
	{
		switch (menu) {
		case MAIN_MENU:
			PerformMainMenuAction(button);
			break;
		case SETUP_MENU:
			PerformSetupMenuAction(button);
			break;
		case SOUND_MENU:
			PerformSoundMenuAction(button);
			break;
		case RESOLUTION_MENU:
			PerformResolutionMenuAction(button);
			break;
		case GAME_MENU:
			PerformGameMenuAction(button);
			break;
		}
	}

	/// <summary>
	/// The main menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformMainMenuAction(int button)
	{
		switch (button) {
		case MAIN_MENU_PLAY_BUTTON:
			GameController.StartGame();
			break;
		case MAIN_MENU_SETUP_BUTTON:
			GameController.AddNewState(GameState.AlteringSettings);
			break;
		case MAIN_MENU_TOP_SCORES_BUTTON:
			GameController.AddNewState(GameState.ViewingHighScores);
			break;
		case MAIN_MENU_SOUND_BUTTON:
			GameController.AddNewState(GameState.AlteringMusic);
			break;
		case MAIN_MENU_RESOLUTION_BUTTON:
			GameController.AddNewState (GameState.AlteringResolution);
			break;
		case MAIN_MENU_QUIT_BUTTON:
			GameController.EndCurrentState();
			break;
		}
	}

	/// <summary>
	/// The setup menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformSetupMenuAction(int button)
	{
		switch (button) {
		case SETUP_MENU_EASY_BUTTON:
			GameController.SetDifficulty(AIOption.Hard);
			break;
		case SETUP_MENU_MEDIUM_BUTTON:
			GameController.SetDifficulty(AIOption.Hard);
			break;
		case SETUP_MENU_HARD_BUTTON:
			GameController.SetDifficulty(AIOption.Hard);
			break;
		}
		//Always end state - handles exit button as well
		GameController.EndCurrentState();
	}

	/// <summary>
	/// The game menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformSoundMenuAction (int button)
	{
		switch (button) {
		case SOUND_MENU_MUTE_BUTTON:
			SwinGame.StopMusic();
			break;
		case SOUND_MENU_TORTUGA_BUTTON:
			SwinGame.PlayMusic(GameResources.GameMusic ("Background1"));
			break;
		case SOUND_MENU_DRUMS_BUTTON:
			SwinGame.PlayMusic(GameResources.GameMusic ("Background2"));
			break;
		case SOUND_MENU_HORROR_BUTTON:
			SwinGame.PlayMusic(GameResources.GameMusic ("Background3"));
			break;
		}
		GameController.EndCurrentState();
	}

	/// <summary>
	/// The game menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformResolutionMenuAction (int button)
	{
		switch (button) {
		case RESOLUTION_MENU_800x600_BUTTON:
			SwinGame.ChangeScreenSize(800, 600);
			GameController.Resolution = GameResolution.Res800x600;
			GameController.ResolutionMultiplier = 1;
			GameController.ResolutionOffsetX = 0;
			GameController.ResolutionOffsetY = 0;
			break;
		case RESOLUTION_MENU_1024x768_BUTTON:
			SwinGame.ChangeScreenSize(1024, 768);
			GameController.Resolution = GameResolution.Res1024x768;
			GameController.ResolutionMultiplier = 1.28;
			GameController.ResolutionOffsetX = 224;
			GameController.ResolutionOffsetY = 168;
			break;
		case RESOLUTION_MENU_1280x960_BUTTON:
			SwinGame.ChangeScreenSize(1280, 960);
			GameController.Resolution = GameResolution.Res1280x960;
			GameController.ResolutionMultiplier = 1.6;
			GameController.ResolutionOffsetX = 480;
			GameController.ResolutionOffsetY = 360;
			break;
		}
		GameController.EndCurrentState();
	}

	/// <summary>
	/// The game menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformGameMenuAction(int button)
	{
		switch (button) {
		case GAME_MENU_RETURN_BUTTON:
			GameController.EndCurrentState ();
			StopWatch.stopWatch.Start ();
			break;
		case GAME_MENU_SURRENDER_BUTTON:
			GameController.EndCurrentState ();
			//end game menu
			GameController.EndCurrentState ();
			//end game
			StopWatch.stopWatch.Stop ();
			StopWatch.stopWatch.Reset ();
			break;
		case GAME_MENU_QUIT_BUTTON:
			GameController.AddNewState (GameState.Quitting);
			break;
		}
	}
}