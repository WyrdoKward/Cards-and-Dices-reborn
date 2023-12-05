using Assets._Scripts.Managers;
using Assets._Scripts.ScriptableObjects;
using Assets._Scripts.StateMachines;
using Assets._Scripts.StateMachines.Cards;
using Assets._Scripts.Systems;
using System;
using UnityEngine;

namespace Assets._Scripts.Cards.Common
{
    public class CardController : MonoBehaviour, IStateContext
    {
        [SerializeField]
        private Canvas canvas;
        public BaseCardSO CardSO;

        // State Machine
        private CardBaseState currentState;
        public CardIdleState IdleState = new();
        public CardMovingState MovingState = new();
        public CardFollowingState FollowingState = new();
        //public CardPausedState PausedState = new();


        public event Action<BaseCardSO> OnStartCard;
        //public event Action OnDragCard;
        public event Action OnCardMouseUp;

        [NonSerialized]
        public bool IsSelected;

        private DragAndDropSystem dragAndDropSystem;
        private RectTransform rectTransform;
        private bool isBeingDragged;
        public GameObject PreviousCardInStack;
        //public GameObject NextCardInStack { get; private set; }
        public GameObject NextCardInStack;

        public bool IsBeingDragged
        {
            get => isBeingDragged;
            set
            {
                if (value != IsBeingDragged)
                    Debug.Log($"{gameObject} isBeingDragged = {value}");
                isBeingDragged = value;
            }
        }

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


        void Update()
        {
            currentState.UpdateState(this);
            // a déplacer et ca marche tolujours paaaas
            if (PreviousCardInStack != null)
            {
                //GetComponent<CardDisplay>().FollowPreviousCard(PreviousCardInStack);
            }

            if (NextCardInStack != null && IsBeingDragged)
            {
                NextCardInStack.GetComponent<CardDisplay>().FollowPreviousCard(gameObject);
            }
        }

        public void SwitchState(IState newState)
        {
            currentState.Exit(this);
            currentState = (CardBaseState)newState;
            currentState.Enter(this);
        }

        #region LinkCards
        public void SetNextCard(GameObject next)
        {
            if (next == gameObject)
            {
                Debug.LogWarning($"Un objet essaye de se référencer lui-même ({CardSO.Name}");
                return;
            }

            UnlinkNextCard();

            if (next == null)
                return;

            NextCardInStack = next;
            GetComponent<CardDisplay>().TransformCollider(true); //déplacer cette logique dans une partie UI dédiée
            next.GetComponent<CardController>().PreviousCardInStack = gameObject;
        }
        public void UnlinkNextCard()
        {
            GetComponent<CardDisplay>().TransformCollider(false);

            if (NextCardInStack == null)
                return;

            NextCardInStack.GetComponent<CardController>().PreviousCardInStack = null;
            NextCardInStack = null;
        }

        public void SetPreviousCard(GameObject previous)
        {
            if (previous == gameObject)
            {
                Debug.LogWarning($"Un objet essaye de se référencer lui-même ({CardSO.Name}");
                return;
            }

            UnlinkPreviousCard();

            if (previous == null)
                return;


            PreviousCardInStack = previous;
            PreviousCardInStack.GetComponent<CardDisplay>().TransformCollider(true); //déplacer cette logique dans une partie UI dédiée
            previous.GetComponent<CardController>().NextCardInStack = gameObject;

        }

        public void UnlinkPreviousCard()
        {
            if (PreviousCardInStack == null)
                return;

            PreviousCardInStack.GetComponent<CardDisplay>().TransformCollider(false);
            PreviousCardInStack.GetComponent<CardController>().NextCardInStack = null;
            PreviousCardInStack = null;
        }
        #endregion

        #region Draggable
        private void OnMouseDown()
        {
            Debug.Log($"Click on {CardSO.name}");
        }

        private void OnMouseDrag() // a implémenter dans le state directement ?
        {
            // TODO Conditionner le départ du drag à un minimum de mvt de la souris depuis la pos initiale pour pouvoir cliquer et afficher qqch sans que ca soit considéré comme du drag
            currentState.OnMouseDrag(this);

            // DnDSys => logic uniquement (set next/previous etc)
            // CardDisplay.MoveThis()
            // CardDisplay.MoveAttached(stackHelper.GetNextInStack())
            // (Voir si c'es tpertienent de mettre les funct du CDisplay dans l'Invoke comme le Dnd ?
        }


        //quand on relache la carte
        private void OnMouseUp()
        {
            currentState.OnMouseUp(this);
            //    Debug.Log($"Up on {CardSO.name}");
            //    transform.localScale = GlobalVariables.CardDefaultScale;
            //    IsBeingDragged = false;

            //    OnCardMouseUp?.Invoke();
            //StackHelper.UpdateCardStack(gameObject, dragAndDropSystem.GetTargetCard(), null, false);
        }
        #endregion
    }
}

