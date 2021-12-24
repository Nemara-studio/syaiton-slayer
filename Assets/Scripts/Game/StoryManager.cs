using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager singleton;

    public GameObject storyPanel;
    public Animator anim;

    public List<string> stories;
    public TMP_Text storyText;
    public float timePerDialogue;
    public float fadeTime;

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

    public void BeginStory(System.Action onCompleted = null)
    {
        StartCoroutine(StartStory(onCompleted));
    }

    private IEnumerator StartStory(System.Action onCompleted = null)
    {
        storyPanel.SetActive(true);

        foreach (string story in stories)
        {
            storyText.text = $"{story}";

            anim.SetTrigger("Fade In");

            yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length + 0.1f);

            yield return new WaitForSeconds(timePerDialogue);

            anim.SetTrigger("Fade Out");

            yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length + 0.1f);
        }

        storyPanel.SetActive(false);
        onCompleted?.Invoke();
    }
}
