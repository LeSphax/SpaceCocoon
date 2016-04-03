using UnityEngine;
using UnityStandardAssets.Utility;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class StaticFirstPersonController : MonoBehaviour
    {
        private enum State
        {
            IDLE,
            MOVING_OUT,
            MOVING_IN,
            ZOOMED_IN
        }

        private State state;

        public Transform center;

        [SerializeField]
        private MouseLook m_MouseLook;

        private Camera m_Camera;
        private Vector2 m_Input;

        private Transform target;

        public float speedCameraMovements = 5;

        // Use this for initialization
        private void Start()
        {
            m_Camera = Camera.main;
            m_MouseLook.Init(transform, m_Camera.transform);
        }


        // Update is called once per frame
        private void Update()
        {
            switch (state)
            {
                case State.IDLE:
                    RotateView();
                    CheckMoveIn();
                    break;
                case State.MOVING_OUT:
                    CheckMoveIn();
                    MoveOut();
                    break;
                case State.MOVING_IN:
                    CheckMoveOut();
                    MoveIn();
                    break;
                case State.ZOOMED_IN:
                    CheckMoveOut();
                    break;
                default:
                    break;
            }
        }

        private void MoveIn()
        {
            if (MoveTowardsTarget())
            {
                state = State.ZOOMED_IN;
                m_MouseLook.SetCursorLock(false);
            }
        }

        private void MoveOut()
        {
            if (MoveTowardsTarget())
            {
                state = State.IDLE;
                m_MouseLook.SetCursorLock(true);
                m_MouseLook.Init(transform, m_Camera.transform);
            }
        }

        private bool MoveTowardsTarget()
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speedCameraMovements);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * speedCameraMovements);
            if (Vector3.Distance(transform.position, target.position) < 0.001f)
            {
                transform.position = target.position;
                return true;
            }
            return false;
        }

        private void CheckMoveOut()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("MovingOut");
                state = State.MOVING_OUT;
                m_MouseLook.SetCursorLock(true);
                target = center;
            }
        }

        private void CheckMoveIn()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    GameObject hitObject = hit.collider.gameObject;
                    if (hitObject.tag == "PointOfInterest")
                    {
                        target = hitObject.GetComponent<PointOfInterest>().GetPointOfInterestCameraTransform();
                        state = State.MOVING_IN;
                        m_MouseLook.SetCursorLock(true);
                    }
                }
            }
        }

        private void RotateView()
        {
            m_MouseLook.LookRotation(transform, m_Camera.transform);
        }
    }
}
