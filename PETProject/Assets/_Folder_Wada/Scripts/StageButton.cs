using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageButton : MonoBehaviour {

	ScrollPanel scrollPanel;
	StageNamePackage stageName;

	public Text stagelabel;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void Initialize(ScrollPanel scrollPanel, StageNamePackage stageName)
	{
		this.scrollPanel = scrollPanel;
		this.stageName = stageName;
		stagelabel.text = stageName.stageName;
	}

	public void OnClickButton()
	{
		scrollPanel.OnClickStageButton(stageName);
	}
}
