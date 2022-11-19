using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MechanicUI : MonoBehaviour
{
    public MechanicSlot[] ships;
    public static UnityAction onShipModuleChange;

    public ShipInfoObject currentShip;

    public Transform weaponsRoot;
    public Transform defensesRoot;

    public GameObject modulePrefab;

    public GameObject currentDropdown;

    public GameObject shipModel;
    public RawImage shipDisplay;
    public RenderTexture fighter, cruiser;
    public TMP_Text nameText;

    public Shipyard shipyard;

    public Button[] buttons;

    public List<ModuleSlot> modules = new();

    public Camera fighterCam, cruiserCam;

    public Inventory inventory;
    public GameObject moduleSelectPrefab;
    public Transform inventoryModuleRoot;
    public Canvas canvas;

    public void Start()
    {
        SetShip(0);
    }

    public void Update()
    {
        for (int i = 0; i < shipyard.shipyardSlots.Length; i++)
        {
            if(shipyard.shipyardSlots[i].isOwned == true)
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }
    }

    public void SetShip(int index)
    {
        currentShip = ships[index].shipInfo;
        SetupModules();

        cruiserCam.enabled = true;
        fighterCam.enabled = true;

        // Visuals
        shipModel.GetComponent<MeshFilter>().sharedMesh = ships[index].prefab.GetComponent<MeshFilter>().sharedMesh;

        nameText.text = ships[index].shipInfo.name;

        if (index == 3)
        {
            shipDisplay.texture = cruiser;
        }
        else
        {
            shipDisplay.texture = fighter;
        }

        //List<float> size = new List<float>();

        //size.Add((shipModel.GetComponent<MeshFilter>().transform.localScale.z * shipModel.GetComponent<MeshFilter>().mesh.bounds.size.z));
        //size.Add((shipModel.GetComponent<MeshFilter>().transform.localScale.x * shipModel.GetComponent<MeshFilter>().mesh.bounds.size.x));
        //size.Add((shipModel.GetComponent<MeshFilter>().transform.localScale.y * shipModel.GetComponent<MeshFilter>().mesh.bounds.size.y));

        //size = size.OrderBy(i => i).ToList();

        //float objectSize = size[0] / 3f;

        //shipModel.GetComponent<MeshFilter>().transform.localScale /= objectSize;
    }

    public void SetupModules()
    {
        ClearModules();

        for (int i = 0; i < currentShip.defenses.Length; i++)
        {
            GameObject go = Instantiate(modulePrefab, defensesRoot);
            go.GetComponent<ModuleSlot>().moduleType = ModuleSlot.ModuleType.defense;
            //go.GetComponent<ModuleSlot>().GetComponent<Button>().onClick.AddListener(() => go.GetComponent<ModuleSlot>().SpawnModules());
            go.GetComponent<ModuleSlot>().mechanicUI = this;
            go.GetComponent<ModuleSlot>().item = currentShip.defenses[i];
            //go.GetComponent<ModuleSlot>().item.equipped = true;
            go.GetComponent<ModuleSlot>().image.sprite = currentShip.defenses[i].sprite;
            go.GetComponent<ModuleSlot>().itemNameText.text = currentShip.defenses[i].Name;

            modules.Add(go.GetComponent<ModuleSlot>());
        }

        for (int i = 0; i < currentShip.weapons.Length; i++)
        {
            GameObject go = Instantiate(modulePrefab, weaponsRoot);
            go.GetComponent<ModuleSlot>().moduleType = ModuleSlot.ModuleType.weapon;
            //go.GetComponent<ModuleSlot>().GetComponent<Button>().onClick.AddListener(() => go.GetComponent<ModuleSlot>().SpawnModules());
            go.GetComponent<ModuleSlot>().mechanicUI = this;
            go.GetComponent<ModuleSlot>().item = currentShip.weapons[i];
            //go.GetComponent<ModuleSlot>().item.equipped = true;
            go.GetComponent<ModuleSlot>().image.sprite = currentShip.weapons[i].sprite;
            go.GetComponent<ModuleSlot>().itemNameText.text = currentShip.weapons[i].Name;

            modules.Add(go.GetComponent<ModuleSlot>());
        }

        for (int i = 0; i < inventory.itemSlots.Count; i++)
        {
            if(inventory.itemSlots[i].item.type == ItemType.weapons || inventory.itemSlots[i].item.type == ItemType.defenses)
            {
                GameObject modulePrefab = Instantiate(moduleSelectPrefab, inventoryModuleRoot);
                modulePrefab.GetComponent<ModuleSelect>().item = inventory.itemSlots[i].item;
                modulePrefab.GetComponent<ModuleSelect>().canvas = canvas;
                modulePrefab.GetComponent<ModuleSelect>().oldParent = inventoryModuleRoot;
                modulePrefab.GetComponent<ModuleSelect>().SetupModule();
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
            if (inventory.itemSlots[i].item.type == ItemType.weapons || inventory.itemSlots[i].item.type == ItemType.defenses)
            {
                GameObject modulePrefab = Instantiate(moduleSelectPrefab, inventoryModuleRoot);
                modulePrefab.GetComponent<ModuleSelect>().item = inventory.itemSlots[i].item;
                modulePrefab.GetComponent<ModuleSelect>().canvas = canvas;
                modulePrefab.GetComponent<ModuleSelect>().oldParent = inventoryModuleRoot;
                modulePrefab.GetComponent<ModuleSelect>().SetupModule();
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
        public GameObject prefab;
    }
}
