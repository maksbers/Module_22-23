using UnityEngine;

public class TargetDirectionalMovableController : Controller
{
    private IDirectionalMovable _movable;
    private InputController _inputController;

    private Vector3 _currentTarget;
    private bool _isMoving;


    public TargetDirectionalMovableController(IDirectionalMovable movable, InputController inputController)
    {
        _movable = movable;
        _inputController = inputController;

        _currentTarget = _inputController.TargetPosition;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (_inputController.TargetPosition != _currentTarget)
        {
            _currentTarget = _inputController.TargetPosition;
            _isMoving = true;
        }

        if (_isMoving == false)
        {
            return;
        }

        Vector3 directionToTarget = CalculateDirectionToTarget();
        _movable.SetMoveDirection(directionToTarget);

        float distanceToTarget = CalculateDistanceToTarget();

        if (distanceToTarget < 0.2f)
        {
            _movable.SetMoveDirection(Vector3.zero);
            _isMoving = false;
        }
    }

    private float CalculateDistanceToTarget()
    {
        return Vector3.Distance(_movable.CurrentPosition, _currentTarget);
    }

    private Vector3 CalculateDirectionToTarget()
    {
        return (_currentTarget - _movable.CurrentPosition).normalized;
    }
}
