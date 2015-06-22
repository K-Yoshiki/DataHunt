using UnityEngine;
using System.Collections;

public class MoonRotate : MonoBehaviour {

	public float speed;

	float angle;



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.localRotation = Quaternion.Euler(new Vector3(0,angle,0));

		angle += speed * Time.deltaTime;

	}
}
