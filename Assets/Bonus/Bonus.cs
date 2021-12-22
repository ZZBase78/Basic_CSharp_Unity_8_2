using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class Bonus_old : GObject
    {
        //public enum BonusType { Score, UpSpeed, DownSpeed, SwapDirection, UpCameraDistantion}

        //private BonusSpawner bonusSpawner;
        //private PrefabLibrary prefabLibrary;
        //private EventManager eventManager;

        //public float x;
        //public float y;
        //private int score;
        //private float time;
        //private BonusType _bonusType;
        //public BonusType bonusType { get { return _bonusType; } }


        ////public Bonus(BonusSpawner bonusSpawner, float x, float y, PrefabLibrary prefabLibrary, GameObjectFactory gameObjectFactory, EventManager eventManager)
        ////{
        ////    this.prefabLibrary = prefabLibrary;
        ////    this.gameObjectFactory = gameObjectFactory;
        ////    this.eventManager = eventManager;
        ////    this.x = x;
        ////    this.y = y;
        ////    this.bonusSpawner = bonusSpawner;
        ////    _bonusType = GetRandomBonusType();
        ////    score = UnityEngine.Random.Range(1, 101);
        ////    time = UnityEngine.Random.Range(1f, 10f);
        ////    Show();
        ////}
        //private static Color RandomColor()
        //{
        //    float r = UnityEngine.Random.Range(0f, 1f);
        //    float g = UnityEngine.Random.Range(0f, 1f);
        //    float b = UnityEngine.Random.Range(0f, 1f);
        //    float a = 1f;
        //    return new Color(r, g, b, a);
        //}
        ////private void Show()
        ////{
        ////    Vector3 position = new Vector3(x, 0, y);
        ////    gameObject = gameObjectFactory.Instantiate(prefabLibrary.bonus, position);
        ////    BonusBehaviour bonusBehaviour = gameObject.AddComponent<BonusBehaviour>();
        ////    bonusBehaviour.onTriggerEnter += OnTriggerEnter;
        ////    gameObject.transform.SetParent(bonusSpawner.GetGameObject().transform);
        ////    Renderer[] renders = gameObject.GetComponentsInChildren<Renderer>();
        ////    foreach (Renderer render in renders)
        ////        render.material.color = RandomColor();
        ////}
        //public int GetScore()
        //{
        //    return score;
        //}
        //public float GetTime()
        //{
        //    return time;
        //}
        //private BonusType GetRandomBonusType()
        //{
        //    BonusType[] bonusTypes = (BonusType[])Enum.GetValues(typeof(BonusType));
        //    return bonusTypes[UnityEngine.Random.Range(0, bonusTypes.Length)];
        //    //return BonusType.Score;
        //}
        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.CompareTag("Player"))
        //    {
        //        eventManager.PlayerTakeBonus(this);
        //        //Спаунер должен выполнить событие последним, т.к. будет его уничтожать
        //        bonusSpawner.DeleteBonus(this);
        //    }
        //}
        //public override void Dispose()
        //{
        //    if (gameObject != null)
        //    {
        //        BonusBehaviour bonusBehaviour = gameObject.GetComponent<BonusBehaviour>();
        //        bonusBehaviour.onTriggerEnter -= OnTriggerEnter;
        //    }
        //    base.Dispose();
        //}
    }
}
