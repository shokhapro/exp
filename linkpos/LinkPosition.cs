using System.Collections.Generic;
using UnityEngine;

namespace Game.Reusable.LinkPos
{
	public class LinkPosition : MonoBehaviour
	{
		[SerializeField] protected Transform target;
		[SerializeField] protected bool x = true;
		[SerializeField] protected bool y = true;
		protected List<LinkPosition> subscribes = new List<LinkPosition>();
	
		protected bool _subscribed = false;
		protected Vector3 _offset;

		protected void Awake()
		{
			if (!enabled || target == null) return;
		
			var linking = target.GetComponent<LinkPosition>();
			if (linking != null)
			{
				linking.subscribes.Add(this);
				_subscribed = true;
			}
			_offset = transform.position - target.position;	
		}

		protected void Update ()
		{
			if (!_subscribed)
				UpdatePosition();
		}

		protected virtual void UpdatePosition()
		{
			if (!x && !y) return;
			var p = transform.position;
			var tp = target.position + _offset;
			transform.SetPositionXY(x ? tp.x : p.x, y ? tp.y : p.y);

			UpdateSubscribes();
		}

		protected void UpdateSubscribes()
		{
			if (subscribes != null)
				foreach (var sublink in subscribes)
					sublink.UpdatePosition();
		}
	}
}