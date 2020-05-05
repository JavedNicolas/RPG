using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ITranslation))]
public class DirectionBasedRotation : MonoBehaviour,IRotate
{
    ITranslation translation;
    Vector3 oldPosition = Vector3.zero;

    private void Start()
    {
        translation = GetComponent<ITranslation>();
    }

    public Vector3 getMovementDirection(Transform transformToRotate)
    {
        return translation.getDirection();
    }

    public void rotate(Transform transformToRotate)
    {
        Vector3 direction = getMovementDirection(transformToRotate);

        Vector3 lootAtPosition = transformToRotate.position + direction;
        transformToRotate.LookAt(lootAtPosition);
    }
}
