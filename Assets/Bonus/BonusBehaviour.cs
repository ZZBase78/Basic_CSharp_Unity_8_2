using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class BonusBehaviour : RootMonoBehaviour
    {
        public event Action<BonusData, Collider> onTriggerEnter = delegate (BonusData bonusData, Collider value) { };
        public BonusData bonusData;

        private void OnTriggerEnter(Collider other)
        {
            onTriggerEnter(bonusData, other);
        }
    }
}
