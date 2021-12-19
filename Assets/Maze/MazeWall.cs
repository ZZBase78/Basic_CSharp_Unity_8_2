using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class MazeWall : MazePoint
    {
        private bool isVertical;
        private bool isOpen;
        private Maze maze;
        private PrefabLibrary prefabLibrary;
        private Settings settings;

        public MazeWall(GameObject _parent, int new_x, int new_y, bool new_vertical, Maze maze, GameObjectFactory gameObjectFactory, PrefabLibrary prefabLibrary, Settings settings) : base(_parent, new_x, new_y)
        {
            this.gameObjectFactory = gameObjectFactory;
            this.prefabLibrary = prefabLibrary;
            this.settings = settings;
            this.maze = maze;
            isVertical = new_vertical;
            isOpen = false;
        }
        public bool GetOpen()
        {
            return isOpen;
        }
        public void SetOpen(bool value)
        {
            isOpen = value;
        }

        public override void Show()
        {
            if (!isOpen)
            {
                Vector3 position = new Vector3(maze.GetWorldXFromMazeX(x), 0, maze.GetWorldYFromMazeY(y));
                gameObject = gameObjectFactory.Instantiate(prefabLibrary.GetMazePrefab(1), position, Quaternion.identity);
                if (isVertical)
                {
                    gameObject.transform.localScale = new Vector3(settings.wallThickness, 1f, settings.cellHeight);
                }
                else
                {
                    gameObject.transform.localScale = new Vector3(settings.cellWidth, 1f, settings.wallThickness);
                }
                gameObject.transform.parent = parent.transform;
            }
        }
    }

}
