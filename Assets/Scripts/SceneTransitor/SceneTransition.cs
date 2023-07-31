using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private Coroutine _sceneLoadCoroutine;
    private readonly int _startTransition = Animator.StringToHash("Start");
    private readonly int _endTransition = Animator.StringToHash("End");
    private readonly WaitForSeconds _waitForSecond = new(1);

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string scene)
    {
        StopLoadScene();
        _sceneLoadCoroutine = StartCoroutine(TransitScene(scene));
    }

    private void StopLoadScene()
    {
        if (_sceneLoadCoroutine != null) StopCoroutine(_sceneLoadCoroutine);
    }

    private IEnumerator TransitScene(string scene)
    {
        _animator.enabled = true;

        _animator.SetTrigger(_endTransition);
        
        yield return _waitForSecond;

        SceneManager.LoadSceneAsync(scene);

        _animator.SetTrigger(_startTransition);
    }
}
