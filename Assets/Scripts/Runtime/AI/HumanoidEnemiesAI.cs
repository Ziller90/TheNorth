using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public enum States
{
    Chase,
    BlindChase,
    Patrol,
    Idle,
    Attack,
    SearchEnemy
}
public class HumanoidEnemiesAI : MonoBehaviour
{
    [SerializeField] Sensors sensors;
    [SerializeField] States AIState;
    [SerializeField] AINavigationManager navigationManager;
    [SerializeField] float distanceToAttack;
    [SerializeField] float distanceToLastEnemyPosition;
    [SerializeField] bool hasPatrolRoute;
    [SerializeField] GameObject debugPoint;
    [SerializeField] ActionManager actionManager;

    [SerializeField] float searchingRadius;
    [SerializeField] float enemySearchingTime;
    [SerializeField] int searhingCornersNumber;

    [SerializeField] HumanoidInventoryContainer AIInventory;

    bool searchEnemy;
    Vector3 lastEnemyPosition;
    bool searhingRouteGenerated;
    Transform currentEnemy;

    public Vector3[] GenerateSearchingRoute(Vector3 LastSeenPosition)
    {
        Vector3[] newRoute = new Vector3[searhingCornersNumber];
        for (int i = 0; i < searhingCornersNumber; i++)
        {
            Vector3 newCorner = LastSeenPosition + Random.insideUnitSphere * searchingRadius;
            newCorner.y = 1.3f;
            NavMeshHit hit;
            NavMesh.SamplePosition(newCorner, out hit, 2, 1);
            newCorner = hit.position;
            newRoute[i] = newCorner;
            Instantiate(debugPoint, newCorner, Quaternion.identity);
        }
        return newRoute;
    }

    void EquipBestItems()
    {
        EquipMostExpensiveItemSuitableForSlot(AIInventory.MainWeaponSlot);
        EquipMostExpensiveItemSuitableForSlot(AIInventory.SecondaryWeaponSlot);
    }

    void EquipMostExpensiveItemSuitableForSlot(Slot slot)
    {
        var suitableSlots = AIInventory.BackpackSlots.Slots.Where(i => !i.isEmpty && slot.IsSuitable(i.ItemStack));
        var mostExpensiveItemSlot = suitableSlots.OrderByDescending(i => i.ItemStack.Item.Cost).FirstOrDefault();

        if (mostExpensiveItemSlot != null)
            ModelUtils.TryMoveFromSlotToSlot(AIInventory, mostExpensiveItemSlot, AIInventory, slot);
    }
}
