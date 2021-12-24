using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameScene;
    private Animator mainMenuAnim;

    [Header("Tutorial")]
    private int inTutor = 0;
    public List<GameObject> tutor;

    private void Start()
    {
        mainMenuAnim = GetComponent<Animator>();
    }

    public void Play()
    {
        SceneManager.LoadScene(gameScene, LoadSceneMode.Single);
    }

    public void ToCredit()
    {
        mainMenuAnim.SetTrigger("ToCredit");
    }

    public void ToMenu()
    {
        mainMenuAnim.SetTrigger("ToMenu");
    }

    public void ToTutor()
    {
        tutor[inTutor].SetActive(true);
        mainMenuAnim.SetTrigger("ToTutor");
    }

    public void NextTutor()
    {
        inTutor += 1;
        if (inTutor < tutor.Count)
        {
            tutor[inTutor - 1].SetActive(false);
            tutor[inTutor].SetActive(true);
        }

        if (inTutor >= tutor.Count)
        {
            tutor[inTutor - 1].SetActive(false);
            inTutor = 0;

            ToMenu();
        }
    }

    public void BackTutor()
    {
        inTutor -= 1;
        if (inTutor >= 0)
        {
            tutor[inTutor + 1].SetActive(false);
            tutor[inTutor].SetActive(true);
        }

        if (inTutor < 0)
        {
            inTutor = 0;
            ToMenu();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    
}
