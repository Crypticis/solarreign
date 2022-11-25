using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class ProductionUI : MonoBehaviour
{
    public ProductionSlot[] productionSlots;

    void Start()
    {
        productionSlots = GetComponentsInChildren<ProductionSlot>();
        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < productionSlots.Length; i++)
        {
            productionSlots[i].Refresh();

            if(productionSlots[i].recipe.RequiredItems.Length <= StatManager.instance.productionLevel + 1)
            {
                //productionSlots[i].transform.Find("Button").GetComponent<Button>().interactable = false;

                productionSlots[i].hasSkill = true;
            }
            else
            {
                //productionSlots[i].transform.Find("Button").GetComponent<Button>().interactable = true;

                productionSlots[i].hasSkill = false;
            }
        }
    }
}
