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
		_l = _n = 0;
	}

	private int[] _array;
	private List<int> _list;
	private int _l, _n;

	private void ResetList()
	{
		_list = new List<int>();
		for (var i = 0; i < _array.Length; i++) _list.Add(i);
	}

	private int GetRandomFromList()
	{
		if (_list.Count.Equals(0)) ResetList();
		var r = _l;
		if (_list.Count.Equals(1)) r = 0;
		else while (r.Equals(_l))
			r = Random.Range(0, _list.Count);
		var value = _list[r];
		_list.RemoveAt(r);
		return value;
	}

	public int Next()
	{
		_l = _n;
		_n = GetRandomFromList();
		return next;
	}

	public int last { get { return _array[_l]; }}
	public int next { get { return _array[_n]; }}
}
