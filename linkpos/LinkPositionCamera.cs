using UnityEngine;

namespace Game.Reusable.LinkPos
{
	public class LinkPositionCamera : LinkPosition
	{
		public static LinkPositionCamera Instance;
	
		[SerializeField] private Vector2 offset;
		[SerializeField] private Vector2 yLimitMinMax = new Vector2(-10f, 15f);
		[SerializeField] private AnimationCurve yCurve;

		private float _yLimitDelta;
		private bool _setting;
		private const float _settingDuration = 1f;
		private float _settingElapsed = 0f;

		private void Awake()
		{
			Instance = this;
			_offset = offset;
		}
		
		private void Start()
		{
			if (target == null) return;
		
			Application.targetFrameRate = 60;
			_yLimitDelta = yLimitMinMax.y - yLimitMinMax.x;
		}
	
		protected override void UpdatePosition()
		{
			if (target == null) return;
		
			if (!x && !y) return;
			var p = transform.position;
			var tp = target.position + _offset;
			var py = Mathf.Lerp(yLimitMinMax.x, yLimitMinMax.y,
				yCurve.Evaluate((tp.y - yLimitMinMax.x) / _yLimitDelta));
		
			if (_setting)
			{
				_settingElapsed = Mathf.Clamp(_settingElapsed + Time.deltaTime, 0, _settingDuration);
				transform.SetPositionXY(Mathf.Lerp(transform.position.x, tp.x, _settingElapsed / _settingDuration),
					Mathf.Lerp(transform.position.y, py, _settingElapsed / _settingDuration));
				if (_settingElapsed == _settingDuration)
				{
					_settingElapsed = 0f;
					_setting = false;
				}
			}
			else
				transform.SetPositionXY(x ? tp.x : p.x, y ? py : p.y);
		
			UpdateSubscribes();
		}

		public void SetTarget(Transform t)
		{
			target = t;
			_setting = true;
			Awake();
			Start();
		}
	
		public void SetPosition(Vector3 value)
		{
			transform.SetPositionXY(value.x, value.y);
		}
	}
}