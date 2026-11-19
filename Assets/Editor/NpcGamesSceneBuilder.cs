#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace NPCGames
{
    public static class NpcGamesSceneBuilder
    {
        private const string SceneFolder = "Assets/Scenes";
        private const string ScenePath = SceneFolder + "/NPC-GAMES-First-Playable.unity";

        [MenuItem("NPC-GAMES/Build First Playable Scene")]
        public static void BuildFirstPlayableScene()
        {
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                return;

            Directory.CreateDirectory(SceneFolder);

            var scene = EditorSceneManager.NewScene(
                NewSceneSetup.EmptyScene,
                NewSceneMode.Single
            );

            var bootstrap = new GameObject("NPC-GAMES Bootstrap");
            bootstrap.AddComponent<NpcGames3DBootstrap>();

            EditorSceneManager.SaveScene(scene, ScenePath);

            EditorBuildSettings.scenes = new[]
            {
                new EditorBuildSettingsScene(ScenePath, true)
            };

            Selection.activeGameObject = bootstrap;
            Debug.Log("NPC-GAMES first playable scene created at " + ScenePath);
        }
    }
}
#endif
