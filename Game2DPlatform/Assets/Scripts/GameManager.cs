using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager GM;
    private Fader fader;

    private void Awake()
    {
        if (GM == null)
        {
            GM = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static void RegisterFader(Fader fD)
    {
        if (GM == null)
            return;

        GM.fader = fD;
    }

    public static void ManagerLoadLevel(int index)
    {
        if (GM == null)
            return;

        GM.fader.SetLevel(index);
    }

    public static void ManagerRestartLevel()
    {
        if (GM == null)
            return;

        GM.fader.RestartLevel();
    }

}
