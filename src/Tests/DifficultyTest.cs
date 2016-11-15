using System;
using SwinGameSDK;
using NUnit.Framework;

namespace Battleships
{
	
	[TestFixture()]
	public class DifficultyTest
	{
		private const int SOUND_MENU = 3;
		private const int SOUND_MENU_MUTE_BUTTON = 0;
		private const int SOUND_MENU_TORTUGA_BUTTON = 1;
		private const int SOUND_MENU_DRUMS_BUTTON = 2;
		private const int SOUND_MENU_HORROR_BUTTON = 3;
		[Test()]
		public void TestDefaultAILevel ()
		{
			Assert.AreEqual (AIOption.Easy, GameController.GetDifficulty ());
		}

		[Test()]
		public void TestSetAILevel ()
		{
			GameController.SetDifficulty (AIOption.Medium);
			Assert.AreEqual (AIOption.Medium, GameController.GetDifficulty ());

			GameController.SetDifficulty (AIOption.Hard);
			Assert.AreEqual (AIOption.Hard, GameController.GetDifficulty ());
		}

		[Test()]
		public void TestTimerInAILevel ()
		{
			GameController.SetDifficulty (AIOption.Medium);
			Assert.AreEqual ("00:00", StopWatch.TimeTaken("elapsedTime"));
		}


	}
}

