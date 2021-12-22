using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public class BonusData
    {
        public enum BonusType { Score, UpSpeed, DownSpeed, SwapDirection, UpCameraDistantion }

        public GameObject gameObject;

        public float x;
        public float y;
        private int score;
        private float time;
        private BonusType _bonusType;
        public BonusType bonusType { get { return _bonusType; } }

        public BonusData(float x, float y)
        {
            this.x = x;
            this.y = y;
            _bonusType = GetRandomBonusType();
            score = UnityEngine.Random.Range(1, 101);
            time = UnityEngine.Random.Range(1f, 10f);
        }
        private BonusType GetRandomBonusType()
        {
            BonusType[] bonusTypes = (BonusType[])Enum.GetValues(typeof(BonusType));
            return bonusTypes[UnityEngine.Random.Range(0, bonusTypes.Length)];
            //return BonusType.Score;
        }
        public int GetScore()
        {
            return score;
        }
        public float GetTime()
        {
            return time;
        }
    }
}
