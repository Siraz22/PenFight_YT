using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenRot : MonoBehaviour
{
    public float rotSpeed; //rotation speed of pen when we rotate it with finger

    public FixedTouchField touchscreenArea;
    public Rigidbody PenRb; //Rigidbody for pen

    public GameObject RotatorGuide;

    public AudioClip[] Clips; //0- Swoosh, 1 - Pen Collide clips
    public AudioSource selfAudioScource;

    public int PenID;

    public float force; // The force with which we'll move the pen in it's forward direction
    public float torque; // To mimic the pen rotation feel

    private bool FirstTouchDetected = false;

    public float StoppingForceRate;

    public GameObject GameOverVFX; //VFX when we die

    private void Start()
    {
        selfAudioScource = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(touchscreenArea.Pressed && PenID == GameManager.GMStaticInstance.CurrentPenID)
        {
            //This current pen is selected

            FirstTouchDetected = true; //Prevent Pen from flicks at start
            RotatorGuide.gameObject.SetActive(true);

            //We have pressed the touchscreen area
            PenRb.isKinematic = true;

            RotatePen();
        }

        if(PenRb.velocity.magnitude > 0)
        {
            //Irrespective of pen ID, we apply an opposite force anyway

            //Object must be moving, then add some opposite force
            PenRb.AddForce(-PenRb.velocity * Time.deltaTime * StoppingForceRate);
        }
    }

    public void GameOver()
    {
        if(PenID == 0)
        {
            GameManager.GMStaticInstance.Player2Score++;
            transform.position = new Vector3(5f, 1.6f, -9.5f);

        }
        else
        {
            GameManager.GMStaticInstance.Player1Score++;
            transform.position = new Vector3(-5f, 1.6f, -9.5f);
        }

        //Put the object back to start position

        if(GameOverVFX)
        {
            Instantiate(GameOverVFX, transform.position, Quaternion.identity);

            selfAudioScource.clip = Clips[2];
            selfAudioScource.Play();
        }

        //Default position for Respawn

        PenRb.velocity = Vector3.zero;
    }

    //Function for moving the pen
    public void FlickPen()
    {
        //Swoosh Effect
        selfAudioScource.clip = Clips[0];
        selfAudioScource.Play();

        PenRb.isKinematic = false;
        RotatorGuide.gameObject.SetActive(false);

        Vector3 direction = RotatorGuide.transform.up;

        //Add Relative force works for local axis of the gameobject's rigidbody in consideration
        PenRb.AddForce(direction * force,ForceMode.Impulse);
        PenRb.AddRelativeTorque(0,0,torque,ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Pen"))
        {
            selfAudioScource.clip = Clips[1];
            selfAudioScource.Play();
        }
    }

    public void RotatePen()
    {
        float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

        Vector3 direction = RotatorGuide.transform.up;

        Debug.DrawRay(RotatorGuide.transform.position, direction * 10f, Color.red);

        RotatorGuide.transform.Rotate(Vector3.forward, rotY); //Local blue axis?? Check again later
    }
}
