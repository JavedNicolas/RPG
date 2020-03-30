using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ITranslation))]
public class TranslationBasedAnimationMovement : MonoBehaviour, IAnimate
{
    ITranslation translation;

    private void Start()
    {
        translation = GetComponent<ITranslation>();
    }

    public void animate(Animator animator, Transform transformToAnimate)
    {
        if(animator != null && transformToAnimate != null)
        {
            float movementSpeedPercent = translation.getDirection().magnitude * translation.getSpeed();
            animator.SetFloat("MovementSpeed", movementSpeedPercent);
        }
    }
}
