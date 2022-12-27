using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Gunshot : MonoBehaviour
{
    // sound effect
    public AudioClip shootSound;
    private AudioSource audioSource;

    public bool multiBullet = false;
    public float bulletSpeed = 10;
    public float Ai = 10;

    // Gun
    public GameObject gun;

    // bullet prefab
    public GameObject bulletPrefab;

    GameManager gm;

    public XRBaseController xr;




    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gm = GameObject.FindObjectOfType<GameManager>();

    }


    public void OnFire()
    {

        Debug.Log("on File");

        audioSource.PlayOneShot(shootSound);

        // spawn a new bullet
        GameObject newBullet = Instantiate(bulletPrefab);

        // pass the game manager
        newBullet.GetComponent<BulletController>().gm = gm;

        // position will be that of the gun
        newBullet.transform.position = gun.transform.position + (gun.transform.forward/2);

        // get rigid body
        Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();

        // give the bullet velocity
        bulletRb.velocity = gun.transform.forward * bulletSpeed;


        if (multiBullet) {

            xr.SendHapticImpulse(0.5f, 0.3f);

            // spawn a new bullet
            newBullet = Instantiate(bulletPrefab);

            // pass the game manager
            newBullet.GetComponent<BulletController>().gm = gm;

            // position will be that of the gun
            newBullet.transform.position = gun.transform.position + (gun.transform.forward / 2) + Vector3.right/3;

            // get rigid body
            bulletRb = newBullet.GetComponent<Rigidbody>();

            // give the bullet velocity
            bulletRb.velocity = (gun.transform.forward + Vector3.right/3) * bulletSpeed;


            // spawn a new bullet
            newBullet = Instantiate(bulletPrefab);

            // pass the game manager
            newBullet.GetComponent<BulletController>().gm = gm;

            // position will be that of the gun
            newBullet.transform.position = gun.transform.position + (gun.transform.forward / 2) + Vector3.left/3;

            // get rigid body
            bulletRb = newBullet.GetComponent<Rigidbody>();

            // give the bullet velocity
            bulletRb.velocity = (gun.transform.forward + Vector3.left/3) * bulletSpeed;
        }
    }

}
