using UnityEngine;

public class BrickView : MonoBehaviour
{

    internal enum State
    {
        IDLE,
        MOVING
    }

    private State state;
    private float currentVelocity = 0;
    private Vector3 currentStartPosition = Vector3.zero;

    BrickModel model;

    private Vector3 target;
    private float rateLerp = 1 / 0.2f;

    void Awake()
    {
        model = GetComponent<BrickModel>();
        state = State.IDLE;
    }

    public void SetPosition(Vector3 position)
    {
        transform.localPosition = position + Vector3.up * 0.3f;
    }

    public void SetTarget(Vector3 target)
    {
        state = State.MOVING;
        currentStartPosition = transform.localPosition;
        this.target = target + Vector3.up * 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.IDLE:
                break;
            case State.MOVING:
                if (currentVelocity < 1.0)
                {
                    currentVelocity += Time.deltaTime * rateLerp;
                    transform.localPosition = Vector3.Lerp(currentStartPosition, target, currentVelocity);
                }
                else if (Mathf.Approximately(Vector3.Distance(transform.localPosition, target), 0))
                {
                    SetState(State.IDLE);
                    Reset();
                    model.ViewReachedTarget();

                }
                break;
            default:
                break;
        }
        // Debug.Log("Le mouvement des briques est trop rapide, il faudrait voir pourquoi le SmoothDamp ne fonctionne pas");

    }

    internal void SetState(State state)
    {
        this.state = state;
    }


    private void Reset()
    {
        currentVelocity = 0;
    }
}
