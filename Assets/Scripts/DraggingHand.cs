using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingHand : MonoBehaviour
{
    [SerializeField] Rotator rotatingGO;
    public void RotateRight()
    {
        rotatingGO.DragRight();
    }
    public void RotateLeft()
    {
        rotatingGO.DragLeft();
    }
}
