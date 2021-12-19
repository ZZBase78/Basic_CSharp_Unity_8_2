using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class InputController : IDisposable
    {
        private bool mainController; // true - управление от клавиатуры, false - управление мышью
        private Timer upSpeedTimer;
        private float upSpeedBonusRate;
        private Timer downSpeedTimer;
        private float downSpeedBonusRate;
        private Timer swapDirectionTimer;
        private bool swapDirection;
        private EventManager eventManager;
        private NotificationController notificationController;

        private Vector3 _force;
        public Vector3 force { get { return _force; } }

        public InputController(EventManager eventManager, NotificationController notificationController)
        {
            this.eventManager = eventManager;
            this.notificationController = notificationController;
            eventManager.actionUpdate += Update;
            eventManager.playerTakeBonus += PlayerTakeBonus;
            mainController = true;
            upSpeedTimer = new Timer(eventManager);
            upSpeedBonusRate = 1f;
            downSpeedTimer = new Timer(eventManager);
            downSpeedBonusRate = 1f;
            swapDirectionTimer = new Timer(eventManager);
            swapDirection = false;
        }
        private void PlayerTakeBonus(Bonus bonus)
        {
            if (bonus.bonusType == Bonus.BonusType.UpSpeed)
            {
                float time = bonus.GetTime();
                //10 + (int)bonus.bonusType : Чтобы каждый тип бонуса был под своим идентификатором информирования
                notificationController.Add(10 + (int)bonus.bonusType, $"Бонус: увеличение скорости", time, true, true);
                upSpeedTimer.AppendTime(time, UpSpeedBonusTimeOut);
                upSpeedBonusRate = 2f;
            }else if (bonus.bonusType == Bonus.BonusType.DownSpeed)
            {
                float time = bonus.GetTime();
                //10 + (int)bonus.bonusType : Чтобы каждый тип бонуса был под своим идентификатором информирования
                notificationController.Add(10 + (int)bonus.bonusType, $"Бонус: уменьшение скорости", time, true, true);
                downSpeedTimer.AppendTime(time, DownSpeedBonusTimeOut);
                downSpeedBonusRate = 2f;
            }else if (bonus.bonusType == Bonus.BonusType.SwapDirection)
            {
                float time = bonus.GetTime();
                //10 + (int)bonus.bonusType : Чтобы каждый тип бонуса был под своим идентификатором информирования
                notificationController.Add(10 + (int) bonus.bonusType, $"Бонус: инверсия управления", time, true, true);
                swapDirectionTimer.AppendTime(time, SwapDirectionBonusTimeOut);
                swapDirection = true;
            }
        }
        private void UpSpeedBonusTimeOut()
        {
            upSpeedBonusRate = 1f;
        }
        private void DownSpeedBonusTimeOut()
        {
            downSpeedBonusRate = 1f;
        }
        private void SwapDirectionBonusTimeOut()
        {
            swapDirection = false;
        }
        private void Update()
        {
            if (mainController)
            {
                float x = Input.GetAxis("Horizontal");
                float y = Input.GetAxis("Vertical");
                _force = new Vector3(x, 0f, y);
            }
            else
            {
                float x = Input.GetAxis("Mouse X");
                float y = Input.GetAxis("Mouse Y");
                _force = new Vector3(x, 0f, y) * 5f;
            }
            _force = _force * upSpeedBonusRate / downSpeedBonusRate;
            
            if (swapDirection) _force = _force * (-1f);

            if (Input.GetKeyDown(KeyCode.Tab)) 
            {
                mainController = !mainController;
                if (mainController)
                {
                    //1 - Идентификатор сообщения изменения управления
                    notificationController.Stop(1);
                    notificationController.Add(1, "Управление с клавиатуры", 3f, false, false);
                }
                else
                {
                    notificationController.Stop(1);
                    notificationController.Add(1, "Управление мышью", 3f, false, false);
                }
            }
            
        }
        public void Dispose()
        {
            upSpeedTimer.Dispose();
            downSpeedTimer.Dispose();
            swapDirectionTimer.Dispose();
            eventManager.playerTakeBonus -= PlayerTakeBonus;
            eventManager.actionUpdate -= Update;
        }
    }
}
