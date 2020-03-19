using UnityEngine;

public static class Vector3Extension 
{
    public static Vector3 inverse(this Vector3 vector)
    {
        float x = vector.x != 0 ? 1 / vector.x : 0;
        float y = vector.z != 0 ? 1 / vector.z : 0;
        float z = vector.z != 0 ? 1 / vector.z : 0;

        return new Vector3(x, y, z);
    }

    public static Vector3 ClampAll(this Vector3 vector, float minValue, float maxValue)
    {
        Vector3 clampVector;
        clampVector.x = Mathf.Clamp(vector.x, minValue, maxValue);
        clampVector.y = Mathf.Clamp(vector.y, minValue, maxValue);
        clampVector.z = Mathf.Clamp(vector.z, minValue, maxValue);

        return clampVector;
    }

    public static Vector3 Clamp(this Vector3 vector, float xMinValue, float xMaxValue, float yMinValue, float yMaxValue, float zMinValue, float zMaxValue)
    {
        Vector3 clampVector;
        clampVector.x = Mathf.Clamp(vector.x, xMinValue, xMaxValue);
        clampVector.y = Mathf.Clamp(vector.y, yMinValue, yMaxValue);
        clampVector.z = Mathf.Clamp(vector.z, zMinValue, zMaxValue);

        return clampVector;
    }
}