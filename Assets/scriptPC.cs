using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scriptPC : MonoBehaviour
{
    private Rigidbody rbd;
    public float vel = 40;
    public bool invulneravel = false;
    private Quaternion rotIni;
    public float velRotY;
    private float countY = 0;
    public GameObject cabeca;
    public LayerMask mascara;
    public float dist;
    public AudioSource coin_sound;
    public AudioSource teleporter_sound;
    public AudioSource power_up_sound;

    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody>();
        rotIni = transform.localRotation;
        velRotY = 150;
        dist = 100;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Coin")
        {
            coin_sound.Play();
            scriptPlacar.addPlacar(1);
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(), true);
            Destroy(collision.gameObject);
        }
        if(collision.collider.tag == "PowerUp")
        {
            invulneravel = true;
            power_up_sound.Play();
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(), true);
            Destroy(collision.gameObject);
            StartCoroutine("PowerUpStart");
        }
        if(collision.collider.tag == "Teleporter"){
            teleporter_sound.Play();
            if(transform.position.x > 0)
            {
                transform.position = new Vector3((-transform.position.x + 2), transform.position.y, transform.position.z);
            }
            else{
                transform.position = new Vector3((-transform.position.x - 2), transform.position.y, transform.position.z);
            }            
        }
    }

    IEnumerator PowerUpStart()
    {
        yield return new WaitForSeconds(8);
        invulneravel = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        float frente = Input.GetAxis("Vertical");
        float lado = Input.GetAxis("Horizontal");
        
        rbd.velocity = transform.TransformDirection(new Vector3(lado * vel, rbd.velocity.y, frente * vel));

        countY += Input.GetAxisRaw("Mouse X") * Time.deltaTime * velRotY;
        Quaternion rotY = Quaternion.AngleAxis(countY, Vector3.up);
        transform.localRotation = rotIni * rotY;
    }
}
