using UnityEngine;
using System.Collections;

namespace RPG.Data
{
    public interface IAction
    {
        string getLocalisationKey();
        bool execute(Being sender, Being target);

    }
}
