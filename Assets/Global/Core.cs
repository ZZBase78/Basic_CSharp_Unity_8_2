using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public class Core
    {
        public Core(World world)
        {
            PrefabLibrary.Init(world);
            EventManager.Init();
            NotificationController.Init();
            GameObjectFactory.Init();
        }
        public void SetCursorVisible(bool value)
        {
            if (value)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        public void Start()
        {
            SetCursorVisible(false);

            //Event system
            GameObjectFactory.Instantiate(PrefabLibrary.GetSystemPrefab(3));

            //Direction light
            GameObjectFactory.Instantiate(PrefabLibrary.GetSystemPrefab(1));

            GameObject mazeParent = GameObjectFactory.InstantiateEmpty("Maze");
            Maze maze = new Maze(mazeParent, Settings.mazeWidth, Settings.mazeHeight);
            maze.Generate();
            maze.Show();

            BonusSpawner bonusSpawner = new BonusSpawner();

            InputController inputController = new InputController();

            Player player = new Player(inputController);

            CameraController cameraController = new CameraController(player);
        }
        public void OnDestroy()
        {
            SetCursorVisible(true);
        }
    }
}
