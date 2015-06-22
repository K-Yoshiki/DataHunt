using UnityEngine;
using System.Collections;

public class PlanetRotate : MonoBehaviour {

	public float speed = 1;

	float angle;
	public float zAxis = 0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localRotation = Quaternion.Euler(new Vector3(0,angle,zAxis));
	
		angle += speed * Time.deltaTime;

	}
}
