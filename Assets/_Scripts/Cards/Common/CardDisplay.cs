using Assets._Scripts.ScriptableObjects;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Cards.Common
{
    /// <summary>
    /// Handle only the UI modifications of the cards
    /// </summary>
    internal class CardDisplay : MonoBehaviour
    {
        public TextMeshProUGUI NameText;
        public TextMeshProUGUI DescriptionText;
        public Image ArtworkImage;
        public TextMeshProUGUI DebugInfo;


        void Awake()
        {
            var cardController = GetComponent<CardController>();
            cardController.OnStartCard += LoadCardData;
        }

        private void Update()
        {
            DebugInfo.text = GetComponent<CardController>().currentMovementState.GetType().Name + " / " + GetComponent<CardController>().currentTimerState.GetType().Name;
            DebugInfo.text += " - sortinglayer: " + StackHelper.ComputeOrderInLayer(gameObject);
        }

        private void LoadCardData(BaseCardSO cardSO)
        {
            NameText.text = cardSO.Name;
            DescriptionText.text = cardSO.Description;
            ArtworkImage.sprite = cardSO.Artwork;
            //cardSO.Print();
        }

        public void ResetToDefaultDisplay()
        {
            transform.localScale = GlobalVariables.CardDefaultScale;
            GetComponent<Canvas>().sortingOrder = 0;
        }

        public void SnapOnCard(GameObject targetCard)
        {
            gameObject.GetComponent<Canvas>().sortingOrder = GlobalVariables.DefaultCardSortingLayer;

            if (targetCard == null)
                return;

            Debug.Log($"Dropped {gameObject.GetComponent<CardDisplay>().name} on {targetCard.GetComponent<CardDisplay>().name}");

            var position = targetCard.GetComponent<RectTransform>().anchoredPosition;
            position.y -= GlobalVariables.CardOffsetOnSnap * targetCard.GetComponent<RectTransform>().localScale.y;

            var sortingOrder = targetCard.GetComponent<Canvas>().sortingOrder;
            sortingOrder += 1;

            gameObject.GetComponent<CardDisplay>().MoveAndSort(position, sortingOrder);

        }

        public void FollowPreviousCard(GameObject previousCard)
        {
            //TODO externaliser la logique dans DnDsystem
            transform.localScale = previousCard.transform.localScale;
            GetComponent<Canvas>().sortingOrder = previousCard.GetComponent<Canvas>().sortingOrder + 1;

            var newPos = previousCard.GetComponent<RectTransform>().anchoredPosition;
            newPos.y -= GlobalVariables.CardOffsetOnSnap * previousCard.GetComponent<RectTransform>().localScale.y;
            GetComponent<RectTransform>().anchoredPosition = newPos;
        }

        public void MoveAndSort(Vector3 newPosition, int newOrder)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = newPosition;
            gameObject.GetComponent<Canvas>().sortingOrder = newOrder;
        }

        /// <summary>
        /// Switch collider between full size or reduced size
        /// </summary>
        internal void ReduceCollider()
        {
            var sizeY = GlobalVariables.CardOffsetOnSnap;
            var offsetY = GlobalVariables.SnapOffsetY;

            ApplyToCollider(sizeY, offsetY);
        }

        internal void ResetCollider()
        {
            var sizeY = GlobalVariables.CardSizeY;
            var offsetY = 0f;
            ApplyToCollider(sizeY, offsetY);
        }

        private void ApplyToCollider(float sizeY, float offsetY)
        {
            var size = GetComponent<BoxCollider2D>().size;
            size.y = sizeY;
            GetComponent<BoxCollider2D>().size = size;

            var offset = GetComponent<BoxCollider2D>().offset;
            offset.y = offsetY;
            GetComponent<BoxCollider2D>().offset = offset;
        }
    }
}
