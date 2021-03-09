using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Boosts.UI
{
    public class ActiveBoostSlot : MonoBehaviour
    {
        [SerializeField]
        private Image bar = null;

        [SerializeField]
        private Image icon = null;
        
        [Header("Animation"), Space]
        [SerializeField, Range(0,1)]
        private float pulseRange = 0.2f;
        
        [SerializeField]
        private new Animator animation = null;

        private bool trigerIsSet = false;


        private float remainTime = 1;
        private float baseRemainTime = 1;

        private void Update()
        {
            if(remainTime > 0)
            {
                if(!trigerIsSet && remainTime < baseRemainTime * pulseRange)
                {
                    animation.SetTrigger("Pulse");
                    trigerIsSet = true;
                }

                remainTime -= Time.deltaTime;
                bar.fillAmount = remainTime.Remap(0, baseRemainTime, 0, 1);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetData(EBoostType boostType, float remainTime)
        {
            this.remainTime = remainTime;
            baseRemainTime = ActiveBoostsManager.instance.GetBaseRemainingTime(boostType);

            icon.sprite = ActiveBoostsManager.instance.GetBoostSprite(boostType);
        }
    }
}