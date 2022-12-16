using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MechanicUI : MonoBehaviour
{
    public List<ShipInfoObject> ships = new();
    public static UnityAction onShipModuleChange;
    [SerializeField] NPCDatabaseObject database;
    public ShipInfoObject currentShip;

    [SerializeField] Transform weaponsRoot;
    [SerializeField] Transform defensesRoot;

    [SerializeField] GameObject modulePrefab;

    public GameObject currentDropdown;

    [SerializeField] Transform spawnedShipModel;
    [SerializeField] Transform shipModelRoot;
    [SerializeField] RawImage shipDisplay;
    [SerializeField] RenderTexture fighter, cruiser;
    [SerializeField] TMP_Text nameText;

    [SerializeField] Shipyard shipyard;

    [SerializeField] Button[] buttons;

    public List<ModuleSlot> modules = new();

    [SerializeField] Camera fighterCam, cruiserCam;

    [SerializeField] Inventory inventory;

    [SerializeField] GameObject moduleSelectPrefabW;
    [SerializeField] GameObject moduleSelectPrefabD;
    [SerializeField] GameObject moduleSelectPrefabU;

    [SerializeField] Transform inventoryModuleRoot;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform buttonsRoot;

    public void Start()
    {
        //Buttons
        for (int i = 0; i < buttonsRoot.childCount; i++)
        {
            Destroy(buttonsRoot.GetChild(i).gameObject);
        }

        for (int i = 0; i < database.shipinfos.Length; i++)
        {
            if (database.shipinfos[i].isOwned)
            {
                int j = i;
                Button button = Instantiate(buttonPrefab, buttonsRoot).GetComponent<Button>();
                button.onClick.AddListener(() => SetShip(j));
                button.GetComponentInChildren<TMP_Text>().text = database.shipinfos[i].Name;
            }
        }
    }

    private void OnEnable()
    {
        SetupShips();
        UpdateButtons();
    }

    void SetupShips()
    {
        ships = new();

        for (int i = 0; i < database.shipinfos.Length; i++)
        {
            if (database.shipinfos[i].isOwned)
                ships.Add(database.shipinfos[i]);
        }
    }

    void UpdateButtons()
    {
        for (int i = 0; i < buttonsRoot.childCount; i++)
        {
            Destroy(buttonsRoot.GetChild(i).gameObject);
        }

        for (int i = 0; i < database.shipinfos.Length; i++)
        {
            if (database.shipinfos[i].isOwned)
            {
                int j = i;
                Button button = Instantiate(buttonPrefab, buttonsRoot).GetComponent<Button>();
                button.onClick.AddListener(() => SetShip(j));
                button.GetComponentInChildren<TMP_Text>().text = database.shipinfos[i].Name;
            }
        }
    }

    public void SetShip(int index)
    {
        currentShip = ships[index];
        SetupModules();
        UpdateVisuals();

        nameText.text = ships[index].name;
    }

    void UpdateVisuals()
    {
        // Visuals
        if (spawnedShipModel)
            Destroy(spawnedShipModel.gameObject);

        spawnedShipModel = Instantiate(database.GetShip[currentShip.ID], shipModelRoot).transform;
        //
    }

    public void SetupModules()
    {
        ClearModules();
        UpdateVisuals();

        for (int i = 0; i < currentShip.defenses.Length; i++)
        {
            ModuleSlot go = Instantiate(modulePrefab, defensesRoot).GetComponent<ModuleSlot>();
            go.moduleType = ModuleSlot.ModuleType.defense;
            go.mechanicUI = this;
            go.item = currentShip.defenses[i];
            go.image.sprite = currentShip.defenses[i].sprite;
            go.itemNameText.text = currentShip.defenses[i].Name;
            go.moduleSize = currentShip.moduleSize;

            modules.Add(go.GetComponent<ModuleSlot>());
        }

        for (int i = 0; i < currentShip.weapons.Length; i++)
        {
            ModuleSlot go = Instantiate(modulePrefab, weaponsRoot).GetComponent<ModuleSlot>();
            go.moduleType = ModuleSlot.ModuleType.weapon;
            go.mechanicUI = this;
            go.item = currentShip.weapons[i];
            go.moduleSize = currentShip.moduleSize;
            go.image.sprite = currentShip.weapons[i].sprite;
            go.itemNameText.text = currentShip.weapons[i].Name;

            modules.Add(go.GetComponent<ModuleSlot>());
        }

        for (int i = 0; i < inventory.itemSlots.Count; i++)
        {
            if(inventory.itemSlots[i].item.type == ItemType.weapons)
            {
                ModuleSelectWeapon modulePrefab = Instantiate(moduleSelectPrefabW, inventoryModuleRoot).GetComponent<ModuleSelectWeapon>();
                modulePrefab.item = (Weapon)inventory.itemSlots[i].item;
                modulePrefab.canvas = canvas;
                modulePrefab.oldParent = inventoryModuleRoot;
                modulePrefab.SetupModule();
            }
            if (inventory.itemSlots[i].item.type == ItemType.defenses)
            {
                ModuleSelectDefense modulePrefab = Instantiate(moduleSelectPrefabD, inventoryModuleRoot).GetComponent<ModuleSelectDefense>();
                modulePrefab.item = (Defense)inventory.itemSlots[i].item;
                modulePrefab.canvas = canvas;
                modulePrefab.oldParent = inventoryModuleRoot;
                modulePrefab.SetupModule();
            }
            if (inventory.itemSlots[i].item.type == ItemType.utility)
            {
                ModuleSelectUtility modulePrefab = Instantiate(moduleSelectPrefabU, inventoryModuleRoot).GetComponent<ModuleSelectUtility>();
                modulePrefab.item = (Utility)inventory.itemSlots[i].item;
                modulePrefab.canvas = canvas;
                modulePrefab.oldParent = inventoryModuleRoot;
                modulePrefab.SetupModule();
            }
        }
    }

    public void UpdateInventoryModules()
    {
        for (int i = 0; i < inventoryModuleRoot.childCount; i++)
        {
            Destroy(inventoryModuleRoot.GetChild(i).gameObject);
        }

        for (int i = 0; i < inventory.itemSlots.Count; i++)
        {
            if (inventory.itemSlots[i].item.type == ItemType.weapons)
            {
                ModuleSelectWeapon modulePrefab = Instantiate(moduleSelectPrefabW, inventoryModuleRoot).GetComponent<ModuleSelectWeapon>();
                modulePrefab.item = (Weapon)inventory.itemSlots[i].item;
                modulePrefab.canvas = canvas;
                modulePrefab.oldParent = inventoryModuleRoot;
                modulePrefab.SetupModule();
            }
            if (inventory.itemSlots[i].item.type == ItemType.defenses)
            {
                ModuleSelectDefense modulePrefab = Instantiate(moduleSelectPrefabD, inventoryModuleRoot).GetComponent<ModuleSelectDefense>();
                modulePrefab.item = (Defense)inventory.itemSlots[i].item;
                modulePrefab.canvas = canvas;
                modulePrefab.oldParent = inventoryModuleRoot;
                modulePrefab.SetupModule();
            }
            if (inventory.itemSlots[i].item.type == ItemType.utility)
            {
                ModuleSelectUtility modulePrefab = Instantiate(moduleSelectPrefabU, inventoryModuleRoot).GetComponent<ModuleSelectUtility>();
                modulePrefab.item = (Utility)inventory.itemSlots[i].item;
                modulePrefab.canvas = canvas;
                modulePrefab.oldParent = inventoryModuleRoot;
                modulePrefab.SetupModule();
            }
        }
    }

    public void ClearModules()
    {
        for (int i = 0; i < inventoryModuleRoot.childCount; i++)
        {
            Destroy(inventoryModuleRoot.GetChild(i).gameObject);
        }

        for (int i = 0; i < weaponsRoot.childCount; i++)
        {
            Destroy(weaponsRoot.GetChild(i).gameObject);
        }

        for (int i = 0; i < defensesRoot.childCount; i++)
        {
            Destroy(defensesRoot.GetChild(i).gameObject);
        }

        modules.Clear();
    }

    public void UpdateModules(int index, ModuleSlot slot)
    {
        if(slot.moduleType == ModuleSlot.ModuleType.weapon)
        {
            for (int i = 0; i < currentShip.weapons.Length; i++)
            {
                if(i == index)
                {
                    currentShip.weapons[i] = (Weapon)slot.item;
                }
            }
        }
        else if(slot.moduleType == ModuleSlot.ModuleType.defense)
        {
            for (int i = 0; i < currentShip.defenses.Length; i++)
            {
                if (i == index)
                {
                    currentShip.defenses[i] = (Defense)slot.item;
                }
            }
        }

        onShipModuleChange.Invoke();
    }

    [System.Serializable]
    public struct MechanicSlot
    {
        public ShipInfoObject shipInfo;
        //public GameObject prefab;
    }
}
