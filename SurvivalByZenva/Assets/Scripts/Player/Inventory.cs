using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Inventory : MonoBehaviour
{
    public ItemSlotUI[] uiSlots; //modyfikuje elementy ui
    public ItemSlot[] slots; //dane dot. slotu

    public GameObject inventoryWindow; //inventory window ui
    public Transform dropPosition;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex; //pozwala œledzic index wybranego przedmiotu
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatNames;
    public TextMeshProUGUI selectedItemStatValues;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unequipButton;
    public GameObject dropButton;

    private int curEquipIndex; //który slot zajmiemy w ekwipnku

    //compoents
    private PlayerControl controller;

    [Header("Events")]
    public UnityEvent onOpeInventory; //bedzie wywolany jak otwieramy ekwipunek (wlaczenie myszki i mozliwosci obracania sie)
    public UnityEvent onColseInventory; // kiedy zamykamy (wylaczenie myszki i przywrocenie mozliwosci obracania sie)

    //Singleton
    public static Inventory instance;

    private void Awake()
    {
        instance =  this;
        controller = GetComponent<PlayerControl>();
    }

    private void Start()
    {
        inventoryWindow.SetActive(false);
        slots = new ItemSlot[uiSlots.Length];

        //initialize the slots 
        for (int x = 0; x < slots.Length; x++)
        {
            slots[x] = new ItemSlot();
            uiSlots[x].index = x;
            uiSlots[x].Clear();
        }
    }

    public void Toggle()
    {

    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    public void AddItem(ItemData item)
    {

    }

    void ThrowItem (ItemData item)
    {

    }

    void UpdateUI ()
    {

    }

    ItemSlot GetItemStack (ItemData item)
    {
        return null;
    }

    ItemSlot GetEmptySlot ()
    {
        return null;
    }

    public void SelectItem (int index)
    {

    }

    public void ClearSelectedItemWindow ()
    {
        
    }

    public void OnUseButton ()
    {

    }

    public void OnEquipButton()
    {

    }

    public void OnUnEquipbutton()
    {
        
    }

    public void OnDropButton ()
    {

    }

    void RemoveSeletedItem ()
    {

    }

    public void RemoveItem (ItemData item)
    {

    }

    public bool HaItems (ItemData item, int quantity)
    {
        return false;
    }

}

public class ItemSlot
{
    public ItemData item;
    public int quantity;
}
