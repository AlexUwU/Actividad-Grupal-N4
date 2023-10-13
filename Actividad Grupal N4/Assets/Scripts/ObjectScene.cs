using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectScene : MonoBehaviour
{

    private void Awake()
    {
        var noDestruir = FindObjectsOfType<ObjectScene>();

        if (noDestruir.Length > 2)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        
    }

}
