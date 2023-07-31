using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapState : IState<Bootstrap>, IEnterable, IExitable
{
    public Bootstrap Initializer { get; }

    private AsyncOperation _asyncUnload;

    public BootstrapState(Bootstrap initializer)
    {
        Initializer = initializer;
    }

    public void OnEnter()
    {
        Debug.Log("State 1 Entered");
        
        Initializer.StateMachine.SwitchState<InitialState>();
    }

    public void OnExit()
    {
        _asyncUnload = SceneManager.UnloadSceneAsync("BootstrapScene");
        _asyncUnload.completed += OnSceneUnloaded;
    }

    private void OnSceneUnloaded(AsyncOperation asyncOperation)
    {
        Debug.Log("State 1 Exited");
        _asyncUnload.completed -= OnSceneUnloaded;
    }
}