using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class EventManager
    {
        public event Action actionUpdate = delegate { };
        public event Action actionLateUpdate = delegate { };
        public event Action actionFixedUpdate = delegate { };

        public event Action<Bonus> playerTakeBonus;

        public EventManager()
        {
            actionUpdate = delegate { };
            actionLateUpdate = delegate { };
            actionFixedUpdate = delegate { };

            playerTakeBonus = delegate (Bonus bonus) { };
        }

        public void PlayerTakeBonus(Bonus bonus)
        {
            playerTakeBonus(bonus);
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
