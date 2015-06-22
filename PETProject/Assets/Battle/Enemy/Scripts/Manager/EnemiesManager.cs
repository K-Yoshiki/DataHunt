using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// エネミーの管理クラス
/// </summary>
public class EnemiesManager
{
	/// <summary>
	/// エネミーの生成機
	/// </summary>
	EnemiesGenerator generator;
	
	/// <summary>
	/// エネミーのキャッシュ
	/// </summary>
	EnemyCache enemiesCache;
	
	/// <summary>
	/// 生成命令後かどうか
	/// </summary>
	bool isGenerated;

	/// <summary>
	/// 現在のエネミー数
	/// </summary>
	/// <value>The enemy count.</value>
	public int EnemyCount { get { return enemiesCache.Count; } }

	/// <summary>
	/// 残りのエネミー生成数
	/// </summary>
	/// <value>The remain count.</value>
	public int RemainGenerateCount { get { return generator.RemainGenerateCount; } }

	/// <summary>
	/// 全てのエネミーを撃退出来たか
	/// </summary>
	/// <returns><c>true</c> if this instance is all hunt enemy; otherwise, <c>false</c>.</returns>
	public bool IsAllHuntEnemy()
	{
		return EnemyCount <= 0 && RemainGenerateCount <= 0 && isGenerated;
	}

	/// <summary>
	/// <see cref="EnemiesManager"/>クラスの生成
	/// </summary>
	public EnemiesManager(DropTable fieldDrop, DropTable bossDrop)
	{
		generator = new GameObject("Enemies").AddComponent<EnemiesGenerator>();
		enemiesCache = new EnemyCache();

		generator.Initialize(enemiesCache, fieldDrop, bossDrop);
	}

	/// <summary>
	/// 初期化
	/// </summary>
	public void Initialize(List<EnemySpawnData> phaseData)
	{
		isGenerated = true;
		generator.GenerateDataSet(phaseData);
	}

	/// <summary>
	/// 全てのエネミーを削除する
	/// </summary>
	public void AllEnemyDelete()
	{
		generator.ForceStopGenerate();
		enemiesCache.Clear();
	}

	/// <summary>
	/// フェーズの終了時
	/// </summary>
	public void PhaseEnd()
	{
		isGenerated = false;
	}

	/// <summary>
	/// 指定レールのエネミーを取得する
	/// </summary>
	/// <param name="railNum">Rail number.</param>
	public List<EnemyBase> GetEnemiesAtRail(int railNum)
	{
		List<EnemyBase> resultList = new List<EnemyBase>();
		enemiesCache.ForEach(delegate(EnemyBase enemy) {
			if (enemy.NowRail == railNum)
				resultList.Add(enemy);
		});
		return resultList;
	}
}

/// <summary>
/// エネミーのキャッシュ管理クラス
/// </summary>
public class EnemyCache
{
	List<EnemyBase> cacheList;

	/// <summary>
	/// 現在のエネミーの数
	/// </summary>
	/// <value>The count.</value>
	public int Count { get { return cacheList.Count; } }

	/// <summary>
	/// エネミーキャッシュリストの生成
	/// </summary>
	public EnemyCache()
	{
		cacheList = new List<EnemyBase>();
	}

	/// <summary>
	/// エネミーのキャッシュ追加
	/// </summary>
	/// <param name="enemy">Enemy.</param>
	public void Add(EnemyBase enemy)
	{
		cacheList.Add(enemy);
		enemy.OnEnemyDead += Remove;
	}

	/// <summary>
	/// エネミーの取得
	/// </summary>
	/// <param name="index">Index.</param>
	public EnemyBase Get(int index)
	{
		if (-1 < index && index < Count)
			return cacheList[index];
		return null;
	}

	/// <summary>
	/// リスト内のインデックス番号の取得
	/// </summary>
	/// <returns>The index.</returns>
	/// <param name="enemy">Enemy.</param>
	public int GetIndex(EnemyBase enemy)
	{
		return cacheList.IndexOf(enemy);
	}

	/// <summary>
	/// リスト内
	/// </summary>
	/// <param name="action">Action.</param>
	public void ForEach(System.Action<EnemyBase> action)
	{
		cacheList.ForEach(action);
	}

	/// <summary>
	/// エネミーの削除
	/// </summary>
	/// <param name="enemy">Enemy.</param>
	public void Remove(EnemyBase enemy, bool isForce = false)
	{
		// 制御側からのKillでは無い場合のみ個別Remove
		if (isForce == false)
		{
			if (cacheList.Contains(enemy))
			{
				cacheList.Remove(enemy);
			}
		}
		GameObject.Destroy(enemy.gameObject);
	}
	
	/// <summary>
	/// エネミーの全消去
	/// </summary>
	public void Clear()
	{
		foreach(EnemyBase enemy in cacheList)
		{
			enemy.Kill(true);
		}
		cacheList.Clear();
	}
}