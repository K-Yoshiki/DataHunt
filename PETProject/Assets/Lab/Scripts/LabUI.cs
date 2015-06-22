using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

/// <summary>
/// ラボのUI管理
/// </summary>
public class LabUI : MonoBehaviour
{
	public Color nonSelectColor;
	public Color selectColor;
	public Button[] uiButtons;
	Button selected;

	/// <summary>
	/// 左面パーツボタンが押された時
	/// </summary>
	/// <param name="self">Self.</param>
	public void PressLeftParts(Button self)
	{
		ChangeSelect(self);
		LabManager.Instance.ChangeSelected(PartsSetPoint.Left);
	}

	/// <summary>
	/// 右面パーツボタンが押された時
	/// </summary>
	/// <param name="self">Self.</param>
	public void PressRightParts(Button self)
	{
		ChangeSelect(self);
		LabManager.Instance.ChangeSelected(PartsSetPoint.Right);
	}

	/// <summary>
	/// 上面パーツボタンが押された時
	/// </summary>
	/// <param name="self">Self.</param>
	public void PressTopParts(Button self)
	{
		ChangeSelect(self);
		LabManager.Instance.ChangeSelected(PartsSetPoint.Top);
	}

	/// <summary>
	/// 背面パーツボタンが押された時
	/// </summary>
	/// <param name="self">Self.</param>
	public void PressBehindParts(Button self)
	{
		ChangeSelect(self);
		LabManager.Instance.ChangeSelected(PartsSetPoint.Behind);
	}

	/// <summary>
	/// 戻るボタンが押された時
	/// </summary>
	public void PressBackButton()
	{
		ChangeInteractible(false);
		LabManager.Instance.BackHome();
	}

	void ChangeInteractible(bool active)
	{
		for (int i = 0; i < uiButtons.Length; i++)
		{
			uiButtons[i].interactable = active;
		}
	}

	void ChangeSelect(Button newSelected)
	{
		if (selected != null)
			selected.targetGraphic.GetComponent<Outline>().effectColor = nonSelectColor;
		newSelected.targetGraphic.GetComponent<Outline>().effectColor = selectColor;
		selected = newSelected;
	}
}
