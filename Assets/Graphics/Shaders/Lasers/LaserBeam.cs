using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public LineRenderer[] lr;
    public Transform point;
    public float damage;
    public GameObject contactEffect;

    public virtual void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 direction = point.position - transform.position;

        for (int i = 0; i < lr.Length; i++)
        {
            lr[i].SetPosition(1, new Vector3(0, 0, 500f));
        }

        contactEffect.SetActive(false);

        if (Physics.Raycast(transform.position, direction, out hit, 100))
        {
            Debug.DrawRay(transform.position, direction, Color.red);

            if (hit.transform && hit.transform.gameObject.layer != this.transform.parent.gameObject.layer)
            {
                //lr.SetPosition(1, new Vector3(0, 0, Vector3.Distance(transform.position, hit.point) * 5));

                for (int i = 0; i < lr.Length; i++)
                {
                    lr[i].SetPosition(1, contactEffect.transform.localPosition);
                }

                if (hit.transform.gameObject.GetComponent<DamageHandler>())
                {
                    contactEffect.SetActive(true);

                    contactEffect.transform.position = hit.point;

                    hit.transform.gameObject.GetComponent<DamageHandler>().TakeDamage(damage, this.transform.root.gameObject, this.gameObject);
                }
            }
        }
    }
}
