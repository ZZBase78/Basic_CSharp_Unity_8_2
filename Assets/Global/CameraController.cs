using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class CameraController : GObject
    {
        private Vector3 defaultOffset = new Vector3(0, 10, 0);
        private float moveSpeed;
        private Player player;
        private Vector3 offset;
        private Timer upCameraDistantionTimer;
        private float upCameraDistantion;
        private PrefabLibrary prefabLibrary;
        private EventManager eventManager;
        private NotificationController notificationController;

        public CameraController(Player player, PrefabLibrary prefabLibrary, EventManager eventManager, GameObjectFactory gameObjectFactory, NotificationController notificationController)
        {
            this.prefabLibrary = prefabLibrary;
            this.eventManager = eventManager;
            this.gameObjectFactory = gameObjectFactory;
            this.notificationController = notificationController;
            moveSpeed = 2f;
            this.player = player;
            gameObject = gameObjectFactory.Instantiate(prefabLibrary.mainCamera);
            offset = defaultOffset;
            eventManager.actionLateUpdate += LateUpdate;
            upCameraDistantionTimer = new Timer(eventManager);
            upCameraDistantion = 0f;
            eventManager.playerTakeBonus += PlayerTakeBonus;
        }
        private void PlayerTakeBonus(BonusData bonus)
        {
            if (bonus.bonusType == BonusData.BonusType.UpCameraDistantion)
            {
                float time = bonus.GetTime();
                //10 + (int)bonus.bonusType : Чтобы каждый тип бонуса был под своим идентификатором информирования
                notificationController.Add(10 + (int)bonus.bonusType, $"Бонус: увеличение обзора", time, true, true);
                upCameraDistantionTimer.AppendTime(time, upCameraDistantionBonusTimeOut);
                upCameraDistantion = 10f;
            }
        }
        private void upCameraDistantionBonusTimeOut()
        {
            upCameraDistantion = 0f;
        }
        private void LateUpdate()
        {
            if (player != null)
            {
                Vector3 targetPosition;
                targetPosition = player.GetPosition() + offset + Vector3.up * upCameraDistantion;

                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPosition, Time.deltaTime * moveSpeed);
            }
        }

        public override void Dispose()
        {
            eventManager.playerTakeBonus -= PlayerTakeBonus;
            upCameraDistantionTimer.Dispose();
            eventManager.actionLateUpdate -= LateUpdate;
            base.Dispose();
        }
    }
}
