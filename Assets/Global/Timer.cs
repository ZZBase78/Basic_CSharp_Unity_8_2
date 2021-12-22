using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class Timer : IDisposable
    {
        private Action action;

        private float _time;
        public float time { get { return _time; } }
        private EventManager eventManager;
        
        public Timer(EventManager eventManager)
        {
            this.eventManager = eventManager;
            _time = 0;
            action = delegate { };
        }
        public void AppendTime(float appendTime, Action action)
        {
            _time += appendTime;
            this.action = action;
            eventManager.actionUpdate -= Update;
            eventManager.actionUpdate += Update;
        }
        private void Update()
        {
            if (_time > 0)
            {
                _time -= Time.deltaTime;
            }
            else
            {
                _time = 0;
                // Выполняем связанное действие и отписываемся от update
                action?.Invoke(); 
                eventManager.actionUpdate -= Update;
            }
        }
        public void Dispose()
        {
            eventManager.actionUpdate -= Update;
        }
    }
}
