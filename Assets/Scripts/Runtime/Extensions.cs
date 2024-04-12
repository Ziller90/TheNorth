using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T FindInParents<T>(this Transform transform) where T : Component
    {
        if (transform == null) return null;

        var component = transform.GetComponent<T>();
        if (component != null)
        {
            return component;
        }

        return transform.parent != null ? FindInParents<T>(transform.parent) : null;
    }
}
