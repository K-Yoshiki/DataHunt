//using UnityEngine;
//using System;
//using System.Collections;
//
//public class BGMFadeIn : IState 
//{
//	private AudioSource source;
//	private float amount;
//	private Action callback;
//	private bool isFinished;
//
//	public void SetData(float fadeTime, AudioSource bgmSource, Action callback)
//	{
//		this.amount = 1 / fadeTime;
//		this.source = bgmSource;
//		this.callback = callback;
//	}
//
//	public void Initialize()
//	{
//		isFinished = false;
//		if(source.isPlaying)
//		{
//			source.Stop();
//			source.volume = 0;
//			//source.play();
//		}
//		Debug.Log("BGMFadeIn : Initialize");
//	}
//
//	public void Update()
//	{
//		source.volume = amount * Time.deltaTime;
//		if (source.volume >= 1 && isFinished == false)
//		{
//			isFinished = true;
//			callback();
//		}
//		Debug.Log("BGMFadeIn : Update");
//	}
//
//	public void Exit()
//	{
//		Debug.Log("BGMFadeIn : Exit");
//	}
//}
