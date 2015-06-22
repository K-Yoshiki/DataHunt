using UnityEngine;


namespace AppUtils.SoundPlayer
{
	class CrossFade : IState
	{
		AudioData audio;
		AudioSource fadeOutSource;
		AudioClip changeClip;
		float fadeInAmount;
		float fadeOutAmount;
		float fadeTime;
		float timer;
		bool isContinue;
		
		public CrossFade(AudioData audio, AudioClip changeClip, float fadeTime, bool isContinue)
		{
			this.audio = audio;
			this.changeClip = changeClip;
			this.fadeTime = fadeTime;
			this.isContinue = isContinue;
		}
		
		public override void Initialize()
		{
			fadeOutSource = audio.source;
			audio.source = fadeOutSource.gameObject.AddComponent<AudioSource>();
			audio.clip = changeClip;
			if (isContinue)
				audio.source.time = fadeOutSource.time;
			audio.loop = true;

			if (SoundVolume.PlayBGMVolume <= 0f)
			{
				timer = fadeTime;
				return;
			}
			
			timer = 0;
			fadeInAmount = SoundVolume.PlayBGMVolume / fadeTime;
			fadeOutAmount = (audio.volume / fadeTime) * -1f;
			audio.volume = 0f;

			audio.source.Play();
		}
		
		public override void Update()
		{
			timer += Time.deltaTime;
			fadeOutSource.volume += fadeOutAmount * Time.deltaTime;
			audio.volume += fadeInAmount * Time.deltaTime;
		}
		
		public override void Exit()
		{
			fadeOutSource.Stop();
			GameObject.DestroyImmediate(fadeOutSource);
		}
		
		public override bool IsEnd()
		{
			return timer >= fadeTime;
		}
	}
}