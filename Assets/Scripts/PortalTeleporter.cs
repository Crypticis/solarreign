using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySteer.Behaviors;

public class PortalTeleporter : MonoBehaviour {

	public Transform reciever;

	[SerializeField] private bool playerIsOverlapping = false;

	public float cooldownTimer = 0;
	public string nameOfDestination;

    private void Start()
    {
		string[] names = this.name.Split('-');
		nameOfDestination = names[1];
    }

    void Update () {

		//cooldownTimer -= Time.deltaTime;

		if (playerIsOverlapping)
		{
            //Vector3 portalToPlayer = Player.playerInstance.transform.position - transform.position;
            //float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            // If this is true: The player has moved across the portal
            //if (/*dotProduct < 0f && */cooldownTimer <= 0)
            //{
            //	reciever.GetComponent<PortalTeleporter>().SetCooldownTimer(5f);

            //	// Teleport him!
            //	float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
            //	rotationDiff += 180;
            //	Player.playerInstance.transform.Rotate(Vector3.up, rotationDiff);

            //	Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
            //	Player.playerInstance.transform.position = reciever.position + positionOffset;

            //	playerIsOverlapping = false;

            //	NavigationManager.instance.SetSystem(nameOfDestination);
            //}

            try
            {
				SceneManager.LoadScene(nameOfDestination);
			}
            catch
            {
				Debug.Log("Destination does not exist, yet.");
            }
		}
	}

	public void SetCooldownTimer(float amount)
    {
		cooldownTimer = amount;
	}

	void OnTriggerStay (Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = true;
		}
		else if (other.tag == "NPC" && other.GetComponentInChildren<SteerForPursuit>().Quarry == this.GetComponent<DetectableObject>())
        {
			Destroy(other.gameObject);
        }
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = false;
		}
	}
}
