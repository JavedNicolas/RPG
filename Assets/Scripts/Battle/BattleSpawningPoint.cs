﻿using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

public class BattleSpawningPoint : MonoBehaviour
{
    [Tooltip("True if the spawn in in the front row")]
    [SerializeField] public bool isFrontSpawn;
    [EnumToggleButtons]
    [SerializeField] public BattlePosition position;
    [SerializeField] public Being actor = null;
    [SerializeField] public GameObject actorGameObject;
}
