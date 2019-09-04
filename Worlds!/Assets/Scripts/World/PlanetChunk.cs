﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralTerrain.MarchingCubes;

public unsafe class PlanetChunk : MonoBehaviour
{
	Planet m_planet;

	//info
	int m_id;

	int m_res;
	float m_scale;

	float[] m_densityMap;

	MeshFilter m_meshFilter;
	MeshCollider m_meshCollider;

	MarchingCubes m_mc;
	public ComputeShader m_MarchingCubesShader;
	public ComputeShader m_ClearVerticesShader;
	public ComputeShader m_CalculateNormalsShader;

	//density map normal pointers
	PlanetChunk m_xChunk, m_yChunk, m_zChunk;

	void Start ()
	{

	}
	
	void Update ()
	{
		
	}

	private void OnDestroy()
	{
		m_mc.Release();
	}
	public void Initalize(int id, int res, float scale, bool sharpEdges)
	{
		m_id = id;
		m_res = res;
		m_scale = scale;

		m_meshFilter = GetComponent<MeshFilter>();
		m_meshCollider = GetComponent<MeshCollider>();

		m_mc = new MarchingCubes();
		m_mc.m_marchingCubesShader = m_MarchingCubesShader;
		m_mc.m_clearVerticesShader = m_ClearVerticesShader;
		m_mc.m_calculateNormalsShader = m_CalculateNormalsShader;
		m_mc.m_recalculateNormals = sharpEdges;
		m_mc.Initalize(m_res, m_res, m_res, m_scale, 0);
	}
	public void SetDensityMap(float[] map)
	{
		m_densityMap = map;
	}

	public void Refresh()
	{
		Mesh mesh = m_mc.ComputeMesh(m_densityMap);
		mesh.name = transform.parent.name + "_" + m_id.ToString();

		m_meshFilter.mesh.Clear();
		m_meshFilter.mesh = mesh;

		m_meshCollider.sharedMesh = mesh;
	}

	public void AssignXChunk(PlanetChunk x_chunk)
	{
		m_xChunk = x_chunk;
	}
	
	public void AssignYChunk(PlanetChunk y_chunk)
	{
		m_yChunk = y_chunk;
	}

	public void AssignZChunk(PlanetChunk z_chunk)
	{
		m_zChunk = z_chunk;
	}
}