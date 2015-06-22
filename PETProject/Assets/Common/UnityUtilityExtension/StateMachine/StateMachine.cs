using UnityEngine;
using System;
using System.Collections.Generic;

public class StateMachine<KEY>
{
	private Dictionary<KEY, IState> mStates;

	public IState CurrentState { get; private set; }

	public StateMachine()
	{
		mStates = new Dictionary<KEY, IState>();
	}

	public void AddState(KEY key, IState state)
	{
		if (!mStates.ContainsKey(key))
		{
			mStates.Add(key, state);
		}
	}
	
	public void SetState(KEY key)
	{
		if (!mStates.ContainsKey(key)) return;
		if (CurrentState != null)
		{
			if (CurrentState.Equals(mStates[key])) return;
			CurrentState.Exit();
		}
		CurrentState = mStates[key];
		CurrentState.Initialize();
	}

	public void SetState(IState state)
	{
		if (CurrentState != null)
		{
			if (CurrentState.Equals(state)) return;
			CurrentState.Exit();
		}
		CurrentState = state;
		CurrentState.Initialize();
	}

	public void Update()
	{
		if (CurrentState != null)
			CurrentState.Update();
	}
	
	public int Count
	{
		get { return mStates.Count; }
	}
}