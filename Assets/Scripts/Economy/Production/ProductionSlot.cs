using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class ProductionSlot : MonoBehaviour
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
        for (int i = 0; i < recipe.RequiredItems.Length; i++)
        {
            var input = Instantiate(inputPrefab, inputTransform);
            input.transform.Find("Name").GetComponentInChildren<TMP_Text>().text = recipe.RequiredItems[i].requiredItem.name;
            if (playerInventory.CheckIfInInventory(recipe.RequiredItems[i].requiredItem))
            {
                input.transform.Find("Owned").transform.Find("Owned Amount").GetComponentInChildren<TMP_Text>().text = playerInventory.CheckAmountInInventory(recipe.RequiredItems[i].requiredItem).ToString();
            }
            else
            {
                input.transform.Find("Owned").transform.Find("Owned Amount").GetComponentInChildren<TMP_Text>().text = "0";
            }
            input.transform.Find("Owned").transform.Find("Required Amount").GetComponentInChildren<TMP_Text>().text = recipe.RequiredItems[i].amount.ToString();
            input.GetComponent<Image>().sprite = recipe.RequiredItems[i].requiredItem.sprite;

            requiredItemSlots.Add(input);
        }

        for (int i = 0; i < recipe.ProducedItems.Length; i++)
        {
            var output = Instantiate(outputPrefab, outputTransform);
            output.GetComponent<Image>().sprite = recipe.ProducedItems[i].producedItem.sprite;
            output.transform.Find("Name").GetComponent<TMP_Text>().text = recipe.ProducedItems[i].producedItem.name;

            if (recipe.ProducedItems[i].amountIsRange)
            {
                output.transform.Find("Amount").GetComponent<TMP_Text>().text = recipe.ProducedItems[i].min.ToString() + "-" + recipe.ProducedItems[i].max.ToString();
            }
            else
            {
                output.transform.Find("Amount").GetComponent<TMP_Text>().text = recipe.ProducedItems[i].amount.ToString();
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
            if (playerInventory.CheckIfInInventory(recipe.RequiredItems[i].requiredItem))
            {
                requiredItemSlots[i].transform.Find("Owned").transform.Find("Owned Amount").GetComponentInChildren<TMP_Text>().text = playerInventory.CheckAmountInInventory(recipe.RequiredItems[i].requiredItem).ToString();
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
        for (int i = 0; i < recipe.RequiredItems.Length; i++)
        {
            if (playerInventory.CheckIfInInventory(recipe.RequiredItems[i].requiredItem))
            {
                if(playerInventory.CheckAmountInInventory(recipe.RequiredItems[i].requiredItem) < recipe.RequiredItems[i].amount)
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
        for (int i = 0; i < recipe.ProducedItems.Length; i++)
        {
            if (recipe.ProducedItems[i].amountIsRange)
            {
                var amount = Random.Range(recipe.ProducedItems[i].min, recipe.ProducedItems[i].max);
                playerInventory.AddItem(recipe.ProducedItems[i].producedItem, amount);
                Ticker.Ticker.AddItem("You have made " + amount + " " + recipe.ProducedItems[i].producedItem + ".");
            }
            else
            {
                playerInventory.AddItem(recipe.ProducedItems[i].producedItem, recipe.ProducedItems[i].amount);
                Ticker.Ticker.AddItem("You have made a " + recipe.ProducedItems[i].producedItem + ".");
            }
        }

        for (int i = 0; i < recipe.RequiredItems.Length; i++)
        {
            playerInventory.RemoveItem(recipe.RequiredItems[i].requiredItem, recipe.RequiredItems[i].amount);
        }

        Refresh();
    }
}
