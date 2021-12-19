using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class MarchingCubesModel : MonoBehaviour
{
    public Color[,,] data;
    public bool dirty = false;

    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Color> colours = new List<Color>();

    public int width = 16;
    public int height = 16;
    public int depth = 16;

    public bool smoothing = false;
    public bool flatShading = false;

    public Material material;


    void Start()
    {
        this.data = new Color[this.width, this.height, this.depth];
        for (int x = 0; x < this.width; x++)
        {
            for (int y = 0; y < this.height; y++)
            {
                for (int z = 0; z < this.depth; z++)
                {
                    if (y < 8)
                        this.data[x, y, z] = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
                    else if (y == 8)
                        this.data[x, y, z] = new Color(1f, 1f, 1f, 1f);
                    else this.data[x, y, z] = new Color(1f, 1f, 1f, 0f);
                }
            }
        }
    }

    public void Generate()
    {
        if (this.dirty)
        {
            this.Clear();
            this.CreateMeshData();
            this.GenerateMesh();
            this.dirty = false;
        }
    }

    private void Clear()
    {
        this.vertices.Clear();
        this.triangles.Clear();
    }

    private void CreateMeshData()
    {
        //TODO IMPLEMENT COLOUR
        MarchingCubes.MarchData(this.data, out this.vertices, out this.triangles, out this.colours, this.smoothing, this.flatShading);
    }

    private void GenerateMesh()
    {
        Mesh m = new Mesh();
        m.vertices = this.vertices.ToArray();
        m.triangles = this.triangles.ToArray();
        m.RecalculateNormals();

        this.GetComponent<MeshFilter>().mesh = m;
        this.GetComponent<MeshCollider>().sharedMesh = m;
        this.GetComponent<MeshRenderer>().material = this.material;
    }
}
