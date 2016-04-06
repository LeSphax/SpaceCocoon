using UnityEngine;

public class Gravity : MonoBehaviour
{

    private float mass;
    private Rigidbody myRigidbody;
    private const float GRAVITY_CONSTANT = 1;
    public GameObject explosion;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        if (gameObject.tag == "Planet")
        {
            myRigidbody.velocity = Vector3.forward * 10;
        }
        mass = myRigidbody.mass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameObject stellarObject in GameObject.FindGameObjectsWithTag(Tags.planet))
        {
            if (stellarObject != gameObject)
            {
                //Debug.Log(stellarObject.name);
                if (Vector3.Distance(transform.position, stellarObject.transform.position) < 1 && gameObject.tag == Tags.star)
                {
                    DestroyPlanet(stellarObject);
                }
                stellarObject.GetComponent<Gravity>().ApplyGravityForce(transform.position, mass);
            }

        }

    }

    public void ApplyGravityForce(Vector3 positionOther, float massOther)
    {

        if (!myRigidbody.isKinematic)
        {
            //Debug.Log(mass * massOther * GRAVITY_CONSTANT + "      " + (mass * massOther * GRAVITY_CONSTANT / Mathf.Pow(Vector3.Distance(transform.position, positionOther), 2)));
        }
        Vector3 force = (mass * massOther * GRAVITY_CONSTANT / Mathf.Pow(Vector3.Distance(transform.position, positionOther), 2)) * (positionOther - transform.position);
        if (!myRigidbody.isKinematic)
        {
            //Debug.Log(Vector3.Distance(transform.position, positionOther) + "      " + force.magnitude);
        }
        myRigidbody.AddForce(force, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other)
    {
        if (gameObject.tag == Tags.star)
        {
            DestroyPlanet(other.gameObject);
        }
    }

    private void DestroyPlanet(GameObject other)
    {
        Instantiate(explosion, other.transform.position, Quaternion.identity);
        Destroy(other);
    }
}
