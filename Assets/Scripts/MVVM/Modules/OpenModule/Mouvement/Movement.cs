using UnityEngine;
using System.Collections;

namespace RPG.OpenModule.View
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] Transform _transformToMove;
        [SerializeField] Animator _animator;

        private ITranslation _translation;
        private IRotate _rotate;
        private IAnimate _animate;

        public void Start()
        {
            this._translation = GetComponent<ITranslation>();
            this._rotate = GetComponent<IRotate>();
            this._animate = GetComponent<IAnimate>();
        }

        private void Update()
        {
            _translation?.move(_transformToMove);
            _rotate?.rotate(_transformToMove);
            _animate?.animate(_animator, _transformToMove);
        }
    }

}
