using UnityEngine;
using System.Collections;

public class LabPetCamera : MonoBehaviour
{
	public float baseSpeed;
	public Vector3 defRot;
	public RotSet rotSet;
	Vector3 targetRot;

	void Start()
	{
		this.transform.localEulerAngles = defRot;
		LabManager.Instance.OnSelectChange += SetTargetRot;
	}

	void Update()
	{
		Vector3 localRot = this.transform.localEulerAngles;
		float distX = Mathf.DeltaAngle(localRot.x, targetRot.x);
		float distY = Mathf.DeltaAngle(localRot.y, targetRot.y);
		float distZ = Mathf.DeltaAngle(localRot.z, targetRot.z);
		localRot.x += baseSpeed * distX * Time.deltaTime;
		localRot.y += baseSpeed * distY * Time.deltaTime;
		localRot.z += baseSpeed * distZ * Time.deltaTime;
		this.transform.localEulerAngles = localRot;
	}

	void SetTargetRot(PartsSetPoint? setPoint)
	{
		this.targetRot = setPoint != null ? rotSet.GetRot((PartsSetPoint)setPoint) : defRot;
	}

	[System.Serializable]
	public class RotSet
	{
		public Vector3 left;
		public Vector3 right;
		public Vector3 top;
		public Vector3 behind;
		
		public Vector3 GetRot(PartsSetPoint setPoint)
		{
			if (setPoint == PartsSetPoint.Left)
				return left;
			if (setPoint == PartsSetPoint.Right)
				return right;
			if (setPoint == PartsSetPoint.Top)
				return top;
			return behind;
		}
	}
}
