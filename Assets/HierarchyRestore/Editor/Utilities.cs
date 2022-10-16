using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace HierarchyRestorePlugin
{
    public class Utilities
    {
        public static ulong GetGameObjectUniqueID(GameObject o)
        {
            return GlobalObjectId.GetGlobalObjectIdSlow(o).targetObjectId;
        }

        /// <summary>
        /// Get a list of all GameObjects which are expanded (aka unfolded) in the Hierarchy view.
        /// </summary>
        public static List<GameObject> GetExpandedGameObjects()
        {
            object sceneHierarchy = GetSceneHierarchy();
            if (sceneHierarchy == null) return new List<GameObject>();

            MethodInfo methodInfo = sceneHierarchy
                .GetType()
                .GetMethod("GetExpandedGameObjects");

            object result = methodInfo.Invoke(sceneHierarchy, new object[0]);

            return (List<GameObject>)result;
        }

        /// <summary>
        /// Set the target GameObject as expanded (aka unfolded) in the Hierarchy view.
        /// </summary>
        static void SetExpanded(GameObject go, bool expand)
        {
            object sceneHierarchy = GetSceneHierarchy();

            MethodInfo methodInfo = sceneHierarchy
                .GetType()
                .GetMethod("ExpandTreeViewItem", BindingFlags.NonPublic | BindingFlags.Instance);

            methodInfo.Invoke(sceneHierarchy, new object[] { go.GetInstanceID(), expand });
        }

        public static object GetSceneHierarchy()
        {
            EditorWindow window = GetHierarchyWindow();
            object sceneHierarchy = null;
            try
            {
                sceneHierarchy = typeof(EditorWindow).Assembly
                    .GetType("UnityEditor.SceneHierarchyWindow")
                    .GetProperty("sceneHierarchy")
                    .GetValue(window);
            }
#pragma warning disable 0168 // suppress value not used warning
            catch (Exception e)
            {
                // Debug.LogError(e.Message +"\n"+ e.StackTrace);
                return null;
            }
#pragma warning restore 0168 // restore value not used warning  
            
            return sceneHierarchy;
        }

        private static EditorWindow GetHierarchyWindow()
        {
            // For it to open, so that it the current focused window.
            EditorApplication.ExecuteMenuItem("Window/General/Hierarchy");
            return EditorWindow.focusedWindow;
        }

        public static void ExpandHeirarchy(HashSet<ulong> expandedGameObjectsIds)
        {
            GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
            foreach (var o in objects)
            {
                ulong objectId = GlobalObjectId.GetGlobalObjectIdSlow(o).targetObjectId;
                if (expandedGameObjectsIds.Contains(objectId))
                {
                    SetExpanded(o, true);
                }
            }
        }
    }
}