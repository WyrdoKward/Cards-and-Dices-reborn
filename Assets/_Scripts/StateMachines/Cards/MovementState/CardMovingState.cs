using Assets._Scripts.Cards.Common;
using Assets._Scripts.Managers;
using Assets._Scripts.Utilities;
using UnityEngine;

namespace Assets._Scripts.StateMachines.Cards.MovementState
{
    public class CardMovingState : CardBaseMovementState
    {
        public bool MovingByMouse = false;
        private GameObject CardHovered;
        private bool isHoveringCard = false;
        public override void Enter(IStateContext uncastController)
        {
            CastContext(uncastController);

            _rectTransform = cardController.GetComponent<RectTransform>();
            cardController.LastPosition = _rectTransform.position;

            Cursor.visible = false;
            cardController.transform.localScale = GlobalVariables.CardBiggerScale;
            cardController.GetComponent<Canvas>().sortingOrder = GlobalVariables.OnDragCardSortingLayer;

            //if next => switch them to follow
            cardController.NextCardInStack?.Follow();
        }

        public override void Exit(IStateContext uncastController)
        {
            CastContext(uncastController);

            MovingByMouse = false;
            Cursor.visible = true;
            cardController.transform.localScale = GlobalVariables.CardDefaultScale;
            cardController.GetComponent<Canvas>().sortingOrder = GlobalVariables.DefaultCardSortingLayer;
        }

        public override void OnMouseDrag(IStateContext uncastController)
        {
            CastContext(uncastController);
            TargetPosition = InputHelper.GetCursorPositionInWorld() - cardController.MouseDelta;

            HighlightInteractableCards();

            CheckIfHoveringAnotherCard();
        }

        private void HighlightInteractableCards()
        {
            var interactablesCards = GameObject.Find("Managers/CardManager").GetComponent<CardProvider>().GetAllCardsThatInteractsWith(cardGO);
            //TODO : appliquer un outline aux interactablesCards
        }

        private void CheckIfHoveringAnotherCard()
        {
            //Si on survole une autre carte, on récupère l'action comme si les cartes étaient stackées
            var targetCard = DragAndDropHelper.FindOverlappedCardIfExists(cardGO);


            if (!isHoveringCard && targetCard != null) //Si on entre sur une carte
            {
                isHoveringCard = true;
                CardHovered = targetCard;
                CardHovered.GetComponent<CardController>().OnHoverEnter(cardGO);

            }
            else if (isHoveringCard && targetCard == null) //Si on sort d'une carte
            {
                isHoveringCard = false;
                CardHovered.GetComponent<CardController>().OnHoverExit();
                CardHovered = null;
            }
            else if (targetCard != null && targetCard != CardHovered) //Si on passe d'une carte à une autre sans transition
            {
                isHoveringCard = true;
                CardHovered.GetComponent<CardController>().OnHoverExit();
                CardHovered = targetCard;
                CardHovered.GetComponent<CardController>().OnHoverEnter(cardGO);
            }
        }

        public override void OnMouseUp(IStateContext uncastController)
        {
            CastContext(uncastController);
            var targetCard = DragAndDropHelper.FindOverlappedCardIfExists(cardGO);
            cardController.SetPreviousCard(targetCard);
            //Debug.Log($"targetCard = {targetCard}");
            if (targetCard == null)
            {
                cardController.SwitchState(cardController.IdleState);
                return;
            }

            //Snap UI on target card
            cardController.GetComponent<CardDisplay>().SnapOnCard(targetCard);

            // Si il y a une recette, on la lance
            var firstcardOfStack = StackHelper.GetFirstCardOfStack(cardGO);
            firstcardOfStack.RunOrResetIfCombination();

            cardController.SwitchState(cardController.FollowingState);
        }

        public override void UpdateState(IStateContext uncastController)
        {
            CastContext(uncastController);
            _rectTransform.position = Vector2.Lerp(_rectTransform.position, TargetPosition, Time.deltaTime * GlobalVariables.LerpSpeed);

            if (MovingByMouse) return;

            // Passe en idle si la cible est atteinte seulement si il est en "pilote auto" (Diperse par ex.)
            // sinon ça hache le mouvement sur un drag à la souris quand la carte atteint sa cible, le curseur
            if ((Vector2)_rectTransform.position == TargetPosition)
            {
                cardController.SwitchState(cardController.IdleState);
            }
        }
    }
}
