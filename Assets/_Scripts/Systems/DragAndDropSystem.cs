using Assets._Scripts.Cards.Common;
using Assets._Scripts.Managers;
using Assets._Scripts.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Systems
{
    [Obsolete]
    internal class DragAndDropSystem
    {
        private readonly Canvas canvas;
        private readonly RectTransform rectTransform;
        private readonly CardController cardController;
        private GameObject _movedCard;
        private GameObject _targetCard;

        public DragAndDropSystem(CardController controller, RectTransform transform)
        {
            cardController = controller;
            //cardController.OnDragCard += DragCard;
            //cardController.OnCardMouseUp += HandleMouseUp;
            rectTransform = transform;
        }


        public void SetCards(GameObject movedCard, GameObject targetCard = null)
        {
            _movedCard = movedCard;
            _targetCard = targetCard;
        }

        public GameObject GetTargetCard()
        {
            return _targetCard;
        }

        //private void DragCard()
        //{
        //    var target = InputHelper.GetCursorPositionInWorld(rectTransform);
        //    _movedCard.GetComponent<Canvas>().sortingOrder = GlobalVariables.OnDragCardSortingLayer;


        //    LerpThisTo(target);
        //}


        private void HandleMouseUp()
        {
            // Determiner les liens à créer/casser selon la situation
            if (!FindOverlappedCardIfExists())
            {
                _movedCard.GetComponent<CardController>().UnlinkPreviousCard();
                //ATTENTION On ne peut pas encore poser un stack sur une carte
            }
            else
            {
                _movedCard.GetComponent<CardController>().SetPreviousCard(_targetCard);
            }

            // Appliquer les modifs sur l'UI
            ProcessDropCardOnUI();

        }

        public void LerpThisTo(Vector3 targetPosition, float speed = GlobalVariables.LerpSpeed)
        {
            rectTransform.position = Vector3.Lerp(rectTransform.position, targetPosition, Time.deltaTime * speed);
        }


        public void LerpThisTo(Quaternion rotation, float speed = GlobalVariables.LerpSpeed)
        {
            rectTransform.rotation = Quaternion.Lerp(rectTransform.rotation, rotation, Time.deltaTime * speed);
        }

        internal void ProcessDropCardOnUI()
        {
            _movedCard.GetComponent<Canvas>().sortingOrder = GlobalVariables.DefaultCardSortingLayer;

            if (_targetCard == null)
                return;

            Debug.Log($"Dropped {_movedCard.GetComponent<CardDisplay>().name} on {_targetCard.GetComponent<CardDisplay>().name}");

            var position = _targetCard.GetComponent<RectTransform>().anchoredPosition;
            position.y -= GlobalVariables.CardOffsetOnSnap * _targetCard.GetComponent<RectTransform>().localScale.y;

            var sortingOrder = _targetCard.GetComponent<Canvas>().sortingOrder;
            sortingOrder += 1;

            _movedCard.GetComponent<CardDisplay>().MoveAndSort(position, sortingOrder);

        }

        private void ResetThisCardStackState()
        {
            StackHelper.UpdateCardStack(_movedCard, null, null, true);
        }

        /// <summary>
        /// Assign the card under "this.movedCard" to "this.targetCard". If it in a stack, assign the last one as the target
        /// </summary>
        /// <returns>False if no card is found</returns>
        private bool FindOverlappedCardIfExists()
        {
            var currentStack = new List<GameObject>() { _movedCard };
            currentStack.AddRange(StackHelper.GetCardsAboveInStack(_movedCard));
            GameObject foundCard = null;
            foreach (var card in GameObject.Find("CardManager").GetComponent<CardManager>().GetAllCardsExcept(currentStack))
            {
                if (card.GetComponent<BoxCollider2D>().bounds.Intersects(_movedCard.GetComponent<BoxCollider2D>().bounds))
                {
                    foundCard = card;
                    break;
                }
            }

            if (foundCard == null)
                return false;

            // Determiner la carte la plus haute du stack pour en faire la target à se snapper dessus
            _targetCard = StackHelper.GetLastCardOfStack(foundCard);
            return true;
        }

        private void MoveAttachedCards(GameObject card, Vector2 delta, float speed = GlobalVariables.LerpSpeed)
        {
            var next = card.GetComponent<CardController>().NextCardInStack;
            if (next == null)
                return;

            //rectTransform.position = Vector3.Lerp(rectTransform.position, targetPosition, Time.deltaTime * speed);
            next.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(next.GetComponent<RectTransform>().anchoredPosition, delta, Time.deltaTime * speed);
            Debug.Log($"{next.transform.position.z}");
            MoveAttachedCards(next, delta);
        }
    }
}
