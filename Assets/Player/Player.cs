using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public class Player : GObject
    {
        private const float defaultSpeed = 5f;
        private Rigidbody rigitBody;
        private float speed;
        private InputController inputController;
        private int score;
        private Maze maze;
        private PrefabLibrary prefabLibrary;
        private EventManager eventManager;
        private NotificationController notificationController;

        public Player(InputController inputController, Maze maze, PrefabLibrary prefabLibrary, EventManager eventManager, GameObjectFactory gameObjectFactory, NotificationController notificationController)
        {
            this.maze = maze;
            this.prefabLibrary = prefabLibrary;
            this.eventManager = eventManager;
            this.gameObjectFactory = gameObjectFactory;
            this.notificationController = notificationController;
            this.inputController = inputController;
            speed = defaultSpeed;
            Vector3 playerPosition = new Vector3(maze.GetWorldXFromMazeX(1), 1f, maze.GetWorldYFromMazeY(1));
            gameObject = gameObjectFactory.Instantiate(prefabLibrary.GetSystemPrefab(2), playerPosition);
            rigitBody = gameObject.GetComponent<Rigidbody>();
            eventManager.actionFixedUpdate += FixedUpdate;
            eventManager.playerTakeBonus += PlayerTakeBonus;
        }
        private void PlayerTakeBonus(Bonus bonus)
        {
            if (bonus.bonusType == Bonus.BonusType.Score)
            {
                int newscore = bonus.GetScore();
                score += newscore;
                notificationController.Add(0, $"������� {newscore}, ����� {score} ����(��)", 3f, false, false);
            }
        }
        private void FixedUpdate()
        {
            Move();
        }
        private void Move()
        {
            rigitBody.AddForce(inputController.force * speed);
        }
        public Vector3 GetPosition()
        {
            return gameObject.transform.position;
        }
        public override void Dispose()
        {
            eventManager.playerTakeBonus -= PlayerTakeBonus;
            eventManager.actionFixedUpdate -= FixedUpdate;
            base.Dispose();
        }
    }
}
