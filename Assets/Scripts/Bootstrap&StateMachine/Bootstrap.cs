using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    public StateMachine<Bootstrap> StateMachine { get; private set; }
    public SceneTransition SceneTransition { get => _sceneTransitionPrefab; }

    [SerializeField] private SceneTransition _sceneTransitionPrefab;
    
    private void Start()
    {
        StateMachine = new StateMachine<Bootstrap>(
            new BootstrapState(this),
            new InitialState(this),
            new LoadingState(this));

        Instantiate(_sceneTransitionPrefab);

        StateMachine.SwitchState<BootstrapState>();
    }
}