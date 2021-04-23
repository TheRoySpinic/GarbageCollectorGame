using UnityEngine;

namespace Map.Generate.ParameterLogic
{
    public class BaseParameterLogic : MonoBehaviour
    {
        public virtual void ParameterAction(int parameter)
        {
            if (parameter == 0)
                return;

            switch (parameter)
            {
                case -1:
                    Transform tr = transform;
                    tr.localScale = new Vector3(tr.localScale.x * -1, tr.localScale.y, tr.localScale.z);
                    break;
            }
        }
        public virtual void ResetParameter(int lastParameter)
        {
            if (lastParameter == 0)
                return;

            switch (lastParameter)
            {
                case -1:
                    Transform tr = transform;
                    tr.localScale = new Vector3(tr.localScale.x * -1, tr.localScale.y, tr.localScale.z);
                    break;
            }
        }
    }
}