using UnityEngine;
using System.Collections;

namespace RPG.OpenModule.View
{
    public class CameraRotation : MonoBehaviour, IRotate
    {
        [SerializeField] float _rotationSpeed;

        public float getRotationFromAxis()
        {
            float axisValue = Input.GetAxis(InputNames.secondaryHorizontalAxis);

            return axisValue;
        }

        public void rotate(Transform transformToRotate)
        {
            transformToRotate.Rotate(Vector3.up * getRotationFromAxis() * _rotationSpeed);
        }
    }

}
