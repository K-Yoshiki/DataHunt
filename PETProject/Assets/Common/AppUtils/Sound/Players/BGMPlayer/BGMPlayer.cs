using UnityEngine;
using System.Collections;
using AppUtils;


namespace AppUtils.SoundPlayer
{
	public sealed class BGMPlayer
	{
		enum PlayState
		{
			Play,
			Pause,
			Stop,
			FadeIn,
			FadeOut,
			CrossFade
		}

		StateMachine<PlayState> stateMachine;
		AudioData audio;

		public BGMPlayer(AudioSource source)
		{
			audio = new AudioData(source);
			audio.volume = SoundVolume.PlayBGMVolume;
			audio.playOnAwake = false;
			audio.loop = true;

			stateMachine = new StateMachine<PlayState>();
			stateMachine.SetState(new StateItem<PlayState>(new Stop(audio), PlayState.Stop));
		}

		/// <summary>
		/// PlayBGM
		/// </summary>
		/// <param name="clip">AudioClip</param>
		/// <param name="fadetime">FadeTime</param>
		public void Play(AudioClip clip, float fadeTime, bool isContinue = false)
		{
			if (fadeTime <= 0f)
			{
				ForcedPlay(clip);
			}
			else if (stateMachine.CurrentKey != PlayState.Stop && stateMachine.CurrentKey != PlayState.Pause)
			{
				if (audio.clip != clip)
				{
					CrossFadePlay(clip, fadeTime, isContinue);
				}
			}
			else
			{
				FadePlay(clip, fadeTime);
			}
		}

		/// <summary>
		/// PauseBGM
		/// </summary>
		/// <param name="fadeTime">Fade Time.</param>
		public void Pause(float fadeTime)
		{
			if (stateMachine.CurrentKey == PlayState.Stop || stateMachine.CurrentKey == PlayState.Pause)
				return;

			if (fadeTime <= 0f)
			{
				ForcedPause();
			}
			else
			{
				FadePause(fadeTime);
			}
		}

		/// <summary>
		/// StopBGM
		/// </summary>
		/// <param name="fadetime">Fadetime</param>
		public void Stop(float fadeTime)
		{
			if (stateMachine.CurrentKey == PlayState.Stop)
				return;

			if (fadeTime <= 0f)
			{
				ForcedStop();
			}
			else
			{
				FadeStop(fadeTime);
			}
		}

		public void ChangeVolume(float value)
		{
			SoundVolume.BGM = value;
			audio.source.volume = SoundVolume.PlayBGMVolume;
		}

		/// <summary>
		/// State Update
		/// </summary>
		public void Update()
		{
			stateMachine.Update();
		}

		void ForcedPlay(AudioClip clip)
		{
			audio.clip = clip;
			var play = new Play(audio);
			stateMachine.SetState(new StateItem<PlayState>(play, PlayState.Play));
		}

		void FadePlay(AudioClip clip, float fadeTime)
		{
			audio.clip = clip;
			var fade = new FadeIn(audio, fadeTime);
			var next = new StateItem<PlayState>(new Play(audio), PlayState.Play);
			stateMachine.SetState(new StateItem<PlayState>(fade, PlayState.FadeIn, true, next));
		}

		void CrossFadePlay(AudioClip clip, float fadeTime, bool isContinue)
		{
			var fade = new CrossFade(audio, clip, fadeTime, isContinue);
			var next = new StateItem<PlayState>(new Play(audio), PlayState.Play);
			stateMachine.SetState(new StateItem<PlayState>(fade, PlayState.CrossFade, true, next));
		}

		void ForcedStop()
		{
			var stop = new Stop(audio);
			stateMachine.SetState(new StateItem<PlayState>(stop, PlayState.Stop));
		}

		void FadeStop(float fadeTime)
		{
			var fade = new FadeOut(audio, fadeTime);
			var next = new StateItem<PlayState>(new Stop(audio), PlayState.Stop);
			stateMachine.SetState(new StateItem<PlayState>(fade, PlayState.FadeOut, true, next));
		}

		void ForcedPause()
		{
			var pause = new Pause(audio);
			stateMachine.SetState(new StateItem<PlayState>(pause, PlayState.Pause));
		}

		void FadePause(float fadeTime)
		{
			var fade = new FadeOut(audio, fadeTime);
			var next = new StateItem<PlayState>(new Pause(audio), PlayState.Pause);
			stateMachine.SetState(new StateItem<PlayState>(fade, PlayState.FadeOut, true, next));
		}
	}
}