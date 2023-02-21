using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftngRecipeUI : MonoBehaviour
{//managing each recepie in the crafting window
    public CraftingRecipe recipe;
    public Image backgroundImage;
    public Image icon;
    public TextMeshProUGUI itemName;
    public Image[] resourceCosts;

    public Color canCraftColor;
    public Color canotCraftColor;
    private bool canCraft;

    //jesli wlaczymy craftingwindow to co ma sie dziac
    private void OnEnable()
    {
        UpdateCanccraft();
    }

    //sprawdza zcy mamy wystarczajaco surowcow
    public void UpdateCanccraft()
    {
        canCraft = true;

        for (int i = 0; i < recipe.cost.Length; i++)//sprawdza czy mamy przedmiot
        {
            if (!Inventory.instance.HasItems(recipe.cost[i].item, recipe.cost[i].quantity))//jesli nie mamy tych przedmiotow i ich ilosci 
            {
                canCraft = false;
                break;
            }
        }

        backgroundImage.color = canCraft ? canCraftColor : canotCraftColor;

    }

    private void Start()
    {
        icon.sprite = recipe.itemToCraft.icon;
        itemName.text = recipe.itemToCraft.displayName;

        //setting up resources icon and quantites, przelatuje przez kazdy resource cost
        for (int i = 0; i < resourceCosts.Length; i++)
        {
            if (i < recipe.cost.Length)
            {
                resourceCosts[i].gameObject.SetActive(true);
                resourceCosts[i].sprite = recipe.cost[i].item.icon;
                resourceCosts[i].transform.GetComponentInChildren<TextMeshProUGUI>().text = recipe.cost[i].quantity.ToString();
            }
            else
            {
                resourceCosts[i].gameObject.SetActive(false);
            }
        }
    }
    public void OnClickButton()
    {
        if (canCraft)
        {
            CraftingWindow.instance.Craft(recipe);
        }
    }
}
