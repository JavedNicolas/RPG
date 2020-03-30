using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Transform), typeof(MeshFilter))]
public class PlaneExtender : MonoBehaviour
{
    MeshFilter _meshFilter;

    public void spawnNewPlane(Vector3 direction, int numberToSpawn)
    {
        GameObject gameObjectToUse = gameObject;

        numberToSpawn = numberToSpawn == 0 ? 1 : numberToSpawn;

        for(int i = 0; i < numberToSpawn; i++)
        {
            MeshFilter meshfilter = gameObjectToUse.GetComponent<MeshFilter>();
            Vector3 size = meshfilter.sharedMesh.bounds.size;

            Vector3 newGOPosition = gameObjectToUse.transform.position + (Vector3.Scale(size, direction));
            GameObject copyGO = GameObject.Instantiate(gameObjectToUse, newGOPosition, Quaternion.identity, gameObjectToUse.transform.parent);
            copyGO.name = gameObjectToUse.name;
            gameObjectToUse = copyGO;
        }
    }

    [Button("Forward")]
    public void ExtendPlaneForward(int numberToSpawn)
    {
        spawnNewPlane(Vector3.forward, numberToSpawn);
    }

    [Button("Left")]
    public void ExtendPlaneLeft(int numberToSpawn)
    {
        spawnNewPlane(Vector3.left, numberToSpawn);
    }

    [Button("Right")]
    public void ExtendPlaneRight(int numberToSpawn)
    {
        spawnNewPlane(Vector3.right, numberToSpawn);
    }

    [Button("Backward")]
    public void ExtendPlaneBackward(int numberToSpawn)
    {
        spawnNewPlane(Vector3.back, numberToSpawn);
    }

    
}