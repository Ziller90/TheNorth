using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ComponentKey : BlackboardKey
{
    [SerializeField] Component obj;
    public override object GetValue() => obj;
    public override void SetValue(object value) => obj = (Component)value;
}

[Serializable]
public class IntKey : BlackboardKey
{
    [SerializeField] int obj;
    public override object GetValue() => obj;
    public override void SetValue(object value) => obj = (int)value;
}

[Serializable]
public class FloatKey : BlackboardKey
{
    [SerializeField] float obj;
    public override object GetValue() => obj;
    public override void SetValue(object value) => obj = (float)value;
}

[Serializable]
public class StringKey : BlackboardKey
{
    [SerializeField] string obj;
    public override object GetValue() => obj;
    public override void SetValue(object value) => obj = (string)value;
}

[Serializable]
public class MovingModeKey : BlackboardKey
{
    [SerializeField] MovingMode obj;
    public override object GetValue() => obj;
    public override void SetValue(object value) => obj = (MovingMode)value;
}

[Serializable]
public class Vector3Key : BlackboardKey
{
    [SerializeField] Vector3 obj;
    public override object GetValue() => obj;
    public override void SetValue(object value) => obj = (Vector3)value;
}
