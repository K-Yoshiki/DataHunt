using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class LabPartsListPanel : MonoBehaviour
{
	public GridFlexiblePanel gridFlexiblePanel;
	public LabPartsButton buttonModel;
	public AudioClip buttonSound;
	Dictionary<PartsSetPoint?, List<LabPartsData>> cacheList;
	List<LabPartsButton> buttonList;


	void Start()
	{
		cacheList = new Dictionary<PartsSetPoint?, List<LabPartsData>>();
		buttonList = new List<LabPartsButton>();
		LabManager.Instance.OnSelectChange += SelectChange;
	}

	/// <summary>
	/// 選択状態が変更された場合
	/// </summary>
	/// <param name="setPoint">Set point.</param>
	void SelectChange(PartsSetPoint? setPoint)
	{
		DeleteButtons();

		if (setPoint == null)
			return;

		// 装備外し用ボタンの追加(左以外)
		if (((PartsSetPoint)setPoint) != PartsSetPoint.Left)
		{
			ButtonInst(new LabPartsData(-1, null));
		}
		
		List<LabPartsData> partsList = GetPartsPrefabList(setPoint);
		foreach (var partsData in partsList)
		{
			ButtonInst(partsData);
		}
		gridFlexiblePanel.FittingSize();
	}

	void ButtonInst(LabPartsData partsData)
	{
		LabPartsButton button = (LabPartsButton)Instantiate(buttonModel);
		button.SetData(partsData, ChangeEquip);
		button.transform.SetParent(gridFlexiblePanel.transform);
		button.transform.localScale = Vector3.one;
		buttonList.Add(button);
	}

	void DeleteButtons()
	{
		foreach (var button in buttonList)
		{
			Destroy(button.gameObject);
		}
		buttonList.Clear();
	}

	void ChangeEquip(LabPartsData partsData)
	{
		PlaySound();
		LabManager.Instance.ChangeEquip(partsData);
	}

	void PlaySound()
	{
		if (buttonSound != null)
			AppUtils.Sound.Instance.PlaySE(buttonSound, AppUtils.Sound.Instance.transform);
	}

	/// <summary>
	/// 指定位置に装備可能な所持パーツプレハブをリストにして取得します.
	/// </summary>
	/// <returns>The parts prefab list.</returns>
	/// <param name="setPoint">Set point.</param>
	List<LabPartsData> GetPartsPrefabList(PartsSetPoint? setPoint)
	{
		if (cacheList.ContainsKey(setPoint))
			return cacheList[setPoint];

		List<LabPartsData> list = new List<LabPartsData>();
		List<PartsData> partsList = UserDataControl.Data.personalParts.GetParts();
		
		foreach (var partsData in partsList)
		{
			PlayerParts partsPrefab = PartsLoader.Load(partsData.partsID);
			if (ChackOnList(partsPrefab.canPartsEquip, setPoint))
			{
				list.Add(new LabPartsData(partsData.personalID, partsPrefab));
			}
		}
		cacheList[setPoint] = list;
		return list;
	}
	
	bool ChackOnList(PartsCanEquip canEquip, PartsSetPoint? setPoint)
	{
		if (setPoint == null)
			return false;
		if (setPoint == PartsSetPoint.Left)
			return canEquip.left;
		if (setPoint == PartsSetPoint.Right)
			return canEquip.right;
		if (setPoint == PartsSetPoint.Top)
			return canEquip.top;
		if (setPoint == PartsSetPoint.Behind)
			return canEquip.behind;
		return false;
	}
}

public class LabPartsData
{
	public int personalID;
	public PlayerParts partsPrefab;

	public LabPartsData(int personalID, PlayerParts partsPrefab)
	{
		this.personalID = personalID;
		this.partsPrefab = partsPrefab;
	}
}