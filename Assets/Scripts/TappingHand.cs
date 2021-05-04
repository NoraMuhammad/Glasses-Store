using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TappingHand : MonoBehaviour
{
    [SerializeField] Transform GOtoTap;
    Vector3 initialPosition;
    Vector3 endTopPosition;
    bool raise;
    private void Start()
    {
        initialPosition = GOtoTap.localPosition;
        endTopPosition = initialPosition + Vector3.up / 2/* + GOtoTap.forward / 2*/;
        RaiseObjectToTap();
    }
    public void RaiseObjectToTap()
    {
        raise = true;
    }
    private void Update()
    {
        if(raise)
        {
            GOtoTap.localPosition = Vector3.Lerp(GOtoTap.localPosition, endTopPosition, 0.01f);
        }

        if(!raise)
        {
            GOtoTap.localPosition = Vector3.Lerp(GOtoTap.localPosition, initialPosition, 0.01f);
        }

        if (Vector3.Distance(GOtoTap.localPosition, endTopPosition) < 0.1f)
        {
            raise = false;
        }

        if (Vector3.Distance(GOtoTap.localPosition, initialPosition) < 0.1f)
        {
            raise = true;
        }

    }
    private void OnDisable()
    {
        GOtoTap.localPosition = initialPosition;
    }
}
