using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private Coroutine _sceneLoadCoroutine;
    private readonly int _crossFadeStart = Animator.StringToHash("Start");
    private readonly int _crossFadeEnd = Animator.StringToHash("End");
    private readonly WaitForSeconds _waitForSecond = new(1);
    private AsyncOperation _sceneLoadOperation;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string scene, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        StopLoadScene();
        _sceneLoadCoroutine = StartCoroutine(TransitScene(scene, loadSceneMode));
    }

    private void StopLoadScene()
    {
        if (_sceneLoadCoroutine != null) StopCoroutine(_sceneLoadCoroutine);
    }

    private IEnumerator TransitScene(string scene, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        _animator.enabled = true;

        _animator.SetTrigger(_crossFadeEnd);
        
        yield return _waitForSecond;

        _sceneLoadOperation = SceneManager.LoadSceneAsync(scene, loadSceneMode);
        _sceneLoadOperation.completed += CrossFadeStart;
    }

    private void CrossFadeStart(AsyncOperation operation)
    {
        _animator.SetTrigger(_crossFadeStart);
        _sceneLoadOperation.completed -= CrossFadeStart;
    }
}
