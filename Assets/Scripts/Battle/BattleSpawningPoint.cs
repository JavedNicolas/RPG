using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using RPG.Data;
using RPG.Data.Team;

namespace RPG.Battle
{
    public class BattleSpawningPoint : MonoBehaviour
    {
        [Tooltip("True if the spawn in in the front row")]
        [SerializeField] public bool isFrontSpawn;
        [EnumToggleButtons]
        [SerializeField] public BattlePosition position;
        [SerializeField] public Being actor = null;
        [SerializeField] public GameObject actorGameObject;

        private void Update()
        {
            if (actor != null && actorGameObject != null)
            {
                Animator animator = actorGameObject.GetComponent<Animator>();
                bool isDead = actor.isDead();
                if (animator.GetBool("Dead") != isDead)
                    animator.SetBool("Dead", isDead);
            }
        }
    }
}

