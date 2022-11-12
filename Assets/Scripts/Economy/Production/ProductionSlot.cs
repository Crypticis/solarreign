using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductionSlot : MonoBehaviour
{
    public Recipe recipe;
    public Inventory playerInventory;

    [Header("Graphics")]

    public Button createButton;

    public bool hasSkill = false;

    public List<GameObject> requiredItemSlots = new List<GameObject>();
    public GameObject inputPrefab;
    public GameObject outputPrefab;
    public Transform inputTransform;
    public Transform outputTransform;

    public TooltipTrigger tooltip;

    private void Start()
    {
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

        for (int i = 0; i < recipe.producedItems.Length; i++)
        {
            var output = Instantiate(outputPrefab, outputTransform);
            output.GetComponent<Image>().sprite = recipe.producedItems[i].producedItem.sprite;
            output.transform.Find("Name").GetComponent<TMP_Text>().text = recipe.producedItems[i].producedItem.name;

            if (recipe.producedItems[i].amountIsRange)
            {
                output.transform.Find("Amount").GetComponent<TMP_Text>().text = recipe.producedItems[i].min.ToString() + "-" + recipe.producedItems[i].max.ToString();
            }
            else
            {
                output.transform.Find("Amount").GetComponent<TMP_Text>().text = recipe.producedItems[i].amount.ToString();
            }
        }
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
        for (int i = 0; i < recipe.producedItems.Length; i++)
        {
            if (recipe.producedItems[i].amountIsRange)
            {
                var amount = Random.Range(recipe.producedItems[i].min, recipe.producedItems[i].max);
                playerInventory.AddItem(recipe.producedItems[i].producedItem, amount);
                Ticker.Ticker.AddItem("You have made " + amount + " " + recipe.producedItems[i].producedItem + ".");
            }
            else
            {
                playerInventory.AddItem(recipe.producedItems[i].producedItem, recipe.producedItems[i].amount);
                Ticker.Ticker.AddItem("You have made a " + recipe.producedItems[i].producedItem + ".");
            }
        }

        for (int i = 0; i < recipe.requiredItems.Length; i++)
        {
            playerInventory.RemoveItem(recipe.requiredItems[i].requiredItem, recipe.requiredItems[i].amount);
        }

        Refresh();
    }
}
