using UnityEngine;
using System.Collections;


/// <summary>
/// 通常弾
/// </summary>
public class NormalBullet : BulletBase
{
	/// <summary>
	/// 発射地点
	/// </summary>
	Vector3 startPoint;

	/// <summary>
	/// 初期化
	/// </summary>
	protected override void Initialize()
	{
		startPoint = transform.position;
	}

	/// <summary>
	/// 更新
	/// </summary>
	protected override void OnUpdate()
	{
		Move();
		DeadCheck();
	}

	/// <summary>
	/// 移動
	/// </summary>
	void Move()
	{
		transform.position += transform.forward * parameters.speed * Time.deltaTime;
	}

	/// <summary>
	/// 自然消滅判定
	/// </summary>
	void DeadCheck()
	{
		// 指定射程を超えたら消滅
		if (Vector3.Distance(startPoint, transform.position) > parameters.range)
		{
			Destroy(this.gameObject);
		}
	}
}