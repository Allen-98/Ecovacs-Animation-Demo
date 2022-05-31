using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject boom;
    public GameManager gm;

    public bool alive=true;

    public float maxHealth = 100;
    public float maxBullets = 10;
    public float currentBullets;
    public float currentHealth;

    public TMP_Text healthText;
    public Slider healthBar;
    public TMP_Text bulletText;
    public Slider BulletBar;

    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        currentHealth = maxHealth;
        currentBullets = maxBullets;

        healthBar.maxValue = maxHealth;
        BulletBar.maxValue = maxBullets;

        UpdateUI();

        InvokeRepeating("AddBullets", 3f, 5f);


    }

    // Update is called once per frame
    void Update()
    {
        AddHealth(0.05f);

        if (currentHealth <= 0)
        {
            PlayerDie();
        }

        UpdateUI();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (currentHealth != 0)
            {
                currentHealth -= 50;
            }

            Destroy(collision.gameObject);

        }
    }

    public void PlayerDie()
    {
        var temp_impact = Instantiate(boom, this.transform.position, Quaternion.LookRotation(this.transform.forward, Vector3.up));

        Destroy(temp_impact, 2f);
        Destroy(this.gameObject);
        alive = false;
    }

    public void AddHealth(float h)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += h;
        }
    }

    public void AddBullets()
    {
        if (currentBullets < maxBullets)
        {
            currentBullets += 1;
        }
    }


    public void UpdateUI()
    {
        healthBar.value = currentHealth;
        BulletBar.value = currentBullets;

        healthText.text = ((int)currentHealth).ToString();
        bulletText.text = ((int)currentBullets).ToString();
    }





    
}
