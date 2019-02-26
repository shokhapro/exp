using UnityEngine;

namespace Game.Reusable.LinkPos
{
	public sealed class LinkPositionSmooth : LinkPosition
	{
		[SerializeField] [Range(0f, 1f)] private float xValue = 0.5f;
		[SerializeField] [Range(0f, 1f)] private float yValue = 0.5f;
	
		private void Start()
		{
			_offset = transform.position - target.position;
		}
	
		protected override void UpdatePosition()
		{
			if (!x && !y) return;
			var p = transform.position;
			var tp = target.position + _offset;
			transform.SetPositionXY(x ? Mathf.Lerp(tp.x, p.x, xValue) : p.x, y ? Mathf.Lerp(tp.y, p.y, yValue) : p.y);
		
			UpdateSubscribes();
		}
	}
}
