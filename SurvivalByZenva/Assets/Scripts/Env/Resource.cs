using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public ItemData itemToGive;
    public int quantityPerHit; //ile dajemy graczowi surowca z uderzeniem
    public int capacity; //ile uderzen jest potrzebne do zniszzcenia tego resource
    public GameObject hitParticle;

    public void Gather(Vector3 hitPoint, Vector3 hitNormal)
    {
        for (int i = 0; i < quantityPerHit; i++)
        {
            if (capacity <= 0) //pozwala na wyjscie z for loop w momencie skonczenie resource z prefaba np drzwa
                break;

            capacity -= 1;

            Inventory.instance.AddItem(itemToGive);
        }

        Destroy(Instantiate(hitParticle, hitPoint, Quaternion.LookRotation(hitNormal, Vector3.up)), 1.0f);

        //sprawdzanie czy drzewo mozna juz zniszczyc 
        if (capacity <= 0)
            Destroy(gameObject);
    }
}
