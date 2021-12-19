using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class BonusSpawner : GObject
    {
        private const int maxBonuses = 10;
        private List<Bonus> list;
        private EventManager eventManager;
        private Settings settings;
        private PrefabLibrary prefabLibrary;
        private Maze maze;

        public BonusSpawner(GameObjectFactory gameObjectFactory, EventManager eventManager, Settings settings, Maze maze, PrefabLibrary prefabLibrary)
        {
            this.gameObjectFactory = gameObjectFactory;
            this.eventManager = eventManager;
            this.settings = settings;
            this.prefabLibrary = prefabLibrary;
            this.maze = maze;
            list = new List<Bonus>();
            gameObject = gameObjectFactory.InstantiateEmpty("Bonuses");
            eventManager.actionUpdate += Update;
        }
        private void Update()
        {
            if (list.Count < maxBonuses) SpawnNewBonus();
        }
        private void SpawnNewBonus() 
        {
            int x = Random.Range(0, settings.mazeWidth) * 2 + 1;
            int y = Random.Range(0, settings.mazeHeight) * 2 + 1;
            float xPosition = maze.GetWorldXFromMazeX(x);
            float yPosition = maze.GetWorldYFromMazeY(y);
            if (!IsBonusInXY(xPosition, yPosition))
            {
                Bonus newBonus = new Bonus(this, xPosition, yPosition, prefabLibrary, gameObjectFactory, eventManager);
                list.Add(newBonus);
            }
        }
        public void DeleteBonus(Bonus bonus)
        {
            list.Remove(bonus);
            bonus.Dispose();
        }
        private bool IsBonusInXY(float x, float y)
        {
            foreach (Bonus bonus in list)
            {
                if ((bonus.x == x) && (bonus.y == y))
                {
                    return true;
                }
            }
            return false;
        }
        public override void Dispose()
        {
            foreach (Bonus bonus in list)
            {
                bonus.Dispose();
            }
            eventManager.actionUpdate -= Update;
            base.Dispose();
        }
    }
}
