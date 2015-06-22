using UnityEngine;
using System;
using System.Collections;


/// <summary>
/// Battle Phase Cycle System
/// </summary>
public class BattlePhaseCycle
{
	public short NowPhase { get; private set; }
	public byte ContinueCount { get; private set; }
	private StateMachine<short> phaseState = new StateMachine<short>();

	public void Initialize(IState[] phases, byte continueCount)
	{
		NowPhase = 0;
		this.ContinueCount = continueCount;

		phaseState = new StateMachine<short>();
		for(short i = 0; i < phases.Length; ++i)
		{
			phaseState.AddState(i, phases[i]);
		}

		phaseState.SetState(NowPhase);
	}
	
	public void Update()
	{
		phaseState.Update();
	}

	public void SetPhase(IState phase)
	{
		phaseState.SetState(phase);
	}
	
	public void NextPhase()
	{
		NowPhase = (short)Mathf.Min(phaseState.Count - 1, NowPhase + 1);
		phaseState.SetState(NowPhase);
	}

	public void ContinuePhase()
	{
		--ContinueCount;
		NowPhase = (short)Mathf.Max(0, NowPhase - 1);
		phaseState.SetState(NowPhase);
	}
}