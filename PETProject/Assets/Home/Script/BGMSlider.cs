using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using AppUtils;


public class BGMSlider : MonoBehaviour
{
	VolumeData volumeData;

	void Start()
	{
		volumeData = UserDataControl.Data.option.volumes;
		GetComponent<Slider>().value = volumeData.bgm;
	}

	public void OnChangeValue(float value)
	{
		volumeData.bgm = value;
		Sound.Instance.ChangeVolume(value);
	}
}
