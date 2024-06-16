using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class scriptNPC : MonoBehaviour
{
    public GameObject PC;
    private NavMeshAgent agente;
    public scriptPC sPC;
    public GameObject[] waypoints = new GameObject[4];
    public float distMin = 5;
    public int indice = 0;
    private GameObject destino;
    private bool persegue = false;
    
    // Start is called before the first frame update
    void Start()
    {
        scriptPC sPC = PC.GetComponent<scriptPC>();
        agente = GetComponent<NavMeshAgent>();
        proximo();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Coin")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(), true);
        }
        if(collision.collider.tag == "PC")
        { 
            if(sPC.invulneravel)
            {              
                Destroy(gameObject);
            }
            else{
                Cursor.lockState = CursorLockMode.Confined;
                SceneManager.LoadSceneAsync(2);
                Destroy(collision.gameObject);
            }            
        }
        if(collision.collider.tag == "PowerUp")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(), true);
        }
    }

    private void proximo()
    {
        destino = waypoints[indice++];
        agente.SetDestination(destino.transform.position);
        if(indice == 6)
            indice = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(persegue || Vector3.Distance(transform.position, PC.transform.position) < 18)
        {
            persegue = true;
            agente.SetDestination(PC.transform.position);
        }        
        else if(Vector3.Distance(transform.position, destino.transform.position) < distMin)
            proximo();
    }
}
