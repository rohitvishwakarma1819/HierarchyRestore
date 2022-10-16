using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;

namespace HierarchyRestorePlugin
{
    public class HeirarchyRestorer
    {
        public static void LoadActiveHeirarchy(Scene scene)
        {
            string projectPath = System.Environment.CurrentDirectory + "/HierarchyHistory";
            string filepath = projectPath + "/" + scene.path.Replace(".", "dot").Replace("/", "#") + ".txt";
            if (System.IO.Directory.Exists(projectPath))
            {
                if (System.IO.File.Exists(filepath))
                {
                    Utilities.ExpandHeirarchy(ReadHierarchyData(filepath));
                }
                else
                {
                    FileStream fs = System.IO.File.Create(filepath);
                    fs.Close();
                    Utilities.ExpandHeirarchy(ReadHierarchyData(filepath));
                }
            }
            else
            {
                System.IO.Directory.CreateDirectory(projectPath);
                FileStream fs = System.IO.File.Create(filepath);
                fs.Close();
                Utilities.ExpandHeirarchy(ReadHierarchyData(filepath));
            }
        }

        static HashSet<ulong> ReadHierarchyData(string filePath)
        {
            string heirarchy = System.IO.File.ReadAllText(filePath);
            HashSet<ulong> heirarchySet = new HashSet<ulong>();
            if (heirarchy.Length > 0)
                foreach (string objId in heirarchy.Split('|'))
                {
                    heirarchySet.Add(ulong.Parse(objId));
                }

            return heirarchySet;
        }

        public static void SaveActiveHierarchy(Scene scene)
        {
            string projectPath = System.Environment.CurrentDirectory + "/HierarchyHistory";
            string filepath = projectPath + "/" + scene.path.Replace(".", "dot").Replace("/", "#") + ".txt";
            GameObject[] gos = GameObject.FindObjectsOfType<GameObject>();
            List<GameObject> expandedGameObjects = Utilities.GetExpandedGameObjects();
            StringBuilder sb = new StringBuilder(gos.Length);
            const string separator = "|";
            foreach (GameObject expandedGameObject in expandedGameObjects)
            {
                sb.Append(Utilities.GetGameObjectUniqueID(expandedGameObject).ToString());
                sb.Append(separator);
            }

            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);
            File.WriteAllText(filepath, sb.ToString());
        }
    }
}