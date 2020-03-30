using UnityEngine;
using System.Collections;
using Unity.Entities;

namespace RPG.Environnement.Entities
{
    public struct BendWhenCollidedComponent : IComponentData
    {
        public string tag;
        public string materialBendFactorName;
        public Vector2 clampBend;
        public float moveToPositionDuration;
        public float moveBackToPositionDuration;
    }

}
