using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMission : MonoBehaviour
{
    public float loadTime;
    public int MM;

    // Start is called before the first frame update
    private void Start()
    {
        loadTime = 200;
        MM = FindObjectOfType<MenuManager>().selectedLevel;
    }

    // Update is called once per frame
    private void Update()
    {
        loadTime--;
        if (loadTime < 0)
        {
            SceneManager.LoadScene(MM);
        }
    }
}