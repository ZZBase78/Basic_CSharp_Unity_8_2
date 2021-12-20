using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBase.Maze
{
    public sealed class MazeWallCross : MazePoint
    {
        Maze maze;
        PrefabLibrary prefabLibrary;
        Settings settings;

        public MazeWallCross(GameObject _parent, int new_x, int new_y, Maze maze, GameObjectFactory gameObjectFactory, PrefabLibrary prefabLibrary, Settings settings) : base(_parent, new_x, new_y)
        {
            this.maze = maze;
            this.gameObjectFactory = gameObjectFactory;
            this.prefabLibrary = prefabLibrary;
            this.settings = settings;
        }

        public override void Show()
        {
            Vector3 position = new Vector3(maze.GetWorldXFromMazeX(x), 0, maze.GetWorldYFromMazeY(y));
            gameObject = gameObjectFactory.Instantiate(prefabLibrary.mazeWallCross, position, Quaternion.identity);
            gameObject.transform.localScale = new Vector3(settings.wallThickness * 2f, 1.1f, settings.wallThickness * 2f);
            gameObject.transform.parent = parent.transform;
        }
    }

}
