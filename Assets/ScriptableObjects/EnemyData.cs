using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData",menuName = "Create/ScriptableObject")]
public class EnemyData : ScriptableObject
{
    public string Name;
    public GameObject EnemyPrefab;
    public EnemyHealth HealthPrefab;
    public int EnemyHealth;
    public CardType Sequence;
}
