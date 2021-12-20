using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class NotificationController
    {
        private GameObject gameObject;
        private List<Notification> list;
        private GameObjectFactory gameObjectFactory;
        private PrefabLibrary prefabLibrary;
        private EventManager eventManager;

        public NotificationController(GameObjectFactory gameObjectFactory, PrefabLibrary prefabLibrary, EventManager eventManager)
        {
            this.gameObjectFactory = gameObjectFactory;
            this.prefabLibrary = prefabLibrary;
            this.eventManager = eventManager;
            list = new List<Notification>();
        }
        private void Show()
        {
            //Canvas
            gameObject = gameObjectFactory.Instantiate(prefabLibrary.canvas);
        }
        private void Hide()
        {
            gameObjectFactory.Destroy(gameObject);
        }
        public void Add(int id, string text, float time, bool showtime, bool appendTime)
        {
            if (list.Count == 0)
            {
                Show();
                eventManager.actionUpdate += Update;
            }

            bool wasAppended = false;
            foreach(Notification notification in list)
            {
                if (notification.id != 0 && notification.id == id)
                {
                    notification.AppendData(time, text, showtime, appendTime);
                    wasAppended = true;
                    break;
                }
            }
            if (!wasAppended)
            {
                Notification notification = new Notification(gameObject, id, 0, 0, text, time, showtime, gameObjectFactory, prefabLibrary, eventManager);
                notification.TimeOut += NotificationTimeOut;
                list.Insert(0, notification);
            }
        }
        public void Stop(int id)
        {
            foreach (Notification notification in list)
                if (notification.id == id) notification.Stop();
        }
        private void NotificationTimeOut(Notification notification)
        {
            list.Remove(notification);
            notification.Dispose();
            if (list.Count == 0)
            {
                eventManager.actionUpdate -= Update;
                Hide();
            }
        }
        private void Update()
        {
            float y = 0;
            foreach(Notification notification in list)
            {
                notification.SetTarget(0, y);
                y += 50;
            }
        }
        public void Dispose()
        {
            eventManager.actionUpdate -= Update;
            foreach (Notification notification in list)
            {
                notification.TimeOut -= NotificationTimeOut;
                notification.Dispose();
            }
            gameObjectFactory.Destroy(gameObject);
        }
    }
}
