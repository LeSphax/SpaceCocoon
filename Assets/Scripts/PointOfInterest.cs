using UnityEngine;

class PointOfInterest : MonoBehaviour
{
    
    public Transform cameraTransform = null;

    public Transform GetPointOfInterestCameraTransform()
    {
        return cameraTransform;
    }
}

