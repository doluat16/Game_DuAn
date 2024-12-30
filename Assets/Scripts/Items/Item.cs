using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected string nameItem;
    protected ItemType type;
    public abstract void Excute();
}
