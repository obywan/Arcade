using System.Collections;
using UnityEngine;

public static class ExtentionMethods
{
    public static IEnumerator Tweeng(this float duration, System.Action<float> onChange, float aa, float zz, System.Action onFinish = null)
    {
        float startTime = Time.time;
        float escapedTime = startTime + duration;
        
        while (Time.time < escapedTime)
        {
            float t = (Time.time - startTime) / duration;
            onChange(Mathf.Lerp(aa, zz, t));
            yield return null;
        }
        
        onChange(zz);
        onFinish?.Invoke();
    }

    public static IEnumerator Tweeng(this float duration, System.Action<Vector3> onChange, Vector3 aa, Vector3 zz, System.Action onFinish = null)
    {
        float startTime = Time.time;
        float escapedTime = startTime + duration;
        
        while (Time.time < escapedTime)
        {
            float t = (Time.time - startTime) / duration;
            onChange(Vector3.Lerp(aa, zz, t));
            yield return null;
        }
        
        onChange(zz);
        onFinish?.Invoke();
    }
}