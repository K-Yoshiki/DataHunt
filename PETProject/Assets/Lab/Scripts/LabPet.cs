using UnityEngine;
using System.Collections;


/// <summary>
/// ラボ用のPETの制御
/// </summary>
[RequireComponent(typeof(PartsConnector))]
public class LabPet : MonoBehaviour
{
	PartsConnector connector;

	void Start()
	{
		connector = GetComponent<PartsConnector>();
		SetUp();
	}

	void SetUp()
	{
		PETEquip equip = UserDataControl.Data.petData.petEquip;
		SetLeft(equip.leftPID);
		SetRight(equip.rightPID);
		SetTop(equip.topPID);
		SetBehind(equip.behindPID);
	}

	// Left
	public void SetLeft(int personalID)
	{
		connector.SetLeft(PartsLoader.LoadByPersonalID(personalID));
	}
	public void SetLeft(PlayerParts partsPrefab)
	{
		connector.SetLeft(partsPrefab);
	}

	// Right
	public void SetRight(int personalID)
	{
		connector.SetRight(PartsLoader.LoadByPersonalID(personalID));
	}
	public void SetRight(PlayerParts partsPrefab)
	{
		connector.SetRight(partsPrefab);
	}


	// Top
	public void SetTop(int personalID)
	{
		connector.SetTop(PartsLoader.LoadByPersonalID(personalID));
	}
	public void SetTop(PlayerParts partsPrefab)
	{
		connector.SetTop(partsPrefab);
	}


	// Behind
	public void SetBehind(int personalID)
	{
		connector.SetBehind(PartsLoader.LoadByPersonalID(personalID));
	}
	public void SetBehind(PlayerParts partsPrefab)
	{
		connector.SetBehind(partsPrefab);
	}
}