using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BountySlot : MonoBehaviour
{
    public BountyUI.Bounty bounty;
    public BountyUI bountyUI;

    public TMP_Text nameText;
    public TMP_Text descText;
    public TMP_Text paymentText;

    public void Start()
    {
        nameText.text = bounty.mission.name;
        descText.text = bounty.mission.description;
        paymentText.text = "$" + bounty.mission.moneyReward.ToString();
    }

    public void Accept()
    {
        bountyUI.AcceptBounty(bounty, this.gameObject);
    }
}
