using UnityEngine;
using System.Collections;


/// <summary>
/// プレイヤーのパーツベース
/// </summary>
public class PlayerParts : MonoBehaviour
{
	/// <summary>
	/// パーツの名前
	/// </summary>
	public string partsName;

	/// <summary>
	/// パーツの大きさ（プレイヤーへの設置に使用）
	/// </summary>
	public float size;

	/// <summary>
	/// パーツ形状
	/// </summary>
	public PartsForm partsForm;

	/// <summary>
	/// 装備可能箇所
	/// </summary>
	public PartsCanEquip canPartsEquip;

	/// <summary>
	/// パーツの基本パラメータ
	/// </summary>
	public PartsParams[] parameters;
	
	void Awake()
	{
		partsName = string.IsNullOrEmpty(partsName) ? "No Name" : partsName;
		foreach (var partsParams in parameters)
		{
			SetData(partsParams);
		}
	}

	/// <summary>
	/// 設定データをパーツ内に入れ込む
	/// </summary>
	void SetData(PartsParams partsParams)
	{
		if (partsParams.shotPoint == null)
			return;

		BulletParams bulletParams = new BulletParams();
		bulletParams.damagePoint = partsParams.damage;
		bulletParams.range = partsParams.range;
		bulletParams.speed = partsParams.speed;
		bulletParams.lifeTime = partsParams.lifeTime;
		bulletParams.targetTag = Tag.Enemy;

		ShooterParams shooterParams = new ShooterParams();
		shooterParams.shotInterval = partsParams.shotInterval;
		shooterParams.BulletPrefab = partsParams.bulletPrefab;
		shooterParams.bulletParams = bulletParams;

		partsParams.shotPoint.SetData(shooterParams);
	}
}

/// <summary>
/// パーツの基本パラメータ
/// </summary>
[System.Serializable]
public struct PartsParams
{
	public ShooterBase shotPoint;
	public BulletBase bulletPrefab;
	public float shotInterval;
	public int damage;
	public float range;
	public float speed;
	public float lifeTime;
}

/// <summary>
/// パーツを設置出来る箇所
/// </summary>
[System.Serializable]
public struct PartsCanEquip
{
	public bool left;
	public bool right;
	public bool top;
	public bool behind;
}

/// <summary>
/// パーツの形状
/// </summary>
public enum PartsForm
{
	/// <summary>
	/// 左右対称
	/// </summary>
	Symmetry,

	/// <summary>
	/// 左右非対称
	/// </summary>
	Asymmetry,
}