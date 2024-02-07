using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BlackboardKey
{
    [SerializeField] string id;
    public string Id { get => id; set => id = value; }

    public abstract object GetValue();
    public abstract void SetValue(object value);
}
