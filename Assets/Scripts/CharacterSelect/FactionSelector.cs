using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionSelector : MonoBehaviour
{
    public static FactionSelector instance;

    public Faction zeroG;
    public Faction gsf;
    public Faction[] factions;
    private void Awake()
    {
        instance = this;
    }

    public void SelectZeroGCorp()
    {
        SetPlayerFaction(zeroG);
    }

    public void SelectGSF()
    {
        SetPlayerFaction(gsf);
    }

    public void SetPlayerFaction(Faction faction)
    {
        for (int i = 0; i < factions.Length; i++)
        {
            if (factions[i] == faction)
            {
                factions[i].playerInFaction = true;
            }
            else
            {
                factions[i].playerInFaction = false;
            }
        }
    }
}
