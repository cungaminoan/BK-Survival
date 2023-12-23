using UnityEngine;
using UnityEngine.AI;


public class SC_NPCEnemy : MonoBehaviour, IEntity
{

    [HideInInspector]
    public SC_EnemySpawner es;

    // Start is called before the first frame update
    void Start()
    {
        //Set Rigidbody to Kinematic to prevent hit register bug
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void DestroyBoids()
    {
        es.EnemyEliminated(this);
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }
}