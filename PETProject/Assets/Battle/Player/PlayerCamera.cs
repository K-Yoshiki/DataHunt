using UnityEngine;
using System.Collections;


public class PlayerCamera : MonoBehaviour
{
	public float maxSlideX = 2.0f;
	public float multSpeed = 0.1f;
	public float accelSpeed = 5.0f;
	private PlayerRotater rotater;

	void Start()
	{
		maxSlideX = Mathf.Abs(maxSlideX);
		multSpeed = Mathf.Abs(multSpeed);
	
		if ((this.rotater = this.GetComponentInParent<PlayerRotater>()) == null)
		{
			this.enabled = false;
		}
	}
	
	void Update()
	{
		SlideCamera(rotater.RotateAmount);
	}
	
	/// <summary>
	/// 操作に応じてカメラの位置を少しずらす処理
	/// </summary>
	/// <param name="speed">現在のプレイヤーの回転スピード値</param>
	void SlideCamera(float speed)
	{
		Vector3 pos = transform.localPosition;
		float accel = 1;
		
		float sign = Mathf.Sign(speed);
		if (sign != 0)
		{
			float posSign = Mathf.Sign(pos.x);
			if (sign == posSign)
			{
				accel = accelSpeed;
			}
		}
		
		pos.x += -speed * multSpeed * accel;
		
		pos.x = Mathf.Clamp(pos.x, -maxSlideX, maxSlideX);
		
		transform.localPosition = pos;
	}
}
