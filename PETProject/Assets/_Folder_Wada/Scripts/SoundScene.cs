using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundScene : MonoBehaviour 
{
	public GameObject missionPanel;
	public GameObject buttonPrefab;
	public GameObject soundList;

	public Text text;

	SoundTest soundTest;
	GameObject button;
	RectTransform buttonRectTrans;
	RectTransform panelTrans;
	float buttonHeight;	
	int soundLength;
	SoundTest test;
	AudioClip testBgm;

	void OnEnable()
	{
		soundLength = soundList.GetComponent<SoundTest>().bgms.Length;
		testBgm = buttonPrefab.GetComponent<SoundTestBgm>().testSound;
	}
	
	// Use this for initialization
	void Start () 
	{		
		CreateStageButton();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log(soundLength);
		//Debug.Log(soundList.GetComponent<SoundTest>().bgms[soundLength].name);
		//Debug.Log(soundList.GetComponent<SoundTest>().bgms[6].bgm);
	}
	
	public void CreateStageButton()
	{
		buttonHeight = 0;
		
		for(int i=0; i<soundLength; i++)
		{
			button = Instantiate(buttonPrefab) as GameObject;
			button.transform.SetParent(missionPanel.transform);
			buttonRectTrans = button.GetComponent<RectTransform>();
			text.text = soundList.GetComponent<SoundTest>().bgms[i].name;
			testBgm = soundList.GetComponent<SoundTest>().bgms[i].bgm;
			buttonHeight = buttonRectTrans.sizeDelta.y;
			buttonRectTrans.localPosition = new Vector2(0, -buttonHeight* i);
			buttonRectTrans.localScale = new Vector2(1,1);
		}
		
		panelTrans = missionPanel.GetComponent<RectTransform>();
		panelTrans.sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, buttonHeight * soundLength);
	}
}
