using UnityEngine;
using System.Collections.Generic;

public class MeshCombiner : MonoBehaviour
{
	public class MeshTarget
	{
		public Transform tf;
		public MeshFilter mf;
		public MeshRenderer mr;

		public MeshTarget(Transform tf, MeshFilter mf, MeshRenderer mr)
		{
			this.tf = tf;
			this.mf = mf;
			this.mr = mr;
		}
	}

	void Start()
	{
		CombineMesh(this.transform);
	}

	public void CombineMesh(Transform setTransform)
	{
		Dictionary<Material, List<MeshFilter>> materialSet = SearchMaterial();

		int index = 0;
		foreach (var set in materialSet)
		{
			MeshTarget target = GetNewMeshObject(string.Format("mesh_{0}", index), setTransform);
			CombineInstance[] combines = GetCombineInstances(set.Value);
			target.mf.mesh.CombineMeshes(combines);
			target.mr.sharedMaterial = set.Key;
			target.tf.gameObject.SetActive(true);
			++index;
		}
	}

	Dictionary<Material, List<MeshFilter>> SearchMaterial()
	{
		MeshRenderer[] renderers = transform.GetComponentsInChildren<MeshRenderer>();
		Dictionary<Material, List<MeshFilter>> searchResult = new Dictionary<Material, List<MeshFilter>>();
		
		foreach (var render in renderers)
		{
			Material mat = render.sharedMaterial;
			if (mat == null) continue;
			if (searchResult.ContainsKey(mat) == false)
				searchResult[mat] = new List<MeshFilter>();
			searchResult[mat].Add(render.GetComponent<MeshFilter>());
		}
		return searchResult;
	}

	CombineInstance[] GetCombineInstances(List<MeshFilter> filters)
	{
		CombineInstance[] combines = new CombineInstance[filters.Count];
		for (int i = 0; i < filters.Count; ++i)
		{
			combines[i].mesh = filters[i].mesh;
			combines[i].transform = filters[i].transform.localToWorldMatrix;
			filters[i].gameObject.SetActive(false);
		}
		return combines;
	}

	MeshTarget GetNewMeshObject(string objectName, Transform setTransform)
	{
		Transform tf = new GameObject(objectName).transform;
		MeshFilter mf = tf.gameObject.AddComponent<MeshFilter>();
		MeshRenderer mr = tf.gameObject.AddComponent<MeshRenderer>();
		tf.SetParent(setTransform);
		tf.gameObject.layer = setTransform.gameObject.layer;
		return new MeshTarget(tf, mf, mr);
	}
}