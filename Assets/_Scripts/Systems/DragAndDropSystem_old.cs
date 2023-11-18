using Assets._Scripts.Cards.Common;
using Assets._Scripts.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._Scripts.Systems
{
    internal class DragAndDropSystem_old
    {
        private readonly Canvas canvas;
        private readonly RectTransform rectTransform;
        private readonly CardController_old cardController;

        public DragAndDropSystem_old(CardController_old controller, RectTransform transform)
        {
            cardController = controller;
            cardController.OnBeginDragCard += BeginDrag;
            cardController.OnDragCard += Drag;
            cardController.OnEndDragCard += EndDrag;
            cardController.OnDropCard += Drop;
            cardController.GetComponent<Canvas>().overrideSorting = true;
            canvas = GameObject.Find("Environement Canvas").GetComponent<Canvas>();
            rectTransform = transform;
        }

        private void BeginDrag(PointerEventData eventData)
        {
            var sortingOrderOnDrag = GlobalVariables.OnDragCardSortingLayer;
            cardController.GetComponent<Canvas>().sortingOrder = sortingOrderOnDrag;
        }

        private void Drag(PointerEventData eventData)
        {
            var deltaPosition = eventData.delta / canvas.scaleFactor;
            rectTransform.anchoredPosition += deltaPosition;
        }

        private void EndDrag(PointerEventData eventData)
        {
            var sortingOrder = GlobalVariables.DefaultCardSortingLayer;
            cardController.GetComponent<Canvas>().sortingOrder = sortingOrder;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData">The card received</param>
        private void Drop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;

            var targetCard = cardController;
            var droppedCard = eventData.pointerDrag.GetComponent<CardController_old>();
            Debug.Log($"{targetCard.CardSO.Name} receiving : {droppedCard.CardSO.Name}");

            ////Si les 2 cartes sont dcans le meme stack, on ne les resnap pas
            //if (targetCard.IsInSameStack(droppedCard))
            //    return;


            var targetPosition = targetCard.GetComponent<RectTransform>().anchoredPosition;
            var baseSortingOrder = targetCard.GetComponent<Canvas>().sortingOrder;

            //foreach (var cardGO in droppedCard.GetNextGameObjectsInStack())
            //{
            //Snapping UI
            targetPosition.y -= 70 * targetCard.GetComponent<RectTransform>().localScale.y;
            droppedCard.GetComponent<RectTransform>().anchoredPosition = targetPosition;

            //Put dragged card on top
            baseSortingOrder += 1;
            droppedCard.GetComponent<Canvas>().sortingOrder = baseSortingOrder;
            //}


        }

    }
}
