using UnityEngine;
using System.Collections.Generic;

//Credit Abdullah Aldandarawy

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(PolygonCollider2D))]
public class Terrain2D : MonoBehaviour
{
    public List<Vector3> nodes = new List<Vector3>();

    public void UpdateTerrain()
    {
        if (nodes.Count < 3) return;
        Triangulator triangulator = new Triangulator(nodes.ToArray());
        int[] indecies = triangulator.Triangulate();
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.sharedMesh;
        mesh.triangles = null;
        mesh.vertices = nodes.ToArray();
        mesh.triangles = indecies;
        mesh.uv = Vec3ToVec2Array(nodes.ToArray());

        PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
        collider.points = Vec3ToVec2Array(nodes.ToArray());
    }

    private Vector2[] Vec3ToVec2Array(Vector3[] data)
    {
        Vector2[] result = new Vector2[data.Length];
        for (int i = 0; i < data.Length; i++)
            result[i] = data[i];
        return result;
    }
}
