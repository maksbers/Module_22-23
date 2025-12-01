using UnityEngine;

public class InputExample : MonoBehaviour
{
    [SerializeField] private Character _character;

    private Controller _characterController;

    private void Awake()
    {
        /*
        _characterController = new CompositeController(
            new RandomDirectionalMovableController(_character, 2f),
            new AlongMovableVelocityRotatableController(_character, _character));
        */

        _characterController = new CompositeController(
            new TargetDirectionalMovableController(_character),
            new AlongMovableVelocityRotatableController(_character, _character));

        _characterController.Enable();
    }

    private void Update()
    {
        _characterController.Update(Time.deltaTime);
    }
}
