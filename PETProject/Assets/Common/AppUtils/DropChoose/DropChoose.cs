using UnityEngine;


public static class Rand
{
	public static int Choose(float[] percents)
	{
		float total, shotPoint;
		total = shotPoint = 0f;

		foreach (var f in percents)
		{
			total += f;
		}

		shotPoint = Random.value * total;

		for (int i = 0; i < percents.Length; ++i)
		{
			if (shotPoint < percents[i])
			{
				return i;
			}
			else
			{
				shotPoint -= percents[i];
			}
		}
		return percents.Length - 1;
	}
}
