using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AppUtilsEditor
{
	public static class EditorWindowUtility
	{
		public static T ShowWindow<T>(float width, float height, bool utility = false, string title = null) where T : EditorWindow
		{
			T window = EditorWindow.GetWindow<T>(utility, title);
			window.Resize(width, height);
			window.Centering();
			window.minSize = new Vector2(width, height);
			return window;
		}
		
		public static EditorWindow Centering(this EditorWindow window)
		{
			window.position = CenterRect(window.position.width, window.position.height);
			return window;
		}

		public static EditorWindow LockResize(this EditorWindow window)
		{
			window.minSize = window.maxSize = window.GetSize();
			return window;
		}

		public static Vector2 GetSize(this EditorWindow window)
		{
			return new Vector2(window.position.width, window.position.height);
		}

		public static EditorWindow Resize(this EditorWindow window, float width, float height)
		{
			Rect rect = window.position;
			rect.width = width;
			rect.height = height;
			window.position = rect;
			return window;
		}
		
		public static EditorWindow Resize(this EditorWindow window, Vector2 size)
		{
			return window.Resize(size.x, size.y);
		}

		static Rect CenterRect(Vector2 size)
		{
			return CenterRect(size.x, size.y);
		}

		static Rect CenterRect(float width, float height)
		{
			Vector2 reso = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
			float left = (reso.x * 0.5f) - (width * 0.5f);
			float top = (reso.y * 0.5f) - (height * 0.5f);
			return new Rect(left, top, width, height);
		}
	}
}

//public class EditorWindowBase : EditorWindow
//{
//	protected static T OpenWindow(string title, Vector2 size, EditorWindowType type)
//	{
//		Vector2 reso = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
//		Rect rect = new Rect((reso.x * 0.5f) - (size.x * 0.5f), (reso.y * 0.5f) - (size.y * 0.5f), size.x, size.y);
//		
//		EditorWindow.GetWindow<T>().Close();
//		T window = EditorWindow.CreateInstance<T>();
//		window.minSize = window.maxSize = size;
//		window.title = title;
//
//		if (type == EditorWindowType.Tabable)
//			window.Show(true);
//		else if (type == EditorWindowType.Utility)
//			window.ShowUtility();
//
//		window.position = rect;
//
//		return window;
//	}
//}
//
//public enum EditorWindowType
//{
//	Tabable,
//	Utility,
//}