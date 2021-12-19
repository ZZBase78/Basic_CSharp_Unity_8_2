using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class MazeCell : MazePoint
    {
        public bool isReachable;
        private PrefabLibrary prefabLibrary;
        private Settings settings;
        private Maze maze;
        public MazeCell(GameObject _parent, int new_x, int new_y, Maze maze, GameObjectFactory gameObjectFactory, PrefabLibrary prefabLibrary, Settings settings) : base(_parent, new_x, new_y)
        {
            this.maze = maze;
            this.gameObjectFactory = gameObjectFactory;
            this.prefabLibrary = prefabLibrary;
            this.settings = settings;
            isReachable = false;
        }
        public override void Show()
        {
            Vector3 position = new Vector3(maze.GetWorldXFromMazeX(x), 0, maze.GetWorldYFromMazeY(y));
            gameObject = gameObjectFactory.Instantiate(prefabLibrary.GetMazePrefab(0), position, Quaternion.identity);
            gameObject.transform.localScale = new Vector3(settings.cellWidth, 1f, settings.cellHeight);
            gameObject.transform.parent = parent.transform;
        }
    }

}
