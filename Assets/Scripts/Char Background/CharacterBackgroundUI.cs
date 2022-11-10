using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBackgroundUI : MonoBehaviour
{
    public GameObject optionsPrefab;
    public Transform optionsRoot;

    public TMP_Text optionDescription;

    public string[,,] pageInfo;
    public int page;
    public int selectedOption;

    public int[] rememberedChoices = new int[4];

    public TMP_Text[] previousSelectedOptions;
    public MainMenu mainMenu;

    public TMP_Text commandText, logisticsText, productionText, missilesText, projectileText, energyText, tradeText;

    public TypewriterEffect typewriterEffect;

    // Start is called before the first frame update
    void Start()
    {
        optionDescription.text = "Your parents...";

        page = 0;
        pageInfo = new string[4, 4, 2];
        // [Page #, Item #, Title/Description]
        pageInfo[0, 0, 0] = "Moondust Traders";
        pageInfo[0, 1, 0] = "Smugglers";
        pageInfo[0, 2, 0] = "Ship Mechanics";
        pageInfo[0, 3, 0] = "Fleet Pilots";

        pageInfo[1, 0, 0] = "Command";
        pageInfo[1, 1, 0] = "Quick Thinking";
        pageInfo[1, 2, 0] = "Planning";
        pageInfo[1, 3, 0] = "Intelligence";

        pageInfo[2, 0, 0] = "Assistant to an Admiral";
        pageInfo[2, 1, 0] = "Flew Drones Part-Time";
        pageInfo[2, 2, 0] = "Worked in Delivery";
        pageInfo[2, 3, 0] = "Apprenticed in a Rig Shop";

        pageInfo[3, 0, 0] = "Led a Mining Expedition";
        pageInfo[3, 1, 0] = "Became a Pilot in the Navy";
        pageInfo[3, 2, 0] = "Ran a Delivery Route";
        pageInfo[3, 3, 0] = "Repaired a Junk Ship";

        pageInfo[0, 0, 1] = "Your parents traded in moondust, an illegal substance in the galaxy. As a result of this, you quickly picked up on trade from an early age. \n+25 Trade Proficiency";
        pageInfo[0, 1, 1] = "Your parents were smugglers who transported contraband or blackmarket items across faction lines. As a result of this, you picked up on logistics from an early age. \n+1 Logistics.";
        pageInfo[0, 2, 1] = "Your parents were ship mechanics who worked day in and day out fixing ships. As a result of this, you came to understand ships and how they were made. \n+1 Production.";
        pageInfo[0, 3, 1] = "Your parents were Fleet Pilots in the faction navy. As a result of this, you learned about different weapons and how to use them. \n+25 Missiles, Projectile, and Energy.";

        pageInfo[1, 0, 1] = "In your early childhood, you showed an predisposition towards commanding other children. You led your group of children. \n+1 Command.";
        pageInfo[1, 1, 1] = "In your early childhood, you showed an ability to quickly solve problems on the fly. These insticts will help in combat. \n+10 Missiles, Projectile, and Energy.";
        pageInfo[1, 2, 1] = "In your early childhood, you showed good planning skills. You were able to plan out your time well compared to other children. \n+1 Logistics.";
        pageInfo[1, 3, 1] = "In your early childhood, you were particularily intelligent. You wanted to understand how things worked. This will assist you in making things. \n+1 Production.";

        pageInfo[2, 0, 1] = "In your adolescence, your parents arranged for you to assist an admiral. You may have only delivered coffee but you were able to learn a lot on how to command. \n+1 Command.";
        pageInfo[2, 1, 1] = "In your adolescence, you flew mining drones part-time. This allowed you to build up skills which will assist in combat. \n+10 Missiles, Projectile, and Energy.";
        pageInfo[2, 2, 1] = "In your adolescence, you worked at a delivery company. This experience in delivery allowed you to build skills in effective travel and resource management. \n+1 Logistics";
        pageInfo[2, 3, 1] = "In your adolescence, you apprenticed in a logal rigging shop. The legality of the rigs or their parts was not on your mind, only the knowledge of parts are assembled. \n+1 Production.";

        pageInfo[3, 0, 1] = "When you became an adult, you led a mining expedition to a nearby asteroid belt. The experience taught you many things, the most important being how to lead. \n+1 Command";
        pageInfo[3, 1, 1] = "When you became an adult, you became a pilot in your factions navy. As a result of your time as a pilot, you became an effective soldier. \n+25 Missiles, Projectile, and Energy.";
        pageInfo[3, 2, 1] = "When you became an adult, you ran your own delivery routes independent of any other company. This taught you how to manage your resources most effectively. \n+1 Logistics.";
        pageInfo[3, 3, 1] = "When you became an adult, you repaired your own junk ship that you recovered in space. The process of repairing ship taught you better how to assemble and repair parts. \n+1 Production.";

        for (int i = 0; i < 4; i++)
        {
            GameObject go = Instantiate(optionsPrefab, optionsRoot);
            go.GetComponentInChildren<TMP_Text>().text = pageInfo[page, i, 0];
            int x = i;
            go.GetComponent<Button>().onClick.AddListener( ()=> ShowDescription(x));
        }
    }

    public void ShowDescription(int index)
    {
        //optionDescription.text = pageInfo[page, index, 1];
        if(typewriterEffect.IsRunning) { typewriterEffect.Stop(); }
        typewriterEffect.Run(pageInfo[page, index, 1], optionDescription);
        selectedOption = index;
    }

    public void Next()
    {
        if (selectedOption != -1)
        {
            switch (page)
            {
                case 0:

                    if(selectedOption == 0)
                    {
                        StatManager.instance.playerStatsObject.startingTrade += 25;
                        rememberedChoices[page] = selectedOption;
                    }
                    else if (selectedOption == 1)
                    {
                        StatManager.instance.playerStatsObject.startingLogisticsLevel += 1;
                        rememberedChoices[page] = selectedOption;
                    }
                    else if (selectedOption == 2)
                    {
                        StatManager.instance.playerStatsObject.startingProductionLevel += 1;
                        rememberedChoices[page] = selectedOption;
                    }
                    else
                    {
                        StatManager.instance.playerStatsObject.startingMissile += 25;
                        StatManager.instance.playerStatsObject.startingProjectile += 25;
                        StatManager.instance.playerStatsObject.startingEnergy += 25;
                        rememberedChoices[page] = selectedOption;
                    }

                    previousSelectedOptions[page].text = pageInfo[page, selectedOption, 0];

                    break;

                case 1:

                    if (selectedOption == 0)
                    {
                        StatManager.instance.playerStatsObject.startingCommandLevel += 1;
                        rememberedChoices[page] = selectedOption;
                    }
                    else if (selectedOption == 1)
                    {
                        StatManager.instance.playerStatsObject.startingMissile += 10;
                        StatManager.instance.playerStatsObject.startingProjectile += 10;
                        StatManager.instance.playerStatsObject.startingEnergy += 10;
                        rememberedChoices[page] = selectedOption;
                    }
                    else if (selectedOption == 2)
                    {
                        StatManager.instance.playerStatsObject.startingLogisticsLevel += 1;
                        rememberedChoices[page] = selectedOption;
                    }
                    else
                    {
                        StatManager.instance.playerStatsObject.startingProductionLevel += 1;
                        rememberedChoices[page] = selectedOption;
                    }

                    previousSelectedOptions[page].text = pageInfo[page, selectedOption, 0];

                    break;

                case 2:

                    if (selectedOption == 0)
                    {
                        StatManager.instance.playerStatsObject.startingCommandLevel += 1;
                        rememberedChoices[page] = selectedOption;
                    }
                    else if (selectedOption == 1)
                    {
                        StatManager.instance.playerStatsObject.startingMissile += 25;
                        StatManager.instance.playerStatsObject.startingProjectile += 25;
                        StatManager.instance.playerStatsObject.startingEnergy += 25;
                        rememberedChoices[page] = selectedOption;
                    }
                    else if (selectedOption == 2)
                    {
                        StatManager.instance.playerStatsObject.startingLogisticsLevel += 1;
                        rememberedChoices[page] = selectedOption;
                    }
                    else
                    {
                        StatManager.instance.playerStatsObject.startingProductionLevel += 1;
                        rememberedChoices[page] = selectedOption;
                    }

                    previousSelectedOptions[page].text = pageInfo[page, selectedOption, 0];

                    break;

                case 3:

                    if (selectedOption == 0)
                    {
                        StatManager.instance.playerStatsObject.startingCommandLevel += 1;
                        rememberedChoices[page] = selectedOption;
                    }
                    else if (selectedOption == 1)
                    {
                        StatManager.instance.playerStatsObject.startingMissile += 25;
                        StatManager.instance.playerStatsObject.startingProjectile += 25;
                        StatManager.instance.playerStatsObject.startingEnergy += 25;
                        rememberedChoices[page] = selectedOption;
                    }
                    else if (selectedOption == 2)
                    {
                        StatManager.instance.playerStatsObject.startingLogisticsLevel += 1;
                        rememberedChoices[page] = selectedOption;
                    }
                    else
                    {
                        StatManager.instance.playerStatsObject.startingProductionLevel += 1;
                        rememberedChoices[page] = selectedOption;
                    }

                    previousSelectedOptions[page].text = pageInfo[page, selectedOption, 0];

                    break;
            }

            commandText.text = StatManager.instance.playerStatsObject.startingCommandLevel.ToString();
            logisticsText.text = StatManager.instance.playerStatsObject.startingLogisticsLevel.ToString();
            productionText.text = StatManager.instance.playerStatsObject.startingProductionLevel.ToString();
            missilesText.text = StatManager.instance.playerStatsObject.startingMissile.ToString();
            projectileText.text = StatManager.instance.playerStatsObject.startingProjectile.ToString();
            energyText.text = StatManager.instance.playerStatsObject.startingEnergy.ToString();
            tradeText.text = StatManager.instance.playerStatsObject.startingTrade.ToString();
        }

        if (page < 3)
        {
            page++;

            for (int i = 0; i < optionsRoot.childCount; i++)
            {
                Destroy(optionsRoot.GetChild(i).gameObject);
            }

            for (int i = 0; i < 4; i++)
            {
                GameObject go = Instantiate(optionsPrefab, optionsRoot);
                go.GetComponentInChildren<TMP_Text>().text = pageInfo[page, i, 0];
                int x = i;
                go.GetComponent<Button>().onClick.AddListener(() => ShowDescription(x));
            }
        }
        else
        {
            mainMenu.NewGame();   
        }

        if (page == 0)
        {
            //optionDescription.text = "Your parents...";
            if(typewriterEffect.IsRunning) { typewriterEffect.Stop(); }
            typewriterEffect.Run("Your parents...", optionDescription);
        }
        else if (page == 1)
        {
            //optionDescription.text = "In your early childhood...";
            if(typewriterEffect.IsRunning) { typewriterEffect.Stop(); }
            typewriterEffect.Run("In your early childhood...", optionDescription);
        }
        else if (page == 2)
        {
            //optionDescription.text = "In your adolescence...";
            if(typewriterEffect.IsRunning) { typewriterEffect.Stop(); }
            typewriterEffect.Run("In your adolescence...", optionDescription);
        }
        else if (page == 3)
        {
            //optionDescription.text = "When you became an adult...";
            if(typewriterEffect.IsRunning) { typewriterEffect.Stop(); }
            typewriterEffect.Run("When you became an adult...", optionDescription);
        }
    }
    
    public void Back()
    {
        if (page - 1 == 0)
        {
            if(typewriterEffect.IsRunning) { typewriterEffect.Stop(); }
            typewriterEffect.Run("Your parents...", optionDescription);
        }
        else if (page - 1 == 1)
        {
            if(typewriterEffect.IsRunning) { typewriterEffect.Stop(); }
            typewriterEffect.Run("In your early childhood...", optionDescription);
        }
        else if (page - 1 == 2)
        {
            if(typewriterEffect.IsRunning) { typewriterEffect.Stop(); }
            typewriterEffect.Run("In your adolescence...", optionDescription);
        }
        else if (page - 1 == 3)
        {
            if(typewriterEffect.IsRunning) { typewriterEffect.Stop(); }
            typewriterEffect.Run("When you became an adult...", optionDescription);
        }

        if (selectedOption != -1)
        {
            switch (page - 1)
            {
                case 0:

                    if (rememberedChoices[page - 1] == 0)
                    {
                        StatManager.instance.playerStatsObject.startingTrade -= 25;
                    }
                    else if (rememberedChoices[page - 1] == 1)
                    {
                        StatManager.instance.playerStatsObject.startingLogisticsLevel -= 1;
                    }
                    else if (rememberedChoices[page - 1] == 2)
                    {
                        StatManager.instance.playerStatsObject.startingProductionLevel -= 1;
                    }
                    else
                    {
                        StatManager.instance.playerStatsObject.startingMissile -= 25;
                        StatManager.instance.playerStatsObject.startingProjectile -= 25;
                        StatManager.instance.playerStatsObject.startingEnergy -= 25;
                    }

                    previousSelectedOptions[page - 1].text = "";

                    break;

                case 1:

                    if (rememberedChoices[page - 1] == 0)
                    {
                        StatManager.instance.playerStatsObject.startingCommandLevel -= 1;
                    }
                    else if (rememberedChoices[page - 1] == 1)
                    {
                        StatManager.instance.playerStatsObject.startingMissile -= 10;
                        StatManager.instance.playerStatsObject.startingProjectile -= 10;
                        StatManager.instance.playerStatsObject.startingEnergy -= 10;
                    }
                    else if (rememberedChoices[page - 1] == 2)
                    {
                        StatManager.instance.playerStatsObject.startingLogisticsLevel -= 1;
                    }
                    else
                    {
                        StatManager.instance.playerStatsObject.startingProductionLevel -= 1;
                    }

                    previousSelectedOptions[page - 1].text = "";

                    break;

                case 2:

                    if (rememberedChoices[page - 1] == 0)
                    {
                        StatManager.instance.playerStatsObject.startingCommandLevel -= 1;
                    }
                    else if (rememberedChoices[page - 1] == 1)
                    {
                        StatManager.instance.playerStatsObject.startingMissile -= 25;
                        StatManager.instance.playerStatsObject.startingProjectile -= 25;
                        StatManager.instance.playerStatsObject.startingEnergy -= 25;
                    }
                    else if (rememberedChoices[page - 1] == 2)
                    {
                        StatManager.instance.playerStatsObject.startingLogisticsLevel -= 1;
                    }
                    else
                    {
                        StatManager.instance.playerStatsObject.startingProductionLevel -= 1;
                    }

                    previousSelectedOptions[page - 1].text = "";

                    break;

                case 3:

                    if (rememberedChoices[page - 1] == 0)
                    {
                        StatManager.instance.playerStatsObject.startingCommandLevel -= 1;
                    }
                    else if (rememberedChoices[page - 1] == 1)
                    {
                        StatManager.instance.playerStatsObject.startingMissile -= 25;
                        StatManager.instance.playerStatsObject.startingProjectile -= 25;
                        StatManager.instance.playerStatsObject.startingEnergy -= 25;
                    }
                    else if (rememberedChoices[page - 1] == 2)
                    {
                        StatManager.instance.playerStatsObject.startingLogisticsLevel -= 1;
                    }
                    else
                    {
                        StatManager.instance.playerStatsObject.startingProductionLevel -= 1;
                    }

                    previousSelectedOptions[page - 1].text = "";

                    break;

            }

            commandText.text = StatManager.instance.playerStatsObject.startingCommandLevel.ToString();
            logisticsText.text = StatManager.instance.playerStatsObject.startingLogisticsLevel.ToString();
            productionText.text = StatManager.instance.playerStatsObject.startingProductionLevel.ToString();
            missilesText.text = StatManager.instance.playerStatsObject.startingMissile.ToString();
            projectileText.text = StatManager.instance.playerStatsObject.startingProjectile.ToString();
            energyText.text = StatManager.instance.playerStatsObject.startingEnergy.ToString();
            tradeText.text = StatManager.instance.playerStatsObject.startingTrade.ToString();
        }

        //selectedOption = -1;

        if (page > 0)
        {
            page--;

            for (int i = 0; i < optionsRoot.childCount; i++)
            {
                Destroy(optionsRoot.GetChild(i).gameObject);
            }

            for (int i = 0; i < 4; i++)
            {
                GameObject go = Instantiate(optionsPrefab, optionsRoot);
                go.GetComponentInChildren<TMP_Text>().text = pageInfo[page, i, 0];
                int x = i;
                go.GetComponent<Button>().onClick.AddListener(() => ShowDescription(x));
            }
        }
        else
        {
            mainMenu.ToggleBackgroundSelect();
        }
    }
}
