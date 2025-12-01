using UnityEngine;

public class Character : MonoBehaviour, IDirectionalMovable, IDirectionalRotatable
{
    private DirectionalMover _mover;
    private DirectionalRotator _rotator;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private LayerMask _groundLayer;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;
    public Quaternion CurrentRotation => _rotator.CurrentRotation;
    public Vector3 CurrentPosition => transform.position;
    public LayerMask GroundLayer => _groundLayer;


    private void Awake()
    {
        _mover = new DirectionalMover(GetComponent<CharacterController>(), _moveSpeed);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);
    }

    private void Update()
    {
        _mover.Update(Time.deltaTime);
        _rotator.Update(Time.deltaTime);
    }

    public void SetMoveDirection(Vector3 inputDirection) => _mover.SetInputDirection(inputDirection);
    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);
}
