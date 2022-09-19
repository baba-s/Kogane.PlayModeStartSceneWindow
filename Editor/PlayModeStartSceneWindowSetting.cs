using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    [FilePath( "UserSettings/Kogane/PlayModeStartSceneWindowSetting.asset", FilePathAttribute.Location.ProjectFolder )]
    internal sealed class PlayModeStartSceneWindowSetting : ScriptableSingleton<PlayModeStartSceneWindowSetting>
    {
        [SerializeField] private SceneAsset m_sceneAsset;

        public SceneAsset SceneAsset
        {
            get => m_sceneAsset;
            set => m_sceneAsset = value;
        }

        public void Save()
        {
            Save( true );
        }
    }
}