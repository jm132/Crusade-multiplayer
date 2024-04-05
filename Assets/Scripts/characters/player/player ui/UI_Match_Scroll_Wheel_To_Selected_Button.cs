using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace JM
{
    public class UI_Match_Scroll_Wheel_To_Selected_Button : MonoBehaviour
    {
        [SerializeField] GameObject currentSelected;
        [SerializeField] GameObject previouslySelected;
        [SerializeField] RectTransform currentSelectedTransform;
        
        [SerializeField] RectTransform contectPanel;
        [SerializeField] ScrollRect scrollRect;

        private void Update()
        {
            currentSelected = EventSystem.current.currentSelectedGameObject;

            if (currentSelected != null )
            {
                if (currentSelected !=  previouslySelected)
                {
                    previouslySelected = currentSelected;
                    currentSelectedTransform = currentSelected.GetComponent<RectTransform>();
                    SnapTo(currentSelectedTransform);
                }
            }
        }

        private void SnapTo(RectTransform target)
        {
            Canvas.ForceUpdateCanvases();
            Vector2 newPosition = 
                (Vector2)scrollRect.transform.InverseTransformPoint(contectPanel.position) - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);

            // only want to lock the position on the y axis (up and down)
            newPosition.x = 0;

            contectPanel.anchoredPosition = newPosition;
        }
    }
}
