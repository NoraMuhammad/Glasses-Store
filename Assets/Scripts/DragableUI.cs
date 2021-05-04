using SO.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragableUI : Scoreable
{
    [Seriablizable]
    public enum DragableType
    {
        Jewelry,
        Event
    }
    //[SerializeField] EventSO onTutorialCompleted;
    [SerializeField] GameObject tutorialGO;
    [SerializeField] AudioSource dropAudioSource;
    [SerializeField] DragableType type;
    [SerializeField] LayerMask raycastLayer;
    [SerializeField] GameObject objectToDragPrefab;
    [SerializeField] Image imageToDrag;
    [SerializeField] GameObject NextButton;
    [SerializeField] Image objectToDragImage;

    List<GameObject> instantiatedObjects;
    GameObject instantiatedObject;
    static string requiredTag;
    bool tutorialCompleted;

    public static int JewelryFinalScore;
    public static int EventFinalScore;

    private void Awake()
    {
        instantiatedObjects = new List<GameObject>();
        tutorialCompleted = false;
    }
    public void StartDrag()
    {
        objectToDragImage.sprite = imageToDrag.sprite;
        objectToDragImage.rectTransform.rect.Set(imageToDrag.rectTransform.rect.x, imageToDrag.rectTransform.rect.y, imageToDrag.rectTransform.rect.width, imageToDrag.rectTransform.rect.height);

        requiredTag = gameObject.tag;

        if(!objectToDragImage.gameObject.activeInHierarchy)
            objectToDragImage.gameObject.SetActive(true);
    }
    private void Update()
    {
        if(objectToDragImage.sprite)
        {
            objectToDragImage.transform.position = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, raycastLayer))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (gameObject.tag == requiredTag)
                    {
                        if (type == DragableType.Jewelry && hitInfo.transform.gameObject.name == "frame")
                        {
                            instantiatedObject = Instantiate(objectToDragPrefab, hitInfo.point + new Vector3(0, 0, -0.01f), objectToDragPrefab.transform.rotation);

                            instantiatedObject.transform.localScale = objectToDragPrefab.transform.lossyScale;
                            instantiatedObject.transform.SetParent(hitInfo.transform);

                            instantiatedObjects.Add(instantiatedObject);

                            if (tutorialGO && tutorialGO.activeInHierarchy)
                            {
                                tutorialGO.SetActive(false);
                            }

                            dropAudioSource.Play();

                            CalculateJewelryScore();
                        }
                        else if (type == DragableType.Event)
                        {
                            if (gameObject.CompareTag("Hat"))
                            {
                                instantiatedObject = Instantiate(objectToDragPrefab, hitInfo.transform.parent.GetComponent<Glasses>().PlaceToDropHat.position, objectToDragImage.transform.rotation);
                                instantiatedObject.transform.SetParent(hitInfo.transform.parent.GetComponent<Glasses>().PlaceToDropHat);

                                if (tutorialGO && tutorialGO.activeInHierarchy && !tutorialCompleted)
                                {
                                    tutorialGO.SetActive(false);

                                    //onTutorialCompleted.Raise();
                                    tutorialCompleted = true;
                                }

                                dropAudioSource.Play();
                            }
                            else if (gameObject.CompareTag("Cup"))
                            {
                                instantiatedObject = Instantiate(objectToDragPrefab, hitInfo.transform.parent.GetComponent<Glasses>().PlaceToDropCup.position, objectToDragImage.transform.rotation);
                                instantiatedObject.transform.SetParent(hitInfo.transform.parent.GetComponent<Glasses>().PlaceToDropCup);

                                if (tutorialGO && tutorialGO.activeInHierarchy && !tutorialCompleted)
                                {
                                    tutorialGO.SetActive(false);

                                    //onTutorialCompleted.Raise();
                                    tutorialCompleted = true;
                                }

                                dropAudioSource.Play();
                            }

                            if (objectToDragImage)
                                objectToDragImage.gameObject.SetActive(false);

                            if (instantiatedObject)
                            {
                                instantiatedObject.transform.localPosition = Vector3.zero;
                                instantiatedObject.transform.localRotation = Quaternion.identity;
                                instantiatedObject.transform.localScale = Vector3.one;
                                
                                instantiatedObjects.Add(instantiatedObject);
                            }

                            CalculateEventScore();
                        }
                    }

                    if (!NextButton.activeInHierarchy)
                        NextButton.SetActive(true);
                }
            }
        }
    }
    public void UndoDragging()
    {
        if (objectToDragImage)
            objectToDragImage.gameObject.SetActive(false);

        if (instantiatedObject != null)
            Destroy(instantiatedObject);
    }

    public void ResetDragging()
    {
        if (objectToDragImage)
            objectToDragImage.gameObject.SetActive(false);

        for (int i = 0; i < instantiatedObjects.Count; i++)
        {
            Destroy(instantiatedObjects[i]);
        }

        EventFinalScore = 0;
        JewelryFinalScore = 0;
    }
    public void CalculateEventScore()
    {
        if (type == DragableType.Event)
        {
            if (EventFinalScore != 0)
            {
                if (EventFinalScore < currentColoringGrade)
                    EventFinalScore = currentColoringGrade;
            }
            else
            {
                EventFinalScore = currentColoringGrade;
            }
        }
    }
    public void CalculateJewelryScore()
    {
        if (type == DragableType.Jewelry)
        {
            if (JewelryFinalScore != 0)
            {
                if (JewelryFinalScore < currentColoringGrade)
                    JewelryFinalScore = currentColoringGrade;
            }
            else
            {
                JewelryFinalScore = currentColoringGrade;
            }
        }
    }
}
