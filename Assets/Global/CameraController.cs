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
        public CameraController(Player player)
        {
            moveSpeed = 2f;
            this.player = player;
            gameObject = GameObjectFactory.Instantiate(PrefabLibrary.GetSystemPrefab(0));
            offset = defaultOffset;
            EventManager.actionLateUpdate += LateUpdate;
            upCameraDistantionTimer = new Timer();
            upCameraDistantion = 0f;
            EventManager.playerTakeBonus += PlayerTakeBonus;
        }
        private void PlayerTakeBonus(Bonus bonus)
        {
            if (bonus.bonusType == Bonus.BonusType.UpCameraDistantion)
            {
                float time = bonus.GetTime();
                //10 + (int)bonus.bonusType : Чтобы каждый тип бонуса был под своим идентификатором информирования
                NotificationController.Add(10 + (int)bonus.bonusType, $"Бонус: увеличение обзора", time, true, true);
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
            EventManager.playerTakeBonus -= PlayerTakeBonus;
            upCameraDistantionTimer.Dispose();
            EventManager.actionLateUpdate -= LateUpdate;
            base.Dispose();
        }
    }
}
