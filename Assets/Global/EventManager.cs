using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public static class EventManager
    {
        public static event Action actionUpdate = delegate { };
        public static event Action actionLateUpdate = delegate { };
        public static event Action actionFixedUpdate = delegate { };

        public static event Action<Bonus> playerTakeBonus;

        public static void Init()
        {
            actionUpdate = delegate { };
            actionLateUpdate = delegate { };
            actionFixedUpdate = delegate { };

            playerTakeBonus = delegate (Bonus bonus) { };
        }

        public static void PlayerTakeBonus(Bonus bonus)
        {
            playerTakeBonus(bonus);
        }

        public static void Update()
        {
            actionUpdate();
        }
        public static void LateUpdate()
        {
            actionLateUpdate();
        }
        public static void FixedUpdate()
        {
            actionFixedUpdate();
        }
    }
}
