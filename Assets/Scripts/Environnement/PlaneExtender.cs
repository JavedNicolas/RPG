using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

namespace RPG.Environnement 
{
    [RequireComponent(typeof(Transform), typeof(MeshFilter))]
    public class PlaneExtender : MonoBehaviour
    {
        Transform _transfrom;

        [Button("Display Gizmo")]
        public void DisplayGizmo()
        {
            _transfrom = GetComponent<Transform>();

            Vector3 position = _transfrom.position;
            Vector3[] gizmoPositions =
            {
                new Vector3(position.x + 1, position.y, position.z),
                new Vector3(position.x - 1, position.y, position.z),
                new Vector3(position.x, position.y, position.z + 1),
                new Vector3(position.x, position.y, position.z - 1),
            };

            for(int i =0; i < 4; i++)
            {
                Vector3 gizmoPosition = gizmoPositions[i];
                Gizmos.DrawMesh(gameObject.GetComponent<MeshFilter>().mesh, i, gizmoPosition);
            }
        }

        public void SpawnNewPlane()
        {

        }

    
    }
}