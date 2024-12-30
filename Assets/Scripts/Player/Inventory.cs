using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Item item;

    public void UseItem()
    {
        if (item != null)
        {
            Item newItem = Instantiate(item, this.transform.position, Quaternion.identity);
            newItem.Excute();
            item = null;
        }
    }

    public void CollectItem(ItemCollectable _item)
    {
        if (_item != null && item == null)
        {
            item = _item.item;
        }
    }
}
