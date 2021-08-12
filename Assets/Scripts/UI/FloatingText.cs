using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = 1f;
    public bool goodAnswer;
    private Renderer rend;

    // Start is called before the first frame update
    private void Start()
    {
        rend = gameObject.GetComponent<Renderer>();

        if (goodAnswer == true)
        {
            rend.material.color = Color.green;
        }
        else
        {
            rend.material.color = Color.red;
        }
        Destroy(gameObject, destroyTime);
    }
}