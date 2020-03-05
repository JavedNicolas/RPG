using UnityEngine;
using System.Collections;

public interface IBattleAction 
{
    string getLocalisationKey();
    bool execute(Being sender, Being target);
    
}
