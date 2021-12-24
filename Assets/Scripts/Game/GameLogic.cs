using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public static GameLogic singleton;

    public enum GameState
    {
        STORY_PHASE,
        PREP_PHASE,
        BATTLE_PHASE,
        END_PHASE
    }

    [Header("GAME DATA")]
    public int point;

    [Header("LEVEL DATA")]
    public int currentWave = 1;
    public int enemyToSpawn;
    public int enemyKilled;
    public int enemySpawned;
    public int increasedEnemy;

    public UnityEvent m_LoseEvent;
    public UnityEvent m_StartEvent;

    public GameState gameState;

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

        if (m_LoseEvent == null)
            m_LoseEvent = new UnityEvent();

        if (m_StartEvent == null)
            m_StartEvent = new UnityEvent();
    }

    private void Start()
    {
        point = 0;
        currentWave = 1;

        // set ui
        GameUIManager.singleton.pointText.text = $"{point}";

        // start the story
        gameState = GameState.STORY_PHASE;
        StoryManager.singleton.BeginStory(StartPrepPhase);
    }

    private void Update()
    {
        if (gameState == GameState.PREP_PHASE && Input.GetKeyDown(KeyCode.P))
        {
            StartBattlePhase();
        }

        if (gameState == GameState.BATTLE_PHASE)
        {
            GameUIManager.singleton.UpdateObjective(enemyKilled, enemyToSpawn);

            if (enemySpawned >= enemyToSpawn && EnemySpawnManager.singleton.isSpawning)
            {
                EnemySpawnManager.singleton.StopSpawn();
            }

            if (enemyKilled >= enemyToSpawn)
            {
                GameWin();
            }
        }
    }

    private void StartPrepPhase()
    {
        Debug.Log("Start PREP PHASE");
        gameState = GameState.PREP_PHASE;
        StartCoroutine(GameUIManager.singleton.ShowCurrentWaveUI(() => GameUIManager.singleton.SetPrepPhaseUI()));
    }

    public void StartBattlePhase()
    {
        if (currentWave == 1)
        {
            GameUIManager.singleton.ShowTutorial();
        }

        gameState = GameState.BATTLE_PHASE;

        EnemySpawnManager.singleton.StartSpawn(currentWave);

        GameUIManager.singleton.SetBattlePhaseUI();
        Debug.Log("GAME START!");
    }

    public void GameWin()
    {
        Debug.Log($"Game Win");
        gameState = GameState.END_PHASE;

        NextLevel();
        // TODO: SETUP WIN UI END PHASE
    }

    public void NextLevel()
    {
        currentWave++;
        enemyKilled = 0;
        enemySpawned = 0;
        enemyToSpawn += increasedEnemy;

        StartPrepPhase();
    }

    public void AddPoint(int amount)
    {
        point += amount;
        GameUIManager.singleton.pointText.text = $"{point}";
    }
}
