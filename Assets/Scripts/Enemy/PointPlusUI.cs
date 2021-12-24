using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointPlusUI : MonoBehaviour
{
    public TMP_Text pointText;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Show");
    }

    public void SetText(string point)
    {
        pointText.text = $"+{point}";
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
