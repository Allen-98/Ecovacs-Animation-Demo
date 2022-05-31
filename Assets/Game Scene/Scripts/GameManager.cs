using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject PlayerShip;
    public GameObject Bullet;
    public GameObject BulletList;
    public GameObject Player;

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
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        SimpleControl();        

        if (!player.alive)
        {
            Invoke("GameOver", 1f);
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
        Time.timeScale = 0f;
        gameOver = true;
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

        if (player.currentBullets != 0)
        {
            Instantiate(Bullet, BulletList.transform.position, Bullet.transform.rotation);
            player.currentBullets -= 1;
        }
    }

    public void TurnRight()
    {
        float angle = PlayerShip.transform.localEulerAngles.z;
        angle = ClampAngle(angle, minRotation, maxRotation);

        if (angle > 335 || angle < 26)
        {
            PlayerShip.transform.Rotate(-PlayerShip.transform.forward, Time.deltaTime * rotateSpeed);

        }

        Player.transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
    }

    public void TurnLeft()
    {

        float angle = PlayerShip.transform.localEulerAngles.z;
        angle = ClampAngle(angle, minRotation, maxRotation);

        if (angle < 25 || angle > 334)
        {
            PlayerShip.transform.Rotate(-PlayerShip.transform.forward, Time.deltaTime * -rotateSpeed);

        }

        Player.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
    }

    public void LongPressRight(bool bStart)
    {

        isPressedRight = bStart;

    }

    public void LongPressLeft(bool bStart)
    {

        isPressedLeft = bStart;

    }

}
