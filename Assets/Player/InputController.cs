using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class InputController : IDisposable
    {
        private bool mainController; // true - ���������� �� ����������, false - ���������� �����
        private Timer upSpeedTimer;
        private float upSpeedBonusRate;
        private Timer downSpeedTimer;
        private float downSpeedBonusRate;
        private Timer swapDirectionTimer;
        private bool swapDirection;

        private Vector3 _force;
        public Vector3 force { get { return _force; } }

        public InputController()
        {
            EventManager.actionUpdate += Update;
            EventManager.playerTakeBonus += PlayerTakeBonus;
            mainController = true;
            upSpeedTimer = new Timer();
            upSpeedBonusRate = 1f;
            downSpeedTimer = new Timer();
            downSpeedBonusRate = 1f;
            swapDirectionTimer = new Timer();
            swapDirection = false;
        }
        private void PlayerTakeBonus(Bonus bonus)
        {
            if (bonus.bonusType == Bonus.BonusType.UpSpeed)
            {
                float time = bonus.GetTime();
                //10 + (int)bonus.bonusType : ����� ������ ��� ������ ��� ��� ����� ��������������� ��������������
                NotificationController.Add(10 + (int)bonus.bonusType, $"�����: ���������� ��������", time, true, true);
                upSpeedTimer.AppendTime(time, UpSpeedBonusTimeOut);
                upSpeedBonusRate = 2f;
            }else if (bonus.bonusType == Bonus.BonusType.DownSpeed)
            {
                float time = bonus.GetTime();
                //10 + (int)bonus.bonusType : ����� ������ ��� ������ ��� ��� ����� ��������������� ��������������
                NotificationController.Add(10 + (int)bonus.bonusType, $"�����: ���������� ��������", time, true, true);
                downSpeedTimer.AppendTime(time, DownSpeedBonusTimeOut);
                downSpeedBonusRate = 2f;
            }else if (bonus.bonusType == Bonus.BonusType.SwapDirection)
            {
                float time = bonus.GetTime();
                //10 + (int)bonus.bonusType : ����� ������ ��� ������ ��� ��� ����� ��������������� ��������������
                NotificationController.Add(10 + (int) bonus.bonusType, $"�����: �������� ����������", time, true, true);
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
                    //1 - ������������� ��������� ��������� ����������
                    NotificationController.Stop(1);
                    NotificationController.Add(1, "���������� � ����������", 3f, false, false);
                }
                else
                {
                    NotificationController.Stop(1);
                    NotificationController.Add(1, "���������� �����", 3f, false, false);
                }
            }
            
        }
        public void Dispose()
        {
            upSpeedTimer.Dispose();
            downSpeedTimer.Dispose();
            swapDirectionTimer.Dispose();
            EventManager.playerTakeBonus -= PlayerTakeBonus;
            EventManager.actionUpdate -= Update;
        }
    }
}
