using UnityEngine;
using System.Collections;

namespace RPG.DataModule
{
    public interface IAction
    {
        string getLocalisationKey();
        bool execute(Being sender, Being target);

    }
}
