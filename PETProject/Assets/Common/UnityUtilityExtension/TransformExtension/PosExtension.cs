using UnityEngine;

public static class PosExtension
{
    public static void SetPos(this Transform transform, float x, float y, float z = 0f)
    {
        transform.position = new Vector3(x, y, z);
    }
    public static void SetPos(this GameObject gameObject, float x, float y, float z = 0f)
    {
        gameObject.transform.position = new Vector3(x, y, z);
    }
    public static void SetLocalPos(this Transform transform, float x, float y, float z = 0f)
    {
        transform.localPosition = new Vector3(x, y, z);
    }
    public static void SetLocalPos(this GameObject gameObject, float x, float y, float z = 0f)
    {
        gameObject.transform.localPosition = new Vector3(x, y, z);
    }


    public static void SetPosX(this Transform transform, float value)
    {
        transform.position = new Vector3(value, transform.position.y, transform.position.z);
    }
    public static void SetPosX(this GameObject gameObject, float value)
    {
        gameObject.transform.position = new Vector3(value, gameObject.transform.position.y, gameObject.transform.position.z);
    }
    public static void SetLocalPosX(this Transform transform, float value)
    {
        transform.localPosition = new Vector3(value, transform.localPosition.y, transform.localPosition.z);
    }
    public static void SetLocalPosX(this GameObject gameObject, float value)
    {
        gameObject.transform.localPosition = new Vector3(value, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
    }
    
    
    public static void SetPosY(this Transform transform, float value)
    {
        transform.position = new Vector3(transform.position.x, value, transform.position.z);
    }
    public static void SetPosY(this GameObject gameObject, float value)
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, value, gameObject.transform.position.z);
    }
    public static void SetLocalPosY(this Transform transform, float value)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, value, transform.localPosition.z);
    }
    public static void SetLocalPosY(this GameObject gameObject, float value)
    {
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, value, gameObject.transform.localPosition.z);
    }


    public static void SetPosZ(this Transform transform, float value)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, value);
    }
    public static void SetPosZ(this GameObject gameObject, float value)
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, value);
    }
    public static void SetLocalPosZ(this Transform transform, float value)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, value);
    }
    public static void SetLocalPosZ(this GameObject gameObject, float value)
    {
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, value);
    }
}