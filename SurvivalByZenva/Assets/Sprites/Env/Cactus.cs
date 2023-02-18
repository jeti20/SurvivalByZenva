using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    public int damage;
    public float damageRate; //jak czesto dmg

    private List<IDamagable> thingsToDamage = new List<IDamagable>();

    IEnumerable DealDamage()
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
}
