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

        //inicializuje sloty
        for (int x = 0; x < slots.Length; x++)
        {
            slots[x] = new ItemSlot();
            uiSlots[x].index = x;
            uiSlots[x].Clear();
        }
    }

    //otwieranie i zamykanie ekwipunktu
    public void Toggle()
    {

    }

    //sprawdza czy ekwipunek jest otwarty
    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    //dodaje wybrane przedmioty do ekwiputnku
    public void AddItem(ItemData item)
    {
        //sprawdza czy przedmiot moze mbyc stackowany


    }

    //spawnuje item po wyrzuceniu
    void ThrowItem (ItemData item)
    {

    }

    //updejtuje UI ekwipunku
    void UpdateUI ()
    {

    }

    //zwraca slot po tym jak zestackowalismy iteemki, zwraca nu;; jesli nie ma mozlowisco stackowania
    ItemSlot GetItemStack (ItemData item)
    {
        return null;
    }

    // zwraca pusty slot w ekwipunku. Jesli nie ma wolnych itemkow to zwraca null
    ItemSlot GetEmptySlot ()
    {
        return null;
    }

    //jest wywolywana jesli kliknie sie w item slot
    public void SelectItem (int index)
    {

    }

    //wywolywana w momencie otwarca ekwipunku lub kiedy wybrany przedmiot jest wyczerpany
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

    //usuwa obecnie wybrany item
    void RemoveSeletedItem ()
    {

    }

    public void RemoveItem (ItemData item)
    {

    }

    //czy gracz ma wystarczajac ilosc itemku 
    public bool HaItems (ItemData item, int quantity)
    {
        return false;
    }

}

//przechowuje informacje o slotach w ekwipunku
public class ItemSlot
{
    public ItemData item;
    public int quantity;
}
