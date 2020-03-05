using UnityEngine;
using System.Collections;

namespace RPG.DataManagement
{
    public interface IAction
    {
        string getLocalisationKey();
        bool execute(Being sender, Being target);

    }
}
