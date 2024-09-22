using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoSingleton<TutorialManager>
{
    [SerializeField] private Player _player;
    [SerializeField] private TutorialFailurePanel _tutorialFailurePanel;
    [SerializeField] private TutorialOverPanel _tutorialOverPanel;

    private void Start()
    {
        _player.HealthCompo.OnDieEvent.AddListener(HandlePlayerDie);
    }

    private void HandlePlayerDie()
    {
        
        _tutorialFailurePanel.Open();
    }

    private IEnumerator PlayerDieCoroutine()
    {
        yield return new WaitForSeconds(1f);
    }

    public void Retry()
    {
        SceneManager.LoadScene("TutorialScene");
    }
    
    public void ExitScene() 
    {
        StartCoroutine(ExitCoroutine());
    }

    private IEnumerator ExitCoroutine()
    {
        _tutorialOverPanel.Open();
        yield return new WaitForSeconds(1f);
        UIManager.Instance.Open(WindowEnum.Dark);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("TitleScene");

    }


}
