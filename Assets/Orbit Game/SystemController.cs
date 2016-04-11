using UnityEngine;
using System.Collections;

public class SystemController : MonoBehaviour
{

    public GameObject planet;

    public float speed;
    private GameObject currentPlanet;

    private Vector3 origin;

    private enum State
    {
        IDLE,
        THROWING,
    }

    private State state;

    void Start()
    {
        state = State.IDLE;
    }

    void Update()
    {
        Vector3 mousePosition;
        switch (state)
        {
            case State.IDLE:
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("wala");
                    origin = GetMouseWorldPosition();
                    if (origin != Vector3.zero)
                    {
                        state = State.THROWING;
                        currentPlanet = (GameObject)Instantiate(planet, origin, Quaternion.identity);
                    }

                }

                break;
            case State.THROWING:
                mousePosition = GetMouseWorldPosition();

                currentPlanet.transform.position = origin;

                if (Input.GetMouseButtonUp(0))
                {
                    currentPlanet.GetComponent<Rigidbody>().velocity = (mousePosition - currentPlanet.transform.position) * speed;
                    currentPlanet = null;
                    state = State.IDLE;
                }
                break;
            default:
                break;
        }
    }







    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log("Hit");
            GameObject plane = hit.collider.gameObject;
            if (plane.tag == Tags.PlanetSystemPlane)
            {
                return hit.point;
            }
        }
        return Vector3.zero;
    }
}
