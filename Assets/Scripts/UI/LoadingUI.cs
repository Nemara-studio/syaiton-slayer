using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingUI : MonoBehaviour
{
    public float waitTime = 3;
    private float currentTime;

    [SerializeField] private GameObject loadingPanel;
    private Animator loadingAnim;

    private void Start()
    {
        // GameLogic.m_StartEvent.AddListener(StartGame);
        loadingAnim = loadingPanel.GetComponent<Animator>();
        loadingPanel.SetActive(true);
        currentTime = waitTime;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            loadingAnim.SetTrigger("Loading-end");
        }
    }

    private void StartGame()
    {
        if (loadingPanel != null)
            loadingPanel.SetActive(false);
    }
}
