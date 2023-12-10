using Assets._Scripts.Managers;
using Assets._Scripts.ScriptableObjects;
using Assets._Scripts.StateMachines;
using Assets._Scripts.StateMachines.Cards.MovementState;
using Assets._Scripts.StateMachines.Cards.TimerState;
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
        public CardBaseMovementState currentMovementState;
        public CardIdleState IdleState = new();
        public CardMovingState MovingState = new();
        public CardFollowingState FollowingState = new();

        public CardBaseTimerState currentTimerState;
        public CardRunningState RunningState = new();
        public CardNoTimerState NoTimerState = new();
        public CardIngredientState IngredientState = new();

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

            currentMovementState = IdleState;
            currentMovementState.Enter(this);

            currentTimerState = NoTimerState;
            currentTimerState.Enter(this);
        }


        void Update()
        {
            currentMovementState.UpdateState(this);
            currentTimerState.UpdateState(this);
        }

        public void SwitchState(IState newState)
        {
            if (newState is CardBaseMovementState movementState)
            {
                currentMovementState.Exit(this);
                currentMovementState = movementState;
                currentMovementState.Enter(this);
                return;
            }

            if (newState is CardBaseTimerState timerState)
            {
                currentTimerState.Exit(this);
                currentTimerState = timerState;
                currentTimerState.Enter(this);
                return;
            }

            Debug.LogError($"{newState} is not a valid state");

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
            currentMovementState.OnMouseDrag(this);
        }


        //quand on relache la carte
        private void OnMouseUp()
        {
            currentMovementState.OnMouseUp(this);
        }
        #endregion
    }
}

