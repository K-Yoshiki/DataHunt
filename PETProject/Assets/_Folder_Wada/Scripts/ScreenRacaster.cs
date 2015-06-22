using UnityEngine;
using System.Collections;

public class ScreenRacaster : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnActive()
	{
		this.gameObject.SetActive(true);
	}

	public void OffActive()
	{
		this.gameObject.SetActive(false);
	}
}
