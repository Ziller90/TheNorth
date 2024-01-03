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

    public static bool IsInRhombus(Vector2 position, Vector2 rhombusCenter, float rhombusWidth, float rhombusHeight)
    {
        Vector2 pointVector = position - rhombusCenter;
        if (Mathf.Abs(pointVector.x) / (rhombusWidth / 2) + Mathf.Abs(pointVector.y) / (rhombusHeight / 2) <= 1)
        {
            return true;
        }
        return false;
    }

    public static bool TryMoveFromSlotToSlot(ContainerBase container1, Slot slot1, ContainerBase container2, Slot slot2)
    {
        var result = TryMoveFromSlotToSlotWithResult(container1, slot1, container2, slot2);
        if (result == TransferResult.Added || result == TransferResult.Merged || result == TransferResult.Swapped)
            return true;
        else 
            return false;  
    }

    public static TransferResult TryMoveFromSlotToSlotWithResult(ContainerBase container1, Slot slot1, ContainerBase container2, Slot slot2)
    {
        if (slot1 == null || slot2 == null)
        {
            return TransferResult.Undefined;
        }

        if (slot1 == slot2)
            return TransferResult.CurrentAndTargetAreSame;

        TransferResult result;
        if (container1.CanRemoveItemStackFromSlot(slot1) && container2.CanAddItemStackInSlot(slot1.ItemStack, slot2))
        {
            var itemStack = slot1.ItemStack;

            result = slot2.TryAddWithResult(itemStack);
            if (result == TransferResult.Added)
            {
                slot1.Pop();
                return result;
            }
            else if (result == TransferResult.Merged)
            {
                if (itemStack.ItemsNumber == 0)
                    slot1.Pop();
                return result;
            }
            else if (result == TransferResult.SlotIsBlocked || result == TransferResult.NotSuitableItemStack)
            {
                return result;
            }
            else if (result == TransferResult.NoFreeSpace && container2.CanRemoveItemStackFromSlot(slot2) && container1.CanAddItemStackInSlot(slot2.ItemStack, slot1))
            {
                return TrySwapSlotsWithResult(slot1, slot2);
            }
        }

        return TransferResult.Undefined;
    }

    public static bool TryMoveFromSlotToSlotGroup(ContainerBase container1, Slot slot1, ContainerBase container2, SlotGroup slotGroup)
    {
        if (container1.CanRemoveItemStackFromSlot(slot1) && container2.CanAddItemStackInSlotGroup(slot1.ItemStack, slotGroup))
        {
            var success = slotGroup.TryAddOrMerge(slot1.ItemStack);
            if (success)
            {
                slot1.Pop();
                return true;
            }
        }

        return false;
    }

    static TransferResult TrySwapSlotsWithResult(Slot slot1, Slot slot2)
    {
        var slot1ItemStackTemp = slot1.Pop();
        var slot2ItemStackTemp = slot2.Pop();

        var result1 = slot1.TryAddWithResult(slot2ItemStackTemp);
        var result2 = slot2.TryAddWithResult(slot1ItemStackTemp);

        if (result1 == TransferResult.Added && result2 == TransferResult.Added)
        {
            return TransferResult.Swapped;
        }
        else
        {
            slot1.TryAdd(slot1ItemStackTemp);
            slot2.TryAdd(slot2ItemStackTemp);
            return TransferResult.CantSwap;
        }
    }

    public static bool IsSuitableItemStack(ItemStack itemStack, List<AndItemTagList> suitableItemTags)
    {
        if (suitableItemTags.Count != 0)
        {
            foreach (var andtagList in suitableItemTags)
            {
                var valid = andtagList.List.All(x => itemStack.Item.Tags.Contains(x));
                if (valid)
                    return true;
            }
            return false;
        }
        return true;
    }

    bool NoWallsOnVisionLine(Vector3 source, Vector3 target, float viewPointOffset)
    {
        RaycastHit hitInfo;
        bool seeCollider = Physics.Raycast(source + (target - source).normalized * viewPointOffset, (target - source), out hitInfo);
        if (seeCollider)
        {
            if (hitInfo.collider.gameObject.transform.position == target)
            {
                return true;
            }
        }

        return false;
    }

    public static List<Transform> FindObjectsInFOV(Transform center, float radius, float angle, List<Transform> objects)
    {
        return null;
    }
}
