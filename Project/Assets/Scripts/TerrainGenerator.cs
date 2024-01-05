using UnityEngine;

[RequireComponent(typeof(Terrain2D))]
public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int _terrainNodes;
    [SerializeField] private float _groundLevel;
    [SerializeField] private float _depthLevel;
    [SerializeField] private float _maxHeight;
    [SerializeField] private float _minHeight;
    [SerializeField] private float _leftBound;
    [SerializeField] private float _rightBound;

    private float _nodeDistance;

    private Terrain2D _terrain;
    void Start()
    {
        _terrain = GetComponent<Terrain2D>();

        _nodeDistance = (_rightBound - _leftBound) / (_terrainNodes - 1);

        GenerateTerrain();
    }


    private void GenerateTerrain()
    {
        _terrain.nodes.Add(new(_leftBound, _depthLevel, 0));
        _terrain.nodes.Add(new(_rightBound, _depthLevel, 0));

        for (int i = 0; i < _terrainNodes; i++)
        {
            _terrain.nodes.Add(new(_rightBound - i * _nodeDistance, Random.Range(_minHeight, _maxHeight), 0));
        }

        _terrain.UpdateTerrain();
    }
}
