using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// エネミーの基本情報
/// </summary>
[System.Serializable]
public class EnemyData : MonoBehaviour
{
	public EnemyBase Prefab;
	public int DefHp;
	public EnemyShooterData ShooterData;
	public EnemyBulletData BulletData;
	
	public EnemyData()
	{
		ShooterData = ShooterData ?? new EnemyShooterData();
		BulletData = BulletData ?? new EnemyBulletData();
	}
	
	public EnemyData(EnemyBase prefab, int defHP, EnemyShooterData shooterData, EnemyBulletData bulletData)
	{
		this.Prefab = prefab;
		this.DefHp = defHP;
		this.ShooterData = shooterData;
		this.BulletData = bulletData;
	}
}

/// <summary>
/// エネミーのシューター情報
/// </summary>
[System.Serializable]
public class EnemyShooterData
{
	public ShooterBase Prefab;
	public float Interval;
	
	public EnemyShooterData(){}
	
	public EnemyShooterData(ShooterBase prefab, float interval)
	{
		this.Prefab = prefab;
		this.Interval = interval;
	}
}

/// <summary>
/// エネミーの弾情報
/// </summary>
[System.Serializable]
public class EnemyBulletData
{
	public BulletBase Prefab;
	public int DP;
	public float Range;
	public float Speed;
	public float LifeTime;
	
	public EnemyBulletData(){}
	
	public EnemyBulletData(BulletBase prefab, int dp, float range, float speed, float lifeTime)
	{
		this.Prefab = prefab;
		this.DP = dp;
		this.Range = range;
		this.Speed = speed;
		this.LifeTime = lifeTime;
	}
}