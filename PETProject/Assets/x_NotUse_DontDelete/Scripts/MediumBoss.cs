//using UnityEngine;
//using System.Collections;
//
//public class MediumBoss : EnemyBase
//{
//	public float deadInterval;
//	public MediumBossChildController parts;
//	
//
//	void Start ()
//	{
//		transform.eulerAngles = Vector3.up * enemyParams.angle;
//		Vector3 targetPos = Vector3.back * FieldManager.Instance.GetRadius(enemyParams.rail);
//		targetPos.y = core.transform.localPosition.y;
//		
//		core.transform.localPosition = new Vector3(Random.Range(-1f, 1f), -30f, Random.Range(-1f, 1f));
//		core.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
//		
//		shooter.transform.SetParent(core.transform);
//		shooter.transform.localPosition = Vector3.zero;
//		shooter.transform.localRotation = Quaternion.identity;
//		
//		StartCoroutine(StartMove(targetPos, Mathf.Abs(enemyParams.speed)));
//
//		parts = this.transform.GetComponent<MediumBossChildController>();
//
//		//parts.PartsGenerate(direction, rotDir, targetPos.z);
//
//		OnEnemyDead += MediumBossDead;
//	}
//
//	// Start Move
//	IEnumerator StartMove(Vector3 pos, float speed)
//	{
//		// Move up
//		while(core.transform.localPosition.y < pos.y)
//		{
//			core.transform.localPosition += Vector3.up * speed * Time.deltaTime;
//			yield return null;
//		}
//		// Move to def rail 
//		while(core.transform.localPosition.z > pos.z)
//		{
//			core.transform.localPosition += Vector3.back * speed * Time.deltaTime;
//			yield return null;
//		}
//		// calibrate local position
//		core.transform.localPosition = pos;
//	}
//
//	void Update ()
//	{
//		transform.Rotate(Vector3.up * enemyParams.speed * Time.deltaTime);
//		shooter.OnShot();
//	}
//	
//	void MediumBossDead(EnemyBase enemy)
//	{
//		enemyParams.speed = 0;
//		core.gameObject.collider.enabled = false;
//		core.gameObject.SetActive(false);
//		parts.DeadParts(deadInterval);
//	}
//}
