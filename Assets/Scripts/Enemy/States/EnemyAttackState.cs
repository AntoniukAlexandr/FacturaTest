using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : State
{
    private NavMeshAgent _agent;
    private Vector3 _target;

    private void Awake() 
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable() 
    {
        _agent.enabled = true;
        Animator.SetBool("Chase", true); 
        _target = _agent.destination;       
    }
   
    private void OnDisable() 
    {        
        Animator.SetBool("Chase", false);
        _agent.enabled = false;        
    }    

    void Update()
    {
        
    }
}
