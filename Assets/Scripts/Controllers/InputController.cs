using UnityEngine;

public class InputController : Controller
{
    private Vector3 _targetPosition;
    private LayerMask _groundLayer;

    private float _timeSinceLastClick = 0f;

    public float TimeSinceLastClick => _timeSinceLastClick;

    public InputController(LayerMask groundLayer)
    {
        _groundLayer = groundLayer;
    }

    public Vector3 TargetPosition => _targetPosition;


    protected override void UpdateLogic(float deltaTime)
    {
        _timeSinceLastClick += deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            _timeSinceLastClick = 0f;
            UpdateTarget();
        }
    }

    private void UpdateTarget()
    {
        Ray ray = GetCameraRay();

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _groundLayer))
        {
            _targetPosition = hitInfo.point;
            _targetPosition.y = 0f;
        }
    }

    private Ray GetCameraRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
