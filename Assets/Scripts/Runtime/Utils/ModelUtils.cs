using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ModelUtils : MonoBehaviour
{
    public static Vector3 GetDirection(float horizontal, float vertical, float fixAngle)
    {
        Quaternion fixQuaternion = Quaternion.Euler(0, fixAngle, 0);
        Vector3 direction = Vector3.ClampMagnitude(new Vector3(horizontal, 0, vertical), 1);
        direction = fixQuaternion * direction;
        return direction;
    }
    public static float round(float number, int roundIndex)
    {
        float temp = number * roundIndex;
        temp = (int)temp;
        temp = temp / roundIndex;
        return (temp);
    }
    public static float SpeedConverter(float kmPerHour)
    {
        return (kmPerHour * 1000f / 3600f) / 50f;
    }
    public static Transform GetNearest(Transform start, List<Transform> points)
    {
        Transform nearestPoint = null;
        float bestDistance = 10000;
        foreach (Transform point in points)
        {
            float distance = Vector3.Distance(start.position, point.position);
            if (distance < bestDistance)
            {
                nearestPoint = point;
                bestDistance = distance;
            }
        }
        return nearestPoint;
    }
    public static Vector3 GetNearest(Vector3 start, List<Vector3> points)
    {
        Vector3 nearestPoint = Vector3.zero;
        float bestDistance = 10000;
        foreach (Vector3 point in points)
        {
            float distance = Vector3.Distance(start, point);
            if (distance < bestDistance)
            {
                nearestPoint = point;
                bestDistance = distance;
            }
        }
        return nearestPoint;
    }
    public static Vector3 CalculateWASDVector()
    {
        float vertical;
        float horizontal;

        if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.S)))
        {
            vertical = 0;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            vertical = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vertical = -1;
        }
        else
        {
            vertical = 0;
        }

        if (Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.D)))
        {
            horizontal = 0;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1;
        }
        else
        {
            horizontal = 0;
        }
        return Vector3.ClampMagnitude(new Vector3(horizontal, 0, vertical), 1);
    }

    public static bool isInRhombus(Vector2 position, Vector2 rhombusCenter, float rhombusWidth, float rhombusHeight)
    {
        Vector2 pointVector = position - rhombusCenter;
        if (Mathf.Abs(pointVector.x) / (rhombusWidth / 2) + Mathf.Abs(pointVector.y) / (rhombusHeight / 2) <= 1)
        {
            return true;
        }
        return false;
    }

    public static bool TryAddOrMergeItemStackToSlotGroup(ItemStack newStack, Slot[] slotGroup)
    {
        bool merged = TryMergeToSlotGroup(newStack, slotGroup);
        if (merged)
            return true;

        bool added = TryAdd(newStack, slotGroup);
        if (added)
            return true;

        return false;
    }

    static int GetFirstEmtySlotInGroupIndex(Slot[] slotGroup)
    {
        for (int i = 0; i < slotGroup.Length; i++)
        {
            if (slotGroup[i].isEmpty)
                return i;
        }
        return -1;
    }

    static bool TryAdd(ItemStack newStack, Slot[] slotGroup)
    {
        int freeStackIndex = GetFirstEmtySlotInGroupIndex(slotGroup);
        if (freeStackIndex == -1)
            return false;

        slotGroup[freeStackIndex].SetItemStack(newStack);
        return true;
    }

    static bool TryMergeToSlotGroup(ItemStack newStack, Slot[] slotGroup)
    {
        if (newStack.Item.MaxStackSize != 1)
        {
            foreach (var slot in slotGroup)
            {
                if (slot.CanBeMerged(newStack))
                {
                    slot.Merge(newStack);
                    return true;
                }
            }
        }
        return false;
    }
}
