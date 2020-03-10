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
        [SerializeField] GameObject _model;
        [SerializeField] List<Action> _actions = new List<Action>();
        [SerializeField] public Sprite _icon;

        public int maxLife => _maxLife; 
        public int currentLife => _currentLife;
        public int attack  => _attack; 
        public int magicAttack => _magicAttack; 
        public int defense => _defense; 
        public int magicDefense  => _magicDefense; 
        public int speed => _speed; 
        public GameObject model  => _model;
        public List<Action> actions => _actions; 
        public Sprite icon => _icon; 

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
            _model = being.model;
            _actions = being.actions;
            _icon = being.icon;
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

