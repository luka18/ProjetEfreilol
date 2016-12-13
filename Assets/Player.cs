using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    [SerializeField]
    InfiniteTerrain Inf;
    public float speed = 25; // Vitesse déplacement
    public int jumpPower = 500; // Puissance saut
    private bool canJump = true; // Peut on sauter ?
    Rigidbody body;
    short pos = 0;
    bool free = true;
    bool candie = true;
    public int numberofcoin = 0;
    // Use this for initialization
    void Start()    
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {





        float checkposx;
        float checkposy;
        float checkposz;
        checkposx = transform.position.x;
        checkposy = transform.position.y;
        checkposz = transform.position.z;

        if (Input.GetKeyDown(KeyCode.Q) && pos>-1&&free)
        {
            StartCoroutine(smoothleft());
            pos--;
        }
        if (Input.GetKeyDown(KeyCode.D)&&pos<1&&free)
        {
            StartCoroutine(smoothright());
            pos++;
        }
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space))&&canJump)
        {
            jump();
        }
        transform.position += new Vector3(0, 0, speed * Time.deltaTime);

        print(pos);
    }

    void OnCollisionEnter(Collision col)
    {
        
        if (col.transform.tag == "mur")
        {
            //No delete
            StopAllCoroutines(); 
            free = true;
            transform.position = new Vector3(0, 2, 0);
            // No Delete

            if (candie == true)
            {
                
                numberofcoin = 0;
                speed = 25;
                
                pos = 0;
                candie = true;
                Inf.ResetAll();
            }
            else
            {
                
                numberofcoin = numberofcoin - 50;
                speed = speed - 15;
                candie = true;
            }
        }
        if(col.transform.tag == "Ground")
        {
            canJump = true;
        }
        
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Coin")
        {
            if (speed < 45) speed = speed + 1;
            print("tag");

            if (numberofcoin<50)numberofcoin++;
            if (numberofcoin == 50)  candie = false;
            
            print(speed + "->" + numberofcoin);
            Inf.DeleteGold(col.gameObject);
        }
    }
 
    IEnumerator smoothleft()
    {
        free = false;
        Vector3 starpos = transform.position;
        Vector3 Endpos = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
        float i = 0;
        while (i < 1)
        {
            transform.position = Vector3.Lerp(starpos, Endpos,Mathf.SmoothStep(0,1,i) );
            i += Time.deltaTime * 15;
            yield return null;

        }
        transform.position = Endpos;
        free = true;
    }
    IEnumerator smoothright()
    {
        free = false;
        Vector3 starpos = transform.position;
        Vector3 Endpos = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
        float i = 0;
        while (i < 1)
        {
            transform.position = Vector3.Lerp(starpos, Endpos, Mathf.SmoothStep(0, 1, i));
            i += Time.deltaTime * 15;
            yield return null;

        }
        transform.position = Endpos;
        free = true;
    }
    void jump()
    {
        body.AddForce(0, jumpPower, 0, ForceMode.Impulse);
        canJump = false;
    }
}


