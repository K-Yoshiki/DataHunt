//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//public class EnemyManager : MonoSingleton<EnemyManager>
//{
//	Transform enemiesParent;
//
//	EnemyBase[] enemys;
//	EnemyBase[] mediumBosses;
//	EnemyBase[] bosses;
//	public EnemyBase enemy;
//	public int createEnemyQuantity;
//	public EnemyBase mediumBoss;
//	public int createMediumBossQuantity;
//	public EnemyBase boss;
//	public int createBossQuantity;
//
//	BattlePhase phase;
//
//	int railRand;
//	int[] railIndex;
//	int maxEnemyNumber;
//
//
//	//Debug Menber
//	//public float height;
//	int killCount;
//
//	public void Initialize(PhaseType type)
//	{
//		enemiesParent = new GameObject("Enemys").transform;
//
//		killCount = 0;
//		EnemyInit(type);
//		//MediumBossGeneration();
//	}
//
//	public void EnemyInit(PhaseType type)
//	{
//		if(type == PhaseType.Start)
//			EnemyGeneration(15);
//		if(type == PhaseType.Normal)
//		{
//			EnemyGeneration(10);
//			DelayGeneration("MediumBoss");
//		}
//		if(type == PhaseType.Boss)
//		{
//			DelayGeneration("Enemy");
//			BossGeneration();
//		}
//	}
//
//	void DelayGeneration(string enemyName)
//	{
//		if(enemyName == "MediumBoss")
//			StartCoroutine(MediumGenerate(10.0f));
//		if(enemyName == "Enemy")
//			StartCoroutine(EnemyGeneration(5.0f));
//	}
//
//	IEnumerator MediumGenerate(float waitTime)
//	{
//		yield return new WaitForSeconds(waitTime);
//		StartCoroutine(MediumBossCoroutine(1.5f));
//	}
//
//	IEnumerator EnemyGeneration(float waitTime)
//	{
//		yield return new WaitForSeconds(waitTime);
//		EnemyGeneration(5);
//	}
//
//	#region EnemyGeneration
//  	public void EnemyGeneration(int enemyLength)
//	{
//		enemys = new EnemyBase[enemyLength];
//		maxEnemyNumber = enemys.Length;
//		if(enemys.Length == 0) return;
//		FieldManager stage = FieldManager.Instance;
//
//		for(int i = 0; i < enemys.Length; i++)
//		{
//			railRand = Random.Range(0,stage.RailCount());
//
//			var enemyObj = Instantiate(enemy,transform.position,Quaternion.identity) as EnemyBase;
//			enemyObj.transform.SetParent(enemiesParent.transform);
//			enemyObj.name = "ENEMY" + i.ToString();
////			enemyObj.OnEnemyDead += KillCount;
//
//			enemys[i] = enemyObj;
//			enemys[i].enemyData.DefRail = railRand;
//
//		}
//	} 
//	#endregion
//
//	#region MediumBossGeneration
//	IEnumerator MediumBossCoroutine(float waitTime)
//	{
//		mediumBosses = new EnemyBase[createMediumBossQuantity];
//		FieldManager stage = FieldManager.Instance;
//
//		for(int i = 0; i < mediumBosses.Length; i++)
//		{
//			yield return new WaitForSeconds(waitTime);
//
//			railRand = Random.Range(0,stage.RailCount());
//
//			var mObj = Instantiate(mediumBoss,transform.position,Quaternion.identity) as EnemyBase;
//			mObj.transform.SetParent(enemiesParent.transform);
//			mObj.name = "MediumBoss" + (i + 1).ToString();
////			mObj.OnEnemyDead += KillCount;
//			mediumBosses[i] = mObj;
//			mediumBosses[i].enemyData.DefRail = railRand;
//		}
//	}
//
//	public void MediumBossGeneration()
//	{
//		StartCoroutine(MediumBossCoroutine(1.0f));
//		/*mediumBosses = new EnemyBase[createMediumBossQuantity];
//		if(mediumBosses.Length == 0) return;
//		
//		StageManager stage = StageManager.Instance;
//		
//		for(int i = 0; i < mediumBosses.Length; i++)
//		{
//			railRand = Random.Range(0,stage.RailCount());
//			
//			var mediumBossObj = Instantiate(mediumBoss,transform.position,Quaternion.identity) as EnemyBase;
//			mediumBossObj.transform.SetParent(obj.transform);
//			mediumBossObj.name = "MEDIUMBOSS" + i.ToString();
//			mediumBossObj.OnEnemyDead += KillCount;
//			
//			mediumBosses[i] = mediumBossObj;
//			mediumBosses[i].setting.railIndex = railRand;
//		}*/
//	}
//	#endregion
//
//	#region BossGeneration
//	public void BossGeneration()
//	{
//		bosses = new EnemyBase[createBossQuantity];
//		if(bosses.Length == 0) return;
//
//
//		for(int i = 0; i < bosses.Length; i++)
//		{
//			var bossObj = Instantiate(boss,transform.position,Quaternion.identity) as EnemyBase;
//			bossObj.transform.SetParent(enemiesParent.transform);
//			bossObj.name = "BOSS" + i.ToString();
////			bossObj.OnEnemyDead += KillCount;
//			bosses[i] = boss;
//		}
//	}
//	#endregion
//
//	void KillCount()
//	{
//		killCount++;
//		if(killCount >= maxEnemyNumber)
//		{
//			//BattleManager.Instance.EnemyAllDown();
//		}
//	}
//
//	public Transform GetShortDistEnemy(Transform player)
//	{
//		for(int i = 0; i < enemys.Length; i++)
//		{
//			if(!enemys[i].IsDead)
//			{
//				return enemys[i].transform;
//			}
//		}
//		return null;
//	}
//}
