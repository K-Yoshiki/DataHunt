//----------------------------------------------
//            Sprite Packer
// Copyright © 2013 Cait Sith Ware.
//----------------------------------------------

using UnityEngine;
using UnityEditor;

using System.Collections.Generic;

namespace CSWTools
{
	public class SpritePackerWindow : EditorWindow
	{
		[MenuItem("CSWTools/Sprite Packer")]
		static void OnOpenFromMenu()
		{
			SpritePackerWindow window = EditorWindow.GetWindow<SpritePackerWindow>();
			window.Initialize();
		}

		private string _SpriteTextureName = "SpriteAtlas";
		private Texture2D _SpriteTarget;
		private int _Padding = 2;

		public enum SpriteAction
		{
			None,
			Add,
			Update,
			Delete,
		};

		public class SpriteElement
		{
			public Texture2D tex;
			public SpriteAction action;
			public SpriteMetaData meta;
		};

		private Dictionary<string,SpriteElement> _Sprites=new Dictionary<string,SpriteElement>();

		private Vector2 _ScrollPos = Vector2.zero;

		public void Initialize()
		{
			title = "Sprite Packer";
		}

		void UpdateSprites(bool clear)
		{
			if( clear )
			{
				_Sprites.Clear ();
			}

			Dictionary<string,SpriteElement> newSprites=new Dictionary<string,SpriteElement>();

			if( _SpriteTarget!=null )
			{
				string atlasPath = AssetDatabase.GetAssetPath( _SpriteTarget.GetInstanceID() );

				TextureImporter textureImporter = AssetImporter.GetAtPath( atlasPath ) as TextureImporter;
				
				foreach( SpriteMetaData sprite in textureImporter.spritesheet )
				{
					string name = sprite.name;

					SpriteElement spriteElement = null;

					if( _Sprites.TryGetValue( name,out spriteElement ) )
					{
						if( spriteElement.action == SpriteAction.Delete )
						{
							spriteElement.action = SpriteAction.Delete;
						}
						else
						{
							spriteElement.action = SpriteAction.None;
						}
					}
					else
					{
						spriteElement = new SpriteElement();
						spriteElement.tex = null;
						
						spriteElement.action = SpriteAction.None;
					}
					spriteElement.meta = sprite;
					
					newSprites.Add( name,spriteElement );
				}
			}

			foreach( Object obj in Selection.objects )
			{
				Texture2D tex = obj as Texture2D;

				if( tex ==null || tex == _SpriteTarget )
				{
					continue;
				}

				SpriteElement spriteElement=null;
				
				string name = tex.name;
				
				if( newSprites.TryGetValue( name,out spriteElement ) )
				{
					spriteElement.tex = tex;
					spriteElement.action = SpriteAction.Update;
				}
				else
				{
					spriteElement = new SpriteElement();
					spriteElement.tex = tex;
					spriteElement.action = SpriteAction.Add;
					
					spriteElement.meta = new SpriteMetaData();
					spriteElement.meta.name = name;
					
					newSprites.Add( name,spriteElement );
				}
			}

			_Sprites = newSprites;
		}

		void OnSelectionChange () 
		{
			Repaint();
		}

		void SetTextureSetting( TextureImporter textureImporter,bool readable )
		{
			TextureImporterSettings settings = new TextureImporterSettings();
			textureImporter.ReadTextureSettings( settings );
			
			settings.readable = readable;
			settings.textureFormat = TextureImporterFormat.ARGB32;
			settings.npotScale = TextureImporterNPOTScale.None;
			
			textureImporter.SetTextureSettings(settings);
		}

		void SetTextureSetting( string path,bool readable )
		{
			TextureImporter textureImporter = AssetImporter.GetAtPath( path ) as TextureImporter;

			SetTextureSetting( textureImporter,readable );

			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport);
		}

		void UpdateTexture()
		{
			Texture2D tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
			
			List<Texture2D> spriteTexs = new List<Texture2D>();
			List<SpriteMetaData> spritesheet = new List<SpriteMetaData>();
			List<string> deleteSprites = new List<string>();
			
			if( _SpriteTarget != null )
			{
				string path = AssetDatabase.GetAssetPath( _SpriteTarget.GetInstanceID() );
				
				SetTextureSetting( path,true );
				
				_SpriteTarget = AssetDatabase.LoadAssetAtPath( path,typeof(Texture2D) ) as Texture2D;
			}
			
			foreach( KeyValuePair<string,SpriteElement> pair in _Sprites )
			{
				string name = pair.Key;
				SpriteElement sprite = pair.Value;
				
				switch( sprite.action )
				{
				case SpriteAction.None:
				{
					if( sprite.tex == null )
					{
						int x = (int)sprite.meta.rect.x;
						int y = (int)sprite.meta.rect.y;
						int width = (int)sprite.meta.rect.width;
						int height = (int)sprite.meta.rect.height;

						sprite.tex = new Texture2D( width,height,TextureFormat.ARGB32,false );
						
						sprite.tex.SetPixels( _SpriteTarget.GetPixels(x,y,width,height) );
						sprite.tex.Apply();
					}
					
					sprite.action = SpriteAction.None;
					
					spritesheet.Add( sprite.meta );
					spriteTexs.Add( sprite.tex );
				}
					
					break;
				case SpriteAction.Add:
				{
					spritesheet.Add( sprite.meta );
					
					string path = AssetDatabase.GetAssetPath( sprite.tex.GetInstanceID() );
					
					SetTextureSetting( path,true );
					
					sprite.tex = AssetDatabase.LoadAssetAtPath( path,typeof(Texture2D) ) as Texture2D;
					sprite.action = SpriteAction.None;
					
					spriteTexs.Add( sprite.tex );
				}
					break;
				case SpriteAction.Update:
				{
					spritesheet.Add( sprite.meta );
					
					string path = AssetDatabase.GetAssetPath( sprite.tex.GetInstanceID() );
					
					SetTextureSetting( path,true );
					
					sprite.tex = AssetDatabase.LoadAssetAtPath( path,typeof(Texture2D) ) as Texture2D;
					sprite.action = SpriteAction.None;
					
					spriteTexs.Add( sprite.tex );
				}
					break;
				case SpriteAction.Delete:
				{
					deleteSprites.Add( name );
				}
					break;
				}
			}
			
			int maxSize = Mathf.Min(SystemInfo.maxTextureSize,2048);
			
			Rect[] rects = tex.PackTextures( spriteTexs.ToArray(),_Padding,maxSize );
			
			bool newTexture=(_SpriteTarget==null);
			string newPath;
			if( newTexture )
			{
				newPath = "Assets/"+_SpriteTextureName +".png";
			}
			else
			{
				newPath = AssetDatabase.GetAssetPath( _SpriteTarget.GetInstanceID() );
			}
			
			byte[] bytes = tex.EncodeToPNG();
			System.IO.File.WriteAllBytes(newPath, bytes);
			bytes = null;
			
			AssetDatabase.SaveAssets();
			if( newTexture )
			{
				AssetDatabase.Refresh();
			}
			
			TextureImporter textureImporter = AssetImporter.GetAtPath( newPath ) as TextureImporter;
			
			textureImporter.textureType = TextureImporterType.Sprite;
			textureImporter.spriteImportMode = SpriteImportMode.Multiple;
			
			for( int i=0;i<spriteTexs.Count;++i )
			{
				SpriteMetaData metaData = spritesheet[i];
				
				Rect rect = rects[i];
				rect.x *= tex.width;
				rect.y *= tex.height;
				rect.width *= tex.width;
				rect.height *= tex.height;
				
				metaData.rect = rect;
				spritesheet[i] = metaData;
			}
			
			textureImporter.spritesheet = spritesheet.ToArray();

			SetTextureSetting( textureImporter,false );
			
			AssetDatabase.ImportAsset( newPath, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport);
			
			_SpriteTarget = AssetDatabase.LoadAssetAtPath( newPath,typeof(Texture2D) ) as Texture2D;
			
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			
			foreach( string name in deleteSprites )
			{
				_Sprites.Remove( name );
			}
			UpdateSprites(true);
			
			spriteTexs.Clear();
			
			UnityEditor.EditorUtility.UnloadUnusedAssets();
		}

		void OnGUI()
		{
			GUILayout.BeginHorizontal();

			bool create = false;
			if( _SpriteTarget == null )
			{
				if( _Sprites.Count == 0 )
				{
					GUI.enabled = false;
				}
				create = GUILayout.Button ( "Create",GUILayout.Width(76f) );
				if( _Sprites.Count == 0 )
				{
					GUI.enabled = true;
				}

				_SpriteTextureName = EditorGUILayout.TextField( _SpriteTextureName );
			}
			else
			{
				create = GUILayout.Button ( "Replace",GUILayout.Width(76f) );

				GUI.enabled=false;
				EditorGUILayout.TextField( _SpriteTarget.name );
				GUI.enabled=true;
			}

			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();

			GUILayout.Label( "Atlas",GUILayout.Width(76f) );

			bool changeSpriteTarget = false;

			Texture2D spriteTarget = EditorGUILayout.ObjectField( _SpriteTarget,typeof(Texture2D),false ) as Texture2D;
			if( spriteTarget != _SpriteTarget )
			{
				if( spriteTarget!=null )
				{
					TextureImporter textureImporter = AssetImporter.GetAtPath( AssetDatabase.GetAssetPath( spriteTarget ) ) as TextureImporter;

					if( textureImporter != null && textureImporter.textureType == TextureImporterType.Sprite )
					{
						_SpriteTarget = spriteTarget;
						changeSpriteTarget = true;
					}
				}
				else
				{
					_SpriteTarget = spriteTarget;
					changeSpriteTarget = true;
				}
			}

			UpdateSprites( changeSpriteTarget );

			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Label( "Padding",GUILayout.Width(76f) );
			_Padding = Mathf.Clamp( EditorGUILayout.IntField( _Padding, GUILayout.Width(50f) ) , 0, 10 );
			GUILayout.EndHorizontal();

			if( create )
			{
				UpdateTexture ();
			}

			if (_Sprites.Count > 0)
			{
				EditorGUILayout.Separator();

				EditorGUILayout.LabelField( "Sprites" );
				GUILayout.BeginVertical();
				
				_ScrollPos = GUILayout.BeginScrollView(_ScrollPos);

				foreach( KeyValuePair<string,SpriteElement> pair in _Sprites )
				{
					string name = pair.Key;
					SpriteElement sprite = pair.Value;

					GUI.backgroundColor = Color.white;
					GUILayout.BeginHorizontal("AS TextArea", GUILayout.MinHeight(20f));

					GUILayout.Label( name,GUILayout.Height(20f) );

					switch( sprite.action )
					{
					case SpriteAction.None:
						if( GUILayout.Button( "Delete",GUILayout.Width(60f) ) )
						{
							sprite.action = SpriteAction.Delete;
						}
						break;
					case SpriteAction.Add:
						GUI.backgroundColor = Color.green;
						GUILayout.Box("Add", GUILayout.Width(60f));
						GUI.backgroundColor = Color.white;
						break;
					case SpriteAction.Update:
						GUI.backgroundColor = Color.yellow;
						GUILayout.Box("Update", GUILayout.Width(60f));
						GUI.backgroundColor = Color.white;
						break;
					case SpriteAction.Delete:
						GUI.backgroundColor = Color.red;
						if( GUILayout.Button( "Delete",GUILayout.Width(60f) ) )
						{
							sprite.action = SpriteAction.None;
						}
						GUI.backgroundColor = Color.white;
						break;
					}

					GUILayout.EndHorizontal();
					GUI.backgroundColor = Color.white;
				}

				GUILayout.EndScrollView();
				GUILayout.EndVertical();
			}
			else
			{
				EditorGUILayout.HelpBox( "Please select one or more textures in the Project View window.",MessageType.Info );
			}
		}
	}
}
