using UnityEngine;

namespace Game.Reusable.LinkPos
{
	public sealed class LinkPositionParallax : LinkPosition
	{
		[SerializeField] private float xValue = 0.5f;
		[SerializeField] private float yValue = 0.5f;
	
		private Vector3 _tOffset;
	
		private void Start()
		{
			_offset = transform.position;
			_tOffset = target.transform.position;
		}
	
		protected override void UpdatePosition()
		{
			if (!x && !y) return;
			transform.SetPositionXY(
				x ? _offset.x + (target.transform.position.x - _tOffset.x) * xValue : transform.position.x,
				y ? _offset.y + (target.transform.position.y - _tOffset.y) * yValue : transform.position.y);
		
			UpdateSubscribes();
		}
	}
}
