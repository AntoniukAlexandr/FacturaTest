using UnityEngine;

public abstract class Transition : MonoBehaviour
{
   [SerializeField] private State _targetState;
    private StateMachine _stateMachine;
    public State TargetState => _targetState;
    public StateMachine GetStateMachine => _stateMachine;
    public State CurrentState => _stateMachine.CurrentState;
    public bool NeedTransit { get; protected set; }   

    private void Awake()
    {        
        _stateMachine = GetComponent<StateMachine>();
    }

    private void OnEnable()
    {
        
        NeedTransit = false;
        //Enable();
    }

    
    //public abstract void Enable();
    public abstract void StartTransition();
}
