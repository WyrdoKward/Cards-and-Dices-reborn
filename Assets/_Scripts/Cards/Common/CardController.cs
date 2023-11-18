using Assets._Scripts.Managers;
using Assets._Scripts.ScriptableObjects;
using Assets._Scripts.StateMachines;
using Assets._Scripts.StateMachines.Cards;
using Assets._Scripts.Systems;
using Assets._Scripts.Utilities;
using System;
using UnityEngine;

namespace Assets._Scripts.Cards.Common
{
    internal class CardController : MonoBehaviour, IStateContext
    {
        [SerializeField]
        private Canvas canvas;
        public BaseCardSO CardSO;

        // State Machine
        private CardBaseState currentState;
        internal CardIdleState IdleState = new();
        internal CardInMotionState InMotionState = new();
        internal CardTimedState TimedState = new();
        internal CardPausedState PausedState = new();


        public event Action<BaseCardSO> OnStartCard;
        public event Action OnDragCard;
        public event Action OnCardMouseUp;

        [NonSerialized]
        public bool IsSelected;

        private DragAndDropSystem dragAndDropSystem;
        private RectTransform rectTransform;
        public bool IsBeingDragged;
        public GameObject PreviousCardInStack;
        public GameObject NextCardInStack;



        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            GameObject.Find("CardManager").GetComponent<CardManager>().RegisterCardToGlobalList(gameObject);
        }

        void Start()
        {
            //Debug.Log("Start CardController");
            dragAndDropSystem = new DragAndDropSystem(this, rectTransform);
            OnStartCard?.Invoke(CardSO);

            currentState = IdleState;
            currentState.Enter(this);
        }

        void FixedUpdate()
        {

        }
        void Update()
        {
            currentState.HandleInput(this);
            currentState.UpdateState(this);
            // a déplacer et ca marche tolujours paaaas
            if (PreviousCardInStack != null)
            {
                GetComponent<CardDisplay>().FollowPreviousCard(PreviousCardInStack);
            }

        }

        public void SwitchState(IState newState)
        {
            currentState.Exit(this);
            currentState = (CardBaseState)newState;
            currentState.Enter(this);
        }

        #region Draggable
        private void OnMouseDown()
        {
            Debug.Log($"Click on {CardSO.name}");
            transform.localScale *= GlobalVariables.CardDragNDropScaleFactor;
            IsBeingDragged = true;
            dragAndDropSystem.SetCards(gameObject);
            // TODO Conditionner le départ du drag à un minimum de mvt de la souris depuis la pos initiale pour pouvoir cliquer et afficher qqch sans que ca soit considéré comme du drag
        }

        private void OnMouseDrag() // a implémenter dans le state directement ?
        {
            if (IsBeingDragged)
            {
                OnDragCard?.Invoke();
            }
        }

        //quand on relache la carte
        private void OnMouseUp()
        {
            Debug.Log($"Up on {CardSO.name}");
            Cursor.visible = true;
            transform.localScale /= GlobalVariables.CardDragNDropScaleFactor;
            IsBeingDragged = false;

            OnCardMouseUp?.Invoke();
            StackHelper.UpdateCardStack(gameObject, dragAndDropSystem.GetTargetCard(), null, false);
        }

        /// <summary>
        /// Créé le chaînage des cartes pour les stacks
        /// </summary>
        /// <param name="previousCard">La carte en dessous</param>
        /// <param name="nextCard">La carte au dessus</param>
        /// <param name="overrideWithNulls">True pour vider les previous/next</param>
        //private void UpdateCardStack(GameObject previousCard, GameObject nextCard, bool overrideWithNulls)
        //{
        //    if (overrideWithNulls)
        //    {
        //        PreviousCardInStack = previousCard;
        //        NextCardInStack = nextCard;
        //    }

        //    if (previousCard != null)
        //    {
        //        PreviousCardInStack = previousCard;
        //        PreviousCardInStack.GetComponent<CardController>().NextCardInStack = gameObject;
        //    }

        //    if (nextCard != null)
        //    {
        //        NextCardInStack = nextCard;
        //        NextCardInStack.GetComponent<CardController>().PreviousCardInStack = gameObject;
        //    }

        //}
        #endregion
    }
}
