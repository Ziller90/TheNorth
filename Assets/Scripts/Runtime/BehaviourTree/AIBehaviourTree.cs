using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AIBehaviourTree : MonoBehaviour
{
    [SerializeReference, SubclassSelector] Node root;
    Dictionary<string, object> data = new Dictionary<string, object>();

    private void Update()
    {
        if (root != null)
            root.Evaluate();
    }

    public void SetData(string key, object value)
    {
        data[key] = value;
    }

    public object GetData(string key)
    {
        data.TryGetValue(key, out object value);
        return value;
    }

    public void ClearData(string key)
    {
        if (data.ContainsKey(key))
            data.Remove(key);
    }
}
