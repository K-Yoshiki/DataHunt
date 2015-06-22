using UnityEngine;
using System;
using System.Collections;
using AppUtils;

/// <summary>
/// Enemy character base class
/// </summary>
public abstract class EnemyBase : CharaBase
{
	/// <summary>
	/// 実際に動くコアオブジェクト
	/// </summary>
	public GameObject core;

	/// <summary>
	/// エネミー撃破時のエフェクト
	/// </summary>
	public EffectBase deadEffect;

	/// <summary>
	/// エネミー撃破時のサウンド
	/// </summary>
	public AudioClip deadSound;

	/// <summary>
	/// エネミーのパラメータ
	/// </summary>
	protected EnemyParams enemyParams;

	/// <summary>
	/// シューター
	/// </summary>
	protected ShooterBase shooter;

	/// <summary>
	/// このエネミーのサブエネミーリスト
	/// </summary>
	protected EnemyCache subEnemies = new EnemyCache();

	/// <summary>
	/// エネミー撃破時のイベント
	/// </summary>
	public event Action<EnemyBase, bool> OnEnemyDead = delegate{};

	
	public void SetData(EnemyData enemyData, int defRail, float defAngle, float moveSpeed, EnemyType enemyType)
	{
		this.enemyParams = new EnemyParams(enemyData.DefHp, defRail, defAngle, moveSpeed, enemyType);
		LoadShooter(enemyData);
	}

	public void AddSub(EnemyBase enemy)
	{
		subEnemies.Add(enemy);
	}

	public int GetSubIndex(EnemyBase self)
	{
		return subEnemies.GetIndex(self);
	}

	public EnemyBase Get(int index)
	{
		return subEnemies.Get(index);
	}
	
	public override void Damage(int damage)
	{
		enemyParams.hp -= Mathf.Abs(damage);

		if(IsDead)
		{
			Kill();
		}
	}

	public override bool IsDead
	{
		get { return enemyParams.hp <= 0; }
	}

	public int NowRail
	{
		get { return enemyParams.rail; }
	}
	
	public void Kill(bool isForce = false)
	{
		KillExe(isForce);
	}

	public void Kill(float delay, bool isForce = false)
	{
		StartCoroutine(DelayCall(delay, isForce));
	}

	public EnemyType GetEnemyType()
	{
		return enemyParams.enemyType;
	}

	void LoadShooter(EnemyData enemyData)
	{
		ShooterBase shooterPrefab = enemyData.ShooterData.Prefab;

		if (shooterPrefab == null)
		{
			return;
		}

		shooter = Instantiate(shooterPrefab) as ShooterBase;

		BulletParams bulletParams = new BulletParams();
		bulletParams.damagePoint = enemyData.BulletData.DP;
		bulletParams.range = enemyData.BulletData.Range;
		bulletParams.speed = enemyData.BulletData.Speed;
		bulletParams.lifeTime = enemyData.BulletData.LifeTime;
		bulletParams.targetTag = Tag.Player;

		ShooterParams shooterParams = new ShooterParams();
		shooterParams.shotInterval = enemyData.ShooterData.Interval;
		shooterParams.BulletPrefab = enemyData.BulletData.Prefab;
		shooterParams.bulletParams = bulletParams;
		
		shooter.SetData(shooterParams);
	}

	void KillExe(bool isForce)
	{
		enemyParams.hp = 0;
		PlayDeadEffect(isForce);
		PlayDeadSound(isForce);
		SubDead(isForce);
		ClearBullets();
		OnEnemyDead(this, isForce);
	}

	void ClearBullets()
	{
		if (shooter != null)
		{
			shooter.ClearBullet();
		}
	}

	void PlayDeadEffect(bool isForce)
	{
		if (deadEffect != null && isForce == false)
		{
			EffectBase effect = Instantiate(deadEffect) as EffectBase;
			effect.transform.position = core.transform.position;
		}
	}

	void PlayDeadSound(bool isForce)
	{
		if (deadSound != null && isForce == false)
		{
			Sound.Instance.PlaySE(deadSound);
		}
	}

	void SubDead(bool isForce, float deadInterval = 0.2f)
	{
		EnemyBase deadEnemy;
		for (int i = 0; i < subEnemies.Count; ++i)
		{
			deadEnemy = subEnemies.Get(i);
			if (deadEnemy != null)
				deadEnemy.Kill(deadInterval * (i + 1), isForce);
		}
	}

	IEnumerator DelayCall(float delayTime, bool isForce)
	{
		yield return new WaitForSeconds(delayTime);
		KillExe(isForce);
	}
}

public struct EnemyParams
{
	public int DefHp { get; private set; }
	public int hp;
	public int rail;
	public float angle;
	public float speed;
	public EnemyType enemyType;


	public EnemyParams(int defHp, int rail, float angle, float speed, EnemyType enemyType)
	{
		this.DefHp = defHp;
		this.hp = defHp;
		this.rail = rail;
		this.angle = angle;
		this.speed = speed;
		this.enemyType = enemyType;
	}
}