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
    public static int GetFirstFreeSpaceIndex<T>(IEnumerable<T> objects) where T : class
    {
        for (int i = 0; i < objects.Count(); i++)
        {
            if (objects.ElementAt(i) == null) 
                return i;
        }
        return -1;
    }

    public static bool AddItemStackToGroup(ItemStack newStack, ItemStack[] group)
    {
        bool merged = TryMerge(newStack, group);
        if (merged)
            return true;

        bool added = TryAdd(newStack, group);
        if (added)
            return true;

        return false;
    }
    public static int GetFirstFreeStackInGroupIndex(ItemStack[] group)
    {
        for (int i = 0; i < group.Length; i++)
        {
            if (group[i].Item == null)
                return i;
        }
        return -1;
    }
    public static bool TryAdd(ItemStack newStack, ItemStack[] group)
    {
        int freeStackIndex = GetFirstFreeStackInGroupIndex(group);
        if (freeStackIndex == -1)
            return false;

        group[freeStackIndex] = newStack;
        return true;
    }
    public static bool CanBeMerged(ItemStack stack1, ItemStack stack2)
    {
        if (stack1.Item == stack2.Item
                    && stack1.Item.MaxStackSize != 1
                    && stack2.Item.MaxStackSize != 1
                    && stack1.Item.MaxStackSize != stack1.ItemsNumber
                    && stack2.Item.MaxStackSize != stack2.ItemsNumber)
        {
            return true;
        }
        return false;
    }
    public static bool TryMerge(ItemStack newStack, ItemStack[] group)
    {
        if (newStack.Item.MaxStackSize != 1)
        {
            foreach (var stack in group)
            {
                if (CanBeMerged(stack, newStack))
                {
                    Merge(newStack, stack);
                    return true;
                }
            }
        }
        return false;
    }
    public static void Merge(ItemStack stack1, ItemStack stack2)
    {
        int MaxStackSize = stack1.Item.MaxStackSize;
        if (stack1.ItemsNumber + stack2.ItemsNumber <= MaxStackSize)
        {
            stack2.ItemsNumber = stack1.ItemsNumber + stack2.ItemsNumber;
            stack1.ItemsNumber = 0;
        }
        else if (stack1.ItemsNumber + stack2.ItemsNumber >= MaxStackSize)
        {
            int deficient = MaxStackSize - stack2.ItemsNumber;
            stack2.ItemsNumber = MaxStackSize;
            stack1.ItemsNumber -= deficient;
        }
    }
}
