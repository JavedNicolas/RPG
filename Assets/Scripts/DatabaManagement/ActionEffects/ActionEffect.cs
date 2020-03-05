﻿using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace RPG.DataManagement
{
    public abstract class ActionEffect : ScriptableObject
    {
        public abstract bool execute(Being sender, Being target);
    }
}
