using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

public enum Direction { Forward, Backward, Left, Right }


public class GameObjectExtender : MonoBehaviour
{
    MeshFilter _meshFilter;
    [EnumToggleButtons, SerializeField] Direction _directions;

    [Button("Spawn")]
    public void spawnBaseOnEnum(int quantityToSpawn)
    {
        switch (_directions)
        {
            case Direction.Forward: spawnCurentObjectCopy(Vector3.forward, quantityToSpawn); break;
            case Direction.Backward: spawnCurentObjectCopy(Vector3.forward, quantityToSpawn); break;
            case Direction.Left: spawnCurentObjectCopy(Vector3.forward, quantityToSpawn); break;
            case Direction.Right: spawnCurentObjectCopy(Vector3.forward, quantityToSpawn); break;
        }
    }

    public void spawnCurentObjectCopy(Vector3 direction, int quantityToSpawn)
    {
        GameObject currentGameObject = gameObject;
        quantityToSpawn = quantityToSpawn == 0 ? 1 : quantityToSpawn;

        for (int i = 0; i < quantityToSpawn; i++)
        {
            MeshFilter meshfilter = currentGameObject.GetComponent<MeshFilter>();
            Vector3 size = meshfilter.sharedMesh.bounds.size;

            Vector3 newGOPosition = currentGameObject.transform.position + (Vector3.Scale(size, direction));
            GameObject copyGO = GameObject.Instantiate(currentGameObject, newGOPosition, Quaternion.identity, currentGameObject.transform.parent);
            copyGO.name = currentGameObject.name;
            currentGameObject = copyGO;
        }
    }

    public GameObject spawnNewObjectFromPrefab(Vector3 direction, Vector3 size, GameObject gameObjectToSpawn)
    {
        Vector3 newGOPosition = gameObject.transform.position + (Vector3.Scale(size, direction));
        return GameObject.Instantiate(gameObjectToSpawn, newGOPosition, Quaternion.identity, gameObject.transform.parent);
    }


}