using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TS.Commons
{
    public static class AssetManager
    {

        public static async Task<T> LoadAssetAsync<T>(string key)
        {
            return await Addressables.LoadAssetAsync<T>(key).Task;
        }

        public static IEnumerator LoadSceneAsync(string key, Action<float> onProgress)
        {
            var handle = Addressables.LoadSceneAsync(key);
            while (!handle.IsDone)
            {
                // 通过回调方法通知调用者现在的加载进度
                onProgress.Invoke(handle.PercentComplete);
                yield return null;
            }
            onProgress.Invoke(1.0f);
        }

    }
}