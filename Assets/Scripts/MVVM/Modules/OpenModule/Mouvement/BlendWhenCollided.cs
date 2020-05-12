using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace RPG.OpenModule.View
{
    public class BlendWhenCollided : MonoBehaviour
    {
        [SerializeField] string _tag;
        [SerializeField] string _materialBendFactorName = "Bend_Factor";
        [SerializeField] Vector2 _clampBend = new Vector2(-1, 1);
        [SerializeField] float _moveToPositionDuration = 0.5f;
        [SerializeField] float _moveBackToPositionDuration = 3f;
        Material _material;
        bool _hasExited = false;
        Vector3 velocity = Vector3.one;

        private void Start()
        {
            _material = gameObject.GetComponent<MeshRenderer>().material;
        }

        private void Update()
        {
            if(_hasExited)
            {
                bend(Vector3.zero, _moveBackToPositionDuration);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag(_tag))
            {
                _hasExited = false;
                Vector3 bendFactor = getBendFactor(other.gameObject);
                bend(bendFactor, _moveToPositionDuration);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(_tag))
            {
                _hasExited = true;
            }
        }

        private void bend(Vector3 vector3, float duration)
        {
            Vector3 vector = Vector3.SmoothDamp(_material.GetVector(_materialBendFactorName), vector3, ref velocity, duration);
            _material.SetVector(_materialBendFactorName, vector);
        }

        private Vector3 getBendFactor(GameObject collider)
        {
            Vector3 direction = collider.transform.position - gameObject.transform.position;

            direction = direction.normalized.inverse();
            direction = new Vector3(direction.x, 0, direction.y);

            return direction.ClampAll(_clampBend.x, _clampBend.y) ;
        }
    }

}
