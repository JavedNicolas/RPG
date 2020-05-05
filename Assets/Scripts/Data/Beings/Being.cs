using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.Data
{
    [System.Serializable]
    public class Being : DatabaseElement
    {
        [SerializeField] int _maxLife;
        [HideInInspector]
        private int _currentLife;
        [SerializeField] int _attack;
        [SerializeField] int _magicAttack;
        [SerializeField] int _defense;
        [SerializeField] int _magicDefense;
        [SerializeField] int _speed;
        [SerializeField] GameObject _standarModeModel;
        [SerializeField] GameObject _dungeonModeModel;
        [SerializeField] GameObject _battleModel;
        [SerializeField] List<Action> _actions = new List<Action>();

        public int maxLife => _maxLife; 
        public int currentLife => _currentLife;
        public int attack  => _attack; 
        public int magicAttack => _magicAttack; 
        public int defense => _defense; 
        public int magicDefense  => _magicDefense; 
        public int speed => _speed; 
        public GameObject standarModeModel  => _standarModeModel;
        public GameObject dungeonModeModel => _dungeonModeModel;
        public GameObject battleModel => _battleModel;
        public List<Action> actions => _actions; 

        public override void initEmpty()
        {
            base.initEmpty();
            _maxLife = 0;
            _currentLife = 0;
            _attack = 0;
            _magicAttack = 0;
            _defense = 0;
            _magicDefense = 0;
            _speed = 0;
            _standarModeModel = null;
            _dungeonModeModel = null;
            _battleModel = null;
            _actions = null;
        }

        public void init(Being being)
        {
            base.init(being);
            _maxLife = being.maxLife;
            _currentLife = being.maxLife;
            _attack = being.attack;
            _magicAttack = being.magicAttack;
            _defense = being.defense;
            _magicDefense = being.magicDefense;
            _speed = being.speed;
            _standarModeModel = being.standarModeModel;
            _dungeonModeModel = being.dungeonModeModel;
            _battleModel = being.battleModel;
            _actions = being.actions;
        }


        /// <summary> Deal damage to the bein, if the value passed is negatif heal the being </summary>
        /// <param name="value"> The value to remove from the being</param>
        public void damage(int value)
        {
            _currentLife = Mathf.Clamp(_currentLife - value, 0, _maxLife);
        }

        public bool isDead()
        {
            return currentLife <= 0 ? true : false;
        }
    }
}

