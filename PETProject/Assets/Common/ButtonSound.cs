using UnityEngine;
using System.Collections;

public class ButtonSound : MonoBehaviour
{
	[SerializeField]
	AudioClip sound;

	public void PlaySound()
	{
		if (sound != null)
			AppUtils.Sound.Instance.PlaySE(sound, AppUtils.Sound.Instance.transform);
	}
}
