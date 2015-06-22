using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AppUtils;


public class SESlider : MonoBehaviour
{
	VolumeData volumeData;

	void Start()
	{
		volumeData = UserDataControl.Data.option.volumes;
		GetComponent<Slider>().value = volumeData.se;
	}

	public void OnChangeValue(float value)
	{
		volumeData.se = value;
		SoundVolume.SE = value;
	}
}
