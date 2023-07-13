using UnityEngine;
using System.Linq;

public class Container : MonoBehaviour
{
    [SerializeField] int maxItemsNumber;
    [SerializeField] Item[] itemsInContainer;
    public int MaxItemsNumber => maxItemsNumber;
    public bool HasFreeSpace => GetFreeContainerIndex() == -1 ? false : true;

    int itemsNumber = 0;

    public void Start()
    {
        itemsInContainer = new Item[maxItemsNumber];
    }
    public void AddNewItem(Item item)
    {
        AddItemInIndex(item, GetFreeContainerIndex());
    }
    public void AddItemInIndex(Item item, int index)
    {
        itemsInContainer[index] = item;
        itemsNumber++;
    }
    int GetFreeContainerIndex()
    {
        for (int i = 0; i < maxItemsNumber; i++)
        {
            if (itemsInContainer[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
    public Item GetItem(int index)
    {
        return itemsInContainer[index];
    }
    public void RemoveItemAtIndex(int index)
    {   
        itemsInContainer[index] = null;
        itemsNumber--;  
    }
    public bool Contains(Item item)
    {
        return itemsInContainer.Contains(item);
    }
    public void Remove(Item item)
    {
        for (int i = 0; i < itemsNumber; i++)
        {
            if (itemsInContainer[i] == item)
            {
                RemoveItemAtIndex(i);
            }
        }
    }
}
