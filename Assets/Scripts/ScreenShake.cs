using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{

    public static ScreenShake instance;

    // Transform of the GameObject you want to shake
    private Transform transform;

    // Desired duration of the shake effect
    private float shakeDuration = 0f;

    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0.1f;

    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 1.0f;

    // The initial position of the GameObject
    Vector3 initialPosition;

    void Start()
    {
        instance = this;

        if (transform == null)
        {
            transform = GetComponent(typeof(Transform)) as Transform;
        }

        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            //transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            var pos = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            transform.localPosition = Vector3.Lerp(transform.localPosition, pos, 2f);

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, 2f);
        }
    }

    public void TriggerShake(float s, float m)
    {
        shakeMagnitude = m;
        shakeDuration = s;
    }
}
