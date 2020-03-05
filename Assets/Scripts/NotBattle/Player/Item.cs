using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = Path.ITEM_SO_MENU_NAME)]
public class Item : ScriptableObject
{
    [SerializeField] public string name;
    [SerializeField] public int quantity;
    [SerializeField] public bool canBeStacked;
    [SerializeField] public Sprite sprite;
    [SerializeField] public GameObject gameObject;
}