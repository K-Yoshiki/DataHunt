using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScrollPanel : MonoBehaviour {

	public GameObject missionPanel;
	public GameObject missionPanelSub;
	public GameObject buttonPrefab;
	public GameObject subButtonPrefab;

	GameObject button;
	GameObject buttonSub;
	RectTransform buttonSubRectTrans;
	RectTransform buttonRectTrans;
	RectTransform panelSubTrans;
	RectTransform panelTrans;
	float subButtonHeight;
	float buttonHeight;

	List<StageNamePackage> names;


	void OnEnable()
	{
		names = BattleDataLoader.GetStageNameList();
	}

	// Use this for initialization
	void Start () 
	{
		names = BattleDataLoader.GetStageNameList();

		CreateStageButton();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	public void CreateStageButton()
	{
		buttonHeight = 0;
		
		for(int i=0; i<names.Count; i++)
		{
			button = Instantiate(buttonPrefab) as GameObject;
			button.transform.SetParent(missionPanel.transform);
			buttonRectTrans = button.GetComponent<RectTransform>();
			button.GetComponent<StageButton>().Initialize(this, names[i]);
			buttonHeight = buttonRectTrans.sizeDelta.y;
			buttonRectTrans.localPosition = new Vector2(0, -buttonHeight* i);
			buttonRectTrans.localScale = new Vector2(1,1);
		}
		
		panelTrans = missionPanel.GetComponent<RectTransform>();
		panelTrans.sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, buttonHeight * names.Count);
	}

	public void OnClickStageButton(StageNamePackage stageNamePack)
	{
		subButtonHeight = 0;

		for(int i=0; i<stageNamePack.levelNames.Count; i++)
		{
			buttonSub = Instantiate(subButtonPrefab) as GameObject;
			buttonSub.transform.SetParent(missionPanelSub.transform);					
			buttonSubRectTrans = buttonSub.GetComponent<RectTransform>();
			buttonSub.GetComponent<LevelButton>().Initialize(stageNamePack,i);
			subButtonHeight = buttonSubRectTrans.sizeDelta.y;
			buttonSubRectTrans.localPosition = new Vector2(0, -subButtonHeight*i);
			buttonSubRectTrans.localScale = new Vector2(1,1);
		}
		
		panelSubTrans = missionPanelSub.GetComponent<RectTransform>();
		panelSubTrans.sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, subButtonHeight * stageNamePack.levelNames.Count);
	}
}
