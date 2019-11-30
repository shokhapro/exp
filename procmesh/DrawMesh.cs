using System.Collections.Generic;
using UnityEngine;

public class DrawMesh
{
	public DrawMesh()
	{
		activeColor = defaultColor;
	}
	
	List<Vector3> vertices = new List<Vector3>();
	List<Color> colors = new List<Color>();
	List<int> triangles = new List<int>();
	
	Color activeColor;
	
	Color defaultColor = Color.gray;
	
	private int addVertex(Vector3 p, bool check = true)
	{
		if (check)
			for (int i = 0; i < vertices.Count; i++)
				if (vertices[i] == p && colors[i] == activeColor)
					return i;

		vertices.Add(p);
		colors.Add(activeColor);
		return vertices.Count - 1;
	}
	
	private void addTriangle(int v0, int v1, int v2)
	{
		if (v0 == v1 || v1 == v2 || v2 == v0) return;
		
		triangles.Add(v0);
		triangles.Add(v1);
		triangles.Add(v2);
	}
	
	private void addQuad(int v0, int v1, int v2, int v3)
	{
		if (v0 == v1 || v1 == v2 || v2 == v0) return;
		
		triangles.Add(v0);
		triangles.Add(v1);
		triangles.Add(v2);
		
		if (v0 == v2 || v2 == v3 || v3 == v0) return;
		
		triangles.Add(v0);
		triangles.Add(v2);
		triangles.Add(v3);
	}
	
	public Mesh GetMesh()
	{
		Mesh mesh = new Mesh();
		mesh.vertices = vertices.ToArray();
		mesh.colors = colors.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.RecalculateNormals();
		return mesh;
	}
	
	public void AddTrigon(Vector3 p0, Vector3 p1, Vector3 p2, Color? color = null)
	{
		activeColor = color ?? defaultColor;
		
		addTriangle(
			addVertex(p0,false),
			addVertex(p1,false),
			addVertex(p2,false)
		);
		
		activeColor = defaultColor;
	}
	
	public void AddTetragon(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, Color? color = null)
	{
		activeColor = color ?? defaultColor;
		
		addQuad(
			addVertex(p0,false),
			addVertex(p1,false),
			addVertex(p2,false),
			addVertex(p3,false)
		);
		
		activeColor = defaultColor;
	}
	
	public void AddChain(Vector3[] px2, Color? color = null)
	{
		activeColor = color ?? defaultColor;
		
		int xCount = Mathf.RoundToInt(px2.Length * 0.5f);
		int p0 = addVertex(px2[0],false);
		int p1= addVertex(px2[1],false);
		for (int x = 1; x < xCount; x++)
		{
			int p2 = addVertex(px2[(x)*2],false);
			int p3 = addVertex(px2[(x)*2+1],false);
			
			addQuad(p0, p2, p3, p1);
			
			p0 = p2;
			p1 = p3;
		}
		
		activeColor = defaultColor;
	}
	
	public void Shell(Vector3 width, bool mid = true)
	{
		if (mid)
		{
			Vector3 mHalf = width * 0.5f;
			for (int i = 0; i < vertices.Count; i++)
				vertices[i] -= mHalf;
		}
		
		int vCount = vertices.Count;
		for (int i = 0; i < vCount; i++)
		{
			vertices.Add(vertices[i] + width);
			colors.Add(colors[i]);
		}
		
		int tCount = Mathf.RoundToInt(triangles.Count / 3f);
		
		int tsCount = triangles.Count;
		int tri = 0;
		for (int i = 0; i < tsCount; i++)
		{
			int o = (tri == 1) ? 1 : (tri == 2) ? -1 : 0;
			triangles.Add(triangles[i+o]+vCount);
			if (tri < 2) tri++; else tri = 0;
		}
		
		for (int i = 0; i < tCount; i++)
		{
			bool b0b1 = false;
			bool b1b2 = false;
			bool b2b0 = false;
			
			for (int u = 0; u < tCount; u++)
			{
				if (i == u) continue;
				
				bool b0 =
					triangles[i*3] == triangles[u*3] ||
					triangles[i*3] == triangles[u*3+1] ||
					triangles[i*3] == triangles[u*3+2];
				bool b1 =
					triangles[i*3+1] == triangles[u*3] ||
					triangles[i*3+1] == triangles[u*3+1] ||
					triangles[i*3+1] == triangles[u*3+2];
				bool b2 =
					triangles[i*3+2] == triangles[u*3] ||
					triangles[i*3+2] == triangles[u*3+1] ||
					triangles[i*3+2] == triangles[u*3+2];
					
				b0b1 = b0b1 || (b0 && b1);
				b1b2 = b1b2 || (b1 && b2);
				b2b0 = b2b0 || (b2 && b0);
			}
			
			if (!b0b1)
				addQuad(
					addVertex(vertices[triangles[i*3]],false),
					addVertex(vertices[triangles[i*3] + vCount],false),
					addVertex(vertices[triangles[i*3+1] + vCount],false),
					addVertex(vertices[triangles[i*3+1]],false)
				);
			if (!b1b2)
				addQuad(
					addVertex(vertices[triangles[i*3+1]],false),
					addVertex(vertices[triangles[i*3+1] + vCount],false),
					addVertex(vertices[triangles[i*3+2] + vCount],false),
					addVertex(vertices[triangles[i*3+2]],false)
				);
			if (!b2b0)
				addQuad(
					addVertex(vertices[triangles[i*3+2]],false),
					addVertex(vertices[triangles[i*3+2] + vCount],false),
					addVertex(vertices[triangles[i*3] + vCount],false),
					addVertex(vertices[triangles[i*3]],false)
				);
		}
	}
}
