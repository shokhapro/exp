using System.Collections;
using UnityEngine;
using UnityEngine.Events;

internal static class CoroutineMethods
{
    public static Coroutine DelayedAction(this MonoBehaviour script, UnityAction action, float delay = -1)
    {
        return script.StartCoroutine(DelayedActionCoroutine(delay, action));
    }
    private static IEnumerator DelayedActionCoroutine(float delay, UnityAction action)
    {
        if (delay > 0) yield return new WaitForSeconds(delay);
        else yield return new WaitForEndOfFrame();

        action.Invoke();
    }

    public delegate void ActionFloat(float value);
    public delegate float FunctionFloat(float value);
    public static Coroutine FloatFade(this MonoBehaviour script, float fromValue, float toValue, float duration, ActionFloat update, UnityAction end = null)
    {
        FunctionFloat f = (t) =>
        {
            return t;
        };

        return script.StartCoroutine(FloatFadeCoroutine(fromValue, toValue, duration, f, update, end));
    }
    public static Coroutine FloatFadeIn(this MonoBehaviour script, float fromValue, float toValue, float duration, ActionFloat update, UnityAction end = null)
    {
        FunctionFloat f = (t) =>
        {
            return Mathf.Pow(t, 2f);
        };

        return script.StartCoroutine(FloatFadeCoroutine(fromValue, toValue, duration, f, update, end));
    }
    public static Coroutine FloatFadeOut(this MonoBehaviour script, float fromValue, float toValue, float duration, ActionFloat update, UnityAction end = null)
    {
        FunctionFloat f = (t) =>
        {
            return 1f - Mathf.Pow(1f - t, 2f);
        };

        return script.StartCoroutine(FloatFadeCoroutine(fromValue, toValue, duration, f, update, end));
    }
    public static Coroutine FloatFadeInOut(this MonoBehaviour script, float fromValue, float toValue, float duration, ActionFloat update, UnityAction end = null)
    {
        FunctionFloat f = (t) =>
        {
            float x = t * 2f - 1f;
            float y = Mathf.Sign(x) * Mathf.Pow(Mathf.Abs(x), 0.5f);
            return (y + 1f) * 0.5f;
        };

        return script.StartCoroutine(FloatFadeCoroutine(fromValue, toValue, duration, f, update, end));
    }
    private static IEnumerator FloatFadeCoroutine(float fromValue, float toValue, float duration, FunctionFloat function, ActionFloat update, UnityAction end)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration; if (t > 1f) t = 1f;
            
            float value = Mathf.Lerp(fromValue, toValue, function(t));

            update(value);

            yield return null;
        }

        if (end != null) end.Invoke();
    }
}
