using UnityEngine;
using System.Collections;


public class NormalShooter : ShooterBase
{
	public override void Initialize()
	{

	}
		
	protected override void Shot()
	{
		CreateBullet();
	}
}
