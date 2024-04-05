using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JM
{
    public class UI_StatBar : MonoBehaviour
    {
        private Slider slider;
        private RectTransform rectTransform;

        [Header("Bar Options")]
        [SerializeField] protected bool scaleBerLengthWithStats = true;
        [SerializeField] protected float widthScaleMultuplier = 1;
        // secondery bar behind may ber for polish effect (yollow bar that shows how much an action/damage takes away from current stat)


        protected virtual void Awake()
        {
            slider = GetComponent<Slider>();
            rectTransform = GetComponent<RectTransform>();
        }

        public virtual void SetStat(int newValue)
        {
            slider.value = newValue;
        }

        public virtual void SetMaxStats(int maxValue)
        {
            slider.maxValue = maxValue;
            slider.value = maxValue;

            if (scaleBerLengthWithStats)
            {
                rectTransform.sizeDelta = new Vector2(maxValue * widthScaleMultuplier, rectTransform.sizeDelta.y);
                
                // Resets the position of the bars based on their layout Group's settings
                PlayerUIManager.instance.playerUIHudManager.RefreshHUD();
            }
        }
    }
}
