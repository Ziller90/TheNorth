using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

[CustomEditor(typeof(Range))]
public class RangeEditor : UnityEditor.Editor
{
    BoxBoundsHandle boundsHandle = new BoxBoundsHandle();

    void OnSceneGUI()
    {
        Range range = (Range)target;

        EditorGUI.BeginChangeCheck();
        switch (range.RangeType)
        {
            case Range.RangeShapeType.Sphere:
                float newRadius = Handles.RadiusHandle(Quaternion.identity, range.transform.position, range.Radius);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(range, "Changed Radius");
                    range.Radius = newRadius;
                }
                break;

            case Range.RangeShapeType.FOV:
                DrawFOV(range);
                break;

            case Range.RangeShapeType.Box:
                boundsHandle.center = range.Bounds.center;
                boundsHandle.size = range.Bounds.size;

                Matrix4x4 handleMatrix = range.transform.localToWorldMatrix;
                using (new Handles.DrawingScope(handleMatrix))
                {
                    boundsHandle.DrawHandle();
                }

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(range, "Changed Bounds");
                    Bounds newBounds = range.Bounds;
                    newBounds.size = boundsHandle.size;
                    range.Bounds = newBounds;
                }
                break;
        }
    }

    private void DrawFOV(Range range)
    {
        Vector3 position = range.transform.position;
        Vector3 forward = range.transform.forward;
        float maxDistance = range.Radius;
        float halfFOV = range.Angle / 2.0f;

        // Calculate the direction vectors at the edges of the FOV
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * forward;
        Vector3 rightRayDirection = rightRayRotation * forward;

        // Draw the lines representing the edges of the FOV
        Handles.DrawLine(position, position + leftRayDirection * maxDistance);
        Handles.DrawLine(position, position + rightRayDirection * maxDistance);

        // Optionally, draw a wire arc to represent the FOV area
        Handles.DrawWireArc(position, Vector3.up, leftRayDirection, range.Angle, maxDistance);
    }
}
