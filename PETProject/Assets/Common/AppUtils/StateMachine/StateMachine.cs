using System;
using System.Collections.Generic;


namespace AppUtils
{
	public class StateItem<KEY>
	{
		public IState State { get; private set; }

		public KEY StateKey { get; private set; }

		public bool IsAutoNext { get; private set; }

		public StateItem<KEY> NextStateItem { get; private set; }

		public StateItem(IState state, KEY stateKey, bool isAutoNext = false, StateItem<KEY> nextStateItem = null)
		{
			this.State = state;
			this.StateKey = stateKey;
			this.IsAutoNext = isAutoNext;
			this.NextStateItem = nextStateItem;
		}
	}

	public class StateMachine<KEY>
	{
		private StateItem<KEY> mCurrentItem;

		public void SetState(StateItem<KEY> item)
		{
			if (mCurrentItem != null)
			{
				mCurrentItem.State.Exit();
			}
			mCurrentItem = item;
			mCurrentItem.State.Initialize();
		}

		public void Update()
		{
			if (mCurrentItem == null)
				return;

			if (mCurrentItem.State.IsEnd())
			{
				if (mCurrentItem.IsAutoNext)
				{
					SetState(mCurrentItem.NextStateItem);
				}
				return;
			}

			mCurrentItem.State.Update();
		}

		public KEY CurrentKey
		{
			get
			{
				if (mCurrentItem != null)
				{
					return mCurrentItem.StateKey;
				}
				return default(KEY);
			}
		}
	}
}