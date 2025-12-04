using UnityEngine;
using UnityEngine.AI;

public class TargetDirectionalMovableController : Controller
{
    private const int MinCornersInPathToMove = 2;
    private const int StartCornerIndex = 0;
    private const int TargetCornerIndex = 1;

    private float _minDistanceToTarget = 0.2f;

    private InputController _inputController;
    private IDirectionalMovable _movable;
    private Vector3 _currentTarget;

    private NavMeshQueryFilter _queryFilter;
    private NavMeshPath _pathToTarget = new NavMeshPath();

    private bool _isMoving;

    public TargetDirectionalMovableController(
        IDirectionalMovable movable,
        InputController inputController,
        NavMeshQueryFilter queryFilter)
    {
        _movable = movable;
        _inputController = inputController;
        _queryFilter = queryFilter;

        _currentTarget = _inputController.TargetPosition;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (IsTargetChanged())
        {
            _currentTarget = _inputController.TargetPosition;
            _isMoving = true;
        }

        if (_isMoving == false)
            return;

        if (NavMeshUtils.TryGetPath(_movable.Position, _currentTarget, _queryFilter, _pathToTarget) == false)
            return;

        if (EnoughCornersInPath(_pathToTarget))
        {
            _movable.SetMoveDirection(_pathToTarget.corners[TargetCornerIndex] - _pathToTarget.corners[StartCornerIndex]);
        }

        float distanceToTarget = NavMeshUtils.GetPathLength(_pathToTarget);

        if (IsTargetReached(distanceToTarget))
        {
            _movable.SetMoveDirection(Vector3.zero);
            _isMoving = false;
            return;
        }
    }

    private bool IsTargetChanged() => _inputController.TargetPosition != _currentTarget;

    private bool EnoughCornersInPath(NavMeshPath pathToTarget) => pathToTarget.corners.Length >= MinCornersInPathToMove;

    private bool IsTargetReached(float distanceToTarget) => distanceToTarget <= _minDistanceToTarget;

}
