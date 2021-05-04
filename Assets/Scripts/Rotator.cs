using SO.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rotator : MonoBehaviour
{
    [SerializeField] public EventSO ontutorialCompleted;
    [SerializeField] public GameObject nextButton;
    [SerializeField] public float rotateSpeed = 20f;
    [SerializeField] public GameObject dragTutoral;
    [SerializeField] EventSO onStartFrameShape;

    Vector3 touqueVal;
    Rigidbody rb;
    float xInput;
    bool dragging;
    bool tutorialCompleteIsRaised;
    public bool IsRotating => rb.angularVelocity.magnitude > 0.5f;
    public bool activateTutorialOnAwake;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if(!TutorialHandler.TutorialEnded)
        {
            if(nextButton)
                nextButton.SetActive(false);
            if (activateTutorialOnAwake && dragTutoral)
                dragTutoral.SetActive(true);
        }
        else if(dragTutoral)
        {
            dragTutoral.SetActive(false);
        }

        dragging = false;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.AddTorque(-touqueVal);
        }
        if (Input.GetMouseButton(0))
        {
            if (RequirementsHandler.ShownSuccessfully)
                dragging = true;

            if(!TutorialHandler.TutorialEnded)
            {
                if (dragTutoral && dragTutoral.activeInHierarchy)
                {
                    dragTutoral.SetActive(false);

                    if (onStartFrameShape)
                        onStartFrameShape.Raise();
                }
            }

            if (dragging && nextButton && !tutorialCompleteIsRaised)
            {
                nextButton.SetActive(true);

                if (ontutorialCompleted)
                {
                    ontutorialCompleted.Raise();
                    tutorialCompleteIsRaised = true;
                }
            }
            //else
            //{
            //    nextButton.SetActive(false);
            //}
        }
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }
    }
    private void FixedUpdate()
    {
        if (dragging)
        {
            xInput = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;

            touqueVal = Vector3.down * xInput;
            rb.AddTorque(touqueVal);
        }
    }
    public void DragRight()
    {
        rb.angularVelocity = Vector3.zero;
        rb.AddTorque(Vector3.down * rotateSpeed * 2 * Time.deltaTime);
    }
    public void DragLeft()
    {
        rb.angularVelocity = Vector3.zero;
        rb.AddTorque(Vector3.up * rotateSpeed * 2 * Time.deltaTime);
    }
}
