//using UnityEngine;
//using System;
//using System.Linq;
//using System.Collections;
//using System.Collections.Generic;
//
//public class BGMPlayer : MonoSingleton<BGMPlayer> 
//{
//	public List<AudioClip> BGMList;
//
//	private AudioSource bgmSource = null;
//	private Dictionary<string,AudioClip> bgmDict = null;
//
//	public void Start()
//	{
//		this.bgmSource = this.gameObject.AddComponent<AudioSource>();
//
//		this.bgmDict = new Dictionary<string,AudioClip>();
//
//		Action<Dictionary<string,AudioClip>,AudioClip> addClipDict = (dict, c) => {
//			if(!dict.ContainsKey(c.name))
//			{
//				dict.Add(c.name,c); 
//			}
//		};
//
//		this.BGMList.ForEach(bgm => addClipDict(this.bgmDict,bgm));
//	}
//
//	public AudioClip SetBGM(string name)
//	{
//		AudioClip clip = (AudioClip)Resources.Load("Sound/BGM/" + name);
//
//		if(clip == null)
//		{
//			throw new ArgumentException(name + "not found","bgmName");
//		}
//		Debug.Log("SetBGM-" + name + ":succes");
//		return clip;
//	}
//
//	public void PlayBGM(string bgmName)
//	{
//		if(!this.bgmDict.ContainsKey(bgmName))
//		{
//			this.bgmDict[bgmName] = SetBGM(name);
//		}
//
//		if(this.bgmSource.clip == this.bgmDict[bgmName]) return;
//		this.bgmSource.Stop();
//		this.bgmSource.clip = this.bgmDict[bgmName];
//		this.bgmSource.Play();
//	}
//
//	public void StopBGM()
//	{
//		this.bgmSource.Stop();
//		this.bgmSource.clip = null;
//	}
//}
//
//
//
