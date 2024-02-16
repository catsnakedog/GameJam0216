using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager
{
    public Dictionary<string, Object> Objects;

    public void Init()
    {
        Objects = new Dictionary<string, Object>();
    }

    public T Load<T>(string name) where T : UnityEngine.Object
    {
        if (Objects.ContainsKey(name))
            return (T)Objects[name];
        else
            Objects[name] = Resources.Load<T>("name");

        return (T)Objects[name];
    }
}
