using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileController : MonoBehaviour, IPointerDownHandler
{
    public TileState MyState { get; set; }
    public Vector2 coordinate;
    [SerializeField] private SpriteRenderer mySpriteRenderer;
    [SerializeField] Color xColor;
    [SerializeField] Color oColor;



  public void  OnPointerDown(PointerEventData eventData)
  {
    Debug.Log("click");

    var state = GameManager.Instance.Turn % 2 == 0 ? TileState.X : TileState.O;
    SetState(state);
    GameManager.Instance.Turn++;
  }

   public void SetState(TileState state)
  {
    MyState = state;
    mySpriteRenderer.color = state == TileState.X ? xColor : oColor;
  }
   public enum TileState
  {
    None,
    X,
    O
  }

}
