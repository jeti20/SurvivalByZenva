using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    public Button button;
    public Image icon;
    public TextMeshProUGUI quantityText;
    private ItemSlot curSlot;
    private Outline outline;

    public int index;
    public bool equipped;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    // wlaczanie outline w ekwipunktu jak jest zajete
    private void OnEnable()
    {
        outline.enabled = equipped;
    }

    //
    public void Set (ItemSlot slot)
    {
        curSlot = slot;

        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;
        quantityText.text = slot.quantity > 1 ? slot.ToString() : string.Empty; //jest quantity jest > 1 to zadzia� slot.ToString, ale jesli warunek jest false to string empty

        if (outline != null)
        {
            outline.enabled = equipped;
        }
    }

    public void Clear()
    {
        curSlot = null;

        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

    public void OnButtonclick()
    {

    }
}
