
namespace AppUtils
{
	public abstract class IState
	{
		public virtual void Initialize()
		{
		}

		public virtual void Update()
		{
		}

		public virtual void Exit()
		{
		}

		public virtual bool IsEnd()
		{
			return false;
		}
	}
}