using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : Equip
{//skrypt umieszczany na prefabach eq

    public float attackRate; //jak czesto mozemy atakowac
    private bool attacking;
    public float attackDistance;

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
        attacking = false;
    }

    public void OnHit()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackDistance))
        {
            //did we hit Resource?
            if (doesGatherResources && hit.collider.GetComponent<Resource>()) //sprawzdza czy uderzyliscmy obiekt z skryptem Resorce
            {
                hit.collider.GetComponent<Resource>().Gather(hit.point, hit.normal); //gather it
            }
            //did we hit damagable?
            if (doesDealDamage && hit.collider.GetComponent<IDamagable>() != null);
            {
                hit.collider.GetComponent<IDamagable>().TakePhysicaldamage(damage); //deal damage
            }
        }
    }

    
}
