using Cinemachine;
using UnityEngine;

public class CMBlendList : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera[] childCameras; 
    public CinemachineVirtualCamera[] GetCMChild()
    {
        return childCameras;
    }
}
