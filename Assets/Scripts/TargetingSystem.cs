/*******************************************************************/
/* \project: Spicy Dice
 * \author: Amir Azmi
 * \date: 11/13/2019
 * \brief: Locks onto the closest object when a key F is pressed and
 *         unlocks when F is pressed again, if the ai gets out of the 
 *         range of the camera collider, then targeting is broken free,
 *         if the camera gets out of range, then it also breaks free
 *         You can also cycle targets with the left and right arrow
 *         key within the m_Angle range
 *         
*/
/*******************************************************************/

using System.Linq; //Sorting for Orderby
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GNB;

public class TargetingSystem : MonoBehaviour
{
    public float m_Angle = 35.0f; //default for angle variable

    [HideInInspector] public GameObject m_ObjectClosestToCamera; //object your targeting
    [HideInInspector] public bool m_IsTargeting = false; //if targeting is true
    private readonly List<GameObject> m_CandidateTargets = new List<GameObject>(); //list of candidate game objects
    private bool m_TargetButton = false; //target button
    private bool m_AxisRight = false; //moving axis to the right
    private bool m_AxisLeft = false; //moving the axis to the left
    private int m_NumberOfTargetsWithinRange = 0; //number of targets within range
    private Animator m_Animator; //animator for palyer
    public GameObject targetIndicator;
    public GameObject leadIndicator;
    public Transform targetedEnemy = null;

    // My stuff

    int oldLength;
    int currentLength;
    public GameObject targetSlot;
    public Transform targetUI;

    private void Start()
    {
        oldLength = m_CandidateTargets.Count;
        //m_Animator = Player.Instance.GetComponent<Animator>();
    }

    private void Update()
    {

        if(targetedEnemy != null && Vector3.Dot(transform.forward, (targetedEnemy.position - transform.position).normalized) > 0.7f)
        {
            targetIndicator.transform.position = Camera.main.WorldToScreenPoint(targetedEnemy.position);

            if(leadIndicator.activeSelf)
                leadIndicator.transform.position = Camera.main.WorldToScreenPoint(FirstOrderIntercept(Player.playerInstance.transform.position, Player.playerInstance.GetComponent<Rigidbody>().velocity, Player.playerInstance.GetComponentInChildren<Gun>().MuzzleVelocity + Player.playerInstance.GetComponent<Rigidbody>().velocity.magnitude, targetedEnemy.position, targetedEnemy.GetComponent<Rigidbody>().velocity));

            targetIndicator.SetActive(true);
            leadIndicator.SetActive(true);
        }
        else
        {
            targetIndicator.SetActive(false);
            leadIndicator.SetActive(false);
        }

        //check if the targeting button has been pressed
        bool isDown = Input.GetButtonDown("Target");

        //if candidate object happens to be null reset targeting
        if (m_ObjectClosestToCamera == null)
        {
            m_TargetButton = false;
        }

        //remove null objects in the list and decrement the counter
        // Could optimize through some onDelete event system, probably really not worth
        for (int i = m_CandidateTargets.Count - 1; i >= 0; --i)
        {
            if (m_CandidateTargets[i] == null || !m_CandidateTargets[i].activeInHierarchy)
            {
                m_CandidateTargets.RemoveAt(i);
                m_NumberOfTargetsWithinRange--;
            }
        }

        //id target button has been pressed and there are targets within the targeting radius
        if (isDown && m_NumberOfTargetsWithinRange > 0)
        {
            //if you want to sort by distance uncomment line below
            //List<GameObject> Sorted_List = m_CandidateTargets.OrderBy(go => (transform.position - go.transform.position).sqrMagnitude).ToList(); // sort objects in order as they enter

            //sorts objects by angle and stores it into the Sorted_List
            List<GameObject> Sorted_List = m_CandidateTargets.OrderBy(go =>
            {
                Vector3 target_direction = go.transform.position - Camera.main.transform.position; //get vector from camera to target
                var camera_forward = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z); //convert camera forward direction into 2D vector
                var target_dir = new Vector2(target_direction.x, target_direction.z); //do the same with target direction
                float angle = Vector2.Angle(camera_forward, target_dir); //get the angle between the two vectors
                return angle;
            }).ToList(); //store the objects based off of the angle into the Sorted_List


            //copy objects into the main game_object list
            //remove objects that happen to die before selecting next target
            for (var i = 0; i < m_CandidateTargets.Count(); ++i)
            {
                m_CandidateTargets[i] = Sorted_List[i];

                if (!m_CandidateTargets[i].activeInHierarchy)
                {
                    m_CandidateTargets.RemoveAt(i);
                    m_NumberOfTargetsWithinRange--;
                }
            }

            GameObject old_object = m_ObjectClosestToCamera;  //current object to untarget

            UnTarget(old_object); //untarget old object shows to not show the indicator

            //Super cool thing to note,  "float angle = Vector2.Angle(camera_forward, target_dir);" sorts by abs(angle) aka unsigned so the first object you target is always the object you are most looking at
            m_ObjectClosestToCamera = m_CandidateTargets.First(); //set target as the target you are most looking at

            //target the new object
            if (old_object != m_ObjectClosestToCamera)
            {
                Target(m_ObjectClosestToCamera);  //show the indicator on the new object
            }

            m_TargetButton = !m_TargetButton; //this handles the unlocking / locking
        }

        //if I am targeting, there are candidate objects within my radius, and current target is not null and the object is alive aka in the scene
        if (m_TargetButton && m_NumberOfTargetsWithinRange > 0 && m_ObjectClosestToCamera != null && m_ObjectClosestToCamera.activeInHierarchy)
        {
            //gets the angle of the between the current game object and camera
            Vector3 target_direction = m_ObjectClosestToCamera.transform.position - Camera.main.transform.position;
            var camera_forward = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
            var target_dir = new Vector2(target_direction.x, target_direction.z);
            float angle = Vector2.Angle(camera_forward, target_dir);

            //check if the object's angle is within the zone of targeting
            if (angle < Mathf.Abs(m_Angle))
            {
                m_IsTargeting = true; //set targeting to true

                if (/*Input.GetAxisRaw("Controller Right Stick X") > 0.0f || */Input.GetButtonDown("RightArrow")) //if the right stick was moved to the right
                {
                    //List<GameObject> Sorted_List = m_CandidateTargets.OrderBy(go => (transform.position - go.transform.position).sqrMagnitude).ToList();

                    if (m_AxisRight == false) //if axis initally was false
                    {
                        //sort objects in the list while targeting, yes you want to do this so if enemies move around it still keeps the list correct
                        List<GameObject> Sorted_List = m_CandidateTargets.OrderBy(go =>
                        {
                            Vector3 target_dir_vec3 = go.transform.position - Camera.main.transform.position;
                            var camera_forward_dir = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
                            var new_target_dir = new Vector2(target_dir_vec3.x, target_dir_vec3.z);
                            float angle_from_sorted_list = Vector2.SignedAngle(camera_forward_dir, new_target_dir);
                            return angle_from_sorted_list;
                        }).ToList();

                        //copy the objects from the sorted list into the main list
                        for (var i = 0; i < m_CandidateTargets.Count(); ++i)
                        {
                            m_CandidateTargets[i] = Sorted_List[i];
                        }

                        //check if there is an object to the right
                        if (m_CandidateTargets.IndexOf(m_ObjectClosestToCamera) - 1 >= 0)
                        {
                            //turn off current idicator
                            UnTarget(m_ObjectClosestToCamera);

                            //check its angle
                            GameObject next_targeted_object = m_CandidateTargets[m_CandidateTargets.IndexOf(m_ObjectClosestToCamera) - 1];
                            Vector3 target_direction_vec3 = next_targeted_object.transform.position - Camera.main.transform.position;
                            var camera_forward_vec2 = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
                            var new_target_dir = new Vector2(target_direction_vec3.x, target_direction_vec3.z);
                            float angle_between_vectors = Vector2.Angle(camera_forward_vec2, new_target_dir);

                            //if the angle is within range of targeting angle
                            if (angle_between_vectors < Mathf.Abs(m_Angle))
                            {
                                //set the new object as the target
                                m_ObjectClosestToCamera = m_CandidateTargets[m_CandidateTargets.IndexOf(m_ObjectClosestToCamera) - 1];
                            }

                            //show the indicator of the target
                            Target(m_ObjectClosestToCamera);

                            //treat angle as button type
                            m_AxisRight = !m_AxisRight;
                        }
                    }
                }
                else if (/*Input.GetAxisRaw("Controller Right Stick X") < 0.0f || */Input.GetButtonDown("LeftArrow"))
                {
                    //initially the leftAxis bool should be false for togglable logic       
                    if (m_AxisLeft == false)
                    {
                        //sort objects by the SignedAngle  for the same reasons statef for right cycling
                        List<GameObject> Sorted_List = m_CandidateTargets.OrderBy(go =>
                        {
                            //direction from  Camera to the target object indicated by go
                            Vector3 targetDir = go.transform.position - Camera.main.transform.position;

                            //convert Camera direction into 2D vector
                            var cameraForward = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
                            //convert target direction into the 2D vector
                            var new_target_dir = new Vector2(targetDir.x, targetDir.z);

                            // calculate angle between the two vectors
                            float angle_between_vectors = Vector2.SignedAngle(cameraForward, new_target_dir);
                            return angle_between_vectors;
                        }).ToList();

                        //put the sorted list into the candidate list
                        for (var i = 0; i < m_CandidateTargets.Count(); ++i)
                        {
                            m_CandidateTargets[i] = Sorted_List[i];
                        }

                        //notice here how I check if there is a next valid object in the list
                        if (m_CandidateTargets.IndexOf(m_ObjectClosestToCamera) + 1 < m_CandidateTargets.Count)
                        {
                            //turn off indicator
                            UnTarget(m_ObjectClosestToCamera);

                            //store this next object into a gameObject 
                            GameObject nextTargetedObject = m_CandidateTargets[m_CandidateTargets.IndexOf(m_ObjectClosestToCamera) + 1];

                            //calcuate the angle to see if the next object is within defined targeting range
                            Vector3 targeDir = nextTargetedObject.transform.position - Camera.main.transform.position;

                            var cameraFor = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);

                            var newtarget_dir = new Vector2(targeDir.x, targeDir.z);
                            float anglex = Vector2.Angle(cameraFor, newtarget_dir);

                            //if the next targeted object is a valid target
                            if (anglex < Mathf.Abs(m_Angle))
                            {
                                // set the m_ObjectClosestToCamera as the next object in the list
                                m_ObjectClosestToCamera = m_CandidateTargets[m_CandidateTargets.IndexOf(m_ObjectClosestToCamera) + 1];
                            }

                            //tell indicator what to do
                            Target(m_ObjectClosestToCamera);

                            //for togglable logic
                            m_AxisLeft = !m_AxisLeft;
                        }
                    }
                }
                else
                {
                    //once the axis is 0 it means the locking has been reset
                    m_AxisRight = false;
                    m_AxisLeft = false;
                }
            }
            else
            {
                //reset the target button
                m_TargetButton = false;
            }
        }
        else
        {
            //set targeting to false here
            m_IsTargeting = false;

            //if th object is not null here make it null and untarget
            if (m_ObjectClosestToCamera != null)
            {
                UnTarget(m_ObjectClosestToCamera);

                m_ObjectClosestToCamera = null;
            }
        }

        //Tell the ANIMATOR
        //m_Animator.SetBool("TargetLocked", m_IsTargeting);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            ++m_NumberOfTargetsWithinRange;
            m_CandidateTargets.Add(other.gameObject);
        }

        //for (int i = 0; i < FactionManager.instance.factions.Length; i++)
        //{
        //    if (FactionManager.instance.factions[i].playerInFaction)
        //    {
        //        for (int j = 0; j < FactionManager.instance.factions[i].enemies.Count; j++)
        //        {
        //            if (other.tag == FactionManager.instance.factions[i].enemies[j].tag)
        //            {
        //                ++m_NumberOfTargetsWithinRange;
        //                m_CandidateTargets.Add(other.gameObject);
        //            }
        //        }
        //    }
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            m_CandidateTargets.Remove(other.gameObject);
            --m_NumberOfTargetsWithinRange;
        }

        //for (int i = 0; i < FactionManager.instance.factions.Length; i++)
        //{
        //    if (FactionManager.instance.factions[i].playerInFaction)
        //    {
        //        for (int j = 0; j < FactionManager.instance.factions[i].enemies.Count; j++)
        //        {
        //            if (other.tag == FactionManager.instance.factions[i].enemies[j].tag)
        //            {
        //                m_CandidateTargets.Remove(other.gameObject);
        //                --m_NumberOfTargetsWithinRange;
        //            }
        //        }
        //    }
        //}
    }

    public void LargeCollisionEnter(Collider other)
    {
        ++m_NumberOfTargetsWithinRange;
        m_CandidateTargets.Add(other.gameObject);
    }

    public void LargeCollisionExit(Collider other)
    {
        m_CandidateTargets.Remove(other.gameObject);
        --m_NumberOfTargetsWithinRange;
    }

    #region Private

    private void Target(GameObject enemy)
    {
        if (enemy != null)
        {
            targetIndicator.SetActive(true);
            if(enemy.GetComponent<Targeting>())
                leadIndicator.SetActive(true);
            targetedEnemy = enemy.transform;
        }
    }

    private void UnTarget(GameObject enemy)
    {
        if (enemy != null)
        {
            targetIndicator.SetActive(false);
            leadIndicator.SetActive(false);
            targetedEnemy = null;
        }
    }

    //private void TargetDies()
    //{
    //    m_CandidateTargets.Remove(agent.gameObject.gameObject);
    //    UnTarget(agent.gameObject);
    //    m_ObjectClosestToCamera = null;
    //}

    #endregion

    public static Vector3 FirstOrderIntercept(Vector3 shooterPosition, Vector3 shooterVelocity, float shotSpeed, Vector3 targetPosition, Vector3 targetVelocity)
    {
        Vector3 targetRelativePosition = targetPosition - shooterPosition;
        Vector3 targetRelativeVelocity = targetVelocity - shooterVelocity;
        float t = FirstOrderInterceptTime
        (
            shotSpeed,
            targetRelativePosition,
            targetRelativeVelocity
        );
        return targetPosition + t * (targetRelativeVelocity);
    }
    //first-order intercept using relative target position
    public static float FirstOrderInterceptTime
    (
        float shotSpeed,
        Vector3 targetRelativePosition,
        Vector3 targetRelativeVelocity
    )
    {
        float velocitySquared = targetRelativeVelocity.sqrMagnitude;
        if (velocitySquared < 0.001f)
            return 0f;

        float a = velocitySquared - shotSpeed * shotSpeed;

        //handle similar velocities
        if (Mathf.Abs(a) < 0.001f)
        {
            float t = -targetRelativePosition.sqrMagnitude /
            (
                2f * Vector3.Dot
                (
                    targetRelativeVelocity,
                    targetRelativePosition
                )
            );
            return Mathf.Max(t, 0f); //don't shoot back in time
        }

        float b = 2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
        float c = targetRelativePosition.sqrMagnitude;
        float determinant = b * b - 4f * a * c;

        if (determinant > 0f)
        { //determinant > 0; two intercept paths (most common)
            float t1 = (-b + Mathf.Sqrt(determinant)) / (2f * a),
                    t2 = (-b - Mathf.Sqrt(determinant)) / (2f * a);
            if (t1 > 0f)
            {
                if (t2 > 0f)
                    return Mathf.Min(t1, t2); //both are positive
                else
                    return t1; //only t1 is positive
            }
            else
                return Mathf.Max(t2, 0f); //don't shoot back in time
        }
        else if (determinant < 0f) //determinant < 0; no intercept path
            return 0f;
        else //determinant = 0; one intercept path, pretty much never happens
            return Mathf.Max(-b / (2f * a), 0f); //don't shoot back in time
    }
}