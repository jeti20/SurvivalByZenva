using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipManager : MonoBehaviour
{//skrypt umieszczany na player


    public Equip curEquip;
    public Transform equipParent;

    private PlayerControl controller;

    //singleton
    public static EquipManager instance;

    private void Awake ()
    {
        instance = this;
        controller = GetComponent<PlayerControl>();
    }

    //Input sytem 
    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && curEquip != null && controller.canLook == true)
        {
            curEquip.OnAttackInput();
        }
    }

    //Input system 
    public void OnAltAtackInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && curEquip != null && controller.canLook == true)
        {
            curEquip.OnAltAttackInput();
        }
    }

    public void EquipNew (ItemData item)
    {
        UnEquip();
        curEquip = Instantiate(item.equipPrefab, equipParent).GetComponent<Equip>();
    }

    public void UnEquip ()
    {
        if (curEquip != null)
        {
            Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }
}
