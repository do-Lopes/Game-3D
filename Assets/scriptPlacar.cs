using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class scriptPlacar : MonoBehaviour
{
    private static int placar = 0;
    private static int level;
    private static GameObject texto;

    public void Start()
    {
        texto = GameObject.Find("txtPlacar");
    }
    public static void addPlacar(int a)
    {
        placar += a;
        texto.GetComponent<TMP_Text>().text = "Pontos: " + placar + "/259";
    
        if(placar == 259){
            placar = 0;
            SceneManager.LoadSceneAsync(1);
        }
    }
}
