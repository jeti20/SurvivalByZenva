using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour, IInteractable
{
    private CraftingWindow craftingWindow;
    private PlayerControl player;

    void Start()
    {
        craftingWindow = FindObjectOfType<CraftingWindow>(true);
        player = FindObjectOfType<PlayerControl>();
    }

    public string GetInteractPrompt()
    {
        return "Craft";
    }

    public void OnInteract()
    {
        craftingWindow.gameObject.SetActive(true);
        player.Togglecursor(true);
    }
}
