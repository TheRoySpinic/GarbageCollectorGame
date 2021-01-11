using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AlphaDebuger
{
    public class LogController : MonoBehaviour
    {
        public static LogController instance;

        public delegate void D_MessageResive();
        public static event D_MessageResive E_MesageResive;

        [SerializeField]
        private GameObject logContent;

        [SerializeField]
        private Text traceText;

        [SerializeField]
        private Text LogCountText;
        [SerializeField]
        private Text WarningCountText;
        [SerializeField]
        private Text ErrorCountText;

        [SerializeField]
        private GameObject slotPrefab;

        private HashSet<LogItem> items = new HashSet<LogItem>();
        private HashSet<LogSlot> slots = new HashSet<LogSlot>();

        public bool showLogType { get; private set; } = true;
        public bool showWarningType { get; private set; } = true;
        public bool showErrorType { get; private set; } = true;

        public bool colapse { get; private set; } = false;
        

        private void Awake()
        {
            instance = this;
            ClearLog();
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        public void Open()
        {
            FillLog();
        }

        public void FilterClick(LogType type)
        {
            switch(type)
            {
                case LogType.Error:
                    showErrorType = !showErrorType;
                    break;
                case LogType.Warning:
                    showWarningType = !showWarningType;
                    break;
                case LogType.Log:
                    showLogType = !showLogType;
                    break;
            }

            FillLog();
        }

        public bool GetFilterState(LogType type)
        {
            switch (type)
            {
                case LogType.Error:
                    return showErrorType;

                case LogType.Warning:
                    return showWarningType;

                case LogType.Log:
                    return showLogType;
            }
            return false;
        }

        public void ColapseClick()
        {
            colapse = !colapse;

            FillLog();
        }

        public void ClearClick()
        {
            items.Clear();
            FillLog();
        }

        public void FillLog()
        {
            ClearLog();

            foreach(LogItem item in items)
            {
                AddMessage(item);
            }

            UpdateCount();
        }

        public void ShowTrace(string trace)
        {
            traceText.text = trace;
        }

        private void UpdateCount()
        {
            LogCountText.text = CountByType(LogType.Log).ToString();
            WarningCountText.text = CountByType(LogType.Warning).ToString();
            ErrorCountText.text = CountByType(LogType.Error).ToString();
        }

        private int CountByType(LogType type)
        {
            int countLog = 0;
            int countError = 0;
            int countWarning = 0;

            foreach(LogItem item in items)
            {
                switch (item.type)
                {
                    case LogType.Assert:
                    case LogType.Error:
                    case LogType.Exception:
                        countError++;
                        break;
                    case LogType.Warning:
                        countWarning++;
                        break;
                    case LogType.Log:
                        countLog++;
                        break;
                }
            }

            switch (type)
            {
                case LogType.Assert:
                case LogType.Error:
                case LogType.Exception:
                    return countError;

                case LogType.Warning:
                    return countWarning;

                case LogType.Log:
                    return countLog;
            }

            return -1;
        }

        private void ClearLog()
        {
            traceText.text = "";

            slots.Clear();

            while (logContent.transform.childCount > 0)
            {
                DestroyImmediate(logContent.transform.GetChild(0).gameObject);
            }
        }

        private void AddMessage(LogItem item)
        {
            if(colapse)
            {
                LogSlot s = GetSlot(item);
                if(s)
                {
                    s.SetCount(s.count + 1);
                    return;
                }
            }

            LogSlot slot = null;

            switch (item.type)
            {
                case LogType.Assert:
                case LogType.Error:
                case LogType.Exception:

                    if (showErrorType)
                    {
                        slot = Instantiate(slotPrefab, logContent.transform).GetComponent<LogSlot>();
                        slots.Add(slot);
                    }
                    break;

                case LogType.Warning:

                    if (showWarningType)
                    {
                        slot = Instantiate(slotPrefab, logContent.transform).GetComponent<LogSlot>();
                        slots.Add(slot);
                    }
                    break;

                case LogType.Log:

                    if (showLogType)
                    {
                        slot = Instantiate(slotPrefab, logContent.transform).GetComponent<LogSlot>();
                        slots.Add(slot);
                    }
                    break;
            }
            
            slot?.SetData(item.output, item.stack, item.type);
        }

        private LogSlot GetSlot(LogItem item)
        {
            foreach(LogSlot slot in slots)
            {
                if(slot.stack.Equals(item.stack))
                {
                    return slot;
                }
            }

            return null;
        }

        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            LogItem item = new LogItem(logString, stackTrace, type);
            items.Add(item);
            AddMessage(item);
            UpdateCount();
            E_MesageResive?.Invoke();
        }

        class LogItem
        {
            public string output;
            public string stack;
            public LogType type;

            public LogItem() { }

            public LogItem(string logString, string stackTrace, LogType type)
            {
                output = logString;
                stack = logString + "\n\n" + stackTrace;
                this.type = type;
            }
        }
    }
}
