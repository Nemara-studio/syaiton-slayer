using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string enemyName;

    [Header("UI")]
    public TMP_Text nameUI;
    public Slider healthBar;

    [Header("ENEMY STATS")]
    [SerializeField] protected DefaultEnemyStats defaultStats;

    protected int level;
    protected EnemyStats currentStats;
    protected int currentHP;

    public GameObject pointPlusUI;
    public GameObject dieEffect;
    public SpriteRenderer graphic;
    protected Animator enemyAnim;

    protected Transform enemyTarget;

    protected virtual void Start()
    {
        SetName(enemyName);
        UpdateHP();
    }

    protected virtual void Move() 
    {
        Flip(enemyTarget.position.x < transform.position.x);
    }

    protected virtual void Flip(bool left)
    {
        graphic.flipX = left;
    }

    public void SetStat(int _level)
    {
        level = _level;
        currentStats = defaultStats.GetResultPercentage(level);
        currentHP = currentStats.hp;
    }

    protected virtual void Die()
    {
        // Instantiate die effect
        Instantiate(dieEffect, transform.position, Quaternion.identity);

        // instantiate point plus ui
        GameObject pointPlus = Instantiate(pointPlusUI, transform.position, Quaternion.identity);
        pointPlus.GetComponent<PointPlusUI>().SetText(currentStats.score.ToString());

        // set data
        if (GameLogic.singleton != null)
        {
            GameLogic.singleton.enemyKilled++;
            GameLogic.singleton.AddPoint(currentStats.score);
        }

        Destroy(this.gameObject);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
        }

        UpdateHP();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log($"Player terserang");
            collision.GetComponent<Player>().TakeDamage(currentStats.damage);
            GameLogic.singleton.enemyKilled++;
            Destroy(gameObject);
        }
    }

    #region Update UI

    protected void UpdateHP()
    {
        healthBar.value = (float) currentHP / (float) currentStats.hp;
    }

    protected void SetName(string _name)
    {
        nameUI.text = $"{_name}";
    }

    #endregion
}
