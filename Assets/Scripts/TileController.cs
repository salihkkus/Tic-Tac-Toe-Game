using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour, IPointerDownHandler
{

  public TileState MyState { get; set; }
  public Vector2 coordinate;
  [SerializeField] private SpriteRenderer mySpriteRenderer;
  [SerializeField] private Sprite xSprite;
  [SerializeField] private Sprite oSprite;

  [SerializeField] Color xColor;
  [SerializeField] Color oColor;

  public void  OnPointerDown(PointerEventData eventData)
  {
    if(MyState != TileState.None)
    return;
    

    var state = GameManager.Instance.Turn % 2 == 0 ? TileState.X : TileState.O;
    SetState(state);
    GameManager.Instance.Turn++;

    var result = GameManager.Instance.HasWinner();
    if(result.Item1)
    {
      Debug.Log($"Winner => {result.Item2}");
      GameManager.Instance.OnGameOver();
    }
  }

  public void SetState(TileState state)
  {
    MyState = state;
    mySpriteRenderer.color = state == TileState.X ? xColor : oColor;
    mySpriteRenderer.sprite = state == TileState.X ? xSprite : oSprite;
  }

  public TileController GetNextTile(Direction direction)
	{
		var nextTileCoordinate = coordinate;
		switch (direction)
		{
			case Direction.Up:
				nextTileCoordinate.y++;
				break;
			case Direction.UpRight:
				nextTileCoordinate.y++;
				nextTileCoordinate.x++;
				break;
			case Direction.Right:
				nextTileCoordinate.x++;
				break;
			case Direction.DownRight:
				nextTileCoordinate.x++;
				nextTileCoordinate.y--;
				break;
			case Direction.Down:
				nextTileCoordinate.y--;
				break;
			case Direction.LeftDown:
				nextTileCoordinate.y--;
				nextTileCoordinate.x--;
				break;
			case Direction.Left:
				nextTileCoordinate.x--;
				break;
			case Direction.UpLeft:
				nextTileCoordinate.x--;
				nextTileCoordinate.y++;
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
		}

    return GameManager.Instance.ListTileController.Find(tile => tile.coordinate == nextTileCoordinate);

}
}

  public enum TileState
  {
    None,
    X,
    O
  }

  public enum Direction
  {
    Up, UpRight, Right, Down, DownRight, Left, UpLeft, LeftDown
  }