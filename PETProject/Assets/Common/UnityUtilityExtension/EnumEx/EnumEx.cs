using System;


public static class EnumEx
{
	/// <summary>
	/// 指定されたビットが含まれているかどうかを返します
	/// </summary>
	public static bool HasFlag(this Enum self, Enum flag)
	{
		if (self.GetType() != flag.GetType())
		{
			return false;
		}

		int selfIndex = Convert.ToInt32(self);
		int flagIndex = Convert.ToInt32(flag);
		return (selfIndex & flagIndex) == flagIndex;
	}
}
