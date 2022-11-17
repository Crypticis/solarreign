using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankUI : MonoBehaviour
{
    public Sprite[] rankSprites;
    Image rankIcon;
    TMP_Text rankText;

    // Start is called before the first frame update
    void Start()
    {
        rankIcon = GetComponentInChildren<Image>();
        rankText = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StatManager.instance.level.currentLevel - 1 <= rankSprites.Length)
        {
            rankIcon.sprite = rankSprites[StatManager.instance.level.currentLevel - 1];
            rankText.text = StatManager.instance.level.currentLevel.ToString();
        }
        else
        {
            rankIcon.sprite = rankSprites[rankSprites.Length - 1];
            rankText.text = StatManager.instance.level.currentLevel.ToString();
        }
    }
}
