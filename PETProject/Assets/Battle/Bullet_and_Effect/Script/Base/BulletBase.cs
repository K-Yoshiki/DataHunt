using UnityEngine;
using System;
using System.Collections;
using AppUtils;


/// <summary>
/// 弾ベース
/// </summary>
public abstract class BulletBase : MonoBehaviour
{
	/// <summary>
	/// 発射時のSE
	/// </summary>
	public AudioClip shotSE;

	/// <summary>
	/// Hit時のエフェクトプレハブ
	/// </summary>
	public EffectBase hitEffect;

	/// <summary>
	/// Hit時のサウンド
	/// </summary>
	public AudioClip hitSound;

	/// <summary>
	/// 弾の消滅時の動作
	/// </summary>
	public Action<BulletBase> OnDead;
	
	/// <summary>
	/// 弾の基本パラメータ情報
	/// </summary>
	protected BulletParams parameters;

	/// <summary>
	/// 初期化
	/// </summary>
	protected abstract void Initialize();

	/// <summary>
	/// 更新
	/// </summary>
	protected abstract void OnUpdate();

	/// <summary>
	/// 弾の基本パラメータをセットする
	/// </summary>
	public void SetData(BulletParams parameters)
	{
		this.parameters = parameters;
	}

	/// <summary>
	/// 弾の衝突時処理
	/// </summary>
	/// <param name="hitObj">衝突対象</param>
	/// <param name="hitPoint">衝突位置</param>
	/// <param name="hitOnce">一回のみの衝突かどうか</param>
	protected void Hit(GameObject hitObj, Vector3 hitPoint, bool hitOnce = true)
	{
		// ターゲットチェック
		if (hitObj.tag != parameters.targetTag) { return; }
		
		// CharaBaseコンポーネントのチェック
		CharaBase chara = hitObj.GetComponentInParent<CharaBase>();
		if (chara == null) { return; }

		// Hitエフェクトの生成
		if (hitEffect != null) { Instantiate(hitEffect, hitPoint, hitEffect.transform.rotation); }

		// Hitサウンドの再生
		if (hitSound != null) { Sound.Instance.PlaySE(hitSound, hitPoint); }

		// ダメージ処理
		chara.Damage(parameters.damagePoint);
		
		// 自壊処理
		if (hitOnce)
		{
			Destroy(this.gameObject);
		}
	}

	/// <summary>
	/// 発射時のSEを再生
	/// </summary>
	protected void PlayShotSE()
	{
		if (shotSE == null)
			return;
		if (parameters.targetTag == Tag.Player)
			return;
		AppUtils.Sound.Instance.PlaySE(shotSE);
	}

	void Start()
	{
		Initialize();
		PlayShotSE();
	}
	
	void Update()
	{
		OnUpdate();
	}
	
	void OnCollisionEnter(Collision other)
	{
		Hit(other.gameObject, other.contacts[0].point);
	}
	
	void OnTriggerEnter(Collider other)
	{
		Hit(other.gameObject, this.transform.position);
	}

	void OnDestroy()
	{
		OnDead(this);
	}
}

/// <summary>
/// 弾のパラメータ
/// </summary>
[System.Serializable]
public struct BulletParams
{
	public int damagePoint;
	public float range;
	public float speed;
	public float lifeTime;
	public string targetTag;
}