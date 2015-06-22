using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			Debug.Log("Hit");
		}
	}
}
