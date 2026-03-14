using UnityEngine;
using UnityEngine.InputSystem;

public class BalllController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private BaseBall _currentBall;

    [SerializeField] private GameObject _cameraHolder;
    [SerializeField] private Transform[] _allCameraPossitions;
    private int _currentViewIndex = 0;

    private BallContext _ctx;
    private bool _isActive;

    private CountdownTimer _countdownTimer;
    private void Start()
    {
        _ctx = new BallContext();
        _ctx.Rb = _rb;
        _ctx.Transform = transform;
        _ctx.TargetPossition = Vector3.zero;

        InputManager.Instance.InputActions.Player.Start.performed += OnStart;
        InputManager.Instance.InputActions.Player.Move.performed += MoveBall;
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
    private void MoveBall(InputAction.CallbackContext context)
    {
        if (_isActive == false) return;

        Vector3 forwordVel = _cameraHolder.transform.forward * context.ReadValue<Vector2>().y;
        Vector3 rightVel = _cameraHolder.transform.right * context.ReadValue<Vector2>().x;

        Vector3 FinalVel = forwordVel + rightVel;
        _ctx.Transform.position += FinalVel;
    }
    private void ChangeView(InputAction.CallbackContext context)
    {
        if (!_isActive) return;

        _currentViewIndex = _currentViewIndex >= _allCameraPossitions.Length - 1 ? 0 : _currentViewIndex + 1;
        
        _cameraHolder.transform.position = _allCameraPossitions[_currentViewIndex].position;
        _cameraHolder.transform.rotation = _allCameraPossitions[_currentViewIndex].rotation;
    }
}
