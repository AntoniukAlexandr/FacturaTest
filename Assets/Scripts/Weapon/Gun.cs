using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IGun
{   
    [SerializeField] private int _bulletPoolCount;
    [SerializeField] private bool _autoExpandBulletPool = true;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _shotTimeout = 0.5f;
    [SerializeField] private LayerMask _hitLayerMask;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _raycastOrigin;
    private float _lastShootTime = 0f;    
    private bool _fire = false;    
    private Ray _ray;    
    private RaycastHit _raycastHit;
    private RotateCarGun _rotate;
    private ObjectsPool<Bullet> _bulletsPool;
    private List<Bullet> _activeBullets = new List<Bullet>();
    public bool isFire => _fire;

    private void Awake() 
    {
        _rotate = GetComponent<RotateCarGun>();
        _bulletsPool = new ObjectsPool<Bullet>(_bulletPrefab, _bulletPoolCount, _raycastOrigin);
        _bulletsPool.AutoExpand = _autoExpandBulletPool;
    }
    public void StartFire()
    {
        _rotate.enabled = true;
        _fire = true;
    }
    public void StopFire()
    {
        _fire = false;
        _rotate.enabled = false;
    }
      
    
    Vector3 GetBulletPosition(Bullet bullet)
    {
        Vector3 gravity = Vector3.down * bullet.Characteristics.Gravity;
        return bullet.lastPosition + (bullet.lastVelocity * bullet.lifeTime) + (0.5f * gravity * bullet.lifeTime * bullet.lifeTime);        
    }

    private void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {   
        if (!bullet.isActive) return;

        Vector3 direction = end - start;
        float distance = direction.magnitude;
        _ray.origin = start;
        _ray.direction = direction;        

        if(Physics.Raycast(_ray, out _raycastHit, distance, _hitLayerMask)) 
        {
            if (_raycastHit.collider.gameObject.TryGetComponent(out IDamageable damageable))
            {              
                damageable.ApplyDamage(bullet.Characteristics.Damage, _raycastHit.point, direction);                
            }

            bullet.Disable(_raycastOrigin.position);
        }        
    }

    private void UpdateBullets()
    {
        if (_bulletsPool != null)
        {
            _bulletsPool.GetActiveObjects(ref _activeBullets);
         
            _activeBullets.ForEach(bullet => {
                Vector3 currentPosition = GetBulletPosition(bullet);
                bullet.transform.position = currentPosition;

                bullet.lifeTime += Time.deltaTime;
                Vector3 nextPosition = GetBulletPosition(bullet);

                RaycastSegment(currentPosition, nextPosition, bullet);
            });
            
            _activeBullets.ForEach(bullet => {                
                if (bullet.lifeTime >= bullet.Characteristics.BulletMaxLifetime && bullet.isActive)
                { 
                    bullet.Disable(_raycastOrigin.transform.position);
                } 
            });
       }        
    }

    private void Fire()
    {
        if (!isFire) return;
        
        _lastShootTime += Time.deltaTime;
        
        if(_lastShootTime < _shotTimeout) return;
        _lastShootTime = 0f;
          
        Vector3 velocity =  _raycastOrigin.forward * _bulletSpeed;
        var bullet = _bulletsPool.GetFreeElement(); 
                     
        bullet.transform.position = _raycastOrigin.position;
        bullet.lastPosition = _raycastOrigin.position;
        bullet.lastVelocity = velocity;
        bullet.transform.rotation = transform.rotation;
        bullet.lifeTime = 0.0f;       
        bullet.transform.SetParent(null);
        bullet.Trail.Clear(); 
        bullet.isActive = true;
    }

    private void FixedUpdate() 
    {
        UpdateBullets();
        Fire();        
    }
}
