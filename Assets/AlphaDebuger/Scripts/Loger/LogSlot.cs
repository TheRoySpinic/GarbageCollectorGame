using UnityEngine;
using UnityEngine.UI;

namespace AlphaDebuger
{
    public class LogSlot : MonoBehaviour
    {
        [HideInInspector]
        public string outputLog;
        [HideInInspector]
        public string stack;
        [HideInInspector]
        public LogType type;
        [HideInInspector]
        public int count;

        [SerializeField]
        private Text message;
        [SerializeField]
        private Text countText;
        [SerializeField]
        private Image icon;

        public void SetData(string output, string stack, LogType type)
        {
            outputLog = output;
            this.stack = stack;
            this.type = type;

            SetMessageText();
            SetIcon();
            SetCount();
        }

        public void ClickAction()
        {
            LogController.instance.ShowTrace(stack);
        }

        private void SetMessageText()
        {
            if(message)
            {
                message.text = outputLog;
            }
        }

        private void SetIcon()
        {
            Color color = Color.white;
            switch(type)
            {
                case LogType.Assert:
                case LogType.Error:
                case LogType.Exception:
                    color = Color.red;
                    break;
                case LogType.Warning:
                    color = Color.yellow;
                    break;
                case LogType.Log:
                    color = Color.white;
                    break;
            }
            icon.color = color;
        }

        public void SetCount(int count = 1)
        {
            this.count = count;
            countText.text = count > 1 ? count.ToString() : "";
        }
    }
}
