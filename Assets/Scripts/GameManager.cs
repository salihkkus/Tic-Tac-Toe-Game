using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance {get; private set;}
  public List<TileController> ListTileController => listTileController;
  [SerializeField] private List<TileController> listTileController;
  public int Turn{get; set;}

  public bool IsStartGame {get; set;}
  [SerializeField] private Canvas canvas;
  [SerializeField] private TextMeshProUGUI textStart;
  [SerializeField] private TextMeshProUGUI textWinner;
  [SerializeField] private Button buttonRestart;

    private void Awake()
    {
        Instance = this;
    }

    public void OnStartGame()
    {
      IsStartGame = true;
      canvas.enabled = false;
      buttonRestart.gameObject.SetActive(false);
      textWinner.enabled = false;
    }
    
    public void OnClick_RestartGameButton()
    {
     OnStartGame();
     foreach(var tile in listTileController)
     {
        tile.SetState(TileState.None);
     }
    }
    public void OnGameOver(TileState tileState)
    {
      IsStartGame = false;
      canvas.enabled = true;
      textStart.enabled = false;
      textWinner.enabled = true;

      string result = "";

      switch(tileState)
      {
        case TileState.X:
        result = "WINNER X";
        break;

        case TileState.O:
        result = "WINNER O";
        break;

        default : 
        result = "DRAW";
        break;
      }
    
    textWinner.text = result;

    buttonRestart.gameObject.SetActive(true);

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
 
public bool HasNoneTile()
	{
		return listTileController.Exists(x => x.MyState == TileState.None);
	}

}