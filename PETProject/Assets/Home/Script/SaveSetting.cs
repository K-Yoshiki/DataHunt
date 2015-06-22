using UnityEngine;
using System.Collections;


public class SaveSetting : MonoBehaviour
{
	public void SaveUserData()
	{
		UserDataControl.Save();
	}
}
