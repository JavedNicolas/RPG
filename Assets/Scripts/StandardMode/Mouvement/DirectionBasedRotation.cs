using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ITranslation))]
public class DirectionBasedRotation : MonoBehaviour,IRotate
{
    ITranslation translation;

    private void Start()
    {
        translation = GetComponent<ITranslation>();
    }

    public Vector3 getMovementDirection()
    {
        return translation.getDirection();
    }

    public void rotate(Transform transformToRotate)
    {
        Vector3 direction = getMovementDirection();

        Vector3 lootAtPosition = transformToRotate.position + direction;
        transformToRotate.LookAt(lootAtPosition);
    }
}
