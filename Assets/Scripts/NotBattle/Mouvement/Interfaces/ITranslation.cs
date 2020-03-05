using UnityEngine;

public interface ITranslation
{
    /// <summary>
    /// Get the direction of the tranlastion
    /// </summary>
    /// <returns></returns>
    Vector3 getDirection();

    /// <summary>
    /// Get the speed of the translation
    /// </summary>
    /// <returns></returns>
    float getSpeed();

    /// <summary>
    /// Make the transform move
    /// </summary>
    void move(Transform transform);
}