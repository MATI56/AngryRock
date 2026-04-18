using UnityEngine;
using UnityEngine.InputSystem;

public class BalllController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private BaseBall _currentBall;

    [SerializeField] private GameObject _ropePoint;
    [SerializeField] private GameObject _cameraHolder;
    [SerializeField] private Transform[] _allCameraPossitions;
    private int _currentViewIndex = 0;

    private BallContext _ctx;
    private bool _isActive;

    private CountdownTimer _countdownTimer;

    private Plane _currentMovemntPlane;
    private bool _isDraging;
    private void Start()
    {
        _ctx = new BallContext();
        _ctx.Rb = _rb;
        _ctx.Transform = transform;
        _ctx.TargetPossition = Vector3.zero;

        InputManager.Instance.InputActions.Player.Start.performed += OnStart;
        InputManager.Instance.InputActions.Player.Drag.performed += MoveBall;
        InputManager.Instance.InputActions.Player.LeftClick.performed += OnMouseDownInput;
        InputManager.Instance.InputActions.Player.LeftClick.canceled += OnMouseUpInput;

        InputManager.Instance.InputActions.Player.ChangeView.performed += ChangeView;

        _cameraHolder.transform.position = _allCameraPossitions[_currentViewIndex].position;
        _cameraHolder.transform.rotation = _allCameraPossitions[_currentViewIndex].rotation;

        _isActive = true;

        _countdownTimer = new CountdownTimer(_ctx.LifeSeconds);
        _countdownTimer.OnTimerStop += Finish;
    }
    public void Update()
    {
        if (_isActive == false) return;

        _countdownTimer.Tick(Time.deltaTime);
        _currentBall.OnUpdate(_ctx);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (_isActive == false) return;

        _currentBall.OnHitEffect(_ctx, collision);
    }
    private void OnStart(InputAction.CallbackContext context)
    {  
        _currentBall.OnStart(_ctx);
        _countdownTimer.SetInitialTime(_ctx.LifeSeconds);
        _countdownTimer.Restart();
    }
    private void Finish()
    {

        _currentBall.OnStop(_ctx);
    }
    private void OnMouseDownInput(InputAction.CallbackContext context)
    {
        _currentMovemntPlane = new Plane(Camera.main.transform.forward, _rb.position);
        _isDraging = true;
    }
    private void OnMouseUpInput(InputAction.CallbackContext context)
    {
        _isDraging = false;
    }
    private void MoveBall(InputAction.CallbackContext context)
    {
        if( _isDraging == false) return;

        Ray ray = Camera.main.ScreenPointToRay(context.ReadValue<Vector2>());

        if(_currentMovemntPlane.Raycast(ray, out float enter))
        {
            if (Vector3.Distance(ray.GetPoint(enter), _ropePoint.transform.position) < 5f)
            {
                _rb.MovePosition(ray.GetPoint(enter));
                _rb.transform.LookAt(_ropePoint.transform.position);
            }
        }
    }
    private void ChangeView(InputAction.CallbackContext context)
    {
        if (!_isActive) return;

        _currentViewIndex = _currentViewIndex >= _allCameraPossitions.Length - 1 ? 0 : _currentViewIndex + 1;
        
        _cameraHolder.transform.position = _allCameraPossitions[_currentViewIndex].position;
        _cameraHolder.transform.rotation = _allCameraPossitions[_currentViewIndex].rotation;
    }
}
