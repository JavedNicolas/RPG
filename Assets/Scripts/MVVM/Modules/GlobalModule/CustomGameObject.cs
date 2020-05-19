using UnityEngine;
using System.Collections;

public class CustomGameObject : MonoBehaviour
{
    [Tooltip("The gameobject service as a base for this object. This will be used to get the object size if needed")]
    [SerializeField] GameObject _base;
    public GameObject baseObject => _base;

    public Vector3 getSize()
    {
        if (_base == null)
            return Vector3.zero;

        if (isUsingTerrain())
        {
            Terrain terrain = _base.GetComponent<Terrain>();
            return terrain.terrainData.size;
        }

        MeshRenderer renderer = _base.GetComponent<MeshRenderer>();
        return renderer == null ? Vector3.zero : renderer.bounds.size;
    }

    bool isUsingTerrain()
    {
        return _base.GetComponent<Terrain>() == null ? false : true;
    }
}
