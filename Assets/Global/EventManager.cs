using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class EventManager
    {
        public event Action actionUpdate;
        public event Action actionLateUpdate;
        public event Action actionFixedUpdate;

        public event Action endGame;

        public event Action<Bonus> playerTakeBonus;

        public EventManager()
        {
            actionUpdate = delegate { };
            actionLateUpdate = delegate { };
            actionFixedUpdate = delegate { };

            endGame = delegate { };

            playerTakeBonus = delegate (Bonus bonus) { };
        }

        public void PlayerTakeBonus(Bonus bonus)
        {
            playerTakeBonus(bonus);
        }

        public void EndGame()
        {
            endGame();
        }

        public void Update()
        {
            actionUpdate();
        }
        public void LateUpdate()
        {
            actionLateUpdate();
        }
        public void FixedUpdate()
        {
            actionFixedUpdate();
        }
    }
}
