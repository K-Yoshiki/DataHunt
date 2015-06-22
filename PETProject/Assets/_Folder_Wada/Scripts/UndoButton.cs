using UnityEngine;
using System.Collections;

public class UndoButton : MonoBehaviour {

	public GameObject BackHomeButton;
	public GameObject BackMissionButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GoToMission()
	{
		BackHomeButton.SetActive(true);
		BackMissionButton.SetActive(false);
	}

	public void GoBackMission()
	{
		BackHomeButton.SetActive(false);
		BackMissionButton.SetActive(true);
	}

}
