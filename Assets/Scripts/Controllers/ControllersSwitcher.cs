using UnityEngine;
using UnityEngine.AI;

public class ControllersSwitcher : MonoBehaviour
{
    [SerializeField] private Character _character;

    private Controller _characterController;

    private InputController _inputController;

    private NavMeshPath _path;

    private void Awake()
    {
        _path = new NavMeshPath();

        _inputController = new InputController(_character.GroundLayer);

        /*
        _characterController = new CompositeController(
            new RandomDirectionalMovableController(_character, 2f),
            new AlongMovableVelocityRotatableController(_character, _character));
        */

        _characterController = new CompositeController(
            new TargetDirectionalMovableController(_character, _inputController),
            new AlongMovableVelocityRotatableController(_character, _character));

        _inputController.Enable();
        _characterController.Enable();
    }

    private void Update()
    {
        _inputController.Update(Time.deltaTime);
        _characterController.Update(Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = NavMesh.AllAreas;

        NavMesh.CalculatePath(_character.CurrentPosition, _inputController.TargetPosition, queryFilter, _path);

        if (_path.status != NavMeshPathStatus.PathInvalid)
        {
            foreach (Vector3 corner in _path.corners)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(corner, 0.2f);
            }
        }
    }
}
