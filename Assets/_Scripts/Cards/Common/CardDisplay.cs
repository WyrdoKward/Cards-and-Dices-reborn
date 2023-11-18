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
            transform.localScale = new Vector3(1, 1, 1);
            GetComponent<Canvas>().sortingOrder = 0;
        }

        public void FollowPreviousCard(GameObject previousCard)
        {
            transform.localScale = previousCard.transform.localScale;
            GetComponent<Canvas>().sortingOrder = previousCard.GetComponent<Canvas>().sortingOrder + 1;

            var isMoving = previousCard.GetComponent<CardController>().IsBeingDragged;
            GetComponent<CardController>().IsBeingDragged = isMoving;

            if (!isMoving)
                return;


            GetComponent<CardController>().IsBeingDragged = true;
            var newPos = previousCard.GetComponent<RectTransform>().anchoredPosition;
            newPos.y -= GlobalVariables.CardOffsetOnSnap * previousCard.GetComponent<RectTransform>().localScale.y;
            GetComponent<RectTransform>().anchoredPosition = newPos;
        }


    }
}
