using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

//Credit Abdullah Aldandarawy for base Terrain Mesh updating later modified
//Credit chaud_hary19 for base RMQ implementation later modified

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(PolygonCollider2D))]
public class Terrain2D : MonoBehaviour
{
    public List<Vector3> nodes = new List<Vector3>();

    private Vector3[,] _lookupTable;
    private Vector3 _rightmostNode;
    private float _nodeDistance;

    public void UpdateTerrain()
    {
        if (nodes.Count < 4) return;
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

        _rightmostNode = nodes[2];

        _nodeDistance = nodes[2].x - nodes[3].x;
        PreprocessRMQ();
    }

    private Vector2[] Vec3ToVec2Array(Vector3[] data)
    {
        Vector2[] result = new Vector2[data.Length];
        for (int i = 0; i < data.Length; i++)
            result[i] = data[i];
        return result;
    }

    private void PreprocessRMQ()
    {
        var terrainNodes = nodes.Skip(2).Select(point => transform.TransformPoint(point)).ToArray();

        _lookupTable = new Vector3[terrainNodes.Length, terrainNodes.Length];

        for (int i = 0; i < terrainNodes.Length; i++)
            _lookupTable[i, 0] = terrainNodes[i];

        // Compute values from smaller to bigger intervals
        for (int j = 1; (1 << j) <= terrainNodes.Length; j++)
        {

            // Compute maximum value for all intervals with
            // size 2^j
            for (int i = 0; (i + (1 << j) - 1) < terrainNodes.Length; i++)
            {

                // For arr[2][10], we compare arr[lookup[0][7]]
                // and arr[lookup[3][10]]
                if (_lookupTable[i, j - 1].y > _lookupTable[i + (1 << (j - 1)), j - 1].y)
                    _lookupTable[i, j] = _lookupTable[i, j - 1];
                else
                    _lookupTable[i, j] = _lookupTable[i + (1 << (j - 1)), j - 1];
            }
        }
    }

    public Vector3 HighestPointBetween(Vector3 leftPosition, Vector3 rightPosition)
    {
        //nodes are stored in the node list in trigonometric order to prevent face culling, thus the indices are from right to left
        int leftIndex = Mathf.CeilToInt((_rightmostNode.x - rightPosition.x) / _nodeDistance);
        int rightIndex = Mathf.FloorToInt((_rightmostNode.x - leftPosition.x) / _nodeDistance);

        // Find highest power of 2 that is smaller
        // than or equal to count of elements in given
        // range
        // For [2, 10], j = 3
        int j = (int)Math.Log(rightIndex - leftIndex + 1);

        // Compute maximum of last 2^j elements with first
        // 2^j elements in range
        // For [2, 10], we compare arr[lookup[0][3]] and
        // arr[lookup[3][3]]
        if (_lookupTable[leftIndex, j].y >= _lookupTable[rightIndex - (1 << j) + 1, j].y)
            return _lookupTable[leftIndex, j];

        else
            return _lookupTable[rightIndex - (1 << j) + 1, j];
    }
}
