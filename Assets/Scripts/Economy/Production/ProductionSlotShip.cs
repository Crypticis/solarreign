using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductionSlotShip : MonoBehaviour
{
    public ShipRecipe recipe;
    public Inventory playerInventory;
    public Fleet fleet;

    [Header("Graphics")]

    public Button createButton;

    public bool hasSkill = false;

    public List<GameObject> requiredItemSlots = new List<GameObject>();
    public GameObject inputPrefab;
    public GameObject outputPrefab;
    public Transform inputTransform;
    public Transform outputTransform;

    TooltipTrigger tooltip;

    private void Start()
    {
        tooltip = GetComponentInChildren<TooltipTrigger>();

        for (int i = 0; i < recipe.requiredItems.Length; i++)
        {
            var input = Instantiate(inputPrefab, inputTransform);
            input.transform.Find("Name").GetComponentInChildren<TMP_Text>().text = recipe.requiredItems[i].requiredItem.name;
            if (playerInventory.CheckIfInInventory(recipe.requiredItems[i].requiredItem))
            {
                input.transform.Find("Owned").transform.Find("Owned Amount").GetComponentInChildren<TMP_Text>().text = playerInventory.CheckAmountInInventory(recipe.requiredItems[i].requiredItem).ToString();
            }
            else
            {
                input.transform.Find("Owned").transform.Find("Owned Amount").GetComponentInChildren<TMP_Text>().text = "0";
            }
            input.transform.Find("Owned").transform.Find("Required Amount").GetComponentInChildren<TMP_Text>().text = recipe.requiredItems[i].amount.ToString();
            input.GetComponent<Image>().sprite = recipe.requiredItems[i].requiredItem.sprite;

            requiredItemSlots.Add(input);
        }

        var output = Instantiate(outputPrefab, outputTransform);
        output.GetComponent<Image>().sprite = recipe.producedShip.GetComponent<HUDElements>().icon;
        output.transform.Find("Name").GetComponent<TMP_Text>().text = recipe.producedShip.GetComponent<HUDElements>().name;

        output.transform.Find("Amount").GetComponent<TMP_Text>().text = "1";
    }

    public void OnEnable()
    {
        Refresh();

        //Debug.Log(HasResources());
    }

    public void Refresh()
    {
        if (!HasResources())
        {
            createButton.interactable = false;

            if (!tooltip)
            {
                tooltip = GetComponentInChildren<TooltipTrigger>();
            }

            if (tooltip)
            {
                tooltip.enabled = true;
                tooltip.content = "Missing required resources.";
            }
        }
        else
        {
            if (tooltip)
            {
                tooltip.enabled = false;
            }

            createButton.interactable = true;
        }
        //else
        //{
        //    createButton.interactable = false;
        //}

        for (int i = 0; i < requiredItemSlots.Count; i++)
        {
            if (playerInventory.CheckIfInInventory(recipe.requiredItems[i].requiredItem))
            {
                requiredItemSlots[i].transform.Find("Owned").transform.Find("Owned Amount").GetComponentInChildren<TMP_Text>().text = playerInventory.CheckAmountInInventory(recipe.requiredItems[i].requiredItem).ToString();
            }
            else
            {
                requiredItemSlots[i].transform.Find("Owned").transform.Find("Owned Amount").GetComponentInChildren<TMP_Text>().text = "0";
            }        
        }
    }

    public void AttemptCraft()
    {
        if(HasResources() == true)
        {
            Craft();
        }

        this.transform.parent.GetComponent<ProductionUI>().Refresh();
    }

    public bool HasResources()
    {
        for (int i = 0; i < recipe.requiredItems.Length; i++)
        {
            if (playerInventory.CheckIfInInventory(recipe.requiredItems[i].requiredItem))
            {
                if(playerInventory.CheckAmountInInventory(recipe.requiredItems[i].requiredItem) < recipe.requiredItems[i].amount)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public void Craft()
    {
        fleet.AddToFleet(recipe.producedShip);

        Ticker.Ticker.AddItem("You have made a " + recipe.producedShip.GetComponent<HUDElements>().name + " and it has been added to your fleet.");

        for (int i = 0; i < recipe.requiredItems.Length; i++)
        {
            playerInventory.RemoveItem(recipe.requiredItems[i].requiredItem, recipe.requiredItems[i].amount);
        }
    }
}
