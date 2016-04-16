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
        Debug.Log("SetTarget" + model.objectiveType +"   "+ target);
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
                else
                {
                    transform.localPosition = Vector3.Lerp(currentStartPosition, target, 1);
                    SetState(State.IDLE);
                    Reset();
                    model.ViewReachedTarget();
                }
                break;
            default:
                break;
        }
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
