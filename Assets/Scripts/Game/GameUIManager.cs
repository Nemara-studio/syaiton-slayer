using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager singleton;

    [Header("START UI")]
    public GameObject startedPhaseUI;
    public TMP_Text levelText;
    public GameObject tutorial;

    [Header("PREPARATION PHASE")]
    public GameObject prepPhaseInfo;
    
    public GameObject shopUI;

    [Header("BATTLE PHASE")]
    public Slider healthBar;
    public TMP_Text pointText;
    public GameObject battlePhaseUI;
    public TMP_Text objectiveText;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPrepPhaseUI()
    {
        battlePhaseUI.SetActive(false);
        prepPhaseInfo.SetActive(true);
    }

    public IEnumerator ShowCurrentWaveUI(System.Action onCompleted = null)
    {
        startedPhaseUI.SetActive(true);

        levelText.text = $"WAVE {GameLogic.singleton.currentWave}";

        yield return new WaitUntil(() => startedPhaseUI.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name == "StartLevelFadeOut");
        yield return new WaitForSeconds(startedPhaseUI.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length + 0.1f);

        startedPhaseUI.SetActive(false);

        onCompleted?.Invoke();
    }

    public void ShowTutorial()
    {
        Time.timeScale = 0;
        tutorial.SetActive(true);
    }

    public void CloseTutorial()
    {
        Time.timeScale = 1;
        tutorial.SetActive(false);
    }

    public void SetBattlePhaseUI()
    {
        prepPhaseInfo.SetActive(false);
        battlePhaseUI.SetActive(true);
    }

    public void UpdateObjective(int killed, int toKilled)
    {
        objectiveText.text = $"SYAITON KILLED ({killed} / {toKilled})";
    }

    public void UpdateHealthBar(int currentHealthPoint, int maxHealth)
    {
        healthBar.value = (float) currentHealthPoint / maxHealth;
    }
}
