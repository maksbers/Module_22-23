using UnityEngine;

public class TargetDirectionalMovableController : Controller
{
    private IDirectionalMovable _movable;

    private Vector3 _targetPosition;
    private bool _hasTarget = false;

    private LayerMask _groundLayer;


    public TargetDirectionalMovableController(IDirectionalMovable movable, LayerMask groundLayer)
    {
        _movable = movable;
        _groundLayer = groundLayer;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _groundLayer))
            {
                _targetPosition = hitInfo.point;
                _targetPosition.y = 0f;

                _hasTarget = true;
            }
        }

        if (_hasTarget)
        {
            Debug.DrawLine(_movable.CurrentPosition, _targetPosition, Color.red);
            Debug.DrawRay(_targetPosition, Vector3.up * 2f, Color.green);
        }

        if (_hasTarget == false)
            return;

        Vector3 directionToTarget = (_targetPosition - _movable.CurrentPosition).normalized;
        _movable.SetMoveDirection(directionToTarget);

        float distanceToTarget = Vector3.Distance(_movable.CurrentPosition, _targetPosition);

        if (distanceToTarget < 0.2f)
        {
            _movable.SetMoveDirection(Vector3.zero);
            _hasTarget = false;
        }
    }
}
