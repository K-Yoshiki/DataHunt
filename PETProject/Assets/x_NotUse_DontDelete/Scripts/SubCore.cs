//using UnityEngine;
//using System.Collections;
//
//public class SubCore : EnemyBase {
//
//	public GameObject core;
//	public float verticalSpeed;
//	public float moveSpeed;
//	public GameObject effect;
//	public MeshRenderer mesh;
//
//
//	// Use this for initialization
//	void Start () {
//		OnEnemyDead += MainCoreDead;
//	}
//
//	public void MoveStart(Vector3 pos)
//	{
//
//		if(core.transform.localPosition.y < pos.y)
//			core.transform.localPosition += Vector3.up * verticalSpeed * Time.deltaTime;
//		else
//		{
//			if(core.transform.localPosition.y >= pos.y)
//			{
//				Vector3 temp = core.transform.localPosition;
//				temp.y = pos.y;
//				core.transform.localPosition = temp;
//			}
//			SideMove(pos);
//		}
//	}
//
//	void SideMove(Vector3 pos)
//	{
//		if(core.transform.localPosition.z > pos.z)
//			core.transform.localPosition += Vector3.back * moveSpeed * Time.deltaTime;
//		else
//		{
//			Vector3 temp = core.transform.localPosition;
//			temp.z = pos.z;
//			core.transform.localPosition = temp;
//		}
//	}
//
//
//	public void MainCoreDead(EnemyBase enemy)
//	{
//		//Instantiate(effect,core.transform.position,Quaternion.identity);
//		mesh.enabled = false;
//		//this.gameObject.SetActive(false);
//	}
//
//	void SubCoreDead()
//	{
//		mesh.enabled = false;
//		//this.gameObject.SetActive(false);
//	}
//
//}
