using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Instrument Object", menuName = "Inventory System/Items/Instrument")]
public class InstrumentObject : ItemObject
{
    public float potencyBonus;
    public void Awake()
    {
        type = ItemType.Instrument;
    }
}
