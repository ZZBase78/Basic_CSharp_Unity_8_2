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

        public Player(InputController inputController)
        {
            this.inputController = inputController;
            speed = defaultSpeed;
            Vector3 playerPosition = new Vector3(Maze.GetWorldXFromMazeX(1), 1f, Maze.GetWorldYFromMazeY(1));
            gameObject = GameObjectFactory.Instantiate(PrefabLibrary.GetSystemPrefab(2), playerPosition);
            rigitBody = gameObject.GetComponent<Rigidbody>();
            EventManager.actionFixedUpdate += FixedUpdate;
            EventManager.playerTakeBonus += PlayerTakeBonus;
        }
        private void PlayerTakeBonus(Bonus bonus)
        {
            if (bonus.bonusType == Bonus.BonusType.Score)
            {
                int newscore = bonus.GetScore();
                score += newscore;
                NotificationController.Add(0, $"Собрано {newscore}, всего {score} очка(ов)", 3f, false, false);
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
            EventManager.playerTakeBonus -= PlayerTakeBonus;
            EventManager.actionFixedUpdate -= FixedUpdate;
            base.Dispose();
        }
    }
}
