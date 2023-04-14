using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TS.Commons
{

    public static class Util
    {
        //用于延迟时间
        public static IEnumerator Delay(float delayTime, Action action)
        {
            yield return new WaitForSeconds(delayTime);
            action.Invoke();
        }
        
        //用于延迟多少帧
        public static IEnumerator DelayFrames(int frames, Action action)
        {
            for (int i = 0; i < frames; i++)
            {
                yield return null;
            }
            action.Invoke();
        }

        //拿到一个随机的值
        public static T GetRandomItem<T>(this T[] array)
        {
            var index = Random.Range(0, array.Length);
            return array[index];
        }

        //取一个近视的值，相差不超过0.05f
        public static bool Approximately(this float value, float target, float tolerance = 0.05f)
        {
            return Mathf.Abs(target - value) <= tolerance;
        }
    }
}