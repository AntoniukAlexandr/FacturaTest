using UnityEngine;

public class EnemyIdleState : State
{
    private void OnEnable() 
    {
        Animator.SetBool("Chase", false);
    }

    private void Update() 
    {
        
    }    
}
