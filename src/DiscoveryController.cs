using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using SwinGameSDK;

/// <summary>
/// The battle phase is handled by the DiscoveryController.
/// </summary>
static class DiscoveryController
{

	/// <summary>
	/// Handles input during the discovery phase of the game.
	/// </summary>
	/// <remarks>
	/// Escape opens the game menu. Clicking the mouse will
	/// attack a location.
	/// </remarks>
	public static void HandleDiscoveryInput()
	{
		if (SwinGame.KeyTyped(KeyCode.vk_ESCAPE)) {
			GameController.AddNewState(GameState.ViewingGameMenu);
		}

		if (SwinGame.MouseClicked(MouseButton.LeftButton)) {
			DoAttack();
		}
	}

	/// <summary>
	/// Attack the location that the mouse if over.
	/// </summary>
	private static void DoAttack()
	{
		Point2D mouse = default(Point2D);

		mouse = SwinGame.MousePosition();

		//Calculate the row/col clicked
		int row = 0;
		int col = 0;
		row = Convert.ToInt32 (Math.Floor ((mouse.Y - (UtilityFunctions.FIELD_TOP * GameController.ResolutionMultiplier)) / ((UtilityFunctions.CELL_HEIGHT + UtilityFunctions.CELL_GAP) * GameController.ResolutionMultiplier)));
		col = Convert.ToInt32 (Math.Floor ((mouse.X - (UtilityFunctions.FIELD_LEFT * GameController.ResolutionMultiplier)) / ((UtilityFunctions.CELL_WIDTH + UtilityFunctions.CELL_GAP) * GameController.ResolutionMultiplier)));

		if (row >= 0 & row < GameController.HumanPlayer.EnemyGrid.Height) {
			if (col >= 0 & col < GameController.HumanPlayer.EnemyGrid.Width) {
				GameController.Attack(row, col);
			}
		}
	}

	/// <summary>
	/// Draws the game during the attack phase.
	/// </summary>s
	public static void DrawDiscovery()
	{
		const int SCORES_LEFT = 172;
		const int SHOTS_TOP = 157;
		const int HITS_TOP = 206;
		const int SPLASH_TOP = 256;
		const int TIMER_LEFT = 102;
		const int TIMER_TOP = 306;

		if ((SwinGame.KeyDown(KeyCode.vk_LSHIFT) | SwinGame.KeyDown(KeyCode.vk_RSHIFT)) & SwinGame.KeyDown(KeyCode.vk_c)) {
			UtilityFunctions.DrawField(GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, true);
		} else {
			UtilityFunctions.DrawField(GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, false);
		}

		UtilityFunctions.DrawSmallField(GameController.HumanPlayer.PlayerGrid, GameController.HumanPlayer);
		UtilityFunctions.DrawMessage();

		SwinGame.DrawText(GameController.HumanPlayer.Shots.ToString(), Color.White, GameResources.GameFont("Menu"), (int)(Math.Round (SCORES_LEFT * GameController.ResolutionMultiplier)), (int)(Math.Round (SHOTS_TOP * GameController.ResolutionMultiplier)));
		SwinGame.DrawText(GameController.HumanPlayer.Hits.ToString(), Color.White, GameResources.GameFont("Menu"), (int)(Math.Round (SCORES_LEFT * GameController.ResolutionMultiplier)), (int)(Math.Round (HITS_TOP * GameController.ResolutionMultiplier)));
		SwinGame.DrawText(GameController.HumanPlayer.Missed.ToString(), Color.White, GameResources.GameFont("Menu"), (int)(Math.Round (SCORES_LEFT * GameController.ResolutionMultiplier)), (int)(Math.Round (SPLASH_TOP * GameController.ResolutionMultiplier)));
		SwinGame.DrawText("Time Taken: " + StopWatch.TimeTaken("elapsedTime"), Color.White, GameResources.GameFont("Menu"), (int)(Math.Round (TIMER_LEFT * GameController.ResolutionMultiplier)), (int)(Math.Round (TIMER_TOP * GameController.ResolutionMultiplier)));

		if (GameController.GetDifficulty() == AIOption.Easy)
		{
			if(StopWatch.TimeTaken("elapsedTime") == "10:00")
			{
				GameController.HumanPlayer.Lost = true;
				GameController.EndCurrentState ();
				GameController.AddNewState (GameState.EndingGame);
			}
		}
		else if (GameController.GetDifficulty() == AIOption.Medium)
		{
			if(StopWatch.TimeTaken("elapsedTime") == "05:00")
			{
				GameController.HumanPlayer.Lost = true;
				GameController.EndCurrentState ();
				GameController.AddNewState (GameState.EndingGame);
			}
		}
		else if (GameController.GetDifficulty() == AIOption.Hard)
		{
			if(StopWatch.TimeTaken("elapsedTime") == "03:00")
			{
				GameController.HumanPlayer.Lost = true;
				GameController.EndCurrentState ();
				GameController.AddNewState (GameState.EndingGame);
			}
		}
	}
}