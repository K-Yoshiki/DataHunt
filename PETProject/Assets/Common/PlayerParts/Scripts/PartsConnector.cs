using UnityEngine;
using System.Collections;


/// <summary>
/// プレイヤーパーツの着脱管理
/// </summary>
public class PartsConnector : MonoBehaviour
{
	[SerializeField]
	PartsPositions partsPos;
	EquipParts _equipParts;
	EquipParts equipParts
	{
		get {
			if (_equipParts == null)
				_equipParts = new EquipParts();
			return _equipParts;
		}
	}

	/// <summary>
	/// PETの左側面に配置
	/// </summary>
	/// <param name="partsPrefab">Parts prefab.</param>
	public PlayerParts SetLeft(PlayerParts partsPrefab)
	{
		DeleteEquip(equipParts.left);
		if (partsPrefab == null) return null;
		Vector3 posOffset = Vector3.left * partsPrefab.size * 0.5f;
		return equipParts.left = SetParts(partsPrefab, partsPos.left, posOffset, Vector3.zero);
	}
	
	/// <summary>
	/// PETの右側面に配置
	/// </summary>
	/// <param name="partsPrefab">Parts prefab.</param>
	public PlayerParts SetRight(PlayerParts partsPrefab)
	{
		DeleteEquip(equipParts.right);
		if (partsPrefab == null) return null;
		Vector3 posOffset = Vector3.right * partsPrefab.size * 0.5f;
		Vector3 localRot = Vector3.zero;
		if (partsPrefab.partsForm == PartsForm.Asymmetry)
		{
			localRot.z = 180f;
		}
		return equipParts.right = SetParts(partsPrefab, partsPos.right, posOffset, localRot);
	}

	/// <summary>
	/// PETの上側面に配置
	/// </summary>
	/// <param name="partsPrefab">Parts prefab.</param>
	public PlayerParts SetTop(PlayerParts partsPrefab)
	{
		DeleteEquip(equipParts.top);
		if (partsPrefab == null) return null;
		Vector3 posOffset = Vector3.up * partsPrefab.size * 0.5f;
		Vector3 localRot = Vector3.zero;
		if (partsPrefab.partsForm == PartsForm.Asymmetry)
		{
			localRot.z = -90f;
		}
		return equipParts.top = SetParts(partsPrefab, partsPos.top, posOffset, localRot);
	}

	/// <summary>
	/// PETの背面に配置
	/// </summary>
	/// <param name="partsPrefab">Parts prefab.</param>
	public PlayerParts SetBehind(PlayerParts partsPrefab)
	{
		DeleteEquip(equipParts.behind);
		if (partsPrefab == null) return null;
		Vector3 posOffset = Vector3.back * partsPrefab.size * 0.5f;
		Vector3 localRot = new Vector3(-90f, 0f, 0f);
		if (partsPrefab.partsForm == PartsForm.Asymmetry)
		{
			localRot.y += -90f;
		}
		return equipParts.behind = SetParts(partsPrefab, partsPos.behind, posOffset, localRot);
	}

	/// <summary>
	/// パーツの配置
	/// </summary>
	PlayerParts SetParts(PlayerParts partsPrefab, Transform parent, Vector3 posOffset, Vector3 localRot)
	{
		if (partsPrefab == null) return null;

		// 配置
		PlayerParts parts = Instantiate(partsPrefab) as PlayerParts;
		parts.transform.SetParent(parent);
		parts.transform.localPosition = Vector3.zero;
		parts.transform.localPosition += posOffset;
		parts.transform.localEulerAngles = localRot;
		SetLayer("Player", parts.transform);
		return parts;
	}

	void SetLayer(string layerName, Transform obj)
	{
		obj.gameObject.layer = LayerMask.NameToLayer(layerName);
		for (int i = 0; i < obj.childCount; ++i)
		{
			SetLayer(layerName, obj.GetChild(i));
		}
	}

	void DeleteEquip(PlayerParts equip)
	{
		if (equip == null)
			return;
		Destroy(equip.gameObject);
	}
}

[System.Serializable]
public class PartsPositions
{
	public Transform left;
	public Transform right;
	public Transform top;
	public Transform behind;
}

public class EquipParts
{
	public PlayerParts left;
	public PlayerParts right;
	public PlayerParts top;
	public PlayerParts behind;
}