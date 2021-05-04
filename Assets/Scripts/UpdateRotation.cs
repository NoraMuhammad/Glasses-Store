using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRotation : MonoBehaviour
{
 
    public void SetRotation()
    {
        this.gameObject.GetComponent<Transform>().rotation = new Quaternion(0, 0, 0,0);
    }
}
