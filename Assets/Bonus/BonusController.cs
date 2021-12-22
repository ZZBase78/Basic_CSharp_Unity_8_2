using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class BonusController
    {
        private PrefabLibrary prefabLibrary;
        private List<BonusData> bonusList;
        private BonusSpawner bonusSpawner;
        private EventManager eventManager;
        private GameObjectFactory gameObjectFactory;
        private Transform parent;

        public BonusController(List<BonusData> list, Transform parent, GameObjectFactory gameObjectFactory, PrefabLibrary prefabLibrary, BonusSpawner bonusSpawner, EventManager eventManager)
        {
            this.bonusList = list;
            this.prefabLibrary = prefabLibrary;
            this.bonusSpawner = bonusSpawner;
            this.eventManager = eventManager;
            this.gameObjectFactory = gameObjectFactory;
            this.parent = parent;

            foreach (BonusData bonusData in bonusList)
            {
                ShowBonus(bonusData);
            }
        }

        public void NewBonus(BonusData bonusData)
        {
            ShowBonus(bonusData);
        }

        public void ShowBonus(BonusData bonusData)
        {
            Vector3 position = new Vector3(bonusData.x, 0, bonusData.y);
            bonusData.gameObject = GameObject.Instantiate(prefabLibrary.bonus, position, Quaternion.identity);
            BonusBehaviour bonusBehaviour = bonusData.gameObject.AddComponent<BonusBehaviour>();
            bonusBehaviour.bonusData = bonusData;
            bonusBehaviour.onTriggerEnter += OnTriggerEnter;
            bonusData.gameObject.transform.SetParent(bonusSpawner.gameObject.transform);
            Renderer[] renders = bonusData.gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer render in renders)
                render.material.color = RandomColor();
        }

        private static Color RandomColor()
        {
            float r = UnityEngine.Random.Range(0f, 1f);
            float g = UnityEngine.Random.Range(0f, 1f);
            float b = UnityEngine.Random.Range(0f, 1f);
            float a = 1f;
            return new Color(r, g, b, a);
        }

        public void Hide(BonusData bonusData)
        {
            GameObject.Destroy(bonusData.gameObject);
        }

        private void OnTriggerEnter(BonusData bonusData, Collider other)
        {
            if (other.CompareTag("Player"))
            {
                eventManager.PlayerTakeBonus(bonusData);

                BonusBehaviour bonusBehaviour = bonusData.gameObject.GetComponent<BonusBehaviour>();
                bonusBehaviour.onTriggerEnter -= OnTriggerEnter;

                bonusSpawner.DeleteBonus(bonusData);

                Hide(bonusData);
            }
        }
    }
}
