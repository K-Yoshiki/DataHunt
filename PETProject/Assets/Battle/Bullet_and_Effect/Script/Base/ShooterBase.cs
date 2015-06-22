using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 弾の発射機ベース
/// </summary>
public abstract class ShooterBase : MonoBehaviour
{
	/// <summary>
	/// シューターのパラメータ情報
	/// </summary>
	ShooterParams parameters;

	/// <summary>
	/// 生成バレットのキャッシュ
	/// </summary>
	BulletCache bulletCache;

	/// <summary>
	/// 発射可否フラグ
	/// </summary>
	bool canShoot;


	/// <summary>
	/// 初期化
	/// </summary>
	public abstract void Initialize();

	/// <summary>
	/// 発射処理
	/// </summary>
	protected abstract void Shot();


	/// <summary>
	/// シューターのパラメータをセット
	/// </summary>
	/// <param name="data">Data.</param>
	public void SetData(ShooterParams shooterParams)
	{
		bulletCache = new BulletCache();
		this.parameters = shooterParams;
	}

	/// <summary>
	/// 指定された弾の発射
	/// </summary>
	public void OnShot()
	{
		if(canShoot)
		{
			Shot();
			canShoot = false;
		}
	}

	/// <summary>
	/// このシューターが生成した全ての弾インスタンスを破棄します
	/// </summary>
	public void ClearBullet()
	{
		bulletCache.Clear();
	}

	/// <summary>
	/// 弾のインスタンスを生成します
	/// </summary>
	/// <returns>The bullet instance.</returns>
	protected BulletBase CreateBullet()
	{
		BulletBase bullet = Instantiate(parameters.BulletPrefab, this.transform.position, this.transform.rotation) as BulletBase;
		bullet.SetData(parameters.bulletParams);
		bulletCache.Add(bullet);
		return bullet;
	}

	void Start()
	{
		canShoot = true;
		StartCoroutine(IntervalTimer());
		Initialize();
	}

	/// <summary>
	/// 発射間隔用タイマー
	/// </summary>
	IEnumerator IntervalTimer()
	{
		while(true)
		{
			if(canShoot)
			{
				// 次フレームまで待機.
				yield return 0;
				// 繰り返し処理の先頭へ.
				continue;
			}

			// 指定時間停止後, 発射可能フラグをTrueに.
			yield return new WaitForSeconds(parameters.shotInterval);
			canShoot = true;
		}
	}
}

/// <summary>
/// シューターのパラメータ
/// </summary>
[System.Serializable]
public struct ShooterParams
{
	public float shotInterval;
	public BulletParams bulletParams;
	public BulletBase BulletPrefab;
}

/// <summary>
/// 生成された弾の管理クラス
/// </summary>
public class BulletCache
{
	List<BulletBase> cacheList;

	/// <summary>
	/// フィールドに存在する弾の数
	/// </summary>
	/// <value>The alive bullets.</value>
	public int AliveBullets { get { return cacheList.Count; } }

	public BulletCache()
	{
		cacheList = new List<BulletBase>();
	}

	public void Add(BulletBase bullet)
	{
		cacheList.Add(bullet);
		bullet.OnDead = Remove;
	}

	public void Remove(BulletBase bullet)
	{
		cacheList.Remove(bullet);
	}

	public void Clear()
	{
		cacheList.ForEach(delegate(BulletBase obj) {
			GameObject.Destroy(obj.gameObject);
		});
		cacheList.Clear();
	}
}