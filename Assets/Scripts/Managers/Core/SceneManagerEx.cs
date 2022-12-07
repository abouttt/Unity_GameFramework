using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return Object.FindObjectOfType<BaseScene>(); } }
    public Enums.Scene CurrentSceneType
    {
        get
        {
            if (_sceneType != Enums.Scene.Unknown)
            {
                return _sceneType;
            }

            return CurrentScene.SceneType;
        }
        private set
        {
            _sceneType = value;
        }
    }

    private Enums.Scene _sceneType = Enums.Scene.Unknown;

    public void LoadScene(Enums.Scene type)
    {
        Managers.Clear();
        CurrentScene.Clear();
        CurrentSceneType = type;
        SceneManager.LoadScene(getSceneName(type));
    }

    private string getSceneName(Enums.Scene type) => System.Enum.GetName(typeof(Enums.Scene), type);
}
