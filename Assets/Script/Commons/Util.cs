using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TS.Commons
{

    public static class Util
    {
        public static IEnumerator Delay(float delayTime, Action action)
        {
            yield return new WaitForSeconds(delayTime);
            action.Invoke();
        }

        public static T GetRandomItem<T>(this T[] array)
        {
            var index = Random.Range(0, array.Length);
            return array[index];
        }
    }
}