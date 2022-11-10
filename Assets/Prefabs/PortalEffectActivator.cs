using UnityEngine;
using UnityEngine.VFX;

public class PortalEffectActivator : MonoBehaviour
{
	VisualEffect portalEffect;

	void Awake()
    {
		portalEffect = GetComponentInChildren<VisualEffect>();
		portalEffect.Stop();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			//portalEffect.enabled = true;
			portalEffect.Play();
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			portalEffect.Stop();
			//portalEffect.enabled = false;
		}
	}
}
