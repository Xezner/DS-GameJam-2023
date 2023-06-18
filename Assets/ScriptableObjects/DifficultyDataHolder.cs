using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyData", menuName = "Create/DifficultyDataHolder")]
public class DifficultyDataHolder : ScriptableObject
{
    public int AttackRate;
    public int RandomRate;
    public int MedicardRate;
    public int EnemyHealth;
    public int GameSpeed;
    public int GameTime = 30;
    public EnemyData EnemyData;
    public Difficulty DifficultyLevel;
}

public enum Difficulty
{
    Novice,
    Expert,
    Legend
}
