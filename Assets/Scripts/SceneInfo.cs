using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInfo : MonoBehaviour
{
    [SerializeField] private scenes scene;
    public scenes Scene => scene;
}

public enum scenes
{
    mainMenu,
    airGame,
    groundGame,
    landingGame,    
}
