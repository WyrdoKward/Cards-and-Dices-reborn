using Assets._Scripts.Cards.Common;
using Assets._Scripts.Managers;
using Assets._Scripts.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Systems
{
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
            cardController.OnDragCard += DragCard;
            cardController.OnCardMouseUp += HandleMouseUp;
            //cardController.GetComponent<Canvas>().overrideSorting = true;
            //canvas = GameObject.Find("Environement Canvas").GetComponent<Canvas>();
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

        private void DragCard()
        {
            Cursor.visible = false;
            var target = InputHelper.GetCursorPositionInWorld(rectTransform);
            _movedCard.GetComponent<Canvas>().sortingOrder = GlobalVariables.OnDragCardSortingLayer;
            var delta = _movedCard.transform.position - target;
            delta.z = 0;


            LerpThisTo(target);
            //MoveAttachedCards(_movedCard, new Vector2(delta.x, delta.y)); //marche pas z fait n'importe quoi
        }


        private void HandleMouseUp()
        {
            // if(dragAndDropSystem.OverlapAnotherCard(thisCard) => true si n'importe quel bout des 2 rectangles se chevauchent + voir comment récupérer la liste de toutes les cartes
            //      dragAndDropSystem.ProcessDropCard(); => p-ê mettre la logique de OverlapAnotherCard dans ProcessDropCard ?

            ProcessDropCard();

        }

        public void LerpThisTo(Vector3 targetPosition, float speed = GlobalVariables.LerpSpeed)
        {
            rectTransform.position = Vector3.Lerp(rectTransform.position, targetPosition, Time.deltaTime * speed);
        }


        public void LerpThisTo(Quaternion rotation, float speed = GlobalVariables.LerpSpeed)
        {
            rectTransform.rotation = Quaternion.Lerp(rectTransform.rotation, rotation, Time.deltaTime * speed);
        }

        internal void ProcessDropCard()
        {
            _movedCard.GetComponent<Canvas>().sortingOrder = GlobalVariables.DefaultCardSortingLayer;

            if (!FindOverlappedCardIfExists())
            {

                StackHelper.ClearPrevious(_movedCard);
                //ATTENTION On ne ,peut pas encore poser un stack sur une carte
                return;
            }


            Debug.Log($"Dropped {_movedCard.GetComponent<CardDisplay>().name} on {_targetCard.GetComponent<CardDisplay>().name}");

            //determiner la carte la plus haute du stack pour en faire la target à se snapper dessus
            _targetCard = StackHelper.GetLastCardOfStack(_targetCard);
            //Debug.Log($"But last card of stack is {_targetCard.GetComponent<CardDisplay>().name}");
            var targetPosition = _targetCard.GetComponent<RectTransform>().anchoredPosition;
            var baseSortingOrder = _targetCard.GetComponent<Canvas>().sortingOrder;


            //Snap cards on UI 
            //TODO : Boucler ici pour les stacks
            targetPosition.y -= GlobalVariables.CardOffsetOnSnap * _targetCard.GetComponent<RectTransform>().localScale.y;
            _movedCard.GetComponent<RectTransform>().anchoredPosition = targetPosition;
            //Put dragged card on top
            baseSortingOrder += 1;
            _movedCard.GetComponent<Canvas>().sortingOrder = baseSortingOrder;

        }

        private void ResetThisCardStackState()
        {
            StackHelper.UpdateCardStack(_movedCard, null, null, true);
        }

        /// <summary>
        /// Assign the card under "this.movedCard" to "this.targetCard"
        /// </summary>
        /// <returns>False if no card is found</returns>
        private bool FindOverlappedCardIfExists()
        {
            var currentStack = new List<GameObject>() { _movedCard };
            currentStack.AddRange(StackHelper.GetCardsAboveInStack(_movedCard));

            foreach (var card in GameObject.Find("CardManager").GetComponent<CardManager>().GetAllCardsExcept(currentStack))
            {
                if (card.GetComponent<BoxCollider2D>().bounds.Intersects(_movedCard.GetComponent<BoxCollider2D>().bounds))
                {
                    _targetCard = card;
                    return true;
                }
            }
            return false;
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
