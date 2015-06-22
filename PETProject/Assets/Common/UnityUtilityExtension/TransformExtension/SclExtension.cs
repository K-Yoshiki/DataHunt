using UnityEngine;

public static class SclExtension
{
    public static void SetScale(this Transform transform, float x, float y, float z = 0f)
    {
        transform.localScale = new Vector3(x, y, z);
    }
    public static void SetScale(this GameObject gameObject, float x, float y, float z = 0f)
    {
        gameObject.transform.localScale = new Vector3(x, y, z);
    }

    
    public static void SetScaleX(this Transform transform, float value)
    {
        transform.localScale = new Vector3(value, transform.localScale.y, transform.localScale.z);
    }
    public static void SetScaleX(this GameObject gameObject, float value)
    {
        gameObject.transform.localScale = new Vector3(value, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }

    
    public static void SetScaleY(this Transform transform, float value)
    {
        transform.localScale = new Vector3(transform.localScale.x, value, transform.localScale.z);
    }
    public static void SetScaleY(this GameObject gameObject, float value)
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, value, gameObject.transform.localScale.z);
    }

    
    public static void SetScaleZ(this Transform transform, float value)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, value);
    }
    public static void SetScaleZ(this GameObject gameObject, float value)
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, value);
    }
}