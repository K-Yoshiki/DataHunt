using UnityEngine;
using System.Collections;

public class LaserShooter : ShooterBase
{
	//private Transform enemy; // 敵	
	
	private float rayLeng;
	[SerializeField]
	private float raySpeed;

	public override void Initialize ()
	{

	}

	protected override void Shot ()
	{
		BulletBase laser = CreateBullet();
		laser.transform.SetParent(this.transform);
	}
}
