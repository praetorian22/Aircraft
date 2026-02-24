using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private List<SceneInfo> scenes = new List<SceneInfo>();

    public void SetScene(scenes scene)
    {
        foreach (SceneInfo info in scenes)
        {
            if (info.Scene == scene) info.gameObject.SetActive(true);
            else info.gameObject.SetActive(false);
        }
    }
    public void AddSceneToActive(scenes scene)
    {
        foreach (SceneInfo info in scenes)
        {
            if (info.Scene == scene) info.gameObject.SetActive(true);
        }
    }
}

