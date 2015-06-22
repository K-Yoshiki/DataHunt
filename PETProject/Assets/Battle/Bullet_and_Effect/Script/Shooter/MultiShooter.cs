using UnityEngine;
using System.Collections;


public class MultiShooter : ShooterBase
{
	public int bulletsCount;
	public float shotAngle;
	

	public override void Initialize()
	{
		
	}
	
	protected override void Shot()
	{
		float addAngle = shotAngle / bulletsCount;
		float angle = shotAngle * 0.5f;
		for(int i = 0; i < bulletsCount; ++i)
		{
			BulletBase bullet = CreateBullet();
			bullet.transform.rotation *= Quaternion.Euler(Vector3.up * angle);
			angle += addAngle;
		}
	}
}