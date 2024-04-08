using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

[CustomEditor(typeof(Route))]
public class RouteEditor : Editor
{
    private ReorderableList reorderableList;
    SerializedProperty loopedProperty, cornersColorProperty, linesColorProperty, navMeshYOffset;
    private int selectedCornerIndex = -1; // Для отслеживания выбранного угла

    private void OnEnable()
    {
        loopedProperty = serializedObject.FindProperty("looped");
        cornersColorProperty = serializedObject.FindProperty("cornersColor");
        linesColorProperty = serializedObject.FindProperty("linesColor");
        navMeshYOffset = serializedObject.FindProperty("navMeshYOffset");

        reorderableList = new ReorderableList(serializedObject, serializedObject.FindProperty("corners"), true, true, true, true);

        reorderableList.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Corners");
        };

        reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            GUIContent label = new GUIContent($"Corner #{index}");

            var element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, label);
        };

        reorderableList.onSelectCallback = (ReorderableList list) =>
        {
            selectedCornerIndex = list.index;
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(cornersColorProperty);
        EditorGUILayout.PropertyField(linesColorProperty);
        EditorGUILayout.PropertyField(loopedProperty);
        EditorGUILayout.PropertyField(navMeshYOffset);

        reorderableList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("AttachToNavMesh"))
        {
            Route route = (Route)target;
            route.AttachCornersToNavMesh();
        }

        if (GUILayout.Button("AttachToColliderBelow"))
        {
            Route route = (Route)target;
            route.AttachCornersToColliderBelow();
        }
    }

    void OnSceneGUI()
    {
        Route route = (Route)target;

        if (route.Corners != null && route.Corners.Count > 0)
        {
            Handles.color = route.LinesColor;

            for (int i = 0; i < route.Corners.Count - 1; i++)
            {
                Handles.DrawLine(route.Corners[i], route.Corners[i + 1]);
            }

            if (route.Looped && route.Corners.Count > 1)
            {
                Handles.DrawLine(route.Corners[route.Corners.Count - 1], route.Corners[0]);
            }

            for (int i = 0; i < route.Corners.Count; i++)
            {
                DrawCornerHandle(route, i);
            }
        }
    }

    void DrawCornerHandle(Route route, int index)
    {
        Handles.color = route.CornersColor;

        // Рисуем номер угла
        GUIStyle textStyle = new GUIStyle();
        textStyle.normal.textColor = Color.white;
        textStyle.alignment = TextAnchor.MiddleCenter;
        Handles.Label(route.Corners[index] + Vector3.up * 0.5f, $"{index}", textStyle);

        // Обрабатываем выделение конуса
        if (Handles.Button(route.Corners[index], Quaternion.LookRotation(Vector3.up), 0.5f, 0.5f, Handles.ConeHandleCap))
        {
            selectedCornerIndex = index;
        }

        // Если этот угол выделен, позволяем его перемещать
        if (index == selectedCornerIndex)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 newTargetPosition = Handles.PositionHandle(route.Corners[index], Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(route, "Move Corner");
                route.Corners[index] = newTargetPosition;
            }
        }
    }
}