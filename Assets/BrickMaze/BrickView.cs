using UnityEngine;
using System.Collections.Generic;

public class BrickView : MonoBehaviour
{

    private enum State
    {
        IDLE,
        MOVING
    }

    private State state;
    private bool finishedAnimation;
    private Vector3 currentVelocity = Vector3.zero;

    private Queue<Vector3> targets;
    private float speed = 0.2f;
    private const float SPEED_IDLE = 3;

    void Awake()
    {
        
        targets = new Queue<Vector3>();
    }

    public void SetPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    public void SetTarget(Vector3 target)
    {
        targets.Enqueue(target);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.IDLE:
                if (finishedAnimation)
                {
                    //if (transform.localPosition.y > 0)
                    //{
                    //    targets.Enqueue(transform.position + Vector3.down * 0.1f);
                    //    finishedAnimation = false;
                    //}
                    //else
                    //{
                    //    targets.Enqueue(transform.position + Vector3.up * 0.1f);
                    //    finishedAnimation = false;
                    //}
                }
                break;
            case State.MOVING:
                if (finishedAnimation)
                {
                    SetIdleState();
                }
                break;
            default:
                break;
        }
        if (targets.Count > 0)
        {
            Debug.Log("Le mouvement des briques est trop rapide, il faudrait voir pourquoi le SmoothDamp ne fonctionne pas");
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targets.Peek(), ref currentVelocity, Time.deltaTime * speed);

            if (Mathf.Approximately(Vector3.Distance(transform.localPosition, targets.Peek()), 0))
            {
                targets.Dequeue();
                currentVelocity = Vector3.zero;
            }
        }
    }

    private void SetIdleState()
    {
        state = State.IDLE;
        speed = SPEED_IDLE;
    }
}
