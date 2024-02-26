using Assets._Scripts.Cards.Logic;
using Assets._Scripts.Managers;
using Assets._Scripts.ScriptableObjects;
using Assets._Scripts.StateMachines;
using Assets._Scripts.StateMachines.Cards.MovementState;
using Assets._Scripts.StateMachines.Cards.TimerState;
using Assets._Scripts.Utilities;
using Assets._Scripts.Utilities.Enums;
using System;
using UnityEngine;

namespace Assets._Scripts.Cards.Common
{
    public class CardController : MonoBehaviour, IStateContext
    {
        //[SerializeField]
        //private Canvas canvas;
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

        // Context elements
        public event Action<BaseCardSO> OnStartCard;

        public GameObject PreviousCardInStack;
        public GameObject NextCardInStack;

        public Vector2 LastPosition;
        public Vector2 MousePosOnMouseDown;
        public Vector2 MouseDelta;

        private void Awake()
        {
            GameObject.Find("CardManager").GetComponent<CardProvider>().RegisterCardToGlobalList(gameObject);
            CardSO.InitializedCardWithScriptableObject(gameObject);
            LastPosition = GetComponent<RectTransform>().position;
        }

        void Start()
        {
            OnStartCard?.Invoke(CardSO);

            //Movement state
            currentMovementState = IdleState;
            currentMovementState.Enter(this);

            //Timer state
            if (GetComponent<CardLogic>().CardType == ECardType.Threat)
            {
                RunningState.TimerDuration = ((ThreatCardSO)CardSO).ExecuteThreatDuration;
                RunningState.EndTimerAction = GetComponent<ThreatLogic>().ExecuteThreatAfterTimer;
                currentTimerState = RunningState;
            }
            else
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
            PreviousCardInStack.GetComponent<CardDisplay>().ReduceCollider();
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
            MousePosOnMouseDown = InputHelper.GetCursorPositionInWorld();
        }

        private void OnMouseDrag()
        {
            // Conditionner le départ du drag à un minimum de mvt de la souris depuis la pos initiale pour pouvoir cliquer et afficher qqch sans que ca soit considéré comme du drag
            if (!MovingState.MovingByMouse && Vector2.Distance(InputHelper.GetCursorPositionInWorld(), MousePosOnMouseDown) > GlobalVariables.DragThresholdDelta)
            {
                //calculer un delta entre la pos de la souris et du centre de la carte pour éviter un gros décalage si on clique dans un coin
                MouseDelta = InputHelper.GetCursorPositionInWorld() - (Vector2)transform.position;
                MovingState.MovingByMouse = true;
            }

            if (MovingState.MovingByMouse)
                currentMovementState.OnMouseDrag(this);
        }


        //quand on relache la carte
        private void OnMouseUp()
        {
            if (!MovingState.MovingByMouse)
                Debug.Log($"Click on {CardSO.name}"); //Afficher la popup d'infos ici

            MousePosOnMouseDown = Vector2.zero;
            currentMovementState.OnMouseUp(this);
        }
        #endregion
    }
}

