using UnityEngine;
using System.Collections;

public class DestoyItem : MonoBehaviour {

	public float destroyTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Destroy(this.gameObject, destroyTime);
	}
}
