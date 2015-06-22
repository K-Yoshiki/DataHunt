using UnityEngine;
using System.Collections;


public class HomePET : MonoBehaviour
{
	public PartsConnector connector;
	
	void Start()
	{
		if (connector != null)
			SetUp();
	}
	
	void SetUp()
	{
		PETEquip equip = UserDataControl.Data.petData.petEquip;
		connector.SetLeft(PartsLoader.LoadByPersonalID(equip.leftPID));
		connector.SetRight(PartsLoader.LoadByPersonalID(equip.rightPID));
		connector.SetTop(PartsLoader.LoadByPersonalID(equip.topPID));
		connector.SetBehind(PartsLoader.LoadByPersonalID(equip.behindPID));
	}
}
