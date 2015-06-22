using UnityEngine;
using System;
using System.Collections;


/// <summary>
/// Player Control Main Class
/// </summary>
public class PlayerController : MonoSingleton<PlayerController>
{
	public event Action ShotEvent = delegate{};
	public event Action ClearEvent = delegate{};
	public event Action<float> RotateEvent = delegate{};

	private Player player;
	private RailChanger railChanger;
	private PlayerRotater rotater;

	public short NowRail { get; private set; }


	public void ControlState(bool enable)
	{
		if (enable)
		{
			AddControl();
		}
		else
		{
			RemoveControl();
		}
	}

	public Transform Core
	{
		get {
			return railChanger.transform;
		}
	}

	public void ClearBullet()
	{
		ClearEvent();
	}
	
	public void SetStartRail()
	{
		NowRail = (short)railChanger.ChangeRail(FieldManager.Instance.RailCount() - 1, RailVec.Inner);
	}

	public bool IsDead
	{
		get { return player.IsDead; }
	}

	protected override void Awake()
	{
		base.Awake();

		player = this.GetComponentInChildren<Player>();
		rotater = this.GetComponent<PlayerRotater>();
		railChanger = this.GetComponentInChildren<RailChanger>();
		railChanger.SetRail = SetRail;
	}
	
	void OnDisable()
	{
		RemoveControl();
	}
	
	void Shot(TouchInfo touch)
	{
		ShotEvent();
	}

	void TouchDragControl(TouchInfo touch)
	{
		float absX, absY;
		absX = Mathf.Abs(touch.deltaPosition.x);
		absY = Mathf.Abs(touch.deltaPosition.y);

		if (absX > absY)
		{
			RotateEvent(touch.deltaPosition.x);
		}
		if (absX < absY)
		{
			if (touch.deltaPosition.y >= 5.0f)
			{
				NowRail = (short)railChanger.ChangeRail(NowRail, RailVec.Inner);
			}
			else if (touch.deltaPosition.y <= -5.0f)
			{
				NowRail = (short)railChanger.ChangeRail(NowRail, RailVec.Outer);
			}
		}
	}

	void SetRail(int railNum)
	{
		NowRail = (short)railNum;
	}
	
	void AddControl()
	{
		TouchSensor.SwipeEvent += TouchDragControl;
		TouchSensor.StayEvent += Shot;
		TouchSensor.SwipeEvent += Shot;
	}
	
	void RemoveControl()
	{
		TouchSensor.SwipeEvent -= TouchDragControl;
		TouchSensor.StayEvent -= Shot;
		TouchSensor.SwipeEvent -= Shot;
		rotater.RotateStop();
	}

	/// <summary>
	/// プレイヤーのショットイベントへの登録
	/// </summary>
	public void RegistShooter(ShooterBase shooter)
	{
		ShotEvent += shooter.OnShot;
		ClearEvent += shooter.ClearBullet;
	}
}