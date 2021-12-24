using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct EnemyStats
{
    public int hp;
    public int damage;
    public float moveSpeed;
    public int score;
}

[CreateAssetMenu(fileName ="Default Enemy Stats", menuName ="Enemy/Default Enemy Stats", order = 0)]
public class DefaultEnemyStats : ScriptableObject
{
    public EnemyStats stats;
    public EnemyStats increasePerLevelPercentage;

    public EnemyStats GetResultPercentage(int level)
    {
        int multiplier = level - 1;
        EnemyStats result = new EnemyStats();
        result.hp = Mathf.CeilToInt(stats.hp + (stats.hp * (multiplier * (float) increasePerLevelPercentage.hp / 100f)));
        result.damage = Mathf.CeilToInt(stats.damage + (stats.damage * (multiplier * (float)increasePerLevelPercentage.damage / 100f)));
        result.moveSpeed = Mathf.CeilToInt(stats.moveSpeed + (stats.moveSpeed * (multiplier * (float)increasePerLevelPercentage.moveSpeed / 100f)));
        result.score = Mathf.CeilToInt(stats.score + (stats.score * (multiplier * (float)increasePerLevelPercentage.score / 100f)));

        return result;
    }
}
