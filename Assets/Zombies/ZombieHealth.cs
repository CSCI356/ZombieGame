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
    private bool dead = false;

    void Awake(){
        animator = GetComponentInChildren<Animator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet"){
            int damage = WeaponManager.Instance.getCurrentBulletDamage();
            health-=damage;
            SoundFXManager.Instance.PlayZombieGroanSound();

            DamageSelfText(damage);

            if((health <= 0) && (!dead)){
                Death();
            }
        }
    }

    void Death(){
        dead = true;
        Debug.Log("Died");
        
        // trigger death animation
        animator.SetTrigger("dead");

        StartCoroutine(DelayedDelete());

        // stop the zombie moving
        GetComponent<ZombieMovement>().StopMovement();

        GameManager.Instance.IncreaseKills();
    }

    IEnumerator DelayedDelete(){
        yield return new WaitForSeconds(5);
        // deletes zombie
        Destroy(this.gameObject);
    }

    //User damage feedback on zombies
    private void DamageSelfText(int amountOfDamage) {
        GameObject damageObjectInstance = Instantiate(damageTextObject, canvasTransform);

        damageObjectInstance.GetComponentInChildren<TMP_Text>().text = "-" + amountOfDamage;
    }
}
