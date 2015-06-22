using UnityEngine;
using System.Collections;

public class GimmickBase : MonoBehaviour {

	void Start () {	
	}

	protected virtual void Update () {

	}

	protected virtual void GimmickEnter()
	{

	}

	protected virtual void GimmickStay()
	{

	}

	protected virtual void GimmickExit()
	{

	}

	//Enter
	void OnCollisionEnter(Collision coli)
	{
		if(coli.gameObject.tag == Tag.Player)
		{
			GimmickEnter();
		}
	}

	void OnTriggerEnter(Collider coli)
	{
		if(coli.gameObject.tag == Tag.Player)
		{
			GimmickEnter();
		}
	}

	//Stay
	void OnCollisionStay(Collision coli)
	{
		if(coli.gameObject.tag == Tag.Player)
		{
			GimmickStay();
		}
	}

	void OnTriggerStay(Collider coli)
	{
		if(coli.gameObject.tag == Tag.Player)
		{
			GimmickStay();
		}
	}

	//Exit
	void OnCollisionExit(Collision coli)
	{
		if(coli.gameObject.tag == Tag.Player)
		{
			GimmickExit();
		}
	}

	void OnTriggerExit(Collider coli)
	{
		if(coli.gameObject.tag == Tag.Player)
		{
			GimmickExit();
		}
	}
}
