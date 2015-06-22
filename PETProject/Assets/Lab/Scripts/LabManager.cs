using UnityEngine;
using System;
using System.Collections.Generic;


/// <summary>
/// ラボシーン内の管理クラス
/// </summary>
public class LabManager : MonoSingleton<LabManager>
{
	public event Action<PartsSetPoint?> OnSelectChange = delegate{};

	public LabPet labPet;
	PartsSetPoint? selectedPoint;

	void Start()
	{
		labPet = (LabPet)Instantiate(labPet);
		labPet.name = labPet.name.Replace("(Clone)", "");
	}

	/// <summary>
	/// ホームに戻る
	/// </summary>
	public void BackHome()
	{
		ChangeSelected(null);
		SceneManager.Instance.SetState(SceneState.Home);
	}

	/// <summary>
	/// 選択しているものを変更する
	/// </summary>
	/// <param name="selectPoint">Select point.</param>
	public void ChangeSelected(PartsSetPoint? selectPoint)
	{
		if (this.selectedPoint != selectPoint)
		{
			this.selectedPoint = selectPoint;
			OnSelectChange(selectedPoint);
		}
	}

	/// <summary>
	/// PETの装備変更
	/// </summary>
	/// <param name="partsData">Parts data.</param>
	public void ChangeEquip(LabPartsData partsData)
	{
		PartsSetPoint setPoint = (PartsSetPoint)selectedPoint;
		PETEquip petEquip = UserDataControl.Data.petData.petEquip;

		if (setPoint == PartsSetPoint.Left)
		{
			labPet.SetLeft(partsData.partsPrefab);
			petEquip.leftPID = partsData.personalID;
		}
		else if (setPoint == PartsSetPoint.Right)
		{
			labPet.SetRight(partsData.partsPrefab);
			petEquip.rightPID = partsData.personalID;
		}
		else if (setPoint == PartsSetPoint.Top)
		{
			labPet.SetTop(partsData.partsPrefab);
			petEquip.topPID = partsData.personalID;
		}
		else if (setPoint == PartsSetPoint.Behind)
		{
			labPet.SetBehind(partsData.partsPrefab);
			petEquip.behindPID = partsData.personalID;
		}
		UserDataControl.Save();
	}
}