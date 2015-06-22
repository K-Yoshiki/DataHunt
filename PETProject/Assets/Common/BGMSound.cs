using UnityEngine;
using System.Collections;


public class BGMSound : MonoBehaviour
{

	[SerializeField]
	AudioClip bgm;

	void Start()
	{
		AppUtils.Sound.Instance.PlayBGM(bgm);
	}

	void OnDestroy()
	{
		if (Application.isPlaying)
		{
			AppUtils.Sound.Instance.StopBGM(0.5f);
		}
	}
}
