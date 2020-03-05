using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
[CreateAssetMenu(menuName = Path.CHARACTER_SO_MENU_NAME)]
public class Character : Being
{
    public void init(Character character)
    {
        base.init(character);
    }
}
