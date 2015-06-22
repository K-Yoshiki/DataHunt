using UnityEngine;


namespace AppUtils.SoundPlayer
{
	class FadeOut : IState
	{
		AudioData audio;
		float fadeAmount;
		float fadeTime;
		float timer;
		
		public FadeOut(AudioData audio, float fadeTime)
		{
			this.audio = audio;
			this.fadeTime = Mathf.Abs(fadeTime);
		}
		
		public override void Initialize()
		{
			timer = 0;
			
			if (SoundVolume.PlayBGMVolume <= 0f)
			{
				timer = fadeTime;
			}
			else
			{
				fadeAmount = (audio.volume / fadeTime) * -1f;
			}
		}
		
		public override void Update()
		{
			timer += Time.deltaTime;
			audio.volume += fadeAmount * Time.deltaTime;
		}
		
		public override void Exit()
		{
			//audio.volume = 0f;
		}
		
		public override bool IsEnd()
		{
			return timer >= fadeTime;
		}
	}
}