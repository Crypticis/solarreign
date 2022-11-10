using UnityEngine;
using TMPro;

namespace FLFlight.UI
{
    /// <summary>
    /// Shows throttle and speed of the player ship.
    /// </summary>
    public class SpeedUI : MonoBehaviour
    {
        private TMP_Text text;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        // Update is called once per frame
        void Update()
        {
            if (text != null && Ship.PlayerShip != null)
            {
                text.text = string.Format("THR: {0}%", (Ship.PlayerShip.Input.Throttle * 100.0f).ToString("000"));
            }
        }
    }
}

