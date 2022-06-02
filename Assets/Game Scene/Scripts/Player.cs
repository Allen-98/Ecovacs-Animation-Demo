using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject boom;
    public GameManager gm;
    public Material mt_warning;
    public GameObject warning;
    public GameObject asList;

    public AudioSource crashAudio;

    public bool alive=true;

    public float maxHealth = 100;
    public float maxBullets = 10;
    public float currentBullets;
    public float currentHealth;

    public TMP_Text healthText;
    public Slider healthBar;
    public TMP_Text bulletText;
    public Slider BulletBar;

    private float value;

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

        mt_warning.SetColor("_FaceColor", new Color(1, 1, 1, 0));

        value = 0;

    }

    // Update is called once per frame
    void Update()
    {
        AddHealth(0.05f);

        UpdateUI();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            crashAudio.Play();

            WarningOn();

            float health = currentHealth - 50;

            if (health >= 0)
            {
                currentHealth -= 50;

            }
            else
            {
                currentHealth = 0;
                PlayerDie();
                //var temp_impact = Instantiate(boom, this.transform.position, Quaternion.LookRotation(this.transform.forward, Vector3.up));
            }

            Destroy(collision.gameObject);

        }
    }

    public void PlayerDie()
    {
        //var temp_impact = Instantiate(boom, this.transform.position, Quaternion.LookRotation(this.transform.forward, Vector3.up));

        Destroy(asList);
        Destroy(warning);

        alive = false;
        gm.GameOver();

        Destroy(this.gameObject);
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

        WarningOff();

    }


    public void WarningOn()
    {

        mt_warning.SetColor("_FaceColor", new Color(1,1,1,1));
    }

    public void WarningOff()
    {
        value = mt_warning.GetColor("_FaceColor").a;

        if (value > 0)
        {
            value -= 0.01f;
            mt_warning.SetColor("_FaceColor", new Color(1, 1, 1, value));
        }
    }
    
}
