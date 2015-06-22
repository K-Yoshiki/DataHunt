using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{
	public Camera targetCamera;
	Transform self;
	Transform target;

	void Awake()
	{
		self = this.transform;

		if (targetCamera == null)
		{
			targetCamera = Camera.main;
			target = targetCamera.transform;
		}
		else
		{
			target = targetCamera.transform;
		}
	}

	void Update()
	{
		self.forward = target.forward;
	}
}
