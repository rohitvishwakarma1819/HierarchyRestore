using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace HierarchyRestorePlugin
{
    public class HierarchyRestorerWindow : EditorWindow
    {

        void OnGUI()
        {
            EditorGUILayout.Space();
            HierarchyRestorerSettings.restoreHierarchy = EditorGUILayout.Toggle("Restore Hierarchy", HierarchyRestorerSettings.restoreHierarchy);
            EditorGUILayout.LabelField("Restart Unity to see changes", GUI.skin.label);
            EditorGUILayout.Space();
            GUIStyle multilineStyles = EditorStyles.label;
            multilineStyles.wordWrap = true;
            EditorGUILayout.Space(5);
            DrawUILine(Color.black);
            EditorGUILayout.LabelField("Please Please Please star the repository if you find this helpful!", multilineStyles);

            if (GUILayout.Button("Go to Repository", GUI.skin.button))
            {
                Application.OpenURL("https://github.com/rohitvishwakarma1819/HierarchyRestore");
            }
            Texture coffeeBtnTexture = (Texture)AssetDatabase.LoadAssetAtPath("Assets/HierarchyRestore/Editor/Sprites/buy-me-coffee.jpeg", typeof(Texture));
             EditorGUILayout.Space(5);
             EditorGUILayout.LabelField("If you want to support my work, please 👇", multilineStyles);
             EditorGUILayout.Space(5);
            if(GUILayout.Button(coffeeBtnTexture, GUILayout.Height(100))) {
                 Application.OpenURL("https://www.buymeacoffee.com/rohitvish");
            }


        }

        public static void DrawUILine(Color color, int thickness = 2, int padding = 10)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            r.height = thickness;
            r.y += padding / 2;
            r.x -= 2;
            r.width += 6;
            EditorGUI.DrawRect(r, color);
        }
    }
}
