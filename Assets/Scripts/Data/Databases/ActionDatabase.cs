﻿using UnityEngine;
using System.Collections;

namespace RPG.Data
{
    [CreateAssetMenu(menuName = Path.ACTION_DB_SO_MENU_NAME)]
    public class ActionDatabase : Database<Action>
    {
        protected override string elementFolderPath => Path.DATABASE_ELEMENT_BASE_FOLDER + Path.ACTION_NAME;
    }
}
