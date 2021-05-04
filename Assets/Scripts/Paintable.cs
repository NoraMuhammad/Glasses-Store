using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintable : MonoBehaviour
{
    public bool Painted = false;
    public bool CanPaint = false;
    public void EnablePainting()
    {
        CanPaint = true;
    }
}
