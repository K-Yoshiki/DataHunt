using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using AppUtils;


namespace SaveFilerTest
{
	/// <summary>
	/// セーブファイル実行のテスト
	/// </summary>
	public class SaveFilerTest : MonoBehaviour
	{
		public Text dataName;
		public Text dataValue;
		public InputField inputID;
		public InputField inputScore;
		List<Data> dataList;

		void Awake()
		{
			dataList = new List<Data>();
		}

		void Start()
		{
			Load();
		}

		public void Save()
		{
			for (int i = 0; i < dataList.Count; ++i)
			{
				SaveData(dataList[i], i);
			}
		}

		void SaveData(Data data, int slot)
		{
			SaveDataFiler<Data>.Save(data, (ushort)slot);
		}

		public void Load()
		{
			dataName.text = "";
			dataValue.text = "";
			
			int count = SaveDataFiler<Data>.SlotCount();
			if (count <= 0) return;

			dataList.Clear();
			for (int i = 0; i < count; ++i)
			{
				dataList.Add(SaveDataFiler<Data>.Load((ushort)i));
			}
			TextUpdate();
		}

		public void Reset()
		{
			dataName.text = "";
			dataValue.text = "";
			for (int i = 0; i < dataList.Count; ++i)
			{
				RemoveScore(i);
			}
		}

		void RemoveScore(int slot)
		{
			SaveDataFiler<Data>.Remove((ushort)slot);
		}

		public void AddScore()
		{
			int id = int.Parse(inputID.text);
			int score = int.Parse(inputScore.text);
			if (dataList.Count > id)
			{
				dataList[id].score = score;
			}
			else 
			{
				dataList.Add(new Data(){ score = score });
			}
			TextUpdate();
		}

		// 表示テキスト更新
		void TextUpdate()
		{
			string names = "";
			string values = "";
			for (int i = 0; i < dataList.Count; ++i)
			{
				names += string.Format("Score[{0}]\n", i);
				values += string.Format("{0}\n", dataList[i].score);
			}
			dataName.text = names;
			dataValue.text = values;
		}
	}

	/// <summary>
	/// セーブファイル実行用のテストデータクラス
	/// </summary>
	[Serializable]
	public class Data
	{
		public int score;
	}
}