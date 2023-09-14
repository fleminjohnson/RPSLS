using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPSLS
{
    public class EventServices : SingletonBehaviour<EventServices>
    {
        public event Action OnTimerElapsed;

        public void InvokeOnTimerElapsed()
        {
            OnTimerElapsed?.Invoke();
        }
    }
}