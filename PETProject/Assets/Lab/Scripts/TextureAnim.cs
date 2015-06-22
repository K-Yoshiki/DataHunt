using UnityEngine;
using System.Collections;


public class TextureAnim : MonoBehaviour
{
	public float rollTime;
	public Texture[] textures;

	IEnumerator Start()
	{
		int index = 0;
		float oneTime = rollTime / Mathf.Max(textures.Length, 1f);
		Material mat = this.GetComponent<MeshRenderer>().material;

		SetMaterial(mat, textures[index]);

		while(true)
		{
			index = Mathf.FloorToInt(Mathf.Repeat(index + 1f, textures.Length - 1));
			yield return new WaitForSeconds(oneTime);
			SetMaterial(mat, textures[index]);
		}
	}

	void SetMaterial(Material mat, Texture tex)
	{
		mat.SetTexture("_MainTex", tex);
	}
}
