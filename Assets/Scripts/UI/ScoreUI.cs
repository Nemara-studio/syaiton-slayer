using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text finalScoreText;

    private void Start()
    {
        // GameLogic.m_LoseEvent.AddListener(FinalScoreUpdate);
    }

    private void Update()
    {
        // time = TimeSpan.FromSeconds(GameLogic.gameScore);

        // scoreText.text = $"{GameLogic.gameScore}";
    }

    private void FinalScoreUpdate()
    {
        // finalScoreText.text = $"Syaiton seng dikalahno : {GameLogic.gameScore}";
    }
}
