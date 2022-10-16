using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace HierarchyRestorePlugin
{
    [InitializeOnLoad]
    public class Startup
    {
        static Startup()
        {
            if (!HierarchyRestorerSettings.restoreHierarchy)
            {
                return;
            }

            EditorSceneManager.sceneOpened += OnEditorSceneManagerSceneOpened;
            EditorSceneManager.sceneClosing += OnEditorSceneClosing;
            EditorApplication.quitting += () =>
            {
                HeirarchyRestorer.SaveActiveHierarchy(SceneManager.GetActiveScene());
            };
            EditorApplication.delayCall += () =>
            {
                HeirarchyRestorer.LoadActiveHeirarchy(SceneManager.GetActiveScene());
            };
        }

        private static void OnEditorSceneManagerSceneOpened(Scene scene, OpenSceneMode mode)
        {
            HeirarchyRestorer.LoadActiveHeirarchy(scene);
        }

        private static void OnEditorSceneClosing(Scene scene, bool removingScene)
        {
            HeirarchyRestorer.SaveActiveHierarchy(scene);
        }
    }
}