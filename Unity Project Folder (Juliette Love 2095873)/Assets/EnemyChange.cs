using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyChange : MonoBehaviour
{
    public bool IsLevel1;
    public bool IsLevel2;
    public bool IsLevel3;

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Level1")
        {
            IsLevel1 = true;

            //Change enemy stats


            IsLevel2 = false;
            IsLevel3 = false;
        }

        if (sceneName == "Level2")
        {
            IsLevel2 = true;

            //Change enemy stats


            IsLevel1 = false;
            IsLevel3 = false;
        }

        if (sceneName == "Level3")
        {
            IsLevel3 = true;

            //Change enemy stats


            IsLevel1 = false;
            IsLevel2 = false;
        }
    }
}
