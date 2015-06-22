using UnityEngine;
using System;
using System.IO;

namespace AppUtils
{
	/// <summary>
	/// セーブデータの管理クラス
	/// </summary>
	public static class SaveDataFiler<T> where T : class
	{
		public static void Save(T saveObject, ushort slot)
		{
			byte[] serializedData;
			string path = AddSlotPath(SaveFolderPath, slot);

			// データのシリアライズ
			XmlSerializeHelper<T>.SerializeToByte(saveObject, out serializedData);

			// データをバイナリで書き込み
			using (var stream = new FileStream(path, FileMode.Create))
			{
				using (var writer = new BinaryWriter(stream))
				{
					writer.Write(serializedData);
				}
			}
		}

		public static T Load(ushort slot)
		{
			string path = AddSlotPath(SaveFolderPath, slot);
			if (new FileInfo(path).Exists == false)
			{
				// ファイルがない場合はデフォルトの指定クラスを返す
				return default(T);
			}

			byte[] readData;

			// データの読み込み
			using (var stream = new FileStream(path, FileMode.Open))
			{
				using (var reader = new BinaryReader(stream))
				{
					readData = reader.ReadBytes((int)reader.BaseStream.Length);
				}
			}

			// データのデシリアライズ
			return XmlSerializeHelper<T>.DeserializeFromByte(ref readData);
		}

		public static void Remove(ushort slot)
		{
			string path = AddSlotPath(SaveFolderPath, slot);
			FileInfo info = new FileInfo(path);
			if (info.Exists == false)
			{
				return;
			}
			info.Delete();
		}

		public static int SlotCount()
		{
			int count = 0;
			while(new FileInfo(AddSlotPath(SaveFolderPath, (ushort)count)).Exists)
			{
				++count;
			}
			return count;
		}

		static string AddSlotPath(string basePath, ushort slotNum)
		{
			return basePath + "/" + slotNum.ToString("00000") + ".data";
		}

		/// <summary>
		/// セーブデータの保存フォルダパス.
		/// </summary>
		static string SaveFolderPath
		{
			get {
				return Application.persistentDataPath;
			}
		}
	}
}