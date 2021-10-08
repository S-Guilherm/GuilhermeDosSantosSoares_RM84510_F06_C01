using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform cameraPlayer;
    public Transform portal;
    public Transform otherPortal;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerOffsetFromPortal = (cameraPlayer.position - otherPortal.position) / 2;
        transform.position = portal.position + playerOffsetFromPortal;

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);

        Quaternion portalRotationDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);

        Vector3 newCameraDirection = portalRotationDifference * cameraPlayer.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}
