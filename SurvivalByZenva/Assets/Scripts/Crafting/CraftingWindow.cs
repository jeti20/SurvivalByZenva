using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingWindow : MonoBehaviour
{//managed UI elemets - canvascrafting

    public CraftngRecipeUI[] recipeUIs;

    public static CraftingWindow instance;

    //singleton
    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        Inventory.instance.onOpeInventory.AddListener(OnOpenInventory);
    }

    private void OnDisable()
    {
        Inventory.instance.onOpeInventory.RemoveListener(OnOpenInventory);
    }

    //wylacza okno wytwarzania
    void OnOpenInventory()
    {
        gameObject.SetActive(false);

    }

    //to bedzie wywolywane przez kazdy z przepisow
    public void Craft (CraftingRecipe recipe)
    {
        for (int i = 0; i < recipe.cost.Length; i++)//przegladamy co dana receptura potrzebuje
        {
            for (int x = 0; x < recipe.cost[i].quantity; x++)//sprawdza ilosc potrzenego surowca
            {
                Inventory.instance.RemoveItem(recipe.cost[i].item);
            }
        }

        Inventory.instance.AddItem(recipe.itemToCraft);

        //po wytworzeniu przedmiotiu pokazuje nam co mozemy teraz craftowac, a czego nie
        for (int i = 0; i < recipeUIs.Length; i++)
        {
            recipeUIs[i].UpdateCanccraft();
        }
    }
}
