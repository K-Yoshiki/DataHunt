using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;


public class ExButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler, IDragHandler, IEndDragHandler
{
	public UnityEvent enter;
	public UnityEvent exit;
	public UnityEvent up;
	public UnityEvent down;
	public UnityEvent click;
	public UnityEvent drag;
	public UnityEvent dragExit;

	public void OnPointerEnter(PointerEventData eventData)
	{
		enter.Invoke();
		//Debug.Log("マウスドラッグ開始 position="+eventData.position);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		exit.Invoke();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		up.Invoke();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		down.Invoke();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		click.Invoke();
		//Debug.Log("マウスドラッグ開始 position="+eventData.position);
	}

	public void OnDrag(PointerEventData eventData) 
	{
		drag.Invoke();
		Debug.Log("マウスドラッグ開始 position="+eventData.position);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		dragExit.Invoke();
		Debug.Log("end");
	}
}
