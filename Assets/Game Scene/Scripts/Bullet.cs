using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 100f;
    public GameObject boom;

    private Transform bulletTransform;
    private Vector3 lastPos;


    // Start is called before the first frame update
    void Start()
    {
        bulletTransform = transform;
        lastPos = bulletTransform.position;
        Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        bulletTransform.Translate(0, bulletSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            var temp_impact = Instantiate(boom, this.transform.position,
            Quaternion.LookRotation(this.transform.forward, Vector3.up));

            Destroy(temp_impact, 2f);
            Destroy(this.gameObject);

        }
    }

}


