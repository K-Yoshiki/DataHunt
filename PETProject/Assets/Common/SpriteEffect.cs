using UnityEngine;
using System.Collections;

/// <summary>
/// スプライトのエフェクト用クラス
/// </summary>
[RequireComponent(typeof(SpriteRenderer), typeof(Billboard))]
public class SpriteEffect : MonoBehaviour
{
	public int frameRate;
	public bool isLoop;
	public Sprite[] sprites;

	IEnumerator Start()
	{
		int index = 0;
		float oneFrameTime = 1f / frameRate;
		SpriteRenderer spriteRender = GetComponent<SpriteRenderer>();

		while (index < sprites.Length)
		{
			spriteRender.sprite = sprites[index];
			++index;

			yield return new WaitForSeconds(oneFrameTime);

			if (index >= sprites.Length)
			{
				if (isLoop)
					index = 0;
			}
		}
	}
}
