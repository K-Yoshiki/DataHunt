//using UnityEngine;
//using System.Collections;
//
//public class MediumBossChildController : MonoBehaviour {
//
//	public GameObject part;
//	public int maxPartLength;
//	GameObject[] parts;
//	public float height = -30f;
//	Vector3 pos;
//	public int railIndex;
//	public float maxInterval;
//	public float minInterval;
//	float interval;
//	bool isAllDead;
//	public GameObject effect;
//
//	IEnumerator PartsGenerateCoroutine(float direction, int rotDir, float currentRail,float waitTime)
//	{
//		parts = new GameObject[maxPartLength];
//		for(int i = 0; i < parts.Length; i++)
//		{
//
//			yield return new WaitForSeconds(waitTime);
//			var obj = Instantiate(part,transform.position,Quaternion.identity) as GameObject;
//			obj.transform.SetParent(this.transform);
//			obj.name = "PARTS" + i.ToString();
//			obj.transform.eulerAngles = Vector3.up * direction;
//			GameObject child = obj.transform.GetChild(0).gameObject;
//			PartsCoreInit(child,i,currentRail);
//			parts[i] = obj;
//
//			if(rotDir == -1)
//				parts[i].transform.SetLocalRotY(this.transform.localRotation.y + (i + 1) * (10 - (railIndex * 2)));
//			else
//				parts[i].transform.SetLocalRotY(this.transform.localRotation.y - (i + 1.6f) * (10 - (railIndex * 2)));
//		}
//	}
//
//	public void PartsGenerate(float direction, int rotDir, float currentRail)
//	{
//		interval = Random.Range(minInterval,maxInterval);
//		StartCoroutine(PartsGenerateCoroutine(direction,rotDir,currentRail,interval));
//	}
//
//	public void PartsCoreInit(GameObject child, int index, float currentRail)
//	{
//		Vector3 temp = child.transform.localPosition;
//		temp.y += height;
//		//temp.z += this.transform.position.z;
//		isAllDead = false;
//
//		child.transform.localPosition = temp;
//	}
//
//	public void SetPos(Vector3 pos)
//	{
//		this.pos = pos;
//	
//	}
//	public void SetRailIndex(int railIndex)
//	{
//		this.railIndex = railIndex;
//	}
//
//	// Update is called once per frame
//	void Update () {
//
//		if(parts == null)
//			return;
//		for(int i = 0; i < parts.Length; i++)
//		{
//			if(parts[i] == null)
//				return;
//			parts[i].GetComponent<SubCore>().MoveStart(pos);
//		}
//	}
//	
//	public void DeadParts(float waitTime)
//	{
//		ChangeCollision(false);
//		StartCoroutine(PartsKilling(waitTime));
//	}
//
//	public void ChangeCollision(bool enable)
//	{
//		foreach(var part in parts)
//		{
//			part.GetComponentInChildren<BoxCollider>().enabled = enable;
//		}
//	}
//
//	IEnumerator PartsKilling(float waitTime)
//	{
//
//		for(int i = 0; i < parts.Length; i++)
//		{
//
//			yield return new WaitForSeconds(waitTime);
//			if(parts[i].GetComponent<SubCore>().IsDead)
//				i++;
//			else
//				Instantiate(effect,parts[i].transform.GetChild(0).position,Quaternion.identity);
//			//parts[i].GetComponent<SubCore>().enemyData.DefHp = 0;
//			if(i > parts.Length - 1)
//				i = i -1;
//			if(i == parts.Length - 1)
//				isAllDead = true;
//		}
//	}
//
//	public bool IsAllDead()
//	{
//		return isAllDead;
//	}
//}
