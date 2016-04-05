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
        if (gameObject.name == "Planet")
        {
            myRigidbody.velocity = Vector3.right * 1;
        }
        mass = myRigidbody.mass;
    }

    // Update is called once per frame
    void Update()
    {
        if (!myRigidbody.isKinematic)
        {
          // Debug.Log(myRigidbody.velocity.magnitude);
        }
        foreach (GameObject stellarObject in GameObject.FindGameObjectsWithTag(Tags.planet))
        {
            if (stellarObject != gameObject)
            {
                //Debug.Log(stellarObject.name);
                stellarObject.GetComponent<Gravity>().ApplyGravityForce(transform.position, mass);
            }
        }

    }

    public void ApplyGravityForce(Vector3 positionOther, float massOther)
    {
        if (!myRigidbody.isKinematic)
        {
            Debug.Log(mass * massOther * GRAVITY_CONSTANT + "      " + (mass * massOther * GRAVITY_CONSTANT / Mathf.Pow(Vector3.Distance(transform.position, positionOther), 2)));
        }
        Vector3 force = (mass * massOther * GRAVITY_CONSTANT / Mathf.Pow(Vector3.Distance(transform.position, positionOther),2)) * (positionOther - transform.position);
        if (!myRigidbody.isKinematic)
        {
            Debug.Log(Vector3.Distance(transform.position, positionOther) + "      " + force.magnitude);
        }
        myRigidbody.AddForce(force, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == Tags.star)
        {
            Instantiate(explosion, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
