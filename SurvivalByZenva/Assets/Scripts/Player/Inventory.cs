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
    private PlayerNeeds needs;

    [Header("Events")]
    public UnityEvent onOpeInventory; //bedzie wywolany jak otwieramy ekwipunek (wlaczenie myszki i mozliwosci obracania sie)
    public UnityEvent onColseInventory; // kiedy zamykamy (wylaczenie myszki i przywrocenie mozliwosci obracania sie)

    //Singleton
    public static Inventory instance;

    private void Awake()
    {
        instance =  this;
        controller = GetComponent<PlayerControl>();
        needs = GetComponent<PlayerNeeds>();
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

        ClearSelectedItemWindow();
    }

    public void OnInventoryButton(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Toggle();
        }
    }

    //otwieranie i zamykanie ekwipunktu
    public void Toggle()
    {
        //jesli eq jest otwarty to pojawia sie myszka i blokuje obracanie sie
        if (inventoryWindow.activeInHierarchy)
        {
            inventoryWindow.SetActive(false);
            onColseInventory.Invoke();
            controller.Togglecursor(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
            onOpeInventory.Invoke();
            ClearSelectedItemWindow();
            controller.Togglecursor(true);
        }
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
        if (item.canStack)
        {
            ItemSlot slotToStackTo = GetItemStack(item); //GetItemStack - szuka w ekwipunktu slota w ktorym moze zestackowac ten item

            //jesli jest w ekwipunku juz przedmiot z ktorym mozna zestackowac
            if (slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                UpdateUI();
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();

        //jesli nie ma w ekwipunku przedmiotu z ktorym moze byc zestackowany, wiec szuka pusetgo slota i dodaje do niego 
        if (emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            UpdateUI();
            return;
        }

        ThrowItem(item);

    }

    //spawnuje item po wyrzuceniu
    void ThrowItem (ItemData item)
    {
        Instantiate(item.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360.0f));
    }

    //updejtuje UI ekwipunku
    void UpdateUI ()
    {
        for (int x = 0; x < slots.Length; x++)
        {
            //czy slot zawiera item, jesli tak to ustawia itemek tak jak powinien sie wyswietlac, jesli to to czysci slot
            if (slots[x].item != null)
            {
                uiSlots[x].Set(slots[x]);
            }
            else
            {
                uiSlots[x].Clear();
            }
                
        }
    }

    //zwraca slot po tym jak zestackowalismy iteemki, zwraca nu;; jesli nie ma mozlowisco stackowania
    ItemSlot GetItemStack (ItemData item)
    {
        for (int x = 0; x < slots.Length; x++)
        {
            //czy przedmiot który sprawdzamy jest tym itemkiem z ktorym mozemy zestacokowac i czy nie ma juz max stack
            if (slots[x].item == item && slots[x].quantity < item.maxStackAmount)
            {
                return slots[x];
            }
        }

        //nie ma przedmiotu do stackownia w ekwipunktu
        return null;
    }

    // zwraca pusty slot w ekwipunku. Jesli nie ma wolnych itemkow to zwraca null
    ItemSlot GetEmptySlot ()
    {
        for (int x = 0; x < slots.Length; x++)
        {
            //jesli itemek w tym slocie nie istnieje to zwracamy slot
            if (slots[x].item == null)
            {
                return slots[x];
            }
        }

        return null;
    }

    //jest wywolywana jesli kliknie sie w item slot
    public void SelectItem (int index)
    {
        // we can't select the slot if there's no item
        if (slots[index].item == null)
            return;

        // set the selected item preview window
        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.item.displayName;
        selectedItemDescription.text = selectedItem.item.description;

        // set stat values and stat names
        selectedItemStatNames.text = string.Empty;
        selectedItemStatValues.text = string.Empty;

        //leci przez np banana i jakie wlasciwosci ma wa array np. thirst i hunger
        for (int i = 0; i < selectedItem.item.consumables.Length; i++)
        {
            selectedItemStatNames.text += selectedItem.item.consumables[i].type.ToString() +"\n";
            selectedItemStatValues.text += selectedItem.item.consumables[i].value.ToString() +"\n";
        }

        useButton.SetActive(selectedItem.item.type == ItemType.Consumable);
        equipButton.SetActive(selectedItem.item.type == ItemType.Equipable && !uiSlots[index].equipped);
        unequipButton.SetActive(selectedItem.item.type == ItemType.Equipable && uiSlots[index].equipped);
        dropButton.SetActive(true);
    }

    //wywolywana w momencie otwarca ekwipunku lub kiedy wybrany przedmiot jest wyczerpany
    public void ClearSelectedItemWindow ()
    {
        //clear the text elemetns
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedItemStatNames.text = string.Empty;
        selectedItemStatValues.text = string.Empty;

        //disable butons
        useButton.SetActive(false);
        equipButton.SetActive(false);
        unequipButton.SetActive(false);
        dropButton.SetActive(false);
    }
    
    //przycisk use w eq
    public void OnUseButton ()
    {
        //spr czy item jest consumable
        if (selectedItem.item.type == ItemType.Consumable)
        {
            for (int i = 0; i < selectedItem.item.consumables.Length; i++)
            {
                //sprawdza ktory consumable to jest, a jesli znajdzie to wywoluje konkretna funckje needs...
                switch (selectedItem.item.consumables[i].type)
                {
                    case consumableType.Hunger: needs.Eat(selectedItem.item.consumables[i].value);
                        break;
                    case consumableType.Thirst: needs.Drinkg(selectedItem.item.consumables[i].value);
                        break;
                    case consumableType.Health: needs.Heal(selectedItem.item.consumables[i].value);
                        break;
                    case consumableType.Sleep: needs.Sleep(selectedItem.item.consumables[i].value);
                        break;
                    default:
                        break;
                }
            }
        }

        RemoveSeletedItem();
    }

    public void OnEquipButton()
    {
        if (uiSlots[curEquipIndex].equipped)//curEquipIndex to index przedmiotu ktory jest obecnie wyposarzony
        {
            UnEquip(curEquipIndex);
        }
        uiSlots[selectedItemIndex].equipped = true;
        curEquipIndex = selectedItemIndex;
        EquipManager.instance.EquipNew(selectedItem.item);
        UpdateUI();

        SelectItem(selectedItemIndex);
    }

    void UnEquip(int index)
    {
        uiSlots[index].equipped = false;
        EquipManager.instance.UnEquip();
        UpdateUI();

        if (selectedItemIndex == index)
        {
            SelectItem(index);
        }
    }

    public void OnUnEquipbutton()
    {
        UnEquip(selectedItemIndex);  
    }

    //przycisk 'drop' w w eq
    public void OnDropButton ()
    {
        ThrowItem(selectedItem.item);
        RemoveSeletedItem();
    }

    //usuwa obecnie wybrany item w eq
    void RemoveSeletedItem ()
    {
        selectedItem.quantity--;

        if(selectedItem.quantity == 0)
        {
            if(uiSlots[selectedItemIndex].equipped == true)
                UnEquip(selectedItemIndex);

            selectedItem.item = null;
            ClearSelectedItemWindow();
        }

        UpdateUI();
    }

    public void RemoveItem (ItemData item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item) //czy slot zawiera item
            {
                slots[i].quantity--;

                if (slots[i].quantity == 0) //czysci slot jestli jest rowny zero
                {
                    if (uiSlots[i].equipped == true)
                    {
                        UnEquip(i);

                        slots[i].item = null;
                        ClearSelectedItemWindow();
                    }
                    UpdateUI();
                    return;
                }
            }
        }
    }


    //czy gracz ma wystarczajac ilosc itemku 
    public bool HasItems (ItemData item, int quantity)
    {
        int amount = 0;

        for (int i = 0; i < slots.Length; i++)//sprawdza, czy mamy wystarczaj¹ca ilosc resources zeby wytworzyc, leic przez wyszstkie sloty, bo drewno moze byc np stakkowane w paru slotach
        {
            if (slots[i].item == item)
            {
                amount += slots[i].quantity;
            }

            if (amount >= quantity)
            {
                return true;
            }
        }
        return false;
    }

}

//przechowuje informacje o slotach w ekwipunku
public class ItemSlot
{
    public ItemData item;
    public int quantity;
}
