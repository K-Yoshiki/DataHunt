using UnityEngine;
using System.Collections;


/// <summary>
/// Skybox animation.
/// </summary>
public class SkyboxAnimation : MonoBehaviour
{
	[SerializeField]
	float animationSpeed = -0.1f;
	[SerializeField]
	bool onStart;

	bool isPlay;

	void Awake()
	{
		animationSpeed = Mathf.Abs(animationSpeed) * -1f;
	}

	void Start()
	{
		if (onStart)
			AnimationStart();
	}

	/// <summary>
	/// アニメーションの開始
	/// </summary>
	public void AnimationStart()
	{
		if (isPlay) return;
		StartCoroutine(AnimationUpdate());
	}
	
	/// <summary>
	/// アニメーションの停止
	/// </summary>
	public void AnimationStop()
	{
		isPlay = false;
	}
	
	IEnumerator AnimationUpdate()
	{
		isPlay = true;
		while(isPlay)
		{
			renderer.material.mainTextureOffset += new Vector2(0, animationSpeed) * Time.deltaTime;
			yield return null;
		}
	}
}
