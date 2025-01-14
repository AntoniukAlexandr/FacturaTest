using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _poolCount;
    [SerializeField] private int _enemyCountOnePoint = 5;
    [SerializeField] private int _randonRangeSpawn = 5;
    [SerializeField] private bool _autoExpand = true;
    private ObjectsPool<Enemy> _enemyPool;

    void Awake()
    {
        _enemyPool = new ObjectsPool<Enemy>(_enemyPrefab, _poolCount, transform);
        _enemyPool.AutoExpand = _autoExpand;        
    }      

    public void SpawnEnemies(Chunk chunck) 
    {
        chunck.StartSpawnPointsEnemy.ForEach(point => {
            for (int i = 0; i < _enemyCountOnePoint; i++)
            {
                var enemy = _enemyPool.GetFreeElement();                
                enemy.transform.position = new Vector3(point.transform.position.x + Random.Range(-_randonRangeSpawn, _randonRangeSpawn), 0, point.transform.position.z + Random.Range(-_randonRangeSpawn, _randonRangeSpawn));
                enemy.transform.parent = chunck.transform;               
                
            }
        });
    }
}
