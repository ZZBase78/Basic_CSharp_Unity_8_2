using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public static class NotificationController
    {
        private static GameObject gameObject;
        private static List<Notification> list;

        public static void Init()
        {
            list = new List<Notification>();
        }
        private static void Show()
        {
            //Canvas
            gameObject = GameObjectFactory.Instantiate(PrefabLibrary.GetSystemPrefab(4));
        }
        private static void Hide()
        {
            GameObjectFactory.Destroy(gameObject);
        }
        public static void Add(int id, string text, float time, bool showtime, bool appendTime)
        {
            if (list.Count == 0)
            {
                Show();
                EventManager.actionUpdate += Update;
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
                Notification notification = new Notification(gameObject, id, 0, 0, text, time, showtime);
                notification.TimeOut += NotificationTimeOut;
                list.Insert(0, notification);
            }
        }
        public static void Stop(int id)
        {
            foreach (Notification notification in list)
                if (notification.id == id) notification.Stop();
        }
        private static void NotificationTimeOut(Notification notification)
        {
            list.Remove(notification);
            notification.Dispose();
            if (list.Count == 0)
            {
                EventManager.actionUpdate -= Update;
                Hide();
            }
        }
        private static void Update()
        {
            float y = 0;
            foreach(Notification notification in list)
            {
                notification.SetTarget(0, y);
                y += 50;
            }
        }
        public static void Dispose()
        {
            EventManager.actionUpdate -= Update;
            foreach (Notification notification in list)
            {
                notification.TimeOut -= NotificationTimeOut;
                notification.Dispose();
            }
            GameObjectFactory.Destroy(gameObject);
        }
    }
}
