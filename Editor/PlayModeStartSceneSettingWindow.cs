using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kogane.Internal
{
    internal sealed class PlayModeStartSceneSettingWindow : EditorWindow
    {
        [MenuItem( "Window/Kogane/Play Mode Start Scene", false, 1541845126 )]
        private static void Open()
        {
            const string title = "Play Mode Start Scene";

            var window = GetWindow<PlayModeStartSceneSettingWindow>( title );

            const int height = 40;

            var position = window.position;
            position.height = height;
            window.position = position;

            var minSize = window.minSize;
            minSize.y      = height;
            window.minSize = minSize;

            var maxSize = window.maxSize;
            maxSize.y      = height;
            window.maxSize = maxSize;
        }

        private void OnGUI()
        {
            EditorSceneManager.playModeStartScene = ( SceneAsset )EditorGUILayout.ObjectField
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