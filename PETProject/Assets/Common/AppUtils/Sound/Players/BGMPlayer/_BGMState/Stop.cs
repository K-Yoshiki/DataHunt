using UnityEngine;


namespace AppUtils.SoundPlayer
{
	class Stop : IState
	{
		AudioData audio;
		
		public Stop(AudioData audio)
		{
			this.audio = audio;
		}
		
		public override void Initialize()
		{
			audio.source.Stop();
		}
	}
}