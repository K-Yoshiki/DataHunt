using UnityEngine;

public static class RotExtension
{
    public static void SetRot(this Transform transform, float x, float y, float z)
    {
        transform.rotation = Quaternion.Euler(x, y, z);
    }
    public static void SetRot(this GameObject gameObject, float x, float y, float z)
    {
        gameObject.transform.rotation = Quaternion.Euler(x, y, z);
    }
    public static void SetLocalRot(this Transform transform, float x, float y, float z)
    {
        transform.localRotation = Quaternion.Euler(x, y, z);
    }
    public static void SetLocalRot(this GameObject gameObject, float x, float y, float z)
    {
        gameObject.transform.localRotation = Quaternion.Euler(x, y, z);
    }
    
    
    public static void SetRotX(this Transform transform, float value)
    {
        Vector3 angles = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(value, angles.y, angles.z);
    }
    public static void SetRotX(this GameObject gameObject, float value)
    {
        Vector3 angles = gameObject.transform.eulerAngles;
        gameObject.transform.rotation = Quaternion.Euler(value, angles.y, angles.z);
    }
    public static void SetLocalRotX(this Transform transform, float value)
    {
        Vector3 angles = transform.localEulerAngles;
        transform.localRotation = Quaternion.Euler(value, angles.x, angles.z);
    }
    public static void SetLocalRotX(this GameObject gameObject, float value)
    {
        Vector3 angles = gameObject.transform.localEulerAngles;
        gameObject.transform.localRotation = Quaternion.Euler(value, angles.y, angles.z);
    }
    
    
    public static void SetRotY(this Transform transform, float value)
    {
        Vector3 angles = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(angles.x, value, angles.z);
    }
    public static void SetRotY(this GameObject gameObject, float value)
    {
        Vector3 angles = gameObject.transform.eulerAngles;
        gameObject.transform.rotation = Quaternion.Euler(angles.x, value, angles.z);
    }
    public static void SetLocalRotY(this Transform transform, float value)
    {
        Vector3 angles = transform.localEulerAngles;
        transform.localRotation = Quaternion.Euler(angles.x, value, angles.z);
    }
    public static void SetLocalRotY(this GameObject gameObject, float value)
    {
        Vector3 angles = gameObject.transform.localEulerAngles;
        gameObject.transform.localRotation = Quaternion.Euler(angles.x, value, angles.z);
    }
    
    
    public static void SetRotZ(this Transform transform, float value)
    {
        Vector3 angles = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(angles.x, angles.y, value);
    }
    public static void SetRotZ(this GameObject gameObject, float value)
    {
        Vector3 angles = gameObject.transform.eulerAngles;
        gameObject.transform.rotation = Quaternion.Euler(angles.x, angles.y, value);
    }
    public static void SetRotLocalZ(this Transform transform, float value)
    {
        Vector3 angles = transform.localEulerAngles;
        transform.localRotation = Quaternion.Euler(angles.x, angles.y, value);
    }
    public static void SetRotLocalZ(this GameObject gameObject, float value)
    {
        Vector3 angles = gameObject.transform.localEulerAngles;
        gameObject.transform.localRotation = Quaternion.Euler(angles.x, angles.y, value);
    }
}