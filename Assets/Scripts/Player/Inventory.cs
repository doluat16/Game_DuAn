using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private int slot;
    public void UseItem()
    {
        if (item != null)
        {
            Item newItem = Instantiate(item, this.transform.position, Quaternion.identity);
            newItem.Excute();
            slot--;
            if (slot <= 0)
                item = null;
        }
    }

    public void CollectItem(ItemCollectable _item)
    {
        if (_item != null && item == null)
        {
            item = _item.item;
        }
        else if (_item.item.nameItem == item.nameItem)
        {
            slot++;
        }
    }
}
