using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiegeUp.Core;
using System;

[ComponentId(7)]
public class SerializingTest : MonoBehaviour
{
    [AutoSerialize(1)] public SerializeTestParentType testField;

    [ContextMenu("SetTestFieldValue")]
    public void SetTestFieldValue()
    {
        testField = new SerializeTestInheritedType() {parentField = 10, childField = 10 };
    }
}

[Serializable]
public class SerializeTestParentType
{
    [AutoSerialize(1)] public int parentField;
}

[Serializable]
public class SerializeTestInheritedType : SerializeTestParentType
{
    [AutoSerialize(2)] public int childField;
}
