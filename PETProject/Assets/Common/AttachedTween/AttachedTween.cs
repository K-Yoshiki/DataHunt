using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;


#region AttachedTween Class
/// <summary>
/// <para>オブジェクトアタッチタイプのiTweenラッパークラス.</para>
/// <para>Version: 3.0</para>
/// <para>Ahthor: Koji Yamamoto</para>
/// </summary>
[AddComponentMenu("iTweenUtils/AttachedTween")]
public class AttachedTween : MonoBehaviour
{
	public AttachedSetting setting;
	CacheData cache = new CacheData();
	bool playing = false;
	
	/// <summary>
	/// 実行
	/// </summary>
	public void Execute(bool isBackward = false)
	{
		if (playing)
			return;
		
		playing = true;
		TweenTarget target = setting.param.target;
		
		if (target == TweenTarget.Position)
			MoveStart(isBackward);
		else if (target == TweenTarget.Rotation)
			RotateStart(isBackward);
		else if (target == TweenTarget.Scale)
			ScaleStart(isBackward);
		else if (target == TweenTarget.Color)
			ColorStart(isBackward);
	}
	
	/// <summary>
	/// 移動
	/// </summary>
	void MoveStart(bool isBackward)
	{
		Vector3 vec = setting.param.option.vec;
		Hashtable hash = SetHashTable("position", vec, "amount", vec);
		TweenMode mode = setting.param.mode;
		
		if (mode == TweenMode.To)
		{
			if (isBackward)
			{
				hash = SetHashTable("position", cache.pos, "amount", cache.pos);
			}
			iTween.MoveTo(Target, hash);
		}
		else if (mode == TweenMode.From)
		{
			if (isBackward)
			{
				hash = SetHashTable("position", cache.pos, "amount", cache.pos);
				Target.transform.localPosition = setting.param.option.vec;
			}
			else
			{
				Target.transform.localPosition = cache.pos;
			}
			iTween.MoveFrom(Target, hash);
		}
		else if (mode == TweenMode.By)
		{
			if (isBackward)
			{
				hash = SetHashTable("position", vec * -1, "amount", vec * -1);
			}
			iTween.MoveBy(Target, hash);
		}
	}
	
	/// <summary>
	/// 回転
	/// </summary>
	void RotateStart(bool isBack)
	{
		Vector3 vec = setting.param.option.vec;
		Hashtable hash = SetHashTable("rotation", vec, "amount", vec);
		TweenMode mode = setting.param.mode;
		
		if (mode == TweenMode.To)
		{
			iTween.RotateTo(Target, hash);
		}
		else if (mode == TweenMode.From)
		{
			iTween.RotateFrom(Target, hash);
		}
		else if (mode == TweenMode.By)
		{
			iTween.RotateBy(Target, hash);
		}
	}
	
	/// <summary>
	/// 拡縮
	/// </summary>
	void ScaleStart(bool isBack)
	{
		Vector3 vec = setting.param.option.vec;
		Hashtable hash = SetHashTable("scale", vec, "amount", vec);
		TweenMode mode = setting.param.mode;
		
		if (mode == TweenMode.To)
		{
			iTween.ScaleTo(Target, hash);
		}
		else if (mode == TweenMode.From)
		{
			iTween.ScaleFrom(Target, hash);
		}
		else if (mode == TweenMode.By)
		{
			iTween.ScaleBy(Target, hash);
		}
	}
	
	/// <summary>
	/// カラー変化
	/// </summary>
	void ColorStart(bool isBack)
	{
		Color from = cache.color;
		Color to = setting.param.option.color;
		Hashtable hash = SetHashTable("from", from, "to", to, "onupdate", "ColorUpdate");
		if (isBack)
		{
			from = setting.param.option.color;
			to = cache.color;
			hash = SetHashTable("from", from, "to", to, "onupdate", "ColorUpdate");
		}
		iTween.ValueTo(Target, hash);
	}
	
	void ColorUpdate(Color value)
	{
		SetColor(value);
	}
	
	Hashtable SetHashTable(params object[] param)
	{
		Hashtable hash = new Hashtable();
		
		if (param.Length % 2 == 0)
		{
			for (int i = 0; i < param.Length; i += 2)
			{
				hash.Add(param[i], param[i + 1]);
			}
		}
		
		// Need Data Add
		hash.Add("time", setting.baseSet.time);
		hash.Add("islocal", true);
		hash.Add("delay", setting.baseSet.delay);
		hash.Add("easetype", setting.baseSet.easeType);
		hash.Add("looptype", setting.baseSet.loopType);
		hash.Add("onstart", "OnStarted");
		hash.Add("oncomplete", "OnEnded");
		
		return hash;
	}
	
	void OnStarted()
	{
		playing = true;
		setting.callbackFunc.onStartedEvent.Invoke();
	}
	
	void OnEnded()
	{
		playing = false;
		setting.callbackFunc.onEndedEvent.Invoke();
	}
	
	void Caching()
	{
		cache.color = GetColor();
		cache.pos = Target.transform.localPosition;
		cache.rot = Target.transform.localEulerAngles;
		cache.scl = Target.transform.localScale;
	}
	
	GameObject Target
	{
		get
		{
			//return (targetObject != null ? targetObject : gameObject);
			return this.gameObject;
		}
	}
	
	Color GetColor()
	{
		var colorType = setting.param.option.colorType;
		
		if (colorType == TargetColorType.MeshRender)
		{
			if (Target.GetComponent<MeshRenderer>() != null)
				return Target.GetComponent<MeshRenderer>().material.color;
		}
		else if (colorType == TargetColorType.SpriteRender)
		{
			if (Target.GetComponent<SpriteRenderer>() != null)
				return Target.GetComponent<SpriteRenderer>().material.color;
		}
		else if (colorType == TargetColorType.Text)
		{
			if (Target.GetComponent<Text>() != null)
				return Target.GetComponent<Text>().color;
		}
		else if (colorType == TargetColorType.Image)
		{
			if (Target.GetComponent<Image>() != null)
				return Target.GetComponent<Image>().color;
		}
		else if (colorType == TargetColorType.ShadowAndOutline)
		{
			if (Target.GetComponents<Shadow>().Length > 0)
				return Target.GetComponent<Shadow>().effectColor;
		}
		else if (colorType == TargetColorType.CanvasGroupAlpha)
		{
			if (Target.GetComponent<CanvasGroup>() != null)
				return new Color(1, 1, 1, Target.GetComponent<CanvasGroup>().alpha);
		}
		return Color.white;
	}
	
	void SetColor(Color color)
	{
		var colorType = setting.param.option.colorType;
		
		if (colorType == TargetColorType.MeshRender)
		{
			if (Target.GetComponent<MeshRenderer>() != null)
				Target.GetComponent<MeshRenderer>().material.color = color;
		}
		else if (colorType == TargetColorType.SpriteRender)
		{
			if (Target.GetComponent<SpriteRenderer>() != null)
				Target.GetComponent<SpriteRenderer>().color = color;
		}
		else if (colorType == TargetColorType.Text)
		{
			if (Target.GetComponent<Text>() != null)
				Target.GetComponent<Text>().color = color;
		}
		else if (colorType == TargetColorType.Image)
		{
			if (Target.GetComponent<Image>() != null)
				Target.GetComponent<Image>().color = color;
		}
		else if (colorType == TargetColorType.ShadowAndOutline)
		{
			List<Shadow> shadows = new List<Shadow>();
			shadows.AddRange(Target.GetComponents<Shadow>());
			
			if (shadows.Count > 0)
				shadows.ForEach(delegate(Shadow s) {
					s.effectColor = color;
				});
		}
		else if (colorType == TargetColorType.CanvasGroupAlpha)
		{
			if (Target.GetComponent<CanvasGroup>() != null)
				Target.GetComponent<CanvasGroup>().alpha = color.a;
		}
	}
	
	void OnEnable()
	{
		if (setting.baseSet.onStart)
			Execute();
	}

	void OnDisable()
	{
		if (setting.param.target == TweenTarget.Color)
		{
			SetColor(cache.color);
		}
	}
	
	void Awake()
	{
		Caching();
	}
	
	void OnDestroy()
	{
		iTween.Stop(Target);
	}
}
#endregion



#region **** TweenEnums ****
/// <summary>
/// iTweenで動作させる要素
/// </summary>
public enum TweenTarget
{
	Position,
	Rotation,
	Scale,
	Color,
}

/// <summary>
/// iTweenのモード要素
/// </summary>
public enum TweenMode
{
	To,
	By,
	From
}

/// <summary>
/// Color選択時の種類
/// </summary>
public enum TargetColorType
{
	MeshRender,
	SpriteRender,
	Text,
	Image,
	ShadowAndOutline,
	CanvasGroupAlpha
}
#endregion *****************



#region AttachedSetting ParameterClass
/// <summary>
/// iTweenの設定データクラス
/// </summary>
[Serializable]
public class AttachedSetting
{
	public BaseSetting baseSet;
	public ActionParam param;
	public CallbackEvent callbackFunc;
	
	public AttachedSetting()
	{
		baseSet = new BaseSetting();
		param = new ActionParam();
		callbackFunc = new CallbackEvent();
	}
}

/// <summary>
/// iTweenの基礎設定
/// </summary>
[Serializable]
public class BaseSetting
{
	public bool onStart;
	public float time;
	public float delay;
	public iTween.EaseType easeType;
	public iTween.LoopType loopType;
	
	public BaseSetting()
	{
		onStart = false;
		time = 1;
		delay = 0;
		easeType = iTween.EaseType.linear;
		loopType = iTween.LoopType.none;
	}
}

/// <summary>
/// アクション用のパラメータ
/// </summary>
[Serializable]
public class ActionParam
{
	public TweenTarget target;
	public TweenMode mode;
	public OptionData option;
	
	public ActionParam()
	{
		target = TweenTarget.Position;
		mode = TweenMode.To;
		option = new OptionData();
	}
	
	[Serializable]
	public class OptionData
	{
		public Vector3 vec;
		public Color color;
		public TargetColorType colorType;
		
		public OptionData()
		{
			vec = Vector3.zero;
			color = Color.white;
			colorType = TargetColorType.Text;
		}
	}
}

/// <summary>
/// コールバック関数の設定
/// </summary>
[Serializable]
public class CallbackEvent
{
	public UnityEvent onStartedEvent;
	public UnityEvent onEndedEvent;
}

/// <summary>
/// 初期状態を保持するキャッシュクラス
/// </summary>
[Serializable]
public class CacheData
{
	public Vector3 pos;
	public Vector3 rot;
	public Vector3 scl;
	public Color color;
	
	public CacheData()
	{
		pos = rot = scl = Vector3.zero;
		color = Color.white;
	}
}
#endregion