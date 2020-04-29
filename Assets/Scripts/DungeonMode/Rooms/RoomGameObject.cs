﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.Battle;

namespace RPG.DungeonMode.Dungeon
{
    public class RoomGameObject : MonoBehaviour
    {
        [SerializeField] List<ActorSpawningPoint> _startPoints;
        [SerializeField] GameObject _ground;
        [SerializeField] Vector3 cameraOffset;
        public List<ActorSpawningPoint> startPoints => _startPoints;

        public Vector3 cameraPosition()
        {
            Vector3 size = getSize();
            float x = transform.position.x + (size.x / 2) + cameraOffset.x;
            float y = transform.position.y + 20 + cameraOffset.y;
            float z = transform.position.z + size.z + cameraOffset.z;

            return new Vector3(x, y, z);
        }

        public Vector3 getSize()
        {
            if (_ground == null)
                return Vector3.zero;

            if (isUsingTerrain())
            {
                Terrain terrain = _ground.GetComponent<Terrain>();
                return terrain.terrainData.size;
            }

            MeshRenderer renderer = _ground.GetComponent<MeshRenderer>();
            return renderer == null ? Vector3.zero : renderer.bounds.size;
        }

        bool isUsingTerrain()
        {
            return _ground.GetComponent<Terrain>() == null ? false: true;
        }
    }

}
