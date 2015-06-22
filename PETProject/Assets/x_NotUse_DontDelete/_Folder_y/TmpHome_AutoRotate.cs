using UnityEngine;
using System.Collections;


public class TmpHome_AutoRotate : MonoBehaviour
{
	public float rotate;
	void Update()
	{
		transform.localRotation *= Quaternion.Euler(Vector3.up * rotate * Time.deltaTime);
	}
}
