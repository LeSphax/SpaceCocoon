using UnityEngine;


public class Objective : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        GetComponent<Renderer>().material.color = Color.green;
    }
}
