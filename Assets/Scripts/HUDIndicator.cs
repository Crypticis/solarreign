using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDIndicator : MonoBehaviour
{
    private Image iconImg;
    public TMP_Text distanceText;
    public TMP_Text nameText;
    public string name;

    public Transform player;
    public Transform target;
    public Camera cam;
    public float targetingRange;

    public float closeEnoughDist;

    void Start()
    {
        iconImg = GetComponent<Image>();
        if(name != null)
            nameText.text = name;
    }

    void Update()
    {

        if(target != null)
        {
            GetDistance();
            CheckOnScreen();
        }

        if (target == null)
        {
            Destroy(gameObject);
        }
    }

    void GetDistance()
    {
        float distance = Vector3.Distance(player.position, target.position);
        float viewedDistance = distance * 10;
        distanceText.text = viewedDistance.ToString("f1") + "m";

        if(distance <= closeEnoughDist)
        {
            ToggleUI(false);
        }
        if(distance >= targetingRange)
        {
            ToggleUI(false);
        }
    }

    void CheckOnScreen()
    {
        float isOnScreen = Vector3.Dot((target.position - cam.transform.position).normalized, cam.transform.forward);
        if(isOnScreen <= 0)
        {
            ToggleUI(false);
        } else
        {
            ToggleUI(true);
            transform.position = cam.WorldToScreenPoint(target.position);
        }
    }

    private void ToggleUI(bool _value)
    {
        iconImg.enabled = _value;
        distanceText.enabled = _value;
        nameText.enabled = _value;
    }
}
