using UnityEngine;
using System.Collections;
using AppUtils;

public class LabBGM : MonoBehaviour
{
	[SerializeField]
	AudioClip clip;

	void Start()
	{
		Sound.Instance.PlayContBGM(clip, 1f);
	}
}
