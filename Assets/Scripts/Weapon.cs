using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("On trigger weapon");
        }
        if (Input.touchCount > 0)
        {
            if (Input.touchCount == 1)
            {
                bool isLeftHalf = Input.GetTouch(0).position.x < Screen.width / 2;
                if (Input.GetTouch(0).phase == TouchPhase.Ended && !isLeftHalf && other.CompareTag("Obstacle"))
                {
                    Debug.Log(other.ToString());
                    Destroy(other.gameObject);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.F) && other.CompareTag("Obstacle")) {
            Debug.Log(other.ToString());
            Destroy(other.gameObject);
        }
    }
}
