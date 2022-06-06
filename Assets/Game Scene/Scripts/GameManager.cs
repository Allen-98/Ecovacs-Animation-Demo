using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject PlayerShip;
    public GameObject Bullet;
    public GameObject BulletList;
    public GameObject Player;
    public GameObject gameOverText;
    public GameObject restartButton;
    public GameObject boom;
    public Transform boomPos;

    public AudioSource shootAudio;
    public AudioSource boomAudio;

    public bool gameOver;

    private bool isPressedRight;
    private bool isPressedLeft;

    private bool isPaused = false;
    private float maxRotation = 25;
    private float minRotation = 335;
    private float rotateSpeed = 200f;
    private float moveSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        gameOver = false;
        gameOverText.SetActive(false);
        restartButton.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (player.alive)
        {
            SimpleControl();
        }

        if (gameOver && !GameObject.Find("Boom"))
        {
            Time.timeScale = 0f;
        }

    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }


    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }


    public void GameOver()
    {
        var temp_impact = Instantiate(boom, boomPos.position, Quaternion.LookRotation(boomPos.forward, Vector3.up));
        temp_impact.name = "Boom";
        Destroy(temp_impact, 1f);
        boomAudio.Play();

        if (GameObject.Find("Boom"))
        {

            gameOver = true;
            gameOverText.SetActive(true);
            restartButton.SetActive(true);
        }

    }


    public void SimpleControl()
    {
        if (Input.GetKey(KeyCode.A) || isPressedLeft)
        {
            TurnLeft();
        }


        if (Input.GetKey(KeyCode.D) || isPressedRight)
        {
            TurnRight();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }




    }

    public float ClampAngle(float angle, float minRotation, float maxRotation)
    {
        if (angle < 90 || angle > 270)
        {       // if angle in the critic region...
            if (angle > 180) angle -= 360;  // convert all angles to -180..+180
            if (maxRotation > 180) maxRotation -= 360;
            if (minRotation > 180) minRotation -= 360;
        }
        angle = Mathf.Clamp(angle, minRotation, maxRotation);
        if (angle < 0) angle += 360;  // if angle negative, convert to 0..360
        return angle;
    }

    public void Shoot()
    {

        if (player.currentBullets != 0 && player.alive)
        {
            Instantiate(Bullet, BulletList.transform.position, Bullet.transform.rotation);
            player.currentBullets -= 1;
            shootAudio.Play();
        }
    }

    public void TurnRight()
    {
        if (player.alive)
        {
            float angle = PlayerShip.transform.localEulerAngles.z;
            angle = ClampAngle(angle, minRotation, maxRotation);

            if (angle > 335 || angle < 26)
            {
                PlayerShip.transform.Rotate(-PlayerShip.transform.forward, Time.deltaTime * rotateSpeed);

            }

            Player.transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }

    }

    public void TurnLeft()
    {
        if (player.alive)
        {

            float angle = PlayerShip.transform.localEulerAngles.z;
            angle = ClampAngle(angle, minRotation, maxRotation);

            if (angle < 25 || angle > 334)
            {
                PlayerShip.transform.Rotate(-PlayerShip.transform.forward, Time.deltaTime * -rotateSpeed);

            }

            Player.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }

    }

    public void LongPressRight(bool bStart)
    {

        isPressedRight = bStart;

    }

    public void LongPressLeft(bool bStart)
    {

        isPressedLeft = bStart;

    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }


}
