using System;
using UnityEngine;

namespace TS.Commons
{

    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField]
        private bool dontDestroyOnLoad;
        
        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance)
            {
                //销毁物体
                Destroy(gameObject);
                return;
            }

            Instance = GetComponent<T>();

            if (dontDestroyOnLoad)
            {
                //如果dontDestroyOnLoad为true，则不销毁当前物体
                DontDestroyOnLoad(gameObject);
            }
        }
    }

}