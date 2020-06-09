using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Entities/Enemy")]
public class EnemyTemplate : ScriptableObject
{
    [Header("Data")]
    public float range = 14f, staggerTime = 1.5f;
}

