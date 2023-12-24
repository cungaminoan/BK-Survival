using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAndBowScript : MonoBehaviour
{
    private Rigidbody myBody;
    public float speed = 30f;
    public float deActiveTimer = 3f;
    public float damage = 30f;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("DeActivateGameObject", deActiveTimer);
    }

    public void Launch(Camera mainCamera)
    {
        myBody.velocity = Camera.main.transform.forward * speed;
        transform.LookAt(transform.position + myBody.velocity);
    }

    protected void DeActivateGameObject()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    protected void OnTriggerEnter(Collider target)
    {
        if(target.tag == Tags.ENEMY_TAG)
        {
            target.GetComponent<HealthScript>().ApplyDamage(damage);
        }
    }
}
