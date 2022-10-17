using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace HierarchyRestorePlugin
{
    public class HierarchySettingsEdiitor : UnityEditor.Editor
    {
        [MenuItem("Window/Hierarchy Restore")]
        public static void ShowWindow()
        {
            EditorWindow wnd = EditorWindow.GetWindow<HierarchyRestorerWindow>();
            wnd.titleContent = new GUIContent("Hierarchy Restore");
        }
    }
}

namespace HierarchyRestorePlugin
{
    public class HierarchyRestorerSettings
    {
        public static bool restoreHierarchy
        {
            get
            {
#if UNITY_EDITOR
                return EditorPrefs.GetBool("RestoreHierarchy", true);
#else
                return true;
#endif
            }
            set
            {
#if UNITY_EDITOR
                EditorPrefs.SetBool("RestoreHierarchy", value);
#endif
            }
        }
    }
}