using UnityEngine;
using System.Collections;

public class SpeedDown : GimmickBase {
	//PlayerController play;
	void Start () {
		//play = GetComponent<PlayerController>();
	}

	protected override void Update () {

	}

	protected override void GimmickEnter ()
	{
		Debug.Log("SpeedDown(Enter)");
	}

	protected override void GimmickStay ()
	{
		Debug.Log("SpeedDown(Stay)");
		//play.maxSpeed = 0.1f;
	}

	protected override void GimmickExit ()
	{
		Debug.Log("SpeedDown(Exit)");
	}
}
