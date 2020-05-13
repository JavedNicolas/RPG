using UnityEngine;
using System.Collections.Generic;

namespace RPG.DataModule.ViewModel
{

    public class RoomViewModel
    {
        #region Dungeon Spawning
        /// <summary>
        /// Get the fittings prefabs from the room prefab list depending of the room links.
        /// </summary>
        /// <param name="rooms"> The grid of rooms </param>
        /// <param name="roomPrefabs"> The list of the available prefabs </param>
        /// <returns> The grid of the prefabs to instantiate </returns>
        public GameObject[,] getGridDungeonRoomsPrefabs(Room[,] rooms, List<GameObject> roomPrefabs, List<GameObject> emptyRoomPrefabs)
        {
            GameObject[,] prefabs = new GameObject[rooms.GetLength(0), rooms.GetLength(1)];

            for (int i = 0; i < rooms.GetLength(0); i++)
                for (int j = 0; j < rooms.GetLength(1); j++)
                {
                    if (rooms[i, j] == null)
                        prefabs[i, j] = emptyRoomPrefabs.getRandomElement();
                    else
                        prefabs[i, j] = getFittingPrefab(rooms[i, j], roomPrefabs);
                }

            return prefabs;
        }

        /// <summary>
        /// Set the instantiate room Game object to the room (so we can access it easily from the model later)
        /// </summary>
        /// <param name="gameObjects"></param>
        /// <param name="rooms"></param>
        public void setRoomGameObjects(GameObject[,] gameObjects, Room[,] rooms)
        {
            for (int i = 0; i < rooms.GetLength(0); i++)
                for (int j = 0; j < rooms.GetLength(1); j++)
                    rooms[i, j].setGameObject(gameObjects[i, j]);
        }

        /// <summary>
        /// get the fitting model based on the layout
        /// </summary>
        /// <param name="roomToSpawn"></param>
        /// <returns></returns>
        GameObject getFittingPrefab(Room roomToSpawn, List<GameObject> roomPrefabs)
        {
            List<GameObject> prefabs = new List<GameObject>(roomToSpawn.scriptableObject.isSpecialRoom ? roomToSpawn.scriptableObject.prefabs : roomPrefabs);

            List<GameObject> machtingPrefabs = new List<GameObject>();
            foreach (GameObject prefab in prefabs)
            {
                string[] splitName = prefab.name.Split('_');
                if (splitName.Length > 0
                    && splitName[0].containUnOrdered(roomToSpawn.linkedRoomString)
                    && splitName[0].Length == roomToSpawn.linkedRoomString.Length)
                    machtingPrefabs.Add(prefab);
            }

            return machtingPrefabs.getRandomElement();
        }

        #endregion

        #region Map generation

        public Sprite[,] getGridDungeonMapSprites(Room[,] rooms, List<Sprite> layoutIcons)
        {
            Sprite[,] sprites = new Sprite[rooms.GetLength(0), rooms.GetLength(1)];

            for (int i = 0; i < rooms.GetLength(0); i++)
                for (int j = 0; j < rooms.GetLength(1); j++)
                    if(rooms[i,j] != null)
                        sprites[i, j] = getSprite(rooms[i, j], layoutIcons);

            return sprites;
        }


        Sprite getSprite(Room roomData, List<Sprite> layoutIcons)
        {
            // search for a sprite with the name containing all the sprite name letters
            List<Sprite> searchingArray = layoutIcons.FindAll(x => x.name.containUnOrdered(roomData.linkedRoomString));
            Sprite fittingSprite = searchingArray.Find(x => x.name.Length == roomData.linkedRoomString.Length);

            return fittingSprite;
        }
        #endregion
    }
}

