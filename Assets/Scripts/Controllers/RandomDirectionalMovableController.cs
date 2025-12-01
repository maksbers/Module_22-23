using UnityEngine;

public class RandomDirectionalMovableController : Controller
{
    private IDirectionalMovable _movable;

    private float _time;
    private float _timeToChangeDirection;

    private Vector3 _inputDirection;

    public RandomDirectionalMovableController(IDirectionalMovable movable, float timeToChangeDirection)
    {
        _movable = movable;
        _timeToChangeDirection = timeToChangeDirection;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _time += deltaTime;

        if (_time >= _timeToChangeDirection)
        {
            _inputDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));

            _time = 0f;
        }

        _movable.SetMoveDirection(_inputDirection);
    }
}
