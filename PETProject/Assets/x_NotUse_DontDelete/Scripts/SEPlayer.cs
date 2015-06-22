//using UnityEngine;
//using System;
//using System.Linq;
//using System.Collections;
//using System.Collections.Generic;
///*SoundManager
//音量管理（volume,mute）を管理
//BGMとSEは仕様が違うので、SEとBGMで別クラスを用意
//各種ボリュームを設定できる他にMasterVolumeも設定できるようにする。
//
//*/
//
////音管理クラス
//public class SEPlayer : MonoSingleton<SEPlayer> 
//{
//	public List<AudioClip> SEList = new List<AudioClip>();
//	public int MaxSE;
//	private List<AudioSource> seSources = null;
//	private Dictionary<string,AudioClip> seDict = null;
//	
//	public void Start()
//	{
//		//create audio sources
//		this.seSources = new List<AudioSource>();
//		
//		//create clip dictionaries
//		this.seDict = new Dictionary<string, AudioClip>();
//		
//		Action<Dictionary<string,AudioClip>,AudioClip> addClipDict = (dict, c) => {
//			if(!dict.ContainsKey(c.name))
//			{
//				dict.Add(c.name,c); 
//			}
//		};
//		this.SEList.ForEach(se => addClipDict(this.seDict,se));
//	}
//	/// <summary>
//	/// SEのセット
//	/// </summary>
//	/// <returns>The S.</returns>
//	/// <param name="name">Name.</param>
//	public AudioClip SetSE(string name)
//	{
//		AudioClip clip = (AudioClip)Resources.Load("Sound/SE/" + name);
//
//		if(clip == null)
//		{
//			throw new ArgumentException(name + "not found","seName");
//		}
//		Debug.Log("SetSE-" + name + ": success");
//		return clip;
//	}
//
//
//	/// <summary>
//	/// SEのプレイ
//	/// </summary>
//	/// <param name="seName">Se name.</param>
//	public void PlaySE(string seName)
//	{
//		if(!this.seDict.ContainsKey(seName))
//		{
//			this.seDict[seName] = SetSE (seName);
//		}
//
//		//AudioClip clip = (AudioClip)Resources.Load(seName);
//
//		AudioSource source = this.seSources.FirstOrDefault(s => !s.isPlaying);
//		if(source == null)
//		{
//			if(this.seSources.Count >= this.MaxSE)
//			{
//				Debug.Log("SE AudioSource is full");
//				return;
//			}
//			
//			source = this.gameObject.AddComponent<AudioSource>();
//			this.seSources.Add(source);
//		}
//
//		source.clip = this.seDict[seName];
//		source.Play();
//	}
//
//	/// <summary>
//	/// SEの停止
//	/// </summary>
//	public void StopSE()
//	{
//		this.seSources.ForEach(s => s.Stop());
//	}
//
//	public void SEDelete(AudioClip clip)
//	{
//		Resources.UnloadAsset(clip);
//		Debug.Log("Delete");
//	}
//}