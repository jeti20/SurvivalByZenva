using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{//skrypt dodany do przemiotow interkatywnych
    public ItemData itemdata;

    public string GetInteractPrompt()
    {
        return string.Format("Pickup {0}", itemdata.displayName);
    }

    public void OnInteract()
    {
        Inventory.instance.AddItem(itemdata);
        Destroy(gameObject);
    }
}
