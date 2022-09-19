using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kogane.Internal
{
    [InitializeOnLoad]
    internal sealed class PlayModeStartSceneWindow : EditorWindow
    {
        static PlayModeStartSceneWindow()
        {
            // 1 フレーム遅らせないと例外が発生する
            EditorApplication.delayCall += () =>
            {
                EditorSceneManager.playModeStartScene = PlayModeStartSceneWindowSetting.instance.SceneAsset;
            };
        }

        [MenuItem( "Window/Kogane/Play Mode Start Scene", false, 1541845126 )]
        private static void Open()
        {
            const string title = "Play Mode Start Scene";

            var window = GetWindow<PlayModeStartSceneWindow>( title );

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
            var setting       = PlayModeStartSceneWindowSetting.instance;
            var oldSceneAsset = setting.SceneAsset;

            var newSceneAsset = ( SceneAsset )EditorGUILayout.ObjectField
            (
                label: "Start Scene",
                obj: oldSceneAsset,
                objType: typeof( SceneAsset ),
                allowSceneObjects: false
            );

            if ( newSceneAsset != oldSceneAsset )
            {
                setting.SceneAsset = newSceneAsset;
                setting.Save();
                EditorSceneManager.playModeStartScene = newSceneAsset;
            }

            var activeScene = SceneManager.GetActiveScene();
            var path        = activeScene.path;
            var filename    = Path.GetFileNameWithoutExtension( path );
            var isValid     = !string.IsNullOrWhiteSpace( path );

            using ( new EditorGUI.DisabledScope( !isValid ) )
            {
                if ( GUILayout.Button( "Set Current Scene: " + filename ) )
                {
                    var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>( path );
                    setting.SceneAsset = sceneAsset;
                    setting.Save();
                    EditorSceneManager.playModeStartScene = sceneAsset;
                }
            }
        }
    }
}