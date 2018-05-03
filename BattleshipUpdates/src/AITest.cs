using System;
using NUnit.Framework;
using SwinGameSDK;
namespace Battleship
{
	[TestFixture()]
	public class AIEasyTest
	{
		[Test()]
		public void TestAI ()
		{
			AIPlayer _ai;

			BattleShipsGame _theGame = new BattleShipsGame ();
			_ai = new AIEasyPlayer (_theGame);
		
			Assert.AreNotSame (_ai.IsDestroyed, true);

		}
	}
}
