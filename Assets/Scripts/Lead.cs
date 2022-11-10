using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FLFlight;
using UnitySteer.Behaviors;

public class Lead : MonoBehaviour
{
    private Image iconImg;

    public Transform player;
    public Transform target;
    public Camera cam;
    public float targettingRange;

    public float closeEnoughDist;
    public Vector3 leadLocation;

    public float projectileSpeed;
    public Vector3 targetVelocity; 

    // Start is called before the first frame update
    void Start()
    {
        iconImg = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        if (target == null)
        {
            Destroy(gameObject);
        }

        if (target != null)
        {
            GetDistance();
            CheckOnScreen();

            projectileSpeed = 200f + Ship.PlayerShip.Velocity.magnitude;
            targetVelocity = target.GetComponent<AutonomousVehicle>().Velocity;
            CalculateLead();
        }
    }

    void GetDistance()
    {
        float distance = Vector3.Distance(player.position, target.position);

        if (distance <= closeEnoughDist)
        {
            ToggleUI(false);
        }
        if (distance >= targettingRange)
        {
            Destroy(gameObject);
        }
    }

    void CheckOnScreen()
    {
        float isOnScreen = Vector3.Dot((target.position - cam.transform.position).normalized, cam.transform.forward);
        if (isOnScreen <= 0)
        {
            ToggleUI(false);
        }
        else
        {
            ToggleUI(true);
            transform.position = cam.WorldToScreenPoint(leadLocation);
        }
    }

    private void ToggleUI(bool _value)
    {
        iconImg.enabled = _value;
    }

    void CalculateLead()
    {
        Vector3 V = targetVelocity;
        Vector3 D = target.transform.position - player.transform.position;
        float A = V.sqrMagnitude - projectileSpeed * projectileSpeed;
        float B = 2 * Vector3.Dot(D, V);
        float C = D.sqrMagnitude;
        if (A >= 0)
        {
            Debug.LogError("No solution exists");
            leadLocation = target.transform.position;
        }
        else
        {
            float rt = Mathf.Sqrt(B * B - 4 * A * C);
            float dt1 = (-B + rt) / (2 * A);
            float dt2 = (-B - rt) / (2 * A);
            float dt = (dt1 < 0 ? dt2 : dt1);
            leadLocation = target.transform.position + V * dt;
        }
    }
}
