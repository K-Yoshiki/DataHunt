using UnityEngine;
using System.Collections;
using AppUtils;


public class SoundTest : MonoBehaviour {

	public SoundTestItem[] bgms;

	void Start()
	{

	}

	void Update()
	{
		//Debug.Log(bgms.Length);
	}

}

[System.Serializable]
public class SoundTestItem
{
	public string name;
	public AudioClip bgm;
}