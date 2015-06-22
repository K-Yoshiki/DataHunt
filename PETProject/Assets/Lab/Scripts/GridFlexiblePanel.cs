using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Gridの要素数でパネルのサイズを変更する
/// </summary>
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(GridLayoutGroup))]
public class GridFlexiblePanel : MonoBehaviour
{
	public bool flexWidth;
	[Range(1, 100)]
	public int rowSize;
	public bool flexHeight;
	[Range(1, 100)]
	public int columnSize;
	GridLayoutGroup grid;
	RectTransform selfRect;

	void Start()
	{
		grid = GetComponent<GridLayoutGroup>();
		selfRect = GetComponent<RectTransform>();
		FittingSize();
	}

	public void FittingSize()
	{
		float count = transform.childCount;
		
		if (flexWidth)
			FitWidth(Mathf.CeilToInt(count / (float)rowSize));
		if (flexHeight)
			FitHeight(Mathf.CeilToInt(count / (float)columnSize));
	}

	void FitWidth(int count)
	{
		float width = count * (grid.cellSize.x + grid.spacing.x); 
		selfRect.sizeDelta = new Vector2(width, selfRect.sizeDelta.y);
	}

	void FitHeight(int count)
	{
		float height = count * (grid.cellSize.y + grid.spacing.y);
		selfRect.sizeDelta = new Vector2(selfRect.sizeDelta.x, height);
	}
}
