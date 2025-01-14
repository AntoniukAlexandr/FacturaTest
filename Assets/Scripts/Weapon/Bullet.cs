using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private BulletCharacteristics _characteristics;
    [SerializeField] private TrailRenderer _trail;
    public TrailRenderer Trail => _trail;
    public BulletCharacteristics Characteristics => _characteristics;
    public float lifeTime = 0f;
    public bool isActive = false;
    public Vector3 lastPosition;
    public Vector3 lastVelocity;
    public abstract void Disable(Vector3 newSpawnPoint);
}
