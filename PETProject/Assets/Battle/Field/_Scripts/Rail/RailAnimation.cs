using UnityEngine;
using System;
using System.Collections;


/// <summary>
/// レールの構築アニメーション用
/// </summary>
public class RailAnimation : MonoBehaviour
{
	public float duration;
	public float targetHeight;
	public iTween.EaseType ease = iTween.EaseType.linear;
	private bool isPlaying;
	private Action callback;
	
	/// <summary>
	/// 落下アニメーション開始時
	/// </summary>
	public void FallStart()
	{
		FallStart(delegate(){});
	}
	
	/// <summary>
	/// 落下アニメーション開始時
	/// </summary>
	public void FallStart(Action callback)
	{
		if (isPlaying) return;
		isPlaying = true;
		this.callback = callback;
		transform.localPosition = Vector3.zero;
		iTween.MoveTo(this.gameObject, CreateHash(targetHeight, "AnimStartComplete"));
	}
	
	/// <summary>
	/// 落下アニメーション終了時
	/// </summary>
	public void FallEnd()
	{
		FallEnd(delegate(){});
	}
	
	/// <summary>
	/// 落下アニメーション終了時
	/// </summary>
	public void FallEnd(Action callback)
	{
		if (isPlaying) return;
		isPlaying = true;
		this.callback = callback;
		transform.localPosition = Vector3.down * targetHeight;
		iTween.MoveTo(this.gameObject, CreateHash(0.0f, "AnimEndComplete"));
	}
	
	/// <summary>
	/// ハッシュデータの作成
	/// </summary>
	private Hashtable CreateHash(float height, string endCallbackFunc)
	{
		return iTween.Hash
		(
			"y", height,
			"islocal", true,
			"time", duration,
			"easetype", ease,
			"oncomplete", endCallbackFunc
		);
	}
	
	private void AnimStartComplete()
	{
		isPlaying = false;
		callback();
	}
	
	private void AnimEndComplete()
	{
		isPlaying = false;
		callback();
	}
	
	private void Awake()
	{
		transform.localPosition = Vector3.up * targetHeight * -1;
	}
		
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			FallStart();
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			FallEnd();
		}
	}
}
