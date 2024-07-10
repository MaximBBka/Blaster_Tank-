using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Game/Data/Enemy")]
public class SOEnemy : ScriptableObject
{
    public ModelEnemy modelEnemy;
}

[Serializable]
public struct ModelEnemy
{
    public EnemyUnit Prefab;
    public float Health;
    public float Speed;
    public SOEnemy[] enemy;
    public float HeightFindLine;
    public Rewarded[] prefabRewarded;
}