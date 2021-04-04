using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomizeList : MonoBehaviour
{
    private static System.Random rng = new System.Random();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static List<E> mixTheList<E>(List<E> list)
    {
        //based on the Fisher-Yates Shuffle
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            E content = list[k];
            list[k] = list[n];
            list[n] = content;

           
        }
        return list;
    }
}
