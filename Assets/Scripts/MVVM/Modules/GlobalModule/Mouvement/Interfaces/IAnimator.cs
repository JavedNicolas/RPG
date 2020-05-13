using UnityEngine;

namespace RPG.GlobalModule.View
{
    public interface IAnimate
    {
        void animate(Animator animator, Transform transformToAnimate);
    }
}
