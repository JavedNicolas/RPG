using UnityEngine;

namespace RPG.OpenModule.View
{
    public interface IAnimate
    {
        void animate(Animator animator, Transform transformToAnimate);
    }
}
