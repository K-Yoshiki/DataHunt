using UnityEngine;
using System.Collections;

public class SoundTestBgm : MonoBehaviour 
{
	public AudioClip testSound;
	
	public void PlaySound()
	{
		if (testSound != null)
			AppUtils.Sound.Instance.PlayContBGM(testSound, 1.0f);
	}

}
