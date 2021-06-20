using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DebugTools
{
    public class UnityEventsFunctionTools : MonoBehaviour
    {
        public void PrintToLog(string str)
        {
            Debug.Log(str);
        }
    }
}