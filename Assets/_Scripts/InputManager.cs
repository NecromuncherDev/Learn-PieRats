using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)]
public class InputManager : Singleton<InputManager>
{
    #region Events
    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;

    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;
    #endregion

    private TouchControls touchControls;
    private Camera cameraMain;
    private Vector2 primaryPosition { get => touchControls.Touch.PrimaryPosition.ReadValue<Vector2>(); }

    protected override void Awake()
    {
        base.Awake();
        touchControls = new TouchControls();
        cameraMain = Camera.main;
    }

    private void Start()
    {
        touchControls.Touch.PrimaryContact.started += ctx => StartTouch(ctx);
        touchControls.Touch.PrimaryContact.canceled += ctx => EndTouch(ctx);

    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        //print("Touch started: "+  primaryPosition);
        OnStartTouch?.Invoke(Utils.ScreenToWorld2D(cameraMain, primaryPosition), (float)context.startTime);
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        //print("Touch ended");
        OnEndTouch?.Invoke(Utils.ScreenToWorld2D(cameraMain, primaryPosition), (float)context.time);
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld2D(cameraMain, primaryPosition);
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }
}
