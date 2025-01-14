using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    
    void Update()
    {
        _target.transform.position = transform.position;        
        _target.transform.rotation = transform.localRotation;        
    }
}
