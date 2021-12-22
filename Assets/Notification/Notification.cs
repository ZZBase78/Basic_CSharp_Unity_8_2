using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZZBase.Maze
{
    public sealed class Notification : GObject
    {
        public float targetX;
        public float targetY;
        private string text;
        private float time;
        private int displayedTime;
        private bool showtime;
        private Text textUI;
        private float targetScale;
        private PrefabLibrary prefabLibrary;
        private EventManager eventManager;

        private int _id;
        public int id { get { return _id; } }

        public event Action<Notification> TimeOut = delegate { };

        private void Show()
        {
            //NotificationPanel
            gameObject = gameObjectFactory.Instantiate(prefabLibrary.notificationPanel);
        }
        private void Hide()
        {
            gameObjectFactory.Destroy(gameObject);
        }
        public Notification(GameObject parent, int id, float x, float y, string text, float time, bool showtime, GameObjectFactory gameObjectFactory, PrefabLibrary prefabLibrary, EventManager eventManager)
        {
            this.gameObjectFactory = gameObjectFactory;
            this.prefabLibrary = prefabLibrary;
            this.eventManager = eventManager;

            Show();
            gameObject.transform.SetParent(parent.transform);
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            targetScale = 1;
            textUI = gameObject.GetComponentInChildren<Text>();
            if (textUI == null) throw new MyException("Notification.cs (constructor): Компонент Text не найден");
            _id = id;
            targetX = x;
            targetY = y;
            this.text = text;
            this.time = time;
            this.showtime = showtime;
            eventManager.actionUpdate += Update;
            UpdateTextUI();
        }
        public void AppendData(float time, string text, bool showtime, bool appendTime)
        {
            if (appendTime) this.time += time; else this.time = time;
            this.text = text;
            this.showtime = showtime;
            UpdateTextUI();
        }
        public void SetTarget(float x, float y)
        {
            targetX = x;
            targetY = y;
        }
        public float ChangeFloat(float start, float end, float maxChange)
        {
            float change = Mathf.Abs(start - end);
            if (change > maxChange) change = maxChange;
            float nextValue;
            if (start > end) nextValue = start - change; else nextValue = start + change;
            return nextValue;
        }
        private void Scaling()
        {
            float scale = ChangeFloat(gameObject.transform.localScale.x, targetScale, Time.deltaTime * 3f);
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
            if (scale == 0) TimeOut(this);
        }
        private void Moving()
        {
            Vector3 targetPosition = new Vector3(targetX, targetY, 0);
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, Time.deltaTime * 150f);
        }
        private void TimeControl()
        {
            time -= Time.deltaTime;
            if (time <= 0) time = 0;
                
            if (time == 0)
            {
                _id = 0; // очистка идентификатора, чтобы не принимал обновления счетчика таймера
                targetScale = 0;
            }
        }
        public void Stop()
        {
            _id = 0;
            time = 0;
        }
        private void UpdateTextUI()
        {
            if (showtime)
            {
                displayedTime = Mathf.RoundToInt(time);
                textUI.text = $"{text} ({displayedTime} сек)";
            }
            else
            {
                displayedTime = 0;
                textUI.text = $"{text}";
            }

        }
        private void Update()
        {
            Moving();
            TimeControl();

            if (showtime)
            {
                int newTime = Mathf.RoundToInt(time);
                if (newTime != displayedTime)
                {
                    UpdateTextUI();
                }
            }
            Scaling();
        }
        public override void Dispose()
        {
            eventManager.actionUpdate -= Update;
            base.Dispose();
        }
    }
}
