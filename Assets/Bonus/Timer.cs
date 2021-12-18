using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class Timer : IDisposable
    {
        private float _time;
        private Action action;
        public float time { get { return _time; } }
        
        public Timer()
        {
            _time = 0;
            action = delegate { };
        }
        public void AppendTime(float appendTime, Action action)
        {
            _time += appendTime;
            this.action = action;
            EventManager.actionUpdate -= Update;
            EventManager.actionUpdate += Update;
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
                EventManager.actionUpdate -= Update;
            }
        }
        public void Dispose()
        {
            EventManager.actionUpdate -= Update;
        }
    }
}
