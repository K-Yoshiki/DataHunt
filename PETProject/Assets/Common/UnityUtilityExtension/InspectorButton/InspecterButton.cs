using UnityEngine;
using System;
using System.Collections;

public sealed class InspecterButton : PropertyAttribute
{
	public string label;
	public Action action = delegate{};
	
	public InspecterButton(string label)
	{
		this.label = label;
	}

	public InspecterButton(string label, Action action)
	{
		this.label = label;
		this.action = action;
	}
}