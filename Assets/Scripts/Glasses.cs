using UnityEngine;
using Cinemachine;
using DG.Tweening;
using SO.Events;
using SO;
using UnityEngine.UI;

public class Glasses : MonoBehaviour
{
    [SerializeField] GameObject customizationIsDoneButton;
    [SerializeField] Transform spotLight;
    [SerializeField] Button captureButton;
    [SerializeField] GameObject hintGO;
    [SerializeField] AudioSource clickedAudioSource;
    [SerializeField] LayerMask raycastLayer;
    [SerializeField] EventSO OnStartCustomization;
    [SerializeField] Rotator standRotator;
    [SerializeField] EventSO OnChoosenFrameChanges;
    [SerializeField] GameObject tutorialGO;

    [SerializeField] Transform placeOnTable;
    [SerializeField] Transform PlaceOf3DViewing;
    [SerializeField] Transform PlaceOnFace;


    public Texture dotsTexture;
    public Texture linessTexture;
    public Transform PlaceToDropCup;
    public Transform PlaceToDropHat;

    VariableSO<GameObject> choosenFrame;
    private void Start()
    {
        choosenFrame = (VariableSO<GameObject>)OnChoosenFrameChanges.Value;
        hintGO.SetActive(false);

        captureButton.interactable = false;
    }

    private void OnMouseDown()
    {
        if (LevelHandler.LevelIsStarted && RequirementsHandler.ShownSuccessfully)
        {
            if (choosenFrame.Value == null && !standRotator.IsRotating)
            {
                choosenFrame.Value = gameObject;

                clickedAudioSource.Play();

                OnStartCustomization.Raise();

                TweenTowardsPlaceOnTable();

                if (tutorialGO && tutorialGO.activeInHierarchy)
                    tutorialGO.SetActive(false);

                if(standRotator.dragTutoral && standRotator.dragTutoral.activeInHierarchy)
                    standRotator.dragTutoral.SetActive(false);

            }
        }
    }
    public void ShowTutorialCanvas()
    {
        if (!TutorialHandler.TutorialEnded && !tutorialGO.activeInHierarchy)
            tutorialGO.SetActive(true);
    }
    public void EnableRotating()
    {
        if (choosenFrame.Value.GetComponent<Rotator>() == null)
        {
            if(standRotator.dragTutoral)
            {
                choosenFrame.Value.AddComponent<Rotator>().dragTutoral = standRotator.dragTutoral;
                choosenFrame.Value.GetComponent<Rotator>().activateTutorialOnAwake = true;

                choosenFrame.Value.GetComponent<Rotator>().nextButton = customizationIsDoneButton;

                choosenFrame.Value.GetComponent<Rotator>().ontutorialCompleted = standRotator.ontutorialCompleted;
            }
            else
            {
                choosenFrame.Value.AddComponent<Rotator>();
            }

            choosenFrame.Value.GetComponent<Rigidbody>().isKinematic = false;

            choosenFrame.Value.transform.DOMove(PlaceOf3DViewing.position, 1).SetEase(Ease.InFlash);
            choosenFrame.Value.transform.DORotate(PlaceOf3DViewing.eulerAngles, 1).SetEase(Ease.InFlash);
        }
    }
    public void TweenToEyes()
    {
        if (choosenFrame.Value.GetComponent<Rotator>() != null)
        {
            Destroy(choosenFrame.Value.GetComponent<Rotator>());
            choosenFrame.Value.GetComponent<Rigidbody>().isKinematic = true;

            choosenFrame.Value.transform.DOMove(PlaceOnFace.position, 1).SetEase(Ease.InFlash).OnComplete(()=> {
                choosenFrame.Value.transform.SetParent(PlaceOnFace);

                choosenFrame.Value.transform.localPosition = Vector3.zero;
                choosenFrame.Value.transform.localRotation = Quaternion.identity;
                choosenFrame.Value.transform.localScale = Vector3.one;

                captureButton.interactable = true;
            });
        }
    }

    void TweenTowardsPlaceOnTable()
    {
        if (choosenFrame.Value.transform.parent != null)
        {
            choosenFrame.Value.transform.SetParent(null);

            choosenFrame.Value.transform.DOMove(placeOnTable.position, 1).SetEase(Ease.InFlash).OnComplete(() => {

                spotLight.SetParent(null, true);
                spotLight.SetParent(choosenFrame.Value.transform, true);

            });
            choosenFrame.Value.transform.DORotate(placeOnTable.eulerAngles, 1).SetEase(Ease.InFlash);
        }
    }
    public void AdjustChoosenFrameLookAt()
    {
        if(choosenFrame.Value != null)
        {
            choosenFrame.Value.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            choosenFrame.Value.GetComponent<Rigidbody>().isKinematic = true;

            choosenFrame.Value.transform.eulerAngles = new Vector3(choosenFrame.Value.transform.eulerAngles.x, -180, choosenFrame.Value.transform.eulerAngles.z);
            choosenFrame.Value.transform.position = PlaceOf3DViewing.position;
        }
    }
    public void EnableHint()
    {
        if(choosenFrame.Value != null)
        {
            choosenFrame.Value.GetComponent<Glasses>().hintGO.SetActive(true);
        }
    }
    public void DisableHint()
    {
        if (choosenFrame.Value != null)
        {
            choosenFrame.Value.GetComponent<Glasses>().hintGO.SetActive(false);
        }
    }
}
