using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{   
    [SerializeField] private int _levelLengths = 10;
    [SerializeField] private int _poolCount = 3;
    [SerializeField] private bool _autoExpand = true;
    [SerializeField] private Chunk _chunkPrefab;  
    [SerializeField] private Chunk _currentChunk;      
    [SerializeField] private PlayerTrigger _playerTrigger; 
    [SerializeField] private EnemySpawner _enemySpawner;
    private ObjectsPool<Chunk> _chunkPool;
    private Chunk _previousChunk;
    private int _chunkCounter = 0;

    void Awake()
    {
        _chunkPool = new ObjectsPool<Chunk>(_chunkPrefab, _poolCount, transform);
        _chunkPool.AutoExpand = _autoExpand;
    }  

    void Start()
    {
        _enemySpawner.SpawnEnemies(_currentChunk);
    }

    private void OnEnable() 
    {
        _playerTrigger.UpdateChunks += OnUpdateChunks;
    }

    private void OnDisable() 
    {
        _playerTrigger.UpdateChunks -= OnUpdateChunks;
    }

    private void OnUpdateChunks()
    {
        _chunkCounter++;

        _previousChunk?.Disable();
        _previousChunk = _currentChunk;

        _currentChunk = _chunkPool.GetFreeElement();        
        _currentChunk.transform.position = new Vector3(0, 0, _previousChunk.transform.position.z + _previousChunk.ChunkLength);

        _enemySpawner.SpawnEnemies(_currentChunk);

        if (_chunkCounter == _levelLengths)
        {
            _currentChunk.SetFinish(true);
        }      
    }
}
