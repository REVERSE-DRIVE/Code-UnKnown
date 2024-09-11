using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoSingleton<TutorialManager>
{
    [SerializeField] private Player _player;
    [SerializeField] private TutorialFailurePanel _tutorialFailurePanel;

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
    
    private void ExitScene()
    {
        SceneManager.LoadScene("TitleScene");
    }


}
