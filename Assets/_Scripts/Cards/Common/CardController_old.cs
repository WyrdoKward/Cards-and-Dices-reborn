using Assets._Scripts.ScriptableObjects;
using Assets._Scripts.StateMachines;
using Assets._Scripts.StateMachines.Cards;
using Assets._Scripts.Systems;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._Scripts.Cards.Common
{
    /// <summary>
    /// Handle the lifecycle of the card. This is the top level entry point.
    /// </summary>
    internal class CardController_old : MonoBehaviour, IStateContext, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        public BaseCardSO CardSO;

        // State Machine
        private CardBaseState currentState;
        internal CardIdleState IdleState = new();
        internal CardTimedState TimedState = new();
        internal CardPausedState PausedState = new();

        //Draggable
        private DragAndDropSystem_old DragAndDrop;

        public event Action<BaseCardSO> OnStartCard;
        public event Action<PointerEventData> OnBeginDragCard;
        public event Action<PointerEventData> OnDragCard;
        public event Action<PointerEventData> OnEndDragCard;
        public event Action<PointerEventData> OnDropCard;


        private Rigidbody _rigidbody;
        private float _startYPos;
        private TableController _board;

        private void Awake()
        {
            Debug.Log("Awake CardController");
            DragAndDrop = new DragAndDropSystem_old(this, GetComponent<RectTransform>());

            _board = GameObject.Find("Table").GetComponent<TableController>();
            _rigidbody = GetComponent<Rigidbody>();

            _startYPos = 0; // Better to not hardcode that one but whatever
        }

        //void Start()
        //{
        //    Debug.Log("Start CardController");

        //    OnStartCard?.Invoke(CardSO);

        //    currentState = IdleState;
        //    currentState.Enter(this);
        //}

        void Start()
        {

            Debug.Log("Start CardController");

            OnStartCard?.Invoke(CardSO);

            currentState = IdleState;
            currentState.Enter(this);
        }


        private void OnMouseDrag()
        {
            Debug.Log("OnMouseDrag");
            var newWorldPosition = new Vector3(_board.CurrentMousePosition.x, _startYPos + 1, _board.CurrentMousePosition.z);

            var difference = newWorldPosition - transform.position;

            var speed = 10 * difference;
            _rigidbody.velocity = speed;
            _rigidbody.rotation = Quaternion.Euler(new Vector3(speed.z, 0, -speed.x));
        }

        public void Update()
        {
            currentState.UpdateState(this);
        }

        public void SwitchState(IState newState)
        {
            currentState.Exit(this);
            currentState = (CardBaseState)newState;
            currentState.Enter(this);
        }

        #region Draggable
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
            OnBeginDragCard?.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnDragCard?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            OnEndDragCard?.Invoke(eventData);
        }

        /// <summary>
        /// When this card is the target
        /// </summary>
        /// <param name="eventData">The card beeing moved</param>
        public void OnDrop(PointerEventData eventData)
        {
            // essayer avec des cartes en 3D ?
            //https://www.youtube.com/watch?v=h6y7QtDNfpA&t=37s

            var moved = eventData.pointerDrag.GetComponent<CardController_old>().CardSO.Name;
            var target = eventData.pointerEnter.transform.parent.gameObject.GetComponent<CardController_old>().CardSO.Name;
            Debug.Log($"Dropping {moved} on {eventData.pointerEnter.name} ({target})");

            // Get the object that the pointer is currently over
            var pointerObject = eventData.pointerCurrentRaycast.gameObject;

            // Check if the pointer is over this object
            if (pointerObject.transform.IsChildOf(transform))
                return;

            Debug.Log("OnDrop");
            OnDropCard?.Invoke(eventData);
            //Buisiness on snapping
            //SnapOnIt(eventData.pointerDrag);
        }
        #endregion

    }
}
