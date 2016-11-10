using System;
using SwinGameSDK;
using NUnit.Framework;

namespace Battleships
{
	[TestFixture()]
	public class ResolutionTest
	{
		/// <summary>
		/// Tests the default resolution state of the game.
		/// </summary>
		[Test()]
		public void TestGetDefaultResolution()
		{
			// The default resolution should be 800x600
			Assert.AreEqual(GameResolution.Res800x600, GameController.Resolution);
		}

		/// <summary>
		/// Tests whether the resolution state of the game can be changed.
		/// </summary>
		[Test()]
		public void TestSetResolution()
		{
			// The default resolution should be 800x600
			Assert.AreEqual(GameResolution.Res800x600, GameController.Resolution);

			GameController.Resolution = GameResolution.Res1024x768;

			// The resolution should now be 1024x768
			Assert.AreEqual (GameResolution.Res1024x768, GameController.Resolution);

			GameController.Resolution = GameResolution.Res800x600;

			// The resolution should now be 800x600
			Assert.AreEqual (GameResolution.Res800x600, GameController.Resolution);
		}

		/// <summary>
		/// Tests whether the resolution multiplier changes successfully when the menu button is pressed.
		/// </summary>
		[Test()]
		public void TestResolutionMultiplier()
		{
			const int RESOLUTION_MENU = 4;
			const int RESOLUTION_MENU_800x600_BUTTON = 0;
			const int RESOLUTION_MENU_1024x768_BUTTON = 1;

			// The resolution multiplier should be 1
			Assert.AreEqual(1, GameController.ResolutionMultiplier);

			MenuController.PerformMenuAction(RESOLUTION_MENU, RESOLUTION_MENU_1024x768_BUTTON);

			// The resolution multiplier should now be 1.28
			Assert.AreEqual(1.28, GameController.ResolutionMultiplier);

			MenuController.PerformMenuAction (RESOLUTION_MENU, RESOLUTION_MENU_800x600_BUTTON);

			// The resolution multiplier should now be 1
			Assert.AreEqual (1, GameController.ResolutionMultiplier);
		}
	}
}
