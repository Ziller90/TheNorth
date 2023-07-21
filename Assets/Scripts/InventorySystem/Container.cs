using UnityEngine;
using System.Linq;


public class Container : MonoBehaviour
{
    [SerializeField] Item[] itemsInContainer;
    public int MaxItemsNumber => itemsInContainer.Length;
    public bool HasFreeSpace => GetFreeIndex() == -1 ? false : true;

    public void AddNewItem(Item item) => AddItemInIndex(item, GetFreeIndex());
    public Item GetItem(int index) => itemsInContainer[index];
    public void RemoveItemAtIndex(int index) => itemsInContainer[index] = null;
    public bool Contains(Item item) => itemsInContainer.Contains(item);
    public void AddItemInIndex(Item item, int index) => itemsInContainer[index] = item;

    int GetFreeIndex()
    {
        for (int i = 0; i < MaxItemsNumber; i++)
        {
            if (itemsInContainer[i] == null)
                return i;
        }
        return -1;
    }

    public void Remove(Item item)
    {
        for (int i = 0; i < MaxItemsNumber; i++)
        {
            if (itemsInContainer[i] == item)
                RemoveItemAtIndex(i);
        }
    }
}
