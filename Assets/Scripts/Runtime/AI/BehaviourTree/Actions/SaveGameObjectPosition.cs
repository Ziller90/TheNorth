using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class SaveGameObjectPosition : ActionNode
{
    [SerializeField] NodeProperty<GameObject> gameObject= new NodeProperty<GameObject>();
    [SerializeField] NodeProperty<Vector3> keyToSavePosition = new NodeProperty<Vector3>();

    protected override State OnUpdate()
    {
        if (gameObject.Value)
        {
            keyToSavePosition.Value = gameObject.Value.transform.position;
            return State.Success;
        }
        return State.Failure;
    }
}
