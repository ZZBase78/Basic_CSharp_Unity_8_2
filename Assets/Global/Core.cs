using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class Core
    {
        private PrefabLibrary prefabLibrary;
        private EventManager eventManager;
        private NotificationController notificationController;
        private GameObjectFactory gameObjectFactory;
        private Settings settings;
        private Maze maze;

        public Core(World world)
        {
            settings = new Settings();
            gameObjectFactory = new GameObjectFactory();
            prefabLibrary = new PrefabLibrary(world);
            eventManager = new EventManager();
            notificationController = new NotificationController(gameObjectFactory, prefabLibrary, eventManager);
        }
        public EventManager GetEventManager()
        {
            return eventManager;
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
            gameObjectFactory.Instantiate(prefabLibrary.GetSystemPrefab(3));

            //Direction light
            gameObjectFactory.Instantiate(prefabLibrary.GetSystemPrefab(1));

            GameObject mazeParent = gameObjectFactory.InstantiateEmpty("Maze");
            maze = new Maze(mazeParent, settings.mazeWidth, settings.mazeHeight, settings, prefabLibrary, gameObjectFactory);
            maze.Generate();
            maze.Show();

            BonusSpawner bonusSpawner = new BonusSpawner(gameObjectFactory, eventManager, settings, maze, prefabLibrary);

            InputController inputController = new InputController(eventManager, notificationController);

            Player player = new Player(inputController, maze, prefabLibrary, eventManager, gameObjectFactory, notificationController);

            CameraController cameraController = new CameraController(player, prefabLibrary, eventManager, gameObjectFactory, notificationController);
        }
        public void OnDestroy()
        {
            SetCursorVisible(true);
        }
    }
}
