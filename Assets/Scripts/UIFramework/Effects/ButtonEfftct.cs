using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIFramework.Effects {
    public class ButtonEfftct : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        public void OnPointerEnter(PointerEventData eventData) {
            transform.DOScale(Vector3.one * 1.2f, 0.5f);
        }

        public void OnPointerExit(PointerEventData eventData) {
            transform.DOScale(Vector3.one * 1f, 0.5f);
        }
    }
}