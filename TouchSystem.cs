using UnityEngine;

namespace Game.Reusable.Touchable
{
	public sealed class TouchSystem : MonoBehaviour
	{
		[SerializeField] private LayerMask touchMask;
		[SerializeField] private float touchRadiusFilter = 100f;

#if UNITY_EDITOR
		private Fingered _onFinger;
#else
	private Fingered[] _onFingers = new Fingered[10];
#endif
		
		private void Update()
		{
#if UNITY_EDITOR
			if (Input.GetMouseButtonDown(0))
			{
				var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				var hit = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, touchMask);
				if (hit)
				{
					var touchable = hit.transform.GetComponent<ITouchable>();
					if (touchable == null) return;
					_onFinger = new Fingered(touchable, Vector3.zero);
					touchable.OnTouchBegan(Input.mousePosition);
				}
			}
		
			if (Input.GetMouseButton(0))
			{
				if (_onFinger != null)
					_onFinger.Obj.OnTouchMoved(Input.mousePosition);
			}
		
			if (Input.GetMouseButtonUp(0))
			{
				if (_onFinger == null) return;
				_onFinger.Obj.OnTouchEnded(Input.mousePosition);
				if (_onFinger.IsClick()) _onFinger.Obj.OnClick();
				_onFinger = null;
			}
#else
			foreach (var touch in Input.touches)
			{
				switch (touch.phase)
				{
					case TouchPhase.Began:
						if (touch.radius > touchRadiusFilter) return;
						var ray = Camera.main.ScreenPointToRay(touch.position);
						var hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, touchMask);
						if (hit)
						{
							var touchable = hit.transform.GetComponent<ITouchable>();
							if (touchable == null) return;
							_onFingers[touch.fingerId] = new Fingered(touchable, touch.position);
							_onFingers[touch.fingerId].Obj.OnTouchBegan(touch.position);
						}
						break;
					case TouchPhase.Moved:
						if (_onFingers[touch.fingerId] != null)
							_onFingers[touch.fingerId].Pos = touch.position;
						break;
					case TouchPhase.Ended:
						if (_onFingers[touch.fingerId] != null)
						{
							_onFingers[touch.fingerId].Obj.OnTouchEnded(touch.position);
							if (_onFingers[touch.fingerId].IsClick()) _onFingers[touch.fingerId].Obj.OnClick();
							_onFingers[touch.fingerId] = null;
						}
						break;
				}
			}
	
			for (var i = 0; i < _onFingers.Length; i++)
				if (_onFingers[i] != null)
					_onFingers[i].Obj.OnTouchMoved(_onFingers[i].Pos);
#endif
		}

		private class Fingered
		{
			public Fingered(ITouchable obj, Vector3 pos)
			{
				Obj = obj;
				Pos = pos;

				_startTime = Time.time;
			}
		
			public ITouchable Obj;
			public Vector3 Pos;

			private readonly float _startTime;
			private const float ClickDelayMax = 0.15f;

			public bool IsClick()
			{
				return Time.time - _startTime < ClickDelayMax;
			}
		}
	}
}
