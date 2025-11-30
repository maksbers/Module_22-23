using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private float _runningThreshold = 0.1f;
    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");

    [SerializeField] private Animator _animator;
    [SerializeField] private Character _character;

    private void Update()
    {
        if (_character.CurrentVelocity.magnitude > _runningThreshold)
            StartRunning();
        else
            StopRunning();
    }

    private void StartRunning()
    {
        _animator.SetBool(IsRunningKey, true);
    }

    private void StopRunning()
    {
        _animator.SetBool(IsRunningKey, false);
    }

}
