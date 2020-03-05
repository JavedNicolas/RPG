using UnityEngine;
using System.Collections;

public static class TransfromExtension
{
    public static void clearChild(this Transform transform)
    {
        for(int i =0; i < transform.childCount; i++)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }


}
