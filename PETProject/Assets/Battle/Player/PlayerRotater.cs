using UnityEngine;
using System.Collections;


public class PlayerRotater : MonoBehaviour
{
	[SerializeField]
	float multSpeed;
	
	[SerializeField]
	float maxSpeed;
	
	[SerializeField]
	float slowDownValue;
	
	public float RotateAmount { get; private set; }

	public void RotateStop()
	{
		RotateAmount = 0f;
	}
	
	void Start()
	{
		RotateAmount = 0;		
		multSpeed = Mathf.Abs(multSpeed);
		
		PlayerController pCtrl;
		if ((pCtrl = this.GetComponent<PlayerController>()) != null)
		{
			pCtrl.RotateEvent += SwipeRotation;
		}
	}
	
	void Update()
	{
		int rail = PlayerController.Instance.NowRail;
		float calcSpeed = CharaBase.CalcSpeed(1.0f, FieldManager.Instance.GetRadius(rail));

		SlowDown();
		this.transform.Rotate(new Vector3(0, RotateAmount * calcSpeed, 0));
	}
	
	void SlowDown()
	{
		// Left or Right Dir Sign
		float sign =  Mathf.Sign(RotateAmount);
		
		if (Mathf.Abs(RotateAmount) >= maxSpeed)
		{
			RotateAmount = maxSpeed * sign;
		}
		
		RotateAmount -= slowDownValue * sign;
	}

	void SwipeRotation(float speed)
	{
		RotateAmount += speed * multSpeed * -1;
	}
}
