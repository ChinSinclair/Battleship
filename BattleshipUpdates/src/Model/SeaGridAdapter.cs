
//using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Data;
using System.Diagnostics;
/// <summary>
/// The SeaGridAdapter allows for the change in a sea grid view. Whenever a ship is
/// presented it changes the view into a sea tile instead of a ship tile.
/// </summary>
public class SeaGridAdapter : ISeaGrid
{


	private SeaGrid _MyGrid;
	/// <summary>
	/// Create the SeaGridAdapter, with the grid, and it will allow it to be changed
	/// </summary>
	/// <param name="grid">the grid that needs to be adapted</param>
	public SeaGridAdapter(SeaGrid grid)
	{
		_MyGrid = grid;
		_MyGrid.Changed += new EventHandler(MyGrid_Changed);
	}

	/// <summary>
	/// MyGrid_Changed causes the grid to be redrawn by raising a changed event
	/// </summary>
	/// <param name="sender">the object that caused the change</param>
	/// <param name="e">what needs to be redrawn</param>
	private void MyGrid_Changed(object sender, EventArgs e)
	{
		if (Changed != null) {
			Changed(this, e);
		}
	}

	#region "ISeaGrid Members"

	/// <summary>
	/// Changes the discovery grid. Where there is a ship we will sea water
	/// </summary>
	/// <param name="x">tile x coordinate</param>
	/// <param name="y">tile y coordinate</param>
	/// <returns>a tile, either what it actually is, or if it was a ship then return a sea tile</returns>
	public TileView this[int x, int y] {
		get {
			TileView result = _MyGrid[x, y];

			if (result == TileView.Ship) {
				return TileView.Sea;
			} else {
				return result;
			}
		}
	}

	/// <summary>
	/// Indicates that the grid has been changed
	/// </summary>
	public event EventHandler Changed;

	/// <summary>
	/// Get the width of a tile
	/// </summary>
	public int Width {
		get { return _MyGrid.Width; }
	}

	/// <summary>
	/// Get the height of the tile
	/// </summary>
	public int Height {
		get { return _MyGrid.Height; }
	}

	/// <summary>
	/// HitTile calls oppon _MyGrid to hit a tile at the row, col
	/// </summary>
	/// <param name="row">the row its hitting at</param>
	/// <param name="col">the column its hitting at</param>
	/// <returns>The result from hitting that tile</returns>
	public AttackResult HitTile(int row, int col)
	{
		return _MyGrid.HitTile(row, col);
	}

	/// <summary>
	/// CheckTile calls _MyGrid to check a tile at the selected row and col
	/// </summary>
	/// <returns>The result of hitting that tile</returns>
	/// <param name="row">Row it is hitting at</param>
	/// <param name="col">Col it is hitting at</param>
	public AttackResult CheckTile(int row, int col)
	{
		return _MyGrid.CheckTile(row, col);
	}
	#endregion

	/// <summary>
	/// ImpAiHitTile is the impossible AI implementation for HitTile.
	/// </summary>
	/// <returns>The result of hitting that tile</returns>
	/// <param name="row">Row it is hitting at</param>
	/// <param name="col">Col it is hitting at</param>
	public AttackResult ImpAIHitTile(int row, int col)
	{
		return _MyGrid.ImpAIHitTile(row, col);
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
