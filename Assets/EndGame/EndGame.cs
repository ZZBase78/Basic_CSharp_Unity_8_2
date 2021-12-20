using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZZBase.Maze
{
    public class EndGame : GObject
    {
        private Button button;
        public EndGame(GameObjectFactory gameObjectFactory, PrefabLibrary prefabLibrary)
        {
            gameObject = gameObjectFactory.Instantiate(prefabLibrary.endGame);
            button = gameObject.GetComponentInChildren<Button>();
        }
        public Button GetRestartButton()
        {
            return button;
        }
    }
}
