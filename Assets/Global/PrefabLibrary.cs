using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class PrefabLibrary
    {
        private GameObject _bonus;
        private GameObject _mazeFloor;
        private GameObject _mazeWall;
        private GameObject _mazeWallCross;
        private GameObject _canvas;
        private GameObject _notificationPanel;
        private GameObject _player;
        private GameObject _directionalLight;
        private GameObject _eventSystem;
        private GameObject _mainCamera;
        private GameObject _endGame;

        public GameObject bonus 
        {
            get
            {
                if (_bonus == null) _bonus = Resources.Load<GameObject>("Bonus/Bonus");
                return _bonus;
            }
        }
        public GameObject mazeFloor
        {
            get
            {
                if (_mazeFloor == null) _mazeFloor = Resources.Load<GameObject>("Maze/Floor");
                return _mazeFloor;
            }
        }
        public GameObject mazeWall
        {
            get
            {
                if (_mazeWall == null) _mazeWall = Resources.Load<GameObject>("Maze/Wall");
                return _mazeWall;
            }
        }
        public GameObject mazeWallCross
        {
            get
            {
                if (_mazeWallCross == null) _mazeWallCross = Resources.Load<GameObject>("Maze/WallCross");
                return _mazeWallCross;
            }
        }
        public GameObject canvas
        {
            get
            {
                if (_canvas == null) _canvas = Resources.Load<GameObject>("Notification/Canvas");
                return _canvas;
            }
        }
        public GameObject notificationPanel
        {
            get
            {
                if (_notificationPanel == null) _notificationPanel = Resources.Load<GameObject>("Notification/NotificationPanel");
                return _notificationPanel;
            }
        }
        public GameObject player
        {
            get
            {
                if (_player == null) _player = Resources.Load<GameObject>("Player/Player");
                return _player;
            }
        }
        public GameObject directionalLight
        {
            get
            {
                if (_directionalLight == null) _directionalLight = Resources.Load<GameObject>("System/DirectionalLight");
                return _directionalLight;
            }
        }
        public GameObject eventSystem
        {
            get
            {
                if (_eventSystem == null) _eventSystem = Resources.Load<GameObject>("System/EventSystem");
                return _eventSystem;
            }
        }
        public GameObject mainCamera
        {
            get
            {
                if (_mainCamera == null) _mainCamera = Resources.Load<GameObject>("System/MainCamera");
                return _mainCamera;
            }
        }
        public GameObject endGame
        {
            get
            {
                if (_endGame == null) _endGame = Resources.Load<GameObject>("EndGame/EndGame");
                return _endGame;
            }
        }
    }
}
