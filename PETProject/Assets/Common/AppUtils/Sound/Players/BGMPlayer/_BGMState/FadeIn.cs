using UnityEngine;


namespace AppUtils.SoundPlayer
{
	public class FadeIn : IState
	{
		AudioData audio;
		float fadeAmount;
		float fadeTime;
		float timer;
	
		public FadeIn(AudioData audio, float fadeTime)
		{
			this.audio = audio;
			this.fadeTime = Mathf.Abs(fadeTime);
		}
	
		public override void Initialize()
		{
			timer = 0;
			audio.volume = 0f;
		
			if (SoundVolume.PlayBGMVolume <= 0f)
				timer = fadeTime;
			else
				fadeAmount = (SoundVolume.PlayBGMVolume - audio.volume) / fadeTime;
		
			audio.source.Play();
		}
	
		public override void Update()
		{
			timer += Time.deltaTime;
			audio.volume += fadeAmount * Time.deltaTime;
		}
	
		public override void Exit()
		{
			//audio.volume = SoundVolume.PlayBGMVolume;
		}
	
		public override bool IsEnd()
		{
			return timer >= fadeTime;
		}
	}
}