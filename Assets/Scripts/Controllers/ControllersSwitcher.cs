using UnityEngine;
using UnityEngine.AI;

public class ControllersSwitcher : MonoBehaviour
{
    [SerializeField] private Character _character;

    private Controller _manualCharacterController;
    private Controller _autoCharacterController;
    private Controller _currentController;

    private InputController _inputController;

    private float _switchTime = 3f;


    private void Awake()
    {
        _inputController = new InputController(_character.GroundLayer);

        _autoCharacterController = new CompositeController(
            new RandomDirectionalMovableController(_character, 2f),
            new AlongMovableVelocityRotatableController(_character, _character));

        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = NavMesh.AllAreas;

        _manualCharacterController = new CompositeController(
            new TargetDirectionalMovableController(_character, _inputController, queryFilter),
            new AlongMovableVelocityRotatableController(_character, _character));

        _inputController.Enable();
        _manualCharacterController.Enable();
        _autoCharacterController.Enable();
    }

    private void Update()
    {
        _inputController.Update(Time.deltaTime);

        if (IsManualControlActive())
            _currentController = _manualCharacterController;
        else
            _currentController = _autoCharacterController;

        _currentController.Update(Time.deltaTime);
    }

    private bool IsManualControlActive() => _inputController.TimeSinceLastClick < _switchTime;
}
