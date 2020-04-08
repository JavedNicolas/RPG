using UnityEngine;
using System.Collections;

namespace RPG.State
{
    public abstract class State
    {
        public abstract void start();
        public abstract void execute();
        public abstract void end();
    }
}

