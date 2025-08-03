using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectUtils
{
    public static GameObject InstantiateTempEffect(GameObject original, Vector3 position, Quaternion rotation,
        Transform parent)
    {
        GameObject obj =  Object.Instantiate(original.gameObject, position, rotation, parent);
        obj.AddComponent<AutoDestructParticleSystem>();
        return obj;
    }
    
    public static GameObject InstantiateTempEffect(GameObject original, Vector3 position, Quaternion rotation)
    {
        GameObject obj =  Object.Instantiate(original.gameObject, position, rotation, GameObject.Find("TempObjects").transform);
        obj.AddComponent<AutoDestructParticleSystem>();
        return obj;
    }
    
    public static GameObject InstantiateTempEffect(GameObject original, Vector3 position, Transform parent)
    {
        GameObject obj =  Object.Instantiate(original.gameObject, position, Quaternion.identity, parent);
        obj.AddComponent<AutoDestructParticleSystem>();
        return obj;
    }
    
    public static GameObject InstantiateTempEffect(GameObject original, Quaternion rotation, Transform parent)
    {
        GameObject obj =  Object.Instantiate(original.gameObject, parent.position, rotation, parent);
        obj.AddComponent<AutoDestructParticleSystem>();
        return obj;
    }
    
    public static GameObject InstantiateTempEffect(GameObject original, Vector3 position)
    {
        GameObject obj =  Object.Instantiate(original.gameObject, position, Quaternion.identity, GameObject.Find("TempObjects").transform);
        obj.AddComponent<AutoDestructParticleSystem>();
        return obj;
    }
    
    public static GameObject InstantiateTempEffect(GameObject original, Quaternion rotation)
    {
        GameObject obj =  Object.Instantiate(original.gameObject, Vector3.zero, rotation, GameObject.Find("TempObjects").transform);
        obj.AddComponent<AutoDestructParticleSystem>();
        return obj;
    }
    
    public static GameObject InstantiateTempEffect(GameObject original, Transform parent)
    {
        GameObject obj =  Object.Instantiate(original.gameObject, parent.position, Quaternion.identity, parent);
        obj.AddComponent<AutoDestructParticleSystem>();
        return obj;
    }
    
    public static GameObject InstantiateTempEffect(GameObject original)
    {
        GameObject obj =  Object.Instantiate(original.gameObject, Vector3.zero, Quaternion.identity, GameObject.Find("TempObjects").transform);
        obj.AddComponent<AutoDestructParticleSystem>();
        return obj;
    }
}
