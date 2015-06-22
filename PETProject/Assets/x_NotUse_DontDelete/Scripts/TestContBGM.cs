using UnityEngine;
using System.Collections;
using AppUtils;

public class TestContBGM : MonoBehaviour
{
	public float fade;
	public AudioClip home;
	public AudioClip lab;

	public void PlayHome()
	{
		Sound.Instance.PlayContBGM(home, fade);
	}

	public void PlayLab()
	{
		Sound.Instance.PlayContBGM(lab, fade);
	}
}
