using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : Equip
{//skrypt umieszczany na prefabach eq

    public float attackRate; //jak czesto mozemy atakowac
    private bool attacking;
    public float attackDistancel;

    [Header("Resourec Gathering")]
    public bool doesGatherResources;

    [Header("Combat")]
    public bool doesDealDamage;
    public int damage;

    //components
    private Animator anim;
    private Camera cam;

    private void Awake()
    {
        //get our components
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }

    public override void OnAttackInput()
    {
        if (!attacking)
        {
            attacking = true;
            anim.SetTrigger("Attack");
            Invoke("OnCanAttack", attackRate); //Invoke pozwala na wywolanie funkcji z opoznieniem czasowym
        }
    }

    void OnCanAttack()
    {
        attacking = true;
    }

    public void OnHit()
    {
        Debug.Log("Hit Detected");
    }

    
}
