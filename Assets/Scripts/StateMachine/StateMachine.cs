using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class StateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;
    private State _currentState;
    public State CurrentState => _currentState;
    private Rigidbody _rigidbody;
    private Animator _animator;    
    public bool IsCurrentState<AnyState>(AnyState checkState) => checkState.Equals(_currentState.GetType());    

    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Start() 
    {       
        _currentState = _firstState;
        _currentState.Enter(_rigidbody, _animator);
    }

    private void OnEnable() 
    {
        _currentState?.Exit();
        _firstState?.Enter(_rigidbody, _animator);
    }
    
    private void Update() 
    { 
        if (_currentState == null) { return; } 

        State nextState = _currentState.GetNexState();

        if (nextState != null) 
        {
            Transit(nextState);
        }       
    }

    private void Transit(State nextState) 
    {
        _currentState?.Exit();
        _currentState = nextState;
        _currentState?.Enter(_rigidbody, _animator);
    }
}
