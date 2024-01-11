using UnityEngine;

[RequireComponent(typeof(Terrain2D))]
public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int _terrainNodes;
    [SerializeField] private float _depthLevel;
    [SerializeField] private float _smoothness;
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

    public void GenerateTerrain()
    {
        float noiseSeed = Random.Range(-100000, 100000);

        _terrain.nodes.Clear();
        _terrain.nodes.Add(new(_leftBound, _depthLevel, 0));
        _terrain.nodes.Add(new(_rightBound, _depthLevel, 0));

        for (int i = 0; i < _terrainNodes; i++)
        {
            float position = _rightBound - i * _nodeDistance;
            _terrain.nodes.Add(new(position, Mathf.PerlinNoise1D((noiseSeed + position) / _smoothness).Map01To(_minHeight, _maxHeight), _maxHeight));
        }

        _terrain.UpdateTerrain();
    }
}
