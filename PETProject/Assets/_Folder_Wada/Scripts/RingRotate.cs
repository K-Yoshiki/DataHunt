using UnityEngine;
using System.Collections;

public class RingRotate : MonoBehaviour {

	//public float speed = 1;
	//public float zAxis = 0;

//	float ringPosY;

	public float speed;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
//		ringPosY = transform.localEulerAngles.y;

	}

	public void RotateRight()
	{
		transform.Rotate(0,120, 0);
	}

	public void RotateLeft()
	{
		transform.Rotate(0,-120,0);
	}
}
