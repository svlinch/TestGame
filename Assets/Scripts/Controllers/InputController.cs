using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    #region Injection
    private EventService _eventService;
    
    public void Inject(EventService eventService)
    {
        _eventService = eventService;
    }
    #endregion

    [SerializeField]
    private InputAction _moveAction;
    [SerializeField]
    private InputAction _rotateAction;
    [SerializeField]
    private InputAction _cannonAction;
    [SerializeField]
    private InputAction _laserAction;

    private void Start()
    {
        _moveAction.performed += HandleMove;
        _moveAction.canceled += HandleMove;

        _rotateAction.performed += HandleRotate;
        _rotateAction.canceled += HandleRotate;

        _cannonAction.performed += HandleCannon;
        _laserAction.performed += HandleLaser;
    }

    private void HandleMove(InputAction.CallbackContext context)
    {
        _eventService.SendMessage(new MoveEvent(context.performed));
    }
    
    private void HandleRotate(InputAction.CallbackContext context)
    {
        _eventService.SendMessage(new RotateEvent(context.ReadValue<Vector2>()));
    }

    private void HandleCannon(InputAction.CallbackContext context)
    {
        _eventService.SendMessage(new ShootEvent(0));
    }

    private void HandleLaser(InputAction.CallbackContext context)
    {
        _eventService.SendMessage(new ShootEvent(1));
    }

    private void OnEnable()
    {
        _moveAction.Enable();
        _rotateAction.Enable();
        _cannonAction.Enable();
        _laserAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _rotateAction.Disable();
        _cannonAction.Disable();
        _laserAction.Disable();
    }
}
