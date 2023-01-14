using UnityEditor;
using UnityEngine;

namespace TS.Editor
{
    public class CommonEditor : MonoBehaviour
    {
        [MenuItem("Tools/清除数据")]
        public static void ClearData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}