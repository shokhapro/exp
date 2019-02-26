using UnityEngine;

namespace Game.Reusable.Touchable
{
	public interface ITouchable
	{
		void OnTouchBegan(Vector3 point);
	
		void OnTouchMoved(Vector3 point);
	
		void OnTouchEnded(Vector3 point);
		
		void OnClick();
	}
}
