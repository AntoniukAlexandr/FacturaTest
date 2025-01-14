using UnityEngine;

public class TurretBullet : Bullet
{
    public override void Disable(Vector3 newSpawnPoint)
    {
        isActive = false;
        lifeTime = 0f;        
        gameObject.SetActive(false);
        
    }
}
