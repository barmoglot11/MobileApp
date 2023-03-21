using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum FieldType
{
    PLAYER_HAND,
    PLAYER_FIELD,
    ENEMY_HAND,
    ENEMY_FIELD
}

public class DropPlaceScript : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public FieldType fieldType;
    public void OnDrop(PointerEventData eventData)
    {
        if (fieldType == FieldType.ENEMY_FIELD || fieldType == FieldType.ENEMY_HAND)
            return;

       CardMovementScript card = eventData.pointerDrag.GetComponent<CardMovementScript>();

        if (card)
            card.DefaultParent = transform;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerDrag == null || fieldType == FieldType.ENEMY_FIELD || fieldType == FieldType.ENEMY_HAND)
            return;

        CardMovementScript card = eventData.pointerDrag.GetComponent<CardMovementScript>();

        if (card)
            card.DefaultTempCardParent = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        CardMovementScript card = eventData.pointerDrag.GetComponent<CardMovementScript>();

        if (card && card.DefaultTempCardParent == transform)
            card.DefaultTempCardParent = card.DefaultParent;
    }
}
