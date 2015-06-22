using UnityEngine;
using System;
using System.Collections;

public class LabPartsButton : MonoBehaviour
{
	LabPartsData partsData;
	Action<LabPartsData> OnClick;

	public void SetData(LabPartsData partsData, Action<LabPartsData> onClick)
	{
		this.partsData = partsData;
		this.OnClick = onClick;
		PartsInstantiate(partsData.partsPrefab);
	}

	public void PressButton()
	{
		if (OnClick != null)
			OnClick(partsData);
	}

	void PartsInstantiate(PlayerParts partsPrefab)
	{
		if (partsPrefab == null)
			return;

		PlayerParts parts = Instantiate(partsPrefab) as PlayerParts;
		parts.transform.SetParent(this.transform);
		parts.transform.localPosition = new Vector3(0, 0, -50f);
		parts.transform.localRotation = Quaternion.Euler(0, -128f, 0);
		parts.transform.localScale = Vector3.one * 100f;
		SetLayer("UI", parts.transform);
	}

	void SetLayer(string layerName, Transform obj)
	{
		obj.gameObject.layer = LayerMask.NameToLayer(layerName);
		for (int i = 0; i < obj.childCount; ++i)
		{
			SetLayer(layerName, obj.GetChild(i));
		}
	}
}
