using UnityEngine;
using System.Collections;

public abstract class LocalizeComponent : MonoBehaviour
{
    [Tooltip("Allow the change of localization to act in update. (Can inpact performance).")]
    public bool executeOnUpdate;
    public string key;
    public abstract KeyTargetType targetType { get; }

    private void OnEnable()
    {
        getComponentToModify();
    }

    // Use this for initialization
    void Start()
    {
        getComponentToModify();
        setBasedOnLocation();
    }

    // Update is called once per frame
    void Update()
    {
        if (executeOnUpdate)
            setBasedOnLocation();
    }

    public abstract void getComponentToModify();

    public abstract void setBasedOnLocation();
}
