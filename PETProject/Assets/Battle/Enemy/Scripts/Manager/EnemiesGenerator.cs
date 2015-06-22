using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// エネミーの生成クラス
/// </summary>
public class EnemiesGenerator : MonoBehaviour
{
	/// <summary>
	/// コルーチンの停止用フラグ
	/// </summary>
	bool isStopGenerate;

	/// <summary>
	/// エネミーのキャッシュ
	/// </summary>
	EnemyCache cacheEnemies;

	/// <summary>
	/// 残りの生成エネミー数
	/// </summary>
	int remainGenerateCount;

	/// <summary>
	/// フィールドのドロップテーブル
	/// </summary>
	DropTable fieldDropTable;
	
	/// <summary>
	/// ボス専用ドロップテーブル
	/// </summary>
	DropTable bossDropTable;


	/// <summary>
	/// エネミー生成機の初期化
	/// </summary>
	/// <param name="cacheEnemies"></param>
	public void Initialize(EnemyCache cacheEnemies, DropTable fieldDrops, DropTable bossDrops)
	{
		// キャッシュリストの共有
		this.cacheEnemies = cacheEnemies;

		// ドロップテーブルのキャッシュ
		this.fieldDropTable = fieldDrops;
		this.bossDropTable = bossDrops;

		remainGenerateCount = 0;
	}

	/// <summary>
	/// 残りのエネミー生成数の取得
	/// </summary>
	/// <value>The remain generate count.</value>
	public int RemainGenerateCount
	{
		get { return remainGenerateCount; }
	}

	/// <summary>
	/// エネミー生成の強制停止
	/// </summary>
	public void ForceStopGenerate()
	{
		isStopGenerate = true;
	}

	/// <summary>
	/// 生成リストの登録
	/// </summary>
	/// <param name="phaseData">Phase data.</param>
	public void GenerateDataSet(List<EnemySpawnData> phaseData)
	{
		remainGenerateCount = phaseData.Count;
		isStopGenerate = false;

		for(int i = 0; i < phaseData.Count; ++i)
		{
			// 生成コルーチンのスタート
			StartCoroutine(Generate(phaseData[i]));
		}
	}

	/// <summary>
	/// エネミー生成コルーチン
	/// </summary>
	IEnumerator Generate(EnemySpawnData spawnData)
	{
		// Wait
		yield return new WaitForSeconds(spawnData.SpawnTime);

		// Is Stop?
		if (isStopGenerate) yield break;

		// Load
		EnemyBase prefab = spawnData.EnemyList[0].Prefab;

		// Null check
		if (prefab == null)
		{
			Debug.Log("Not found Prefab");
			yield break;
		}

		// Generate
		EnemyBase enemy = GenerateEnemy(
			prefab,
			spawnData.EnemyList[0],
			spawnData.DefRail,
			spawnData.DefAngle,
			spawnData.MoveSpeed,
			spawnData.EnemyType);

		// Regist Drop Method
		enemy.OnEnemyDead += DropChoose;

		// Add cache
		cacheEnemies.Add(enemy);
		
		// Count Down
		remainGenerateCount--;

		// Generate Sub Enemy
		if (spawnData.EnemyType == EnemyType.Medium)
		{
			StartCoroutine(SubGenerate(enemy, spawnData));
		}
	}

	/// <summary>
	/// サブエネミー生成コルーチン
	/// </summary>
	IEnumerator SubGenerate(EnemyBase leader, EnemySpawnData spawnData)
	{
		for (int i = 1; i < spawnData.EnemyList.Count; ++i)
		{
			// Wait
			yield return new WaitForSeconds(0.1f);

			// Is Stop?
			if (isStopGenerate) yield break;

			// Load
			EnemyBase prefab = spawnData.EnemyList[i].Prefab;

			// Null Check
			if (prefab == null)
			{
				Debug.Log("Not found Prefab");
				yield break;
			}
			
			// Generate
			LesserEnemy subEnemy = GenerateEnemy(
				prefab,
				spawnData.EnemyList[i],
				spawnData.DefRail,
				spawnData.DefAngle,
				spawnData.MoveSpeed,
				spawnData.EnemyType) as LesserEnemy;

			// Regist leader
			subEnemy.RegistLeader(leader);

			// Add leader in sub
			leader.AddSub(subEnemy);
		}
	}

	/// <summary>
	/// エネミー生成
	/// </summary>
	/// <returns>The enemy.</returns>
	/// <param name="prefab">Prefab.</param>
	/// <param name="setData">Set data.</param>
	/// <param name="defRail">Def rail.</param>
	/// <param name="defAngle">Def angle.</param>
	EnemyBase GenerateEnemy(EnemyBase prefab, EnemyData setData, int defRail, float defAngle, float moveSpeed, EnemyType enemyType)
	{
		// Generate
		EnemyBase enemy = GameObject.Instantiate(prefab) as EnemyBase;
		enemy.transform.SetParent(this.transform);
		enemy.transform.localPosition = Vector3.zero;
		enemy.name = enemy.name.Replace("(Clone)", "");
		enemy.SetData(setData, defRail, defAngle, moveSpeed, enemyType);

		// Return result
		return enemy;
	}

	/// <summary>
	/// ドロップの判定
	/// </summary>
	/// <param name="enemy">Enemy.</param>
	/// <param name="isForce">If set to <c>true</c> is force.</param>
	void DropChoose(EnemyBase enemy, bool isForce)
	{
		if (isForce) return;

		if (enemy.GetEnemyType() != EnemyType.Boss)
		{
			// Field Drop Table
			Drop(fieldDropTable.DropChoose(), enemy.core.transform.position);
		}
		else
		{
			// Boss Drop Table
			Drop(bossDropTable.DropChoose(), enemy.core.transform.position);
		}
	}

	void Drop(DropData data, Vector3 pos)
	{
		if (data == null) return;
		BattleRecord.Instance.AddItem(data.itemID, pos);
	}
}