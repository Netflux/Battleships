using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using SwinGameSDK;
/// <summary>
/// This includes a number of utility methods for
/// drawing and interacting with the Mouse.
/// </summary>
static class UtilityFunctions
{
	public const int FIELD_TOP = 122;
	public const int FIELD_LEFT = 349;
	public const int FIELD_WIDTH = 418;

	public const int FIELD_HEIGHT = 418;

	public const int MESSAGE_TOP = 548;
	public const int CELL_WIDTH = 40;
	public const int CELL_HEIGHT = 40;

	public const int CELL_GAP = 2;

	public const int SHIP_GAP = 3;
	private static readonly Color SMALL_SEA = SwinGame.RGBAColor(6, 60, 94, 255);
	private static readonly Color SMALL_SHIP = Color.Gray;
	private static readonly Color SMALL_MISS = SwinGame.RGBAColor(1, 147, 220, 255);

	private static readonly Color SMALL_HIT = SwinGame.RGBAColor(169, 24, 37, 255);
	private static readonly Color LARGE_SEA = SwinGame.RGBAColor(6, 60, 94, 255);
	private static readonly Color LARGE_SHIP = Color.Gray;
	private static readonly Color LARGE_MISS = SwinGame.RGBAColor(1, 147, 220, 255);

	private static readonly Color LARGE_HIT = SwinGame.RGBAColor(252, 2, 3, 255);
	private static readonly Color OUTLINE_COLOR = SwinGame.RGBAColor(5, 55, 88, 255);
	private static readonly Color SHIP_FILL_COLOR = Color.Gray;
	private static readonly Color SHIP_OUTLINE_COLOR = Color.White;

	private static readonly Color MESSAGE_COLOR = SwinGame.RGBAColor(2, 167, 252, 255);
	public const int ANIMATION_CELLS = 7;

	public const int FRAMES_PER_CELL = 8;
	/// <summary>
	/// Determines if the mouse is in a given rectangle.
	/// </summary>
	/// <param name="x">the x location to check</param>
	/// <param name="y">the y location to check</param>
	/// <param name="w">the width to check</param>
	/// <param name="h">the height to check</param>
	/// <returns>true if the mouse is in the area checked</returns>
	public static bool IsMouseInRectangle(int x, int y, int w, int h)
	{
		Point2D mouse = default(Point2D);
		bool result = false;

		mouse = SwinGame.MousePosition();

		//if the mouse is inline with the button horizontally
		if (mouse.X >= x & mouse.X <= x + w) {
			//Check vertical position
			if (mouse.Y >= y & mouse.Y <= y + h) {
				result = true;
			}
		}

		return result;
	}

	/// <summary>
	/// Draws a large field using the grid and the indicated player's ships.
	/// </summary>
	/// <param name="grid">the grid to draw</param>
	/// <param name="thePlayer">the players ships to show</param>
	/// <param name="showShips">indicates if the ships should be shown</param>
	public static void DrawField(ISeaGrid grid, Player thePlayer, bool showShips)
	{
		DrawCustomField(grid, thePlayer, false, showShips, (int)(Math.Round (FIELD_LEFT * GameController.ResolutionMultiplier)), (int)(Math.Round (FIELD_TOP * GameController.ResolutionMultiplier)), (int)(Math.Round (FIELD_WIDTH * GameController.ResolutionMultiplier)), (int)(Math.Round (FIELD_HEIGHT * GameController.ResolutionMultiplier)), (int)(Math.Round (CELL_WIDTH * GameController.ResolutionMultiplier)), (int)(Math.Round (CELL_HEIGHT * GameController.ResolutionMultiplier)), (int)(Math.Round (CELL_GAP * GameController.ResolutionMultiplier)));
	}

	/// <summary>
	/// Draws a small field, showing the attacks made and the locations of the player's ships
	/// </summary>
	/// <param name="grid">the grid to show</param>
	/// <param name="thePlayer">the player to show the ships of</param>
	public static void DrawSmallField(ISeaGrid grid, Player thePlayer)
	{
		const int SMALL_FIELD_LEFT = 39;
		const int SMALL_FIELD_TOP = 373;
		const int SMALL_FIELD_WIDTH = 166;
		const int SMALL_FIELD_HEIGHT = 166;
		const int SMALL_FIELD_CELL_WIDTH = 13;
		const int SMALL_FIELD_CELL_HEIGHT = 13;
		const int SMALL_FIELD_CELL_GAP = 4;

		DrawCustomField(grid, thePlayer, true, true, (int)(Math.Round(SMALL_FIELD_LEFT * GameController.ResolutionMultiplier)), (int)(Math.Round (SMALL_FIELD_TOP * GameController.ResolutionMultiplier)), (int)(Math.Round (SMALL_FIELD_WIDTH * GameController.ResolutionMultiplier)), (int)(Math.Round (SMALL_FIELD_HEIGHT * GameController.ResolutionMultiplier)), (int)(Math.Round (SMALL_FIELD_CELL_WIDTH * GameController.ResolutionMultiplier)), (int)(Math.Round (SMALL_FIELD_CELL_HEIGHT * GameController.ResolutionMultiplier)), (int)(Math.Round (SMALL_FIELD_CELL_GAP * GameController.ResolutionMultiplier)));
	}

	/// <summary>
	/// Draws the player's grid and ships.
	/// </summary>
	/// <param name="grid">the grid to show</param>
	/// <param name="thePlayer">the player to show the ships of</param>
	/// <param name="small">true if the small grid is shown</param>
	/// <param name="showShips">true if ships are to be shown</param>
	/// <param name="left">the left side of the grid</param>
	/// <param name="top">the top of the grid</param>
	/// <param name="width">the width of the grid</param>
	/// <param name="height">the height of the grid</param>
	/// <param name="cellWidth">the width of each cell</param>
	/// <param name="cellHeight">the height of each cell</param>
	/// <param name="cellGap">the gap between the cells</param>
	private static void DrawCustomField(ISeaGrid grid, Player thePlayer, bool small, bool showShips, int left, int top, int width, int height, int cellWidth, int cellHeight,
	int cellGap)
	{
		//SwinGame.FillRectangle(Color.Blue, left, top, width, height)

		int rowTop = 0;
		int colLeft = 0;

		//Draw the grid
		for (int row = 0; row <= 9; row++) {
			rowTop = top + (cellGap + cellHeight) * row;

			for (int col = 0; col <= 9; col++) {
				colLeft = left + (cellGap + cellWidth) * col;

				Color fillColor = default(Color);
				bool draw = false;
				bool hit = false;

				draw = true;

				switch (grid[row, col]) {
					//case TileView.Ship:
					//	draw = false;
					//	break;
					//If small Then fillColor = _SMALL_SHIP Else fillColor = _LARGE_SHIP
					case TileView.Miss:
						if (small)
							fillColor = SMALL_MISS;
						else
							fillColor = LARGE_MISS;
						break;
					case TileView.Hit:
                        hit = true;
						if (small)
							fillColor = SMALL_HIT;
						else
							fillColor = LARGE_HIT;
						break;
					case TileView.Sea:
                    case TileView.Ship:
						if (small)
							fillColor = SMALL_SEA;
						else
							draw = false;
						break;
				}

				if (draw) {
					SwinGame.FillRectangle(fillColor, colLeft, rowTop, cellWidth, cellHeight);
					if (!small) {
						SwinGame.DrawRectangle(OUTLINE_COLOR, colLeft, rowTop, cellWidth, cellHeight);
                        if (hit)
						{
							SwinGame.DrawBitmap ("explode.png", colLeft, rowTop);
							/*if (GameController.Resolution == GameResolution.Res800x600) {
								SwinGame.DrawBitmap ("Explosion", (int)(colLeft * GameController.ResolutionMultiplier), (int)(rowTop * GameController.ResolutionMultiplier));
							} else if (GameController.Resolution == GameResolution.Res1024x768) {
								SwinGame.DrawBitmap ("Explosion2", (int)(colLeft * GameController.ResolutionMultiplier), (int)(rowTop * GameController.ResolutionMultiplier));
							} else if (GameController.Resolution == GameResolution.Res1280x960) {
								SwinGame.DrawBitmap ("Explosion3", (int)(colLeft * GameController.ResolutionMultiplier), (int)(rowTop * GameController.ResolutionMultiplier));
							}*/
						}
					}
				}
			}
		}

		if (!showShips) {
			return;
		}

		int shipHeight = 0;
		int shipWidth = 0;
		string shipName = null;

		//Draw the ships
		foreach (Ship s in thePlayer) {
			if (s == null || !s.IsDeployed)
				continue;
			rowTop = top + (cellGap + cellHeight) * s.Row + SHIP_GAP;
			colLeft = left + (cellGap + cellWidth) * s.Column + SHIP_GAP;

			if (s.Direction == Direction.LeftRight) {
				shipName = "ShipLR" + s.Size;
				shipHeight = cellHeight - (SHIP_GAP * 2);
				shipWidth = (cellWidth + cellGap) * s.Size - (SHIP_GAP * 2) - cellGap;
			} else {
				//Up down
				shipName = "ShipUD" + s.Size;
				shipHeight = (cellHeight + cellGap) * s.Size - (SHIP_GAP * 2) - cellGap;
				shipWidth = cellWidth - (SHIP_GAP * 2);
			}

			if (!small) {
				SwinGame.DrawBitmap(GameResources.GameImage(shipName), colLeft, rowTop);
			} else {
				SwinGame.FillRectangle(SHIP_FILL_COLOR, colLeft, rowTop, shipWidth, shipHeight);
				SwinGame.DrawRectangle(SHIP_OUTLINE_COLOR, colLeft, rowTop, shipWidth, shipHeight);
			}
		}
	}


	private static string _message;
	/// <summary>
	/// The message to display
	/// </summary>
	/// <value>The message to display</value>
	/// <returns>The message to display</returns>
	public static string Message {
		get { return _message; }
		set { _message = value; }
	}

	/// <summary>
	/// Draws the message to the screen
	/// </summary>
	public static void DrawMessage()
	{
		SwinGame.DrawText(Message, MESSAGE_COLOR, GameResources.GameFont("Courier"), (int)(Math.Round (FIELD_LEFT * GameController.ResolutionMultiplier)), (int)(Math.Round (MESSAGE_TOP * GameController.ResolutionMultiplier)));
	}

	/// <summary>
	/// Draws the background for the current state of the game
	/// </summary>

	public static void DrawBackground()
	{
		switch (GameController.CurrentState) {
			case GameState.ViewingMainMenu:
			case GameState.ViewingGameMenu:
			case GameState.AlteringSettings:
			case GameState.ViewingHighScores:
			case GameState.AlteringMusic:
			case GameState.AlteringResolution:
				if(GameController.Resolution == GameResolution.Res800x600) {
					SwinGame.DrawBitmap (GameResources.GameImage ("Menu"), 0, 0);
				} else if (GameController.Resolution == GameResolution.Res1024x768) {
					SwinGame.DrawBitmap (GameResources.GameImage ("Menu2"), 0, 0);
				} else if (GameController.Resolution == GameResolution.Res1280x960) {
					SwinGame.DrawBitmap (GameResources.GameImage ("Menu3"), 0, 0);
				}
				break;
			case GameState.Discovering:
			case GameState.EndingGame:
				if(GameController.Resolution == GameResolution.Res800x600) {
					SwinGame.DrawBitmap (GameResources.GameImage ("Discovery"), 0, 0);
				} else if (GameController.Resolution == GameResolution.Res1024x768) {
					SwinGame.DrawBitmap (GameResources.GameImage ("Discovery2"), 0, 0);
				} else if (GameController.Resolution == GameResolution.Res1280x960) {
					SwinGame.DrawBitmap (GameResources.GameImage ("Discovery3"), 0, 0);
				}
				break;
			case GameState.Deploying:
				if(GameController.Resolution == GameResolution.Res800x600) {
					SwinGame.DrawBitmap (GameResources.GameImage ("Deploy"), 0, 0);
				} else if (GameController.Resolution == GameResolution.Res1024x768) {
					SwinGame.DrawBitmap (GameResources.GameImage ("Deploy2"), 0, 0);
				} else if (GameController.Resolution == GameResolution.Res1280x960) {
					SwinGame.DrawBitmap (GameResources.GameImage ("Deploy3"), 0, 0);
				}
				break;
			default:
				SwinGame.ClearScreen();
				break;
		}

		SwinGame.DrawFramerate((int)(Math.Round(675 * GameController.ResolutionMultiplier)), (int)(Math.Round(585 * GameController.ResolutionMultiplier)), GameResources.GameFont("CourierSmall"));
	}

	/// <summary>
	/// Adds the explosion.
	/// </summary>
	/// <param name="row">Row.</param>
	/// <param name="col">Col.</param>
	public static void AddExplosion(int row, int col)
	{
		if (GameController.Resolution == GameResolution.Res800x600) {
			AddAnimation (row, col, "Explosion");
		} else if (GameController.Resolution == GameResolution.Res1024x768) {
			AddAnimation (row, col, "Explosion2");
		} else if (GameController.Resolution == GameResolution.Res1280x960) {
			AddAnimation (row, col, "Explosion3");
		}
	}

	/// <summary>
	/// Adds the splash.
	/// </summary>
	/// <param name="row">Row.</param>
	/// <param name="col">Col.</param>
	public static void AddSplash(int row, int col)
	{
		if (GameController.Resolution == GameResolution.Res800x600) {
			AddAnimation (row, col, "Splash");
		} else if (GameController.Resolution == GameResolution.Res1024x768) {
			AddAnimation (row, col, "Splash2");
		} else if (GameController.Resolution == GameResolution.Res1280x960) {
			AddAnimation (row, col, "Splash3");
		}
	}


	private static List<Sprite> _Animations = new List<Sprite>();
	/// <summary>
	/// Adds the animation.
	/// </summary>
	/// <param name="row">Row.</param>
	/// <param name="col">Col.</param>
	/// <param name="image">Image.</param>
	private static void AddAnimation(int row, int col, string image)
	{
		Sprite s = default(Sprite);
		Bitmap imgObj = default(Bitmap);

		imgObj = GameResources.GameImage(image);
		imgObj.SetCellDetails((int)(Math.Round (40 * GameController.ResolutionMultiplier)), (int)(Math.Round (40 * GameController.ResolutionMultiplier)), 3, 3, 7);

		AnimationScript animation = default(AnimationScript);
		animation = SwinGame.LoadAnimationScript("splash.txt");

		s = SwinGame.CreateSprite(imgObj, animation);
		s.X = (int)(Math.Round((FIELD_LEFT + col * (CELL_WIDTH + CELL_GAP)) * GameController.ResolutionMultiplier));
		s.Y = (int)(Math.Round((FIELD_TOP + row * (CELL_HEIGHT + CELL_GAP)) * GameController.ResolutionMultiplier));
		
		s.StartAnimation("splash");
		_Animations.Add(s);
	}
	/// <summary>
	/// Updates the animations.
	/// </summary>
	public static void UpdateAnimations()
	{
		List<Sprite> ended = new List<Sprite>();
		foreach (Sprite s in _Animations) {
			SwinGame.UpdateSprite(s);
			if (s.AnimationHasEnded) {
				ended.Add(s);
			}
		}

		foreach (Sprite s in ended) {
			_Animations.Remove(s);
			SwinGame.FreeSprite(s);
		}
	}
	/// <summary>
	/// Draws the animations.
	/// </summary>
	public static void DrawAnimations()
	{
		foreach (Sprite s in _Animations) {
			SwinGame.DrawSprite(s);
		}
	}
	/// <summary>
	/// Draws the animation sequence.
	/// </summary>
	public static void DrawAnimationSequence()
	{
		int i = 0;
		for (i = 1; i <= ANIMATION_CELLS * FRAMES_PER_CELL; i++) {
			UpdateAnimations();
			GameController.DrawScreen();
		}
	}
}