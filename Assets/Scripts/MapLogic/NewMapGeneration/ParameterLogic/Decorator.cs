using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Generate.ParameterLogic
{
    public class Decorator : BaseParameterLogic
    {
        [SerializeField]
        private Decor[] decors = new Decor[0];

        public override void ParameterAction(int parameter)
        {
            base.ParameterAction(parameter);

            if (parameter == 0)
                return;

            Decor dec = parameter < decors.Length && parameter >= 0 ? decors[parameter] : null;

            if (dec == null)
                return;

            dec.item.gameObject.SetActive(true);
            dec.item.localPosition = new Vector3(UnityEngine.Random.Range(dec.shiftLeft, dec.shiftRight), dec.item.localPosition.y, dec.item.localPosition.z);
            dec.createEvents?.Invoke();
        }

        public override void ResetParameter(int lastParameter)
        {
            base.ResetParameter(lastParameter);

            if (lastParameter == 0)
                return;

            Decor dec = lastParameter < decors.Length && lastParameter >= 0 ? decors[lastParameter] : null;

            if (dec == null)
                return;

            dec.item.gameObject.SetActive(false);
        }

        [Serializable]
        private class Decor
        {
            public Transform item = null;
            [Range(-5f, 0)]
            public float shiftLeft = 0;
            [Range(0, 5f)]
            public float shiftRight = 0;
            public UnityEvent createEvents = new UnityEvent();
        }
    }
}