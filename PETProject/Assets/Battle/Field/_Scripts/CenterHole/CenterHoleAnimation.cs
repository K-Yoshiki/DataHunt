using UnityEngine;
using System.Collections;


[RequireComponent(typeof(MeshRenderer))]
public class CenterHoleAnimation : MonoBehaviour
{
	[SerializeField]
	float speed = 7.5f;

	Material material;
	bool isPlay;

	void Awake()
	{
		material = GetComponent<MeshRenderer>().material;
		speed = Mathf.Abs(speed) * -1f;
	}
	
	public void StartAnim()
	{
		if (isPlay) return;
		StartCoroutine(MoveUpdate());
	}

	public void StopAnim()
	{
		isPlay = false;
	}

	IEnumerator MoveUpdate()
	{
		isPlay = true;
		while(isPlay)
		{
			material.mainTextureOffset += new Vector2(0, speed) * Time.deltaTime;
			yield return null;
		}
	}
}
