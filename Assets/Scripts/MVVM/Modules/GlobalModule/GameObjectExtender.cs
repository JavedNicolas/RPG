using Sirenix.OdinInspector;
using UnityEngine;

namespace RPG.GlobalModule.View
{
    public enum GameObjectDirection { Forward, Backward, Left, Right }

    public class GameObjectExtender : MonoBehaviour
    {
        MeshFilter _meshFilter;
        [EnumToggleButtons, SerializeField] GameObjectDirection _directions;

        [Button("Spawn")]
        public void spawnBaseOnEnum(int quantityToSpawn)
        {
            switch (_directions)
            {
                case GameObjectDirection.Forward: spawnCurentObjectCopy(Vector3.forward, quantityToSpawn); break;
                case GameObjectDirection.Backward: spawnCurentObjectCopy(Vector3.back, quantityToSpawn); break;
                case GameObjectDirection.Left: spawnCurentObjectCopy(Vector3.left, quantityToSpawn); break;
                case GameObjectDirection.Right: spawnCurentObjectCopy(Vector3.right, quantityToSpawn); break;
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

        public GameObject spawnPrefabBaseOnDirection(GameObjectDirection direction, Vector3 size, GameObject gameObjectToSpawn)
        {
            switch (direction)
            {
                case GameObjectDirection.Forward: return spawnNewObjectFromPrefab(Vector3.forward, size, gameObjectToSpawn);
                case GameObjectDirection.Backward: return spawnNewObjectFromPrefab(Vector3.back, size, gameObjectToSpawn);
                case GameObjectDirection.Left: return spawnNewObjectFromPrefab(Vector3.left, size, gameObjectToSpawn);
                case GameObjectDirection.Right: return spawnNewObjectFromPrefab(Vector3.right, size, gameObjectToSpawn);
            }

            return null;
        }

        public GameObject spawnNewObjectFromPrefab(Vector3 direction, Vector3 size, GameObject gameObjectToSpawn)
        {
            Vector3 newGOPosition = gameObject.transform.position + (Vector3.Scale(size, direction));
            Vector3 boxCastCenter = newGOPosition + (Vector3.Scale(size, direction));
            GameObject spawnedGameObject = null;
            Debug.DrawRay(newGOPosition, direction, Color.red);

            return spawnedGameObject;
        }
    }
}