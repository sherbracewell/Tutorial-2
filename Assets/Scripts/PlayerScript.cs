using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
     private Rigidbody2D rd2d;

     public float speed;

     public AudioClip musicClipOne;

     public AudioClip musicClipTwo;

     public AudioSource musicSource;

      public Text score;

      public Text win;

      public Text lives;

     private int scoreValue = 0;

     private int livesValue = 3;

     Animator anim;

     private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
         score.text = "Score: " + scoreValue.ToString();
         win.text = "";
         lives.text = "Lives: " + livesValue.ToString();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

     if (Input.GetKeyDown(KeyCode.D))

        {

          anim.SetInteger("State", 1);

         }

     if (Input.GetKeyUp(KeyCode.D))

        {

          anim.SetInteger("State", 0);

         }

     if (Input.GetKeyDown(KeyCode.A))

        {

          anim.SetInteger("State", 1);

         }

     if (Input.GetKeyUp(KeyCode.A))

        {

          anim.SetInteger("State", 0);

         }

     if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
    else if (facingRight == true && hozMovement < 0)
        {
             Flip();
        }

     if (Input.GetKey("escape"))
        {
            Application.Quit();
        } 
        
        }
    
     private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);

             if (scoreValue >= 9)
                {
                     win.text = "You win! \n Game created by: \n Sherrye Bracewell!";
                     musicSource.clip = musicClipTwo;
                     musicSource.Play();
                     gameObject.SetActive(false);
            

                    
                }
        }

        if (collision.collider.tag == "Enemy")
            {
                livesValue -= 1;
                lives.text = "Lives: " + livesValue.ToString();
                Destroy(collision.collider.gameObject);

                    if (livesValue <= 0)
                    {
                       Destroy(this.gameObject);  
                       win.text = "You lose!";
                        
                    }
            }

         if (scoreValue == 4) 
        {
            transform.position = new Vector2(39.9f, 0.0f);
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
    
     }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {


            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                
            }
            
        }
         if (collision.collider.tag == "Platform")
        {


            if (Input.GetKeyDown(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
                anim.SetInteger("State", 2);
            }
        }
    }
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
}
