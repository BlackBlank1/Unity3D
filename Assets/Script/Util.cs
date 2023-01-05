using System;
using System.Collections;
using UnityEngine;

public static class Util
{
    public static IEnumerator Delay(float delayTime, Action action)
    {
        yield return new WaitForSeconds(delayTime);
        action.Invoke();
    }
}
