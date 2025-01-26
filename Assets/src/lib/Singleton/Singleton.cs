using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null) 
            {
                instance = (T)FindObjectOfType(typeof(T)); 

                if(instance == null) 
                {
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                    instance = obj.GetComponent<T>();
                }
            }
            return instance;
        }
    }

    /*
    public void Awake()
    {
        if (transform.parent != null && transform.root != null) // 5
        {
            DontDestroyOnLoad(this.transform.root.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject); // 4
        }
        
    }
    */
}