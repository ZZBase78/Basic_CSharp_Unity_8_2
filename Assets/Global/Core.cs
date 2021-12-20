using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        private Player player;

        public Core()
        {
            settings = new Settings();
            gameObjectFactory = new GameObjectFactory();
            prefabLibrary = new PrefabLibrary();
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

            eventManager.endGame += EndGame;

            //Event system
            gameObjectFactory.Instantiate(prefabLibrary.eventSystem);

            //Direction light
            gameObjectFactory.Instantiate(prefabLibrary.directionalLight);

            GameObject mazeParent = gameObjectFactory.InstantiateEmpty("Maze");
            maze = new Maze(mazeParent, settings, prefabLibrary, gameObjectFactory);
            maze.Generate();
            maze.Show();

            BonusSpawner bonusSpawner = new BonusSpawner(gameObjectFactory, eventManager, settings, maze, prefabLibrary);

            InputController inputController = new InputController(eventManager, notificationController);

            player = new Player(inputController, maze, prefabLibrary, eventManager, gameObjectFactory, notificationController, settings);

            CameraController cameraController = new CameraController(player, prefabLibrary, eventManager, gameObjectFactory, notificationController);
        }

        private void EndGame()
        {
            player.Stop();
            SetCursorVisible(true);
            EndGame endGame = new EndGame(gameObjectFactory, prefabLibrary);
            endGame.GetRestartButton().onClick.AddListener(RestartLevel);
        }
        private void RestartLevel()
        {
            SceneManager.LoadScene(0);
        }
        public void OnDestroy()
        {
            eventManager.endGame -= EndGame;
            SetCursorVisible(true);
        }
    }
}
