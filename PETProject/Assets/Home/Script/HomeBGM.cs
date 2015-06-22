using UnityEngine;
using System.Collections;

public class HomeBGM : MonoBehaviour
{
	public AudioClip homeBGM;

	void Start()
	{
		AppUtils.Sound.Instance.PlayContBGM(homeBGM, 1f);
	}
}
