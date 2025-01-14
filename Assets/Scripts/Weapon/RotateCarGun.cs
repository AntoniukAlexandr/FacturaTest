using UnityEngine;

public class RotateCarGun : MonoBehaviour
{
    [SerializeField] private TouchManager _touchManager;
    private float _rotationLerp = 10f;
    private float _angleX;
    private float _angleY;

    private void Start()
    {
        _angleX = transform.rotation.x;
        _angleY = transform.rotation.y;
    }

    private void OnEnable() 
    {
        _touchManager.RotateGun += RotateGun;
    }
    private void OnDisable() 
    {
        _touchManager.RotateGun -= RotateGun;
    }

    private void RotateGun(float deltaX)
    {        
        transform.rotation = Quaternion.Euler(_angleX, -deltaX / _rotationLerp, 0);
    }
    
    // void Update()
    // {
    //     if(Input.GetMouseButton(0))
    //     {            
    //         _angleY += Input.GetAxis("Mouse X") * 2;
    //         transform.rotation = Quaternion.Euler(_angleX, _angleY, 0);
    //     }
    // }
}
