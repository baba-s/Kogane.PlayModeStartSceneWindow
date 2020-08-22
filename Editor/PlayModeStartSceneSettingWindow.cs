using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kogane.Internal
{
	internal sealed class PlayModeStartSceneSettingWindow : EditorWindow
	{
		[MenuItem( "Window/UniPlayModeStartSceneSettingWindow" )]
		private static void Open()
		{
			var title   = "UniPlayModeStartSceneSettingWindow";
			var window  = GetWindow<PlayModeStartSceneSettingWindow>( title );
			var minSize = window.minSize;

			minSize.y      = 40;
			window.minSize = minSize;
		}

		private void OnGUI()
		{
			EditorSceneManager.playModeStartScene = ( SceneAsset ) EditorGUILayout.ObjectField
			(
				label: new GUIContent( "Start Scene" ),
				obj: EditorSceneManager.playModeStartScene,
				objType: typeof( SceneAsset ),
				allowSceneObjects: false
			);

			var activeScene = SceneManager.GetActiveScene();
			var path        = activeScene.path;
			var filename    = Path.GetFileNameWithoutExtension( path );
			var isValid     = !string.IsNullOrWhiteSpace( path );

			using ( new EditorGUI.DisabledScope( !isValid ) )
			{
				if ( GUILayout.Button( "Set Current Scene: " + filename ) )
				{
					var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>( path );
					EditorSceneManager.playModeStartScene = sceneAsset;
				}
			}
		}
	}
}