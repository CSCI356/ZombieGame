using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField] private int health = 30;
    private Animator animator;

    [Header("UI")]
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private GameObject damageTextObject;
    [SerializeField] private GameObject heart;
    [SerializeField] private GameObject wall;
    public bool dead = false;
    private Rigidbody rb;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            int damage = WeaponManager.Instance.getCurrentBulletDamage();
            health -= damage;
            SoundFXManager.Instance.PlayZombieGroanSound();

            DamageSelfText(damage);

            if ((health <= 0) && (!dead))
            {
                Death();
            }
        }
    }

    void Death()
    {
        dead = true;
        Debug.Log("Died");
        rb.constraints = RigidbodyConstraints.FreezeAll;


        // trigger death animation
        animator.SetTrigger("dead");

        // possibly drop a heart
        float randomNum = Random.Range(0.0f, 100.0f);
        if (randomNum <= 20.0f)
        {
            GameObject new_heart = Instantiate(heart);
            
            // sets correct position
            new_heart.transform.position = new Vector3(
                this.transform.position.x, 
                1,
                this.transform.position.z);

            // detach from zombie otherwise will get destroyed
            // with it
            new_heart.transform.parent = null;
        }else if(randomNum <= 40.0f){
            GameObject new_wall = Instantiate(wall);
            
            // sets correct position
            new_wall.transform.position = new Vector3(
                this.transform.position.x, 
                0.1f,
                this.transform.position.z);

            // detach from zombie otherwise will get destroyed
            // with it
            new_wall.transform.parent = null;
        }

        StartCoroutine(DelayedDelete());

        // stop the zombie moving
        GetComponent<ZombieMovement>().StopMovement();

        GameManager.Instance.IncreaseKills();
    }

    IEnumerator DelayedDelete()
    {
        yield return new WaitForSeconds(5);
        // deletes zombie
        Destroy(this.gameObject);
    }

    //User damage feedback on zombies
    private void DamageSelfText(int amountOfDamage)
    {
        GameObject damageObjectInstance = Instantiate(damageTextObject, canvasTransform);

        damageObjectInstance.GetComponentInChildren<TMP_Text>().text = "-" + amountOfDamage;
    }
}
