using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{   
    [SerializeField] [Range(0, 100)] private float _hp = 100;
    [SerializeField] private ParticleSystem _enemyParticles;
    private Transition _attackStateTransition;     
    public Transition idleStateTransition;
    private NavMeshAgent _agent;    

    private void Start() 
    {
        _attackStateTransition = GetComponent<EnemyAttackTransition>();
        idleStateTransition = GetComponent<EnemyIdleTransition>();
        _agent = GetComponent<NavMeshAgent>();
    } 

    private void OnEnable() 
    {        
        _enemyParticles.gameObject.transform.SetParent(gameObject.transform);
        _enemyParticles.gameObject.transform.localPosition = new Vector3(0, 0.5f, 0);
    }
    
    public void SetPlayerPosition(Vector3 position)
    {
        _attackStateTransition.StartTransition();
        if (_agent.enabled)
        {
            _agent.SetDestination(position);
        }        
    }    

    public bool ApplyDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        if(_hp > 0)
        {
            _hp -= damage;
        } 

        if(_hp <= 0)           
        { 
            _enemyParticles.gameObject.transform.forward = hitNormal;           
            _enemyParticles.gameObject.transform.SetParent(null);
            
            _enemyParticles.Play();
            
            _hp = 100f; 

            gameObject.SetActive(false);          
        }
        return true;
    }    
}
