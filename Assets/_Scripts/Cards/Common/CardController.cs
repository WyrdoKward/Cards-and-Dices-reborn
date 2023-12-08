using Assets._Scripts.Managers;
using Assets._Scripts.ScriptableObjects;
using Assets._Scripts.StateMachines;
using Assets._Scripts.StateMachines.Cards;
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
        public CardBaseState currentState;
        public CardIdleState IdleState = new();
        public CardMovingState MovingState = new();
        public CardFollowingState FollowingState = new();
        public CardRunningState RunningState = new();
        //public CardPausedState PausedState = new();

        public event Action<BaseCardSO> OnStartCard;

        public GameObject PreviousCardInStack;
        public GameObject NextCardInStack;


        private void Awake()
        {
            GameObject.Find("CardManager").GetComponent<CardManager>().RegisterCardToGlobalList(gameObject);
            CardSO.InitializedCardWithScriptableObject(gameObject);
        }

        void Start()
        {
            OnStartCard?.Invoke(CardSO);

            currentState = IdleState;
            currentState.Enter(this);
        }


        void Update()
        {
            currentState.UpdateState(this);
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
            GetComponent<CardDisplay>().ReduceCollider(); //déplacer cette logique dans une partie UI dédiée
            next.GetComponent<CardController>().PreviousCardInStack = gameObject;
        }
        public void UnlinkNextCard()
        {
            GetComponent<CardDisplay>().ResetCollider();

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
            PreviousCardInStack.GetComponent<CardDisplay>().ReduceCollider(); //déplacer cette logique dans une partie UI dédiée
            previous.GetComponent<CardController>().NextCardInStack = gameObject;

        }

        public void UnlinkPreviousCard()
        {
            if (PreviousCardInStack == null)
                return;

            PreviousCardInStack.GetComponent<CardDisplay>().ResetCollider();
            PreviousCardInStack.GetComponent<CardController>().NextCardInStack = null;
            PreviousCardInStack = null;
        }
        #endregion

        #region Draggable
        private void OnMouseDown()
        {
            Debug.Log($"Click on {CardSO.name}");
        }

        private void OnMouseDrag()
        {
            // TODO Conditionner le départ du drag à un minimum de mvt de la souris depuis la pos initiale pour pouvoir cliquer et afficher qqch sans que ca soit considéré comme du drag
            currentState.OnMouseDrag(this);
        }


        //quand on relache la carte
        private void OnMouseUp()
        {
            currentState.OnMouseUp(this);
        }
        #endregion
    }
}

