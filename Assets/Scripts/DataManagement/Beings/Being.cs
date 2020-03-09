using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DataManagement
{
    [System.Serializable]
    public class Being : DatabaseElement
    {
        [SerializeField] float _maxLife;
        [HideInInspector]
        private float _currentLife;
        [SerializeField] float _attack;
        [SerializeField] float _magicAttack;
        [SerializeField] float _defense;
        [SerializeField] float _magicDefense;
        [SerializeField] float _speed;
        [SerializeField] GameObject _model;
        [SerializeField] List<Action> _actions = new List<Action>();
        [SerializeField] public Sprite _icon;

        public float maxLife { get => _maxLife; }
        public float currentLife { get => _currentLife; }
        public float attack { get => _attack; }
        public float magicAttack { get => _magicAttack; }
        public float defense { get => _defense; }
        public float magicDefense { get => _magicDefense; }
        public float speed { get => _speed; }
        public GameObject model { get => _model; }
        public List<Action> actions { get => _actions; }
        public Sprite icon { get => _icon; }

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


        /// <summary> Deal damage to the bein, value passed is negatif heal the being </summary>
        /// <param name="value"> The value to remove from the being</param>
        public void damage(float value)
        {

        }

        /// <summary>Give the being his max action point</summary>
        /// <param name="malus">a malus to apply</param>
        public void resetActionPoint(float malus)
        {

        }


        /// <summary>Add action points</summary>
        /// <param name="actionPoint"></param>
        public void giveActionPoint(float actionPoint)
        {

        }

        public bool isDead()
        {
            return currentLife == 0 ? true : false;
        }
    }
}

