using UnityEngine;

public class LoadingState : IState<Bootstrap>, IEnterable, IExitable
{
    public Bootstrap Initializer { get; }

    public LoadingState(Bootstrap initializer)
    {
        Initializer = initializer;
    }

    public void OnEnter()
    {
        Initializer.SceneTransition.LoadScene("LoadingScene");
    }

    public void OnExit()
    {
        Debug.Log("State 3 Exited");
    }
}