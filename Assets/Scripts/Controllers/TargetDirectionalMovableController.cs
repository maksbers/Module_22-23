using UnityEngine;

public class TargetDirectionalMovableController : Controller
{
    private IDirectionalMovable _movable;

    public TargetDirectionalMovableController(IDirectionalMovable movable)
    {
        _movable = movable;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.y = 0f;

        Vector3 directionToTarget = (targetPosition - _movable.CurrentPosition).normalized;
        _movable.SetMoveDirection(directionToTarget);
    }
}
