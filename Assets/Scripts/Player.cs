using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float _initialHP = 100f;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _visibilityRadius = 15f;
    [SerializeField] private bool _lose = false;
    [SerializeField] private Button _loseButton;  
    [SerializeField] private LayerMask _layerEnemy;  
    [SerializeField] private ProgressBar _healthBar;   
    private IGun _gun;
    private float _hp;
    
    private void Awake() 
    {
        _hp = _initialHP;
        _gun = GetComponentInChildren<IGun>();
    }
    private void OnEnable() 
    {
        _gun.StartFire();
    }

    public bool ApplyDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        if(_hp > 0)
        {
            _hp -= damage;
        }
        else
        {
            _hp = 0;
            _lose = true;
            _loseButton.gameObject.SetActive(true);
            _gun.StopFire();
        }
        
        _healthBar.SetProgress(_hp / _initialHP);
        return true;
    }    
    
    void FixedUpdate()
    {
        if (!_lose) 
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position+Vector3.forward, _speed*Time.deltaTime);
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _visibilityRadius, _layerEnemy);

            foreach (var hitCollider in hitColliders)
            {            
                if (hitCollider.gameObject.activeSelf && hitCollider.gameObject.TryGetComponent(out Enemy enemy))
                {
                    enemy.SetPlayerPosition(transform.position);
                }            
            }
        }        
    }
}
