using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public static partial class ExtendedMethods
{
	public static void SetPositionX(this Transform xyz, float x)
	{
		Vector3 pos = xyz.position;
		pos.x = x;
		xyz.position = pos;
	}

	public static void SetPositionY(this Transform xyz, float y)
	{
		Vector3 pos = xyz.position;
		pos.y = y;
		xyz.position = pos;
	}

	public static void SetPositionXY(this Transform xyz, float x, float y)
	{
		Vector3 pos = xyz.position;
		pos.x = x;
		pos.y = y;
		xyz.position = pos;
	}
	
	public static void SetPositionXY(this Transform xyz, Vector3 from)
	{
		Vector3 pos = xyz.position;
		pos.x = from.x;
		pos.y = from.y;
		xyz.position = pos;
	}

	public static void SetPositionZ(this Transform xyz, float z)
	{
		Vector3 pos = xyz.position;
		pos.z = z;
		xyz.position = pos;
	}

	public static void SetLocalPositionZ(this Transform xyz, float z)
	{
		Vector3 pos = xyz.localPosition;
		pos.z = z;
		xyz.localPosition = pos;
	}

	public static void SetVelocityX(this Rigidbody2D physical, float x)
	{
		physical.velocity = new Vector2(x, physical.velocity.y);
	}

	public static Vector2 ToVector2(this Vector3 xyz)
	{
		return new Vector2(xyz.x, xyz.y);
	}

	public static Vector3 ToVector3(this Vector2 xyz, float z)
	{
		return new Vector3(xyz.x, xyz.y, z);
	}

	public static Vector3 WithX(this Vector3 xyz, float x)
	{
		xyz.x = x;
		return xyz;
	}

	public static Vector3 WithY(this Vector3 xyz, float y)
	{
		xyz.y = y;
		return xyz;
	}

	public static Vector3 WithZ(this Vector3 xyz, float z)
	{
		xyz.z = z;
		return xyz;
	}

	public static Vector2 WithX(this Vector2 xyz, float x)
	{
		xyz.x = x;
		return xyz;
	}

	public static Vector2 WithY(this Vector2 xyz, float y)
	{
		xyz.y = y;
		return xyz;
	}

	public static Vector2 GetVector2(this Vector3 xyz)
	{
		return new Vector2(xyz.x, xyz.y);
	}

	public static T GetRandomElement<T>(this T[] array)
	{
		return array[Random.Range(0, array.Length)];
	}

	public static Color WithAlpha(this Color color, float a)
	{
		color.a = a;
		return color;
	}
	
	public static float GetPosX(this RectTransform rtransform)
    {
	Vector2 wh = rtransform.offsetMax - rtransform.offsetMin;
	Vector2 p = rtransform.pivot;
	return rtransform.offsetMin.x + wh.x * p.x;
    }

    public static float GetPosY(this RectTransform rtransform)
    {
	Vector2 wh = rtransform.offsetMax - rtransform.offsetMin;
	Vector2 p = rtransform.pivot;
	return rtransform.offsetMin.y + wh.y * p.y;
    }

    public static Vector2 GetPosXY(this RectTransform rtransform)
    {
	Vector2 wh = rtransform.offsetMax - rtransform.offsetMin;
	Vector2 p = rtransform.pivot;
	return new Vector2(rtransform.offsetMin.x + wh.x * p.x, rtransform.offsetMin.y + wh.y * p.y);
    }

    public static void SetPosX(this RectTransform rtransform, float x)
    {
	Vector2 wh = rtransform.offsetMax - rtransform.offsetMin;
	Vector2 p = rtransform.pivot;
	rtransform.offsetMax = new Vector2(x + wh.x * (1 - p.x), rtransform.offsetMax.y);
	rtransform.offsetMin = new Vector2(x - wh.x * p.x, rtransform.offsetMin.y);
    }

    public static void SetPosY(this RectTransform rtransform, float y)
    {
	Vector2 wh = rtransform.offsetMax - rtransform.offsetMin;
	Vector2 p = rtransform.pivot;
	rtransform.offsetMax = new Vector2(rtransform.offsetMax.x, y + wh.y * (1 - p.y));
	rtransform.offsetMin = new Vector2(rtransform.offsetMin.x, y - wh.y * p.y);
    }

    public static void SetPosXY(this RectTransform rtransform, Vector2 xy)
    {
	Vector2 wh = rtransform.offsetMax - rtransform.offsetMin;
	Vector2 p = rtransform.pivot;
	rtransform.offsetMax = new Vector2(xy.x + wh.x * (1 - p.x), xy.y + wh.y * (1 - p.y));
	rtransform.offsetMin = new Vector2(xy.x - wh.x * p.x, xy.y - wh.y * p.y);
    }

    public static void SetPosXY(this RectTransform rtransform, float x, float y)
    {
	Vector2 wh = rtransform.offsetMax - rtransform.offsetMin;
	Vector2 p = rtransform.pivot;
	rtransform.offsetMax = new Vector2(x + wh.x * (1 - p.x), y + wh.y * (1 - p.y));
	rtransform.offsetMin = new Vector2(x - wh.x * p.x, y - wh.y * p.y);
    }

    public static void DelayedAction(this MonoBehaviour script, UnityAction action, float delay = 0.1f)
    {
	script.StartCoroutine(ActionDelayed(delay, action));
    }

    static IEnumerator ActionDelayed(float delay, UnityAction action)
    {
	yield return new WaitForSeconds(delay);
	action.Invoke();
    }

    public static void SetRotation(this RectTransform rtransform, float value)
    {
	rtransform.localEulerAngles = new Vector3(rtransform.localEulerAngles.x, rtransform.localEulerAngles.y, value);
    }
}
