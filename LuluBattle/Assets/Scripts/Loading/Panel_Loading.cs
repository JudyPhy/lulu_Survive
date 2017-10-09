using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Panel_Loading : WindowsBasePanel
{
    private UIProgressBar _progressBar;
    private uint _toProgress;
    private uint _displayProgress;
    private AsyncOperation async;

    public override void OnAwake()
    {
        base.OnAwake();
        _toProgress = 0;
        _progressBar = transform.FindChild("ProgressBar").GetComponent<UIProgressBar>();       
    }

    public override void OnStart()
    {
        StartCoroutine(StartLoading(1));
    }

    private IEnumerator StartLoading(int scene)
    {
        async = SceneManager.LoadSceneAsync("Battle");
        async.allowSceneActivation = false;
        while (async.progress < 0.9f)
        {
            _toProgress = (uint)(async.progress * 100);
            while (_displayProgress < _toProgress)
            {
                _displayProgress++;
                _progressBar.value = _displayProgress / 100.0f;
                yield return new WaitForEndOfFrame();
            }
        }
        _toProgress = 100;
        while (_displayProgress < _toProgress)
        {
            _displayProgress++;
            _progressBar.value = _displayProgress / 100.0f;
            yield return new WaitForEndOfFrame();
        }           
    }

    private void Update()
    {
        if (_progressBar.value == 1 && Player.Instance.HasBorn)
        {
            async.allowSceneActivation = true;
        }
    }

}
