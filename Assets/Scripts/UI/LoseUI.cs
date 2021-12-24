using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseUI : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;
    [SerializeField] private string menuScene;

    private void Start()
    {
        losePanel.SetActive(false);
        // GameLogic.m_LoseEvent.AddListener(StartLoseUI);
    }

    private void StartLoseUI()
    {
        if (losePanel != null)
            losePanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackMenu()
    {
        SceneManager.LoadScene(menuScene);
    }
}
