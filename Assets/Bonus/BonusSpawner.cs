using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class BonusSpawner : GObject
    {
        private const int maxBonuses = 10;
        private List<Bonus> list;

        public BonusSpawner()
        {
            list = new List<Bonus>();
            gameObject = GameObjectFactory.InstantiateEmpty("Bonuses");
            EventManager.actionUpdate += Update;
        }
        private void Update()
        {
            if (list.Count < maxBonuses) SpawnNewBonus();
        }
        private void SpawnNewBonus() 
        {
            int x = Random.Range(0, Settings.mazeWidth) * 2 + 1;
            int y = Random.Range(0, Settings.mazeHeight) * 2 + 1;
            float xPosition = Maze.GetWorldXFromMazeX(x);
            float yPosition = Maze.GetWorldYFromMazeY(y);
            if (!IsBonusInXY(xPosition, yPosition))
            {
                Bonus newBonus = new Bonus(this, xPosition, yPosition);
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
            EventManager.actionUpdate -= Update;
            base.Dispose();
        }
    }
}
