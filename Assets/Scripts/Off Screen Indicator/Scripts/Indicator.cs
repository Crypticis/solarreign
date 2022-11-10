using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Assign this script to the indicator prefabs.
/// </summary>
public class Indicator : MonoBehaviour
{
    [SerializeField] private IndicatorType indicatorType;
    [SerializeField] Image indicatorImage;
    [SerializeField] TMP_Text distanceText;
    public TMP_Text nameText;
    public Image factionIcon;

    /// <summary>
    /// Gets if the game object is active in hierarchy.
    /// </summary>
    public bool Active
    {
        get
        {
            return transform.gameObject.activeInHierarchy;
        }
    }

    /// <summary>
    /// Gets the indicator type
    /// </summary>
    public IndicatorType Type
    {
        get
        {
            return indicatorType;
        }
    }

    /// <summary>
    /// Sets the image color for the indicator.
    /// </summary>
    /// <param name="color"></param>
    public void SetImageColor(Color color)
    {
        indicatorImage.color = color;
    }

    /// <summary>
    /// Sets the image color for the indicator.
    /// </summary>
    /// <param name="color"></param>
    public void SetSprite(Sprite sprite)
    {
        indicatorImage.sprite = sprite;
    }

    /// <summary>
    /// Sets the distance text for the indicator.
    /// </summary>
    /// <param name="value"></param>
    public void SetDistanceText(float value)
    {
        distanceText.text = value >= 0 ? (value / 10).ToString("n0") + " AU" : "";
                            //value >= 0 ? Mathf.Floor(value) * 10 + " m" : "";
    }

    public void SetNameText(string value)
    {
        nameText.text = value.ToString();
    }

    /// <summary>
    /// Sets the distance text rotation of the indicator.
    /// </summary>
    /// <param name="rotation"></param>
    public void SetTextRotation(Quaternion rotation)
    {
        distanceText.rectTransform.rotation = rotation;
    }

    /// <summary>
    /// Sets the indicator as active or inactive.
    /// </summary>
    /// <param name="value"></param>
    public void Activate(bool value)
    {
        transform.gameObject.SetActive(value);
    }

    public void SetFactionIcon(Sprite sprite)
    {
        factionIcon.sprite = sprite;
    }

    public void DisableFactionIcon()
    {
        factionIcon.enabled = false;
    }
}

public enum IndicatorType
{
    BOX,
    ARROW
}
