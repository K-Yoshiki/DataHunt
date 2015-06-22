using UnityEngine;
using System;
using System.Collections.Generic;


/// <summary>
/// Battle Data
/// </summary>
[Serializable]
public class BattleData : MonoBehaviour
{
	public bool isTutorial;
	public string StageName;
	public string LevelName;
	public AudioClip StageBGM;
	public AudioClip BossBGM;
	public DropTable FieldDropTable;
	public DropTable BossDropTable;
	public List<PhaseData> PhaseList;
	
	public BattleData()
	{
		PhaseList = PhaseList ?? new List<PhaseData>();
		BossDropTable = BossDropTable ?? new DropTable();
	}

	public BattleData(string stageName, string levelName, List<PhaseData> phaseList, DropTable fieldDrop, DropTable bossDrop)
	{
		this.StageName = stageName;
		this.LevelName = levelName;
		this.PhaseList = phaseList;
		this.FieldDropTable = fieldDrop;
		this.BossDropTable = bossDrop;
	}
}

/// <summary>
/// 各フェーズのデータ
/// </summary>
[Serializable]
public class PhaseData
{
	public byte Rails;
	public List<EnemySpawnData> EnemySpawnList;

	public PhaseData()
	{
		this.Rails = 3;
		this.EnemySpawnList = EnemySpawnList ?? new List<EnemySpawnData>();
	}

	public PhaseData(byte rails, List<EnemySpawnData> enemySpawnList)
	{
		this.Rails = rails;
		this.EnemySpawnList = enemySpawnList;
	}
}


/// <summary>
/// エネミーのスポーン情報
/// </summary>
[Serializable]
public class EnemySpawnData
{
	public float SpawnTime;
	public int DefRail;
	public float DefAngle;
	public float MoveSpeed;
	public EnemyType EnemyType;
	public List<EnemyData> EnemyList;

	public EnemySpawnData()
	{
		EnemyList = EnemyList ?? new List<EnemyData>();
	}

	public EnemySpawnData(float spawnTime, int defRail, float defAngle, float moveSpeed, EnemyType enemyType, List<EnemyData> enemyList)
	{
		this.SpawnTime = spawnTime;
		this.DefRail = defRail;
		this.DefAngle = defAngle;
		this.EnemyType = enemyType;
		this.EnemyList = enemyList;
	}
}

/// <summary>
/// ドロップテーブル
/// </summary>
[Serializable]
public class DropTable
{
	public float dropPercent;
	public List<DropData> dropDataTable;

	public DropTable()
	{
		dropPercent = 100f;
		dropDataTable = new List<DropData>();
	}

	/// <summary>
	/// ドロップの判定.
	/// ドロップしなかった場合はnullを返します.
	/// </summary>
	/// <returns>The choose.</returns>
	public DropData DropChoose()
	{
		float[] percents = new float[]{ dropPercent, 100f - dropPercent };
		if (percents[Rand.Choose(percents)] != dropPercent)
			return null;

		percents = new float[dropDataTable.Count];
		for (int i = 0; i < percents.Length; ++i)
		{
			percents[i] = dropDataTable[i].selectPercent;
		}

		return dropDataTable[Rand.Choose(percents)];
	}
}

/// <summary>
/// アイテムドロップに関する情報
/// </summary>
[Serializable]
public class DropData
{
	public int itemID;
	public float selectPercent;

	public DropData()
	{
		itemID = 0;
		selectPercent = 0f;
	}
}