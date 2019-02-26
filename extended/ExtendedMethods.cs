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

	public static void Timeout(this MonoBehaviour script, float time, UnityAction action)
	{
		script.StartCoroutine(TimeCoroutine(time, action));
	}

	private static IEnumerator TimeCoroutine(float time, UnityAction action)
	{
		yield return new WaitForSeconds(time);
		action.Invoke();
	}

	public static Color WithAlpha(this Color color, float a)
	{
		color.a = a;
		return color;
	}
}