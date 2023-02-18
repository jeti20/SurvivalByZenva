using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    public int damage;
    public float damageRate; //jak czesto dmg

    private List<IDamagable> thingsToDamage = new List<IDamagable>();

    private void Start()
    {
        StartCoroutine(DealDamage());
    }

    IEnumerator DealDamage()
    {
        while (true)
        {
            for (int i = 0; i < thingsToDamage.Count; i++)
            {
                thingsToDamage[i].TakePhysicaldamage(damage);
            }
            yield return new WaitForSeconds(damageRate);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<IDamagable>() != null) //zwraca true, poniewa¿ Player jest damagable, czy nie mo¿e byæ null po dotkniecu kaktusa
        {
            thingsToDamage.Add(collision.gameObject.GetComponent<IDamagable>()); //dodaje do listy Idamagable 
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<IDamagable>() != null)
        { 
            thingsToDamage.Remove(collision.gameObject.GetComponent<IDamagable>()); //usuwa z listy Idamagable jesli gracz przestanie dotykaæ kaktusa
        }
    }
}
