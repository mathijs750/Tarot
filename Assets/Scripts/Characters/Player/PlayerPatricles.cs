using UnityEngine;
using System.Collections;

public class PlayerPatricles : MonoBehaviour {

    public GameObject RunDust;

	void Start ()
    {
	}
	
	void Update ()
    {
        if (Input.GetAxis("Horizontal") > 0.1 || Input.GetAxis("Horizontal") < -0.1 || Input.GetAxis("Vertical") > 0.1 || Input.GetAxis("Vertical") < -0.1)
        {
            RunDust.SetActive(true);
        }
        else
        {
            RunDust.SetActive(false);
        }
    }
}
