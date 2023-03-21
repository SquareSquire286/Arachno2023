using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remote : RoomParameterGrabbable
{
    public GameObject leftHand, rightHand;
    public AbstractButton button;

    public override void Start()
    {
        isGrabbed = false;
        rigidbody = GetComponent<Rigidbody>();
    }

    public override void WhileGrabbed()
    {
        transform.parent = handGrabbingMe.transform;
        rigidbody.isKinematic = true;

        updateIterations++;

        if (updateIterations == 10)
        {
            updateIterations = 0;
            
            previousPosition = transform.position;
            previousRotation = transform.rotation;
        }

        this.RemoveHighlight();

        if ((OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && handGrabbingMe == rightHand) || (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger) && handGrabbingMe == leftHand))
        {
            float timeLastPressed;

            if (handGrabbingMe == rightHand)
                timeLastPressed = rightHand.GetComponent<Grabber>().GetTimeLastPressed();

            else timeLastPressed = leftHand.GetComponent<Grabber>().GetTimeLastPressed();

            if (Time.time - timeLastPressed > 0.6f)
            {
                button.OnPress();
                rightHand.GetComponent<Grabber>().SetTimeLastPressed(Time.time);
                leftHand.GetComponent<Grabber>().SetTimeLastPressed(Time.time);
            }
        }

        else if ((OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger) && handGrabbingMe == rightHand) || (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger) && handGrabbingMe == leftHand))
        {
            button.OnRelease();
        }
    }

    public override void WhenReleased()
    {
        this.SetGrabStatus(false, null);

        transform.parent = null;
        rigidbody.isKinematic = false;

        Vector3 velocity = (transform.position - previousPosition) / (10 * Time.deltaTime);
        Vector3 angularVelocity = (transform.rotation.eulerAngles - previousRotation.eulerAngles) / (5 * Time.deltaTime);

        rigidbody.drag = 0;
        rigidbody.velocity = velocity;
        rigidbody.angularVelocity = angularVelocity;

        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }
}
