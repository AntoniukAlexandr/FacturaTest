using UnityEngine;

public interface IDamageable
{    
    bool ApplyDamage(float damage, Vector3 hitPosition, Vector3 hitNormal);
}
