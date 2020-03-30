using UnityEngine;
using System.Collections;

public class InputAxisBasedTranslation : MonoBehaviour, ITranslation
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _accelerationTime;

    Transform _tranformToMove;

    public Vector3 getDirection()
    {
        float _horizontal = Input.GetAxisRaw(InputNames.mainHorizontalAxis);
        float _vertical =  Input.GetAxisRaw(InputNames.mainVerticalAxis);

        return new Vector3(_horizontal, 0, _vertical).normalized;
    }

    public float getSpeed()
    {
        return _moveSpeed;
    }

    public void move(Transform transformToMove)
    {
        transformToMove.position += getDirection() * Time.deltaTime * getSpeed();
    }
}
