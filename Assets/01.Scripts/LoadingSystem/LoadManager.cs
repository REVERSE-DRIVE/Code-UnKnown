using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoSingleton<LoadManager>
{
    [SerializeField] private LoadingInfinityScroll[] _loadingInfinityScroll;
    [SerializeField] private LoadUIManager _loadUIManager;
    [SerializeField] private FadeInOut _fadeImage;
    private float _progress = 0f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartLoad(string sceneName, Action callback = null)
    {
        SceneManager.LoadScene("LoadingScene");
        StartCoroutine(LoadingCoroutine(sceneName, callback));
    }

    private IEnumerator LoadingCoroutine(string sceneName, Action callback)
    {
        yield return new WaitForSeconds(1f);
        _loadUIManager = FindObjectOfType<LoadUIManager>();
        _loadingInfinityScroll = FindObjectsOfType<LoadingInfinityScroll>();
        _fadeImage = FindObjectOfType<FadeInOut>();
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        
        while (!operation.isDone)
        {
            _progress = operation.progress;
            _loadUIManager.SetProgress(_progress);
            if (operation.progress >= 0.9f)
            {
                _progress = 1f;
                for (int i = 0; i < 2; i++)
                {
                    _loadingInfinityScroll[i].IsScrolling = false;
                }
                _loadUIManager.SetProgress(_progress);
                yield return new WaitForSeconds(1f);
                _fadeImage.Fade(1f, 1f);
                yield return new WaitForSeconds(1f);
                operation.allowSceneActivation = true;
                
                callback?.Invoke();
            }

            yield return null;
        }
    }
}