using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class TouchManager : MonoBehaviour
{     
    private PlayerInput _playerInput;
    private InputAction _touchPositionAction;
    private InputAction _touchPressAction;
    private bool _drag = false;
    private Vector2 _startTouchPosition;
    public event UnityAction<float> RotateGun;

    private void Awake() 
    {
        _playerInput = GetComponent<PlayerInput>();
        _touchPositionAction = _playerInput.actions.FindAction("TouchPosition");
        _touchPressAction = _playerInput.actions.FindAction("TouchPress");
    }

    private void OnEnable() 
    {
        _touchPressAction.started += OnTouchDown;        
        _touchPressAction.canceled += OnTouchUp;        
    }

    private void OnDisable() 
    {
        _touchPressAction.started -= OnTouchDown;         
        _touchPressAction.canceled -= OnTouchUp;         
    }
    
    private void OnTouchDown(InputAction.CallbackContext context) 
    {       
        _startTouchPosition = _touchPositionAction.ReadValue<Vector2>();
        _drag = true;        
    }

    private void OnTouchUp(InputAction.CallbackContext context) 
    {
        _drag = false;       
    }

    private void Update() 
    {
        if(_drag)
        {         
            RotateGun?.Invoke(_startTouchPosition.x - _touchPositionAction.ReadValue<Vector2>().x);
        }        
    }
    
}
