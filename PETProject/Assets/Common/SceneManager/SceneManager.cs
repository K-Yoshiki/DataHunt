using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AppUtils;

public enum SceneState
{
	Start,
	Title,
	Home,
	Battle,
	Lab,
	Loading,
	Credit,
}

public class SceneManager : MonoSingleton<SceneManager>
{
	public SceneState startState;
	private StateMachine<SceneState> stateMachine;
	private object belowSceneData; 

	public void SetState(SceneState state)
	{
		stateMachine.SetState(state);
	}

	public void SetSceneData(object sceneData)
	{
		belowSceneData = sceneData;
	}

	public T GetSceneData<T>() where T : class
	{
		return belowSceneData as T;
	}

	void Start()
	{
		// Fps Setting
		Application.targetFrameRate = 60;

		// Sound Volumes Setting
		UserDataControl.Load();
		VolumeData volumes = UserDataControl.Data.option.volumes;
		SoundVolume.Master = volumes.master;
		SoundVolume.BGM = volumes.bgm;
		SoundVolume.SE = volumes.se;
		SoundVolume.IsMute = volumes.isMute;

		// Initialize State machine
		stateMachine = new StateMachine<SceneState>();
		stateMachine.AddState(SceneState.Start, new StartState());
		stateMachine.AddState(SceneState.Title, new TitleState());
		stateMachine.AddState(SceneState.Home, new HomeState());
		stateMachine.AddState(SceneState.Battle, new BattleState());
		stateMachine.AddState(SceneState.Lab, new LabState());
		stateMachine.AddState(SceneState.Loading, new LoadingState());
		stateMachine.AddState(SceneState.Credit, new CreditState());

		// Dafault Set State
		stateMachine.SetState(startState);

		// Destroy Saving
		DontDestroyOnLoad(this);
	}

	void Update()
	{
		stateMachine.Update();
	}
}