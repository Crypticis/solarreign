using HighlightPlus;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnitySteer.Behaviors;

public class UnitController : MonoBehaviour
{
    public bool inStrategyMode = true;
    public GameObject unit = null;
    public LayerMask unitLayer;

    public Transform pointDisplay;
    public Transform positionPlane;
    public float verticalOffset;

    public GameObject strategyCamera;
    public Transform cameraFocalPoint; 
    public Camera flightCamera;

    public TMP_Text modeText;
    public GameObject commandHUD;
    public GameObject flightHud;
    public GameObject indicatorManager;

    public StartBattle startBattle;

    //public Color enemyColor;
    //public Color allyColor;
    //public Color selectedColor;

    void Update()
    {
        //if(startBattle.hasStarted == false)
        //{
        //    return;
        //}

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inStrategyMode = !inStrategyMode;
            if(inStrategyMode == true)
            {
                cameraFocalPoint.position = new Vector3(Player.playerInstance.transform.position.x, Player.playerInstance.transform.position.y + 20, Player.playerInstance.transform.position.z - 20);
                Player.playerInstance.controlsEnabled = false;
                //modeText.text = "Command";
                flightHud.SetActive(false);
                commandHUD.SetActive(true);
                indicatorManager.SetActive(false);
                Cursor.visible = true;

                //strategyCamera.GetComponent<StrategyCameraController>().rotateOrigin.position = Player.playerInstance.transform.position;

                for (int i = 0; i < BattleManager.instance.enemyFleet.Count; i++)
                {
                    BattleManager.instance.enemyFleet[i].GetComponent<HighlightEffect>().overlayColor = Color.red;
                    BattleManager.instance.enemyFleet[i].GetComponent<HighlightEffect>().highlighted = true;
                }
                for (int i = 0; i < BattleManager.instance.fleet.Count; i++)
                {
                    BattleManager.instance.fleet[i].GetComponent<HighlightEffect>().overlayColor = Color.blue;
                    BattleManager.instance.fleet[i].GetComponent<HighlightEffect>().highlighted = true;
                }
            }
            else
            {
                Time.timeScale = 1f;
                Player.playerInstance.controlsEnabled = true;
                //modeText.text = "Flight";
                flightHud.SetActive(true);
                commandHUD.SetActive(false);
                indicatorManager.SetActive(true);
                Cursor.visible = false;

                for (int i = 0; i < BattleManager.instance.enemyFleet.Count; i++)
                {
                    BattleManager.instance.enemyFleet[i].GetComponent<HighlightEffect>().highlighted = false;
                }
                for (int i = 0; i < BattleManager.instance.fleet.Count; i++)
                {
                    BattleManager.instance.fleet[i].GetComponent<HighlightEffect>().highlighted = false;
                }
            }

            //if (unit)
            //{
            //    unit.GetComponent<HighlightEffect>().highlighted = false;
            //}
        }

        if (Input.GetKey(KeyCode.F1))
        {
            for (int i = 0; i < BattleManager.instance.fleet.Count; i++)
            {
                BattleManager.instance.fleet[i].GetComponent<Targeting>().commandMode = CommandMode.automatic;
                Ticker.Ticker.AddItem("Units switch to automatic targeting.");
            }
        } 
        else if (Input.GetKey(KeyCode.F2))
        {
            for (int i = 0; i < BattleManager.instance.fleet.Count; i++)
            {
                BattleManager.instance.fleet[i].GetComponent<Targeting>().commandMode = CommandMode.manual;
                Ticker.Ticker.AddItem("Units switch to manual targeting.");
            }
        }

        if (inStrategyMode)
        {
            Time.timeScale = 0f;

            strategyCamera.SetActive(true);
            flightCamera.enabled = false;

            verticalOffset += Input.mouseScrollDelta.y * 5f;

            pointDisplay.gameObject.SetActive(true);

            Ray myRay = strategyCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            Physics.Raycast(myRay, out hitInfo, 10000);

            pointDisplay.position = hitInfo.point;

            if (Input.GetMouseButton(0))
            {
                if (unit)
                    //unit.GetComponent<HighlightEffect>().highlighted = false;
                    unit.GetComponent<HighlightEffect>().overlayColor = Color.blue;

                unit = null;

                //Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                //RaycastHit hitInfo;

                if (Physics.Raycast(myRay, out hitInfo, 10000, unitLayer))
                {
                    if (hitInfo.collider.gameObject)
                    {
                        if(hitInfo.collider.gameObject.tag == "Ally")
                        {
                            unit = hitInfo.collider.gameObject;
                            //unit.GetComponent<HighlightEffect>().highlighted = true;
                            unit.GetComponent<HighlightEffect>().overlayColor = Color.yellow;
                            //unit.GetComponent<HighlightPlus.HighlightEffect>().HitFX();
                        }
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (AudioManager.instance)
                    {
                        AudioManager.instance.Play("Click2");
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (AudioManager.instance)
                    {
                        AudioManager.instance.Play("Click3");
                    }
                }
            }

            if (unit != null)
            {
                positionPlane.gameObject.SetActive(true);
                positionPlane.transform.position = new Vector3(unit.transform.position.x, unit.transform.position.y + verticalOffset, unit.transform.position.z);

                if (Input.GetMouseButtonDown(1))
                {
                    //Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    //RaycastHit hitInfo;

                    if (Physics.Raycast(myRay, out hitInfo, 10000))
                    {

                        if (hitInfo.collider.gameObject)
                        {
                            if (hitInfo.collider.gameObject.tag == "Enemy")
                            {
                                unit.GetComponent<Targeting>().target = hitInfo.collider.gameObject;
                                //unit.GetComponent<HighlightPlus.HighlightEffect>().HitFX();
                                unit.GetComponent<SteerForPoint>().enabled = false;
                                unit.GetComponent<SteerToFollow>().enabled = false;

                                Debug.Log("Set " + unit + " to attack " + hitInfo.collider.gameObject);
                            }
                            else if(hitInfo.collider.gameObject.tag == "Ally" || hitInfo.collider.gameObject.tag == "Player")
                            {
                                unit.GetComponent<SteerForPoint>().enabled = false;
                                unit.GetComponent<Targeting>().target = null;
                                unit.GetComponent<SteerToFollow>().enabled = true;
                                unit.GetComponent<SteerToFollow>().Target = (hitInfo.collider.gameObject.transform);

                                Debug.Log("Set " + unit + " to follow " + hitInfo.collider.gameObject);
                            }
                            else
                            {
                                unit.GetComponent<SteerForPoint>().enabled = true;
                                unit.GetComponent<Targeting>().target = null;
                                unit.GetComponent<SteerToFollow>().enabled = false;
                                unit.GetComponent<SteerForPoint>().TargetPoint = (hitInfo.point);

                                Debug.Log("Set " + unit + " to go to " + hitInfo.point);
                            }
                        }
                        else
                        {
                            unit.GetComponent<SteerForPoint>().enabled = true;
                            unit.GetComponent<Targeting>().target = null;
                            unit.GetComponent<SteerToFollow>().enabled = false;
                            unit.GetComponent<SteerForPoint>().TargetPoint = (hitInfo.point);

                            Debug.Log("Set " + unit + " to go to " + hitInfo.point);
                        }
                    }
                }
            }
            else
            {
                positionPlane.gameObject.SetActive(false);
            }
        } 
        else
        {
            //Time.timeScale = 1f;
            positionPlane.gameObject.SetActive(false);
            pointDisplay.gameObject.SetActive(false);
            verticalOffset = 0;
            strategyCamera.SetActive(false);
            flightCamera.enabled = true;
            if (unit)
                //unit.GetComponent<HighlightEffect>().highlighted = false;
                unit.GetComponent<HighlightEffect>().overlayColor = Color.blue;
            unit = null;
        }
    }
}
