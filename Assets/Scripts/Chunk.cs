using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private GameObject _finish; 
    [SerializeField] private List<Transform> _startSpawnPointEnemy;     
    private int _chunkLengths = 105;
    public int ChunkLength => _chunkLengths;
    public List<Transform>  StartSpawnPointsEnemy => _startSpawnPointEnemy;
   
    public void Disable()
    {           
        gameObject.SetActive(false);
    }

    public void SetFinish(bool finish)
    {
        _finish.SetActive(finish);
    }    
}
