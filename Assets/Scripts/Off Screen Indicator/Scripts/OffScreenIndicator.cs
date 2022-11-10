using PixelPlay.OffScreenIndicator;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Attach the script to the off screen indicator panel.
/// </summary>
[DefaultExecutionOrder(-1)]
public class OffScreenIndicator : MonoBehaviour
{
    [Range(0.5f, 0.9f)]
    [Tooltip("Distance offset of the indicators from the centre of the screen")]
    [SerializeField] private float screenBoundOffset = 0.9f;

    private Camera mainCamera;
    private Vector3 screenCentre;
    private Vector3 screenBounds;

    private List<Target> targets = new List<Target>();

    public static Action<Target, bool> TargetStateChanged;

    public Sprite[] indicatorSprites;

    void Awake()
    {
        mainCamera = Camera.main;
        screenCentre = new Vector3(Screen.width, Screen.height, 0) / 2;
        screenBounds = screenCentre * screenBoundOffset;
        TargetStateChanged += HandleTargetStateChanged;
    }

    public void Update()
    {
        if (mainCamera != Camera.main)
            mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        DrawIndicators();
    }

    /// <summary>
    /// Draw the indicators on the screen and set thier position and rotation and other properties.
    /// </summary>
    void DrawIndicators()
    {
        foreach(Target target in targets)
        {
            Vector3 screenPosition = OffScreenIndicatorCore.GetScreenPosition(mainCamera, target.transform.position);
            bool isTargetVisible = OffScreenIndicatorCore.IsTargetVisible(screenPosition);
            float distanceFromCamera = target.NeedDistanceText ? target.GetDistanceFromCamera(mainCamera.transform.position) : float.MinValue;// Gets the target distance from the camera.
            Indicator indicator = null;

            if (target.gameObject.GetComponent<HUDElements>())
            {
                target.nameText = target.NeedNameText ? target.gameObject.GetComponent<HUDElements>().name : ""; // Gets the target name from the target.
            }

            if (target.gameObject.GetComponent<SettlementInfo>())
            {
                target.nameText = target.NeedNameText ? target.gameObject.GetComponent<SettlementInfo>().Name : ""; // Gets the target name from the target.
                target.sprite = target.NeedFactionIcon ? target.gameObject.GetComponent<SettlementInfo>().faction.icon : null;
            }

            if (target.NeedBoxIndicator && isTargetVisible)
            {
                screenPosition.z = 0;
                indicator = GetIndicator(ref target.indicator, IndicatorType.BOX); // Gets the box indicator from the pool.
                indicator.SetNameText(target.nameText);

                if (target.NeedFactionIcon)
                    indicator.SetFactionIcon(target.sprite);
                else
                    indicator.DisableFactionIcon();

                switch (target.targetType)
                {
                    case Target.TargetType.ship:

                        indicator.SetSprite(indicatorSprites[0]);

                        break;

                    case Target.TargetType.station:

                        indicator.SetSprite(indicatorSprites[1]);

                        break;

                    case Target.TargetType.asteroid:

                        indicator.SetSprite(indicatorSprites[2]);

                        break;

                    case Target.TargetType.planet:

                        indicator.SetSprite(indicatorSprites[3]);

                        break;

                    case Target.TargetType.stargate:

                        indicator.SetSprite(indicatorSprites[4]);

                        break;

                    case Target.TargetType.star:

                        indicator.SetSprite(indicatorSprites[5]);

                        break;
                }
            }
            else if(target.NeedArrowIndicator && !isTargetVisible)
            {
                float angle = float.MinValue;
                OffScreenIndicatorCore.GetArrowIndicatorPositionAndAngle(ref screenPosition, ref angle, screenCentre, screenBounds);
                indicator = GetIndicator(ref target.indicator, IndicatorType.ARROW); // Gets the arrow indicator from the pool.
                indicator.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg); // Sets the rotation for the arrow indicator.
            }
            else
            {
                target.indicator?.Activate(false);
                target.indicator = null;
            }
            if(indicator)
            {
                var distance = Vector3.Distance(mainCamera.transform.position, target.transform.position);
                if (distance >= 500)
                    distance = 500;

                Color color = new Color(target.TargetColor.r, target.TargetColor.g, target.TargetColor.b, (Mathf.Abs(500 - distance) / 500) + .25f);
                //Debug.Log(Vector3.Distance(mainCamera.transform.position, target.transform.position));
                indicator.SetImageColor(color);// Sets the image color of the indicator.
                indicator.SetDistanceText(distanceFromCamera); //Set the distance text for the indicator.
                indicator.transform.position = screenPosition; //Sets the position of the indicator on the screen.
                indicator.SetTextRotation(Quaternion.identity); // Sets the rotation of the distance text of the indicator.
            }

            //float distanceForLimit = target.GetDistanceFromCamera(mainCamera.transform.position);

            //if (target.distanceLimit)
            //{
            //    if (distanceForLimit >= target.distanceToDisable)
            //    {
            //        target.enabled = false;
            //    }
            //}
        }
    }

    /// <summary>
    /// 1. Add the target to targets list if <paramref name="active"/> is true.
    /// 2. If <paramref name="active"/> is false deactivate the targets indicator, 
    ///     set its reference null and remove it from the targets list.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="active"></param>
    private void HandleTargetStateChanged(Target target, bool active)
    {
        if(active)
        {
            targets.Add(target);
        }
        else
        {
            target.indicator?.Activate(false);
            target.indicator = null;
            targets.Remove(target);
        }
    }

    /// <summary>
    /// Get the indicator for the target.
    /// 1. If its not null and of the same required <paramref name="type"/> 
    ///     then return the same indicator;
    /// 2. If its not null but is of different type from <paramref name="type"/> 
    ///     then deactivate the old reference so that it returns to the pool 
    ///     and request one of another type from pool.
    /// 3. If its null then request one from the pool of <paramref name="type"/>.
    /// </summary>
    /// <param name="indicator"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private Indicator GetIndicator(ref Indicator indicator, IndicatorType type)
    {
        if(indicator != null)
        {
            if(indicator.Type != type)
            {
                indicator.Activate(false);
                indicator = type == IndicatorType.BOX ? BoxObjectPool.current.GetPooledObject() : ArrowObjectPool.current.GetPooledObject();
                indicator.Activate(true); // Sets the indicator as active.
            }
        }
        else
        {
            indicator = type == IndicatorType.BOX ? BoxObjectPool.current.GetPooledObject() : ArrowObjectPool.current.GetPooledObject();
            indicator.Activate(true); // Sets the indicator as active.
        }
        return indicator;
    }

    private void OnDestroy()
    {
        TargetStateChanged -= HandleTargetStateChanged;
    }
}
