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


        void Awake()
        {
            var cardController = GetComponent<CardController>();
            cardController.OnStartCard += LoadCardData;
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

        public void FollowPreviousCard(GameObject previousCard)
        {
            Debug.Log($"{gameObject} following {previousCard}");
            //TODO externaliser la logique dans DnDsystem
            transform.localScale = previousCard.transform.localScale;
            GetComponent<Canvas>().sortingOrder = previousCard.GetComponent<Canvas>().sortingOrder + 1;

            var isMoving = previousCard.GetComponent<CardController>().IsBeingDragged;
            GetComponent<CardController>().IsBeingDragged = isMoving;

            if (!isMoving)
            {
                //Debug.LogWarning($"NOT MOVING ({gameObject})");
                transform.localScale = GlobalVariables.CardDefaultScale;
                return;
            }


            //Debug.Log($"MOVING ({gameObject})");

            GetComponent<CardController>().IsBeingDragged = true;
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
        /// <param name="reducing"></param>
        internal void TransformCollider(bool reducing = false)
        {
            var sizeY = 140f;
            var offsetY = 0f;

            if (reducing)
            {
                sizeY = GlobalVariables.CardOffsetOnSnap;
                offsetY = 62.5f; ;
            }


            var size = GetComponent<BoxCollider2D>().size;
            size.y = sizeY;
            GetComponent<BoxCollider2D>().size = size;

            var offset = GetComponent<BoxCollider2D>().offset;
            offset.y = offsetY;
            GetComponent<BoxCollider2D>().offset = offset;
        }
    }
}
