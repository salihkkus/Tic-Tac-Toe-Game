using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
public static GameManager Instance {get; private set;}
public List<TileController> ListTileController => listTileController;

[SerializeField] private List<TileController> listTileController;

public int Turn{get; set;}

    private void Awake()
    {
        Instance = this;
    }

  public (bool, TileState) HasWinner()
	{
		foreach (var tile in listTileController)
		{
			if (tile == null) continue;

			foreach (var direction in Enum.GetValues(typeof(Direction)))
			{
				var next = tile.GetNextTile((Direction) direction);
				if (!next) continue;

				if (next.MyState != tile.MyState) continue;

				var lastTile = next.GetNextTile((Direction) direction);
				if (!lastTile) continue;

				if (lastTile.MyState != tile.MyState) continue;
				
				return (true, tile.MyState);
			}
		}

		return (false, TileState.None);
}


}
