using System.Collections.Generic;
using UnityEngine;

public class URandom
{
	public URandom(int min, int max)
	{
		if (min > max) min = max;
		_array = new int[max - min + 1];
		for (var i = 0; i < _array.Length; i++) _array[i] = min + i;
		ResetList();
		_lastId = -1;
		_nextId = 0;
	}

	private int[] _array;
	private List<int> _list;
	private int _lastId, _nextId;

	private void ResetList()
	{
		_list = new List<int>();
		for (var i = 0; i < _array.Length; i++) _list.Add(i);
	}

	private int GetRandomFromList()
	{
		if (_list.Count.Equals(0)) ResetList();
		var randId = _lastId;
		if (_list.Count.Equals(1)) randId = 0;
		else while (randId.Equals(_lastId))
			randId = Random.Range(0, _list.Count);
		var value = _list[randId];
		_list.RemoveAt(randId);
		return value;
	}

	public int Next()
	{
		_lastId = _nextId;
		_nextId = GetRandomFromList();
		return next;
	}

	public int last { get { return _lastId == -1 ? -1 : _array[_lastId]; }}
	public int next { get { return _array[_nextId]; }}
}
