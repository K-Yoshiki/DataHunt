using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Projectフォルダ内のプレハブしかアタッチ出来ないようにするAttribute
/// </summary>
public sealed class OnlyPrefab : PropertyAttribute
{
	public Type attachType;
	
	public OnlyPrefab(Type type)
	{
		this.attachType = type;
	}
}