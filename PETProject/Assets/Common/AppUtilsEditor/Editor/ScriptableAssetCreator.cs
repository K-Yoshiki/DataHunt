using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;


public class ScriptableAssetCreator
{
	[MenuItem("Assets/ScriptableObject To Asset")]
	static void CreateAsset()
	{
		Object[] objects = Selection.objects;
		for (int i = 0; i < objects.Length; ++i)
		{
			Object selectedObject = objects[i];

			// Get path
			string path = SavePath(selectedObject);
			
			// Create instance
			ScriptableObject obj = ScriptableObject.CreateInstance(selectedObject.name);
			AssetDatabase.CreateAsset(obj, path);
			AssetDatabase.SaveAssets();
		}
	}

	public T CreateAsset<T>(string path) where T : ScriptableObject
	{
		var obj = ScriptableObject.CreateInstance<T>();
		AssetDatabase.CreateAsset(obj, path);
		AssetDatabase.SaveAssets();
		return obj;
	}
	
	static string SavePath(Object selectedObject)
	{
		string objectName = selectedObject.name;
		string dirPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(selectedObject));
		string path = string.Format("{0}/{1}.asset", dirPath, objectName);

		if (File.Exists(path))
		{
			int i = 1;
			while (true)
			{
				path = string.Format("{0}/{1}({2}).asset", dirPath, objectName, i);
				if (File.Exists(path) == false) break;
				++i;
			}
		}
		return path;
	}
}