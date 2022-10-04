using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 100;
    public Image healthBar;
    public Sprite healthBarFull, healthBar75, healthBar50, healthBar25, healthBar0;
    public TextMeshProUGUI healthText;

    private SoundFXManager soundFXManager;

    void Start(){
        soundFXManager = SoundFXManager.Instance;
    }

    void OnCollisionStay(Collision collision)
    {
        if((collision.gameObject.tag == "Zombies") && (health>0)){
            // for now, just default -1 damage
            // TODO: make 'damage' an attribute on the zombies
            health-=1;

            soundFXManager.PlayDamageSound();

            Debug.Log(string.Format("Player damaged: {0}", health));

            // trigger UI change
            UIManager.Instance.UpdateHealth(health);

            if(health <= 0){
                Death();
            }
        }
        
        //hit heart
        if((collision.gameObject.tag == "Item") && (health <= 100))
        {
            soundFXManager.PlayCollectibleSound();
            if (health > 90)
            {
                health = 100;
            }
            else
            {
                health += 10;
            }
            Destroy(collision.gameObject);
            UIManager.Instance.UpdateHealth(health);
        }

        if (health <= 100 && health > 75) {
                healthBar.sprite = healthBarFull;
                healthText.color = Color.white;
        }

        if (health <= 75 && health > 50) {
                healthBar.sprite = healthBar75;
                healthText.color = Color.black;
        }

        if (health <= 50 && health > 25) {
                healthBar.sprite = healthBar50;
                healthText.color = Color.white;
        }

        if (health <= 25 && health > 0) {
                healthBar.sprite = healthBar25;
                healthText.color = Color.white;
        }

        if (health == 0) {
                healthBar.sprite = healthBar0;
                healthText.color = Color.white;
        }
    }

    void Death(){
        Debug.Log("Died");
        // trigger death animation

        // trigger gameover sequence
        GameManager.Instance.GameOver();
                
        // deletes player
        // Destroy(this.gameObject);
    }
}
