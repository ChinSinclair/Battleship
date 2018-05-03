
//using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Data;
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
    public static void HandleDiscoveryInput ()
    {
        if (SwinGame.KeyTyped (KeyCode.vk_ESCAPE)) {
            GameController.TheGame.StopWatch.Stop ();
            GameController.AddNewState (GameState.ViewingGameMenu);
        }

        if (SwinGame.MouseClicked (MouseButton.LeftButton)) {
            DoAttack ();
        }

        if (SwinGame.MouseClicked (MouseButton.RightButton)) {
            DoCheck ();
        }
    }

    /// <summary>
    /// Attack the location that the mouse if over.
    /// </summary>
    private static void DoAttack ()
    {
        Point2D mouse = default (Point2D);

        mouse = SwinGame.MousePosition ();

        //Calculate the row/col clicked
        int row = 0;
        int col = 0;
        row = Convert.ToInt32 (Math.Floor ((mouse.Y - UtilityFunctions.FIELD_TOP) / (UtilityFunctions.CELL_HEIGHT + UtilityFunctions.CELL_GAP)));
        col = Convert.ToInt32 (Math.Floor ((mouse.X - UtilityFunctions.FIELD_LEFT) / (UtilityFunctions.CELL_WIDTH + UtilityFunctions.CELL_GAP)));

        if (row >= 0 & row < GameController.HumanPlayer.EnemyGrid.Height) {
            if (col >= 0 & col < GameController.HumanPlayer.EnemyGrid.Width) {
                GameController.Attack (row, col);
            }
        }
    }
    private static void DoCheck ()
    {
        Point2D mouse = default (Point2D);
        mouse = SwinGame.MousePosition ();

        int row = 0;
        int col = 0;
        row = Convert.ToInt32 (Math.Floor ((mouse.Y - UtilityFunctions.FIELD_TOP) / (UtilityFunctions.CELL_HEIGHT + UtilityFunctions.CELL_GAP)));
        col = Convert.ToInt32 (Math.Floor ((mouse.X - UtilityFunctions.FIELD_LEFT) / (UtilityFunctions.CELL_WIDTH + UtilityFunctions.CELL_GAP)));

        if (row >= 0 & row < GameController.HumanPlayer.EnemyGrid.Height) {
            if (col >= 0 & col < GameController.HumanPlayer.EnemyGrid.Width) {
                GameController.Check (row, col);
            }
        }
    }


    /// <summary>
    /// Draws the game during the attack phase.
    /// </summary>s
    public static void DrawDiscovery ()
    {
        const int SCORES_LEFT = 172;
        const int SHOTS_TOP = 157;
        const int HITS_TOP = 206;
        const int SPLASH_TOP = 256;
        const int TIMER_TOP = 75;
        const int TIMER_LEFT = 670;
        const int TSCORE_TOP = 85;
        const int TSCORE_LEFT = 150;
        const int SHIP_LEFT = 50;
        const int SHIP_TOP = 310;

        if ((SwinGame.KeyDown (KeyCode.vk_LSHIFT) | SwinGame.KeyDown (KeyCode.vk_RSHIFT)) & SwinGame.KeyDown (KeyCode.vk_c)) {
            UtilityFunctions.DrawField (GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, true);
        } else {
            UtilityFunctions.DrawField (GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, false);
        }

        UtilityFunctions.DrawSmallField (GameController.HumanPlayer.PlayerGrid, GameController.HumanPlayer);
        UtilityFunctions.DrawMessage ();

        SwinGame.DrawText (GameController.HumanPlayer.Shots.ToString (), Color.White, GameResources.GameFont ("Menu"), SCORES_LEFT, SHOTS_TOP);
        SwinGame.DrawText (GameController.HumanPlayer.Hits.ToString (), Color.White, GameResources.GameFont ("Menu"), SCORES_LEFT, HITS_TOP);
        SwinGame.DrawText (GameController.HumanPlayer.Missed.ToString (), Color.White, GameResources.GameFont ("Menu"), SCORES_LEFT, SPLASH_TOP);

        //Display Elapsed Time on the Game Board
        SwinGame.DrawText (GameController.TheGame.StopWatch.Elapsed.ToString ("mm\\:ss"), Color.Yellow, GameResources.GameFont ("Time"), TIMER_LEFT, TIMER_TOP);

        //Display Score on the Game Board
        SwinGame.DrawText ("TOTAL SCORE:      " + GameController.HumanPlayer.Score.ToString (), Color.Yellow, GameResources.GameFont ("Score"), TSCORE_LEFT, TSCORE_TOP);

        //Display Enemy's Ships
        SwinGame.DrawText ("ENEMY'S SHIPS", Color.Yellow, GameResources.GameFont ("Score"), SHIP_LEFT + 50, SHIP_TOP - 25);


        List<Ship> destroyedList = new List<Ship> ();

        foreach (Ship s in GameController.ComputerPlayer) {
            if (s == null || !s.IsDestroyed)
                continue;
            destroyedList.Add (s);
        }

        if (destroyedList.Count > 0) {
            foreach (Ship s in destroyedList) {
                SwinGame.DrawBitmap (GameResources.GameImage (s.ShipImage), 351 + s.Column * 42, 124 + s.Row * 42);
            }
        }

        if (!GameController.ComputerPlayer.Ship (ShipName.Tug).IsDestroyed) {
            SwinGame.DrawText ("[1] " + ShipName.Tug.ToString (), Color.White, GameResources.GameFont ("Menu"), SHIP_LEFT, SHIP_TOP);
        } else {
            SwinGame.DrawText ("[1] " + ShipName.Tug.ToString (), Color.Red, GameResources.GameFont ("Menu"), SHIP_LEFT, SHIP_TOP);
        }

        if (!GameController.ComputerPlayer.Ship (ShipName.Submarine).IsDestroyed) {
            SwinGame.DrawText ("[2] " + ShipName.Submarine.ToString (), Color.White, GameResources.GameFont ("Menu"), SHIP_LEFT, SHIP_TOP + 10);
        } else {
            SwinGame.DrawText ("[2] " + ShipName.Submarine.ToString (), Color.Red, GameResources.GameFont ("Menu"), SHIP_LEFT, SHIP_TOP + 10);

        }

        if (!GameController.ComputerPlayer.Ship (ShipName.Destroyer).IsDestroyed) {
            SwinGame.DrawText ("[3] " + ShipName.Destroyer.ToString (), Color.White, GameResources.GameFont ("Menu"), SHIP_LEFT, SHIP_TOP + 20);
        } else {
            SwinGame.DrawText ("[3] " + ShipName.Destroyer.ToString (), Color.Red, GameResources.GameFont ("Menu"), SHIP_LEFT, SHIP_TOP + 20);
        }

        if (!GameController.ComputerPlayer.Ship (ShipName.Battleship).IsDestroyed) {
            SwinGame.DrawText ("[4] " + ShipName.Battleship.ToString (), Color.White, GameResources.GameFont ("Menu"), SHIP_LEFT + 120, SHIP_TOP);
        } else {
            SwinGame.DrawText ("[4] " + ShipName.Battleship.ToString (), Color.Red, GameResources.GameFont ("Menu"), SHIP_LEFT + 120, SHIP_TOP);
        }

        if (!GameController.ComputerPlayer.Ship (ShipName.AircraftCarrier).IsDestroyed) {
            SwinGame.DrawText ("[5] " + ShipName.AircraftCarrier.ToString (), Color.White, GameResources.GameFont ("Menu"), SHIP_LEFT + 120, SHIP_TOP + 10);
        } else {
            SwinGame.DrawText ("[5] " + ShipName.AircraftCarrier.ToString (), Color.Red, GameResources.GameFont ("Menu"), SHIP_LEFT + 120, SHIP_TOP + 10);
        }
    }

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
