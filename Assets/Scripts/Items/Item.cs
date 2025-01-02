using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string nameItem;
    public ItemType type;
    public abstract void Excute();
}
