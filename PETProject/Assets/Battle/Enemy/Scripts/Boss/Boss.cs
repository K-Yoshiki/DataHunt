using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// ボスクラス
/// </summary>
public class Boss : EnemyBase
{
	public bool lookAtPlayer = true;
	public float lookSpeed = 5f;
	public BossShotParams[] shotParams;
	Transform player;
	bool setUpEnd;

	void Start()
	{
		setUpEnd = false;
		player = PlayerController.Instance.Core;
		ShootersSetUp();
		StartCoroutine(StartMove());
	}

	IEnumerator StartMove()
	{
		this.transform.SetLocalPosY(15f);
		float moveAmount = this.transform.position.y / 3f;

		while (true)
		{
			if (this.transform.position.y > 0f)
			{
				this.transform.position += Vector3.down * moveAmount * Time.deltaTime;
				yield return null;
				continue;
			}
			break;
		}
		BattleUI.Instance.ShowBossHP(this);
		yield return new WaitForSeconds(0.5f);
		setUpEnd = true;
	}

	void Update()
	{
		Shot();
		LookAtPlayer();
	}

	void LookAtPlayer()
	{
		if (lookAtPlayer == false || IsDead)
			return;

		var newRotation = Quaternion.LookRotation(player.transform.position - core.transform.position).eulerAngles;
		newRotation.x = newRotation.z = 0f;
		core.transform.rotation = Quaternion.Slerp(core.transform.rotation , Quaternion.Euler(newRotation), Time.deltaTime * lookSpeed);
	}

	void Shot()
	{
		if (setUpEnd == false || IsDead)
			return;

		foreach (var shotParam in shotParams)
		{
			shotParam.shotPoint.OnShot();
		}
	}

	void ShootersSetUp()
	{
		foreach (var shotParam in shotParams)
		{
			SetData(shotParam);
		}
	}

	void SetData(BossShotParams bossShotParams)
	{
		if (bossShotParams.shotPoint == null)
			return;
		
		BulletParams bulletParams = new BulletParams();
		bulletParams.damagePoint = bossShotParams.damage;
		bulletParams.range = bossShotParams.range;
		bulletParams.speed = bossShotParams.speed;
		bulletParams.lifeTime = bossShotParams.lifeTime;
		bulletParams.targetTag = Tag.Player;
		
		ShooterParams shooterParams = new ShooterParams();
		shooterParams.shotInterval = bossShotParams.shotInterval;
		shooterParams.BulletPrefab = bossShotParams.bulletPrefab;
		shooterParams.bulletParams = bulletParams;
		
		bossShotParams.shotPoint.SetData(shooterParams);
	}

	IEnumerator DeadAnimation(float targetPosY)
	{
		float timer = 0f;
		float moveAmount = targetPosY / 3f;
		
		while (timer <= 5f)
		{
			if (this.transform.position.y < targetPosY)
			{
				this.transform.position += Vector3.up * moveAmount * Time.deltaTime;
			}
			timer += Time.deltaTime;
			yield return null;
		}
		Kill();
	}

	IEnumerator ShakeAnimation(float moveSize)
	{
		while (true)
		{
			this.core.SetLocalPosX(Mathf.Sin(Time.frameCount) * moveSize);
			yield return null;
		}
	}

	void ClearBullets()
	{
		foreach (var shotParam in shotParams)
		{
			shotParam.shotPoint.ClearBullet();
		}
	}

	public int HP
	{
		get { return enemyParams.hp; }
	}
	
	public int DefHP
	{
		get { return enemyParams.DefHp; }
	}

	public override void Damage(int damage)
	{
		if (setUpEnd == false || IsDead)
			return;

		enemyParams.hp -= Mathf.Abs(damage);

		if (IsDead)
		{
			ClearBullets();
			BattleUI.Instance.CloseBossHP();
			StartCoroutine(DeadAnimation(5f));
			StartCoroutine(ShakeAnimation(0.25f));
		}
	}
}

[Serializable]
public class BossShotParams
{
	public ShooterBase shotPoint;
	public BulletBase bulletPrefab;
	public float shotInterval;
	public int damage;
	public float range;
	public float speed;
	public float lifeTime;
}