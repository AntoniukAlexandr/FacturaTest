using UnityEngine;

[CreateAssetMenu(fileName = "Characteristics", menuName = "Weapon/BulletCharacteristics", order = 52)]
public class BulletCharacteristics : ScriptableObject
{
    [SerializeField] private float _bulletMaxLifetime = 3.0f;
    [SerializeField] private float _gravity = 9.8f;
    [SerializeField] private bool _explosive = false;
    [SerializeField] private float _damage = 50.0f;

    public float BulletMaxLifetime => _bulletMaxLifetime;
    public float Gravity => _gravity;
    public bool Explosive => _explosive;
    public float Damage => _damage;
}
