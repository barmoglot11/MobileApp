using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.EventSystems;
using UnityEngine.EventSystems;

public class CardMovementScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /// <summary>
    /// Основная камера на поле
    /// </summary>
    Camera MainCamera;
    Vector3 offset;
    public Transform DefaultParent, DefaultTempCardParent;
    GameObject TempCardGO;
    public bool isDraggable;
    public GameManagerScript gameManager;

    void Awake()
    {
        MainCamera = Camera.allCameras[0];
        TempCardGO = GameObject.Find("TempCardGO");
        gameManager = FindObjectOfType<GameManagerScript>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);

        DefaultParent = DefaultTempCardParent = transform.parent;

        isDraggable = DefaultParent.GetComponent<DropPlaceScript>().fieldType == 
                        FieldType.PLAYER_HAND && gameManager.isPlayerTurn;
        if (!isDraggable)
            return;

        TempCardGO.transform.SetParent(DefaultParent);
        TempCardGO.transform.SetSiblingIndex(transform.GetSiblingIndex());


        transform.SetParent(DefaultParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggable)
            return;

        Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position);
        transform.position = newPos + offset;

        CheckPosition();

        if (TempCardGO.transform.parent != DefaultTempCardParent)
            TempCardGO.transform.SetParent(DefaultTempCardParent);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDraggable)
            return;

        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;


        transform.SetSiblingIndex(TempCardGO.transform.GetSiblingIndex());
        TempCardGO.transform.SetParent(GameObject.Find("Canvas").transform);
        TempCardGO.transform.localPosition = new Vector3(2365, 0);
    }

    void CheckPosition()
    {
        int newIndex = DefaultTempCardParent.childCount;

        for(int i = 0; i < DefaultTempCardParent.childCount; i++)
        {
            if(transform.position.x < DefaultTempCardParent.GetChild(i).position.x)
            {
                newIndex = i;

                if (TempCardGO.transform.GetSiblingIndex() < newIndex)
                    newIndex--;

                break;
            }
        }

        TempCardGO.transform.SetSiblingIndex(newIndex);
    }
}
