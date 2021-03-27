using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;
    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private void Awake()   
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

    private bool triggerLand = false;

    Animator animator;
    SpriteRenderer sRenderer;
    public windEffect windScript;

    public Animator sailAnimator;
    public GameObject sail;
    public GameObject head;
    SpriteRenderer hRenderer;

    Rigidbody2D body;
    float horizontal;
    float vertical;
    public float speed = 20.0f;
    public float boatSpeed = 4;
    public bool onLand = true;

    public Vector2 windDirection;
    public Vector2 playerDirection;
    public float WithWindSpeed = 2f;
    public float AgainstwindSpeed = 0f;
    [Range(0.0f, 1.0f)]
    public float windSpeed = 1f;

    public string WindDirect;

    float prevX = 0f, prevY = 0f;
    bool canFlip = true;

    public bool inUnderworld;
    public GameObject Overworld;
    public GameObject Underworld;

    public bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        hRenderer = head.GetComponent<SpriteRenderer>();
        sailAnimator = sail.GetComponent<Animator>();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
        setWindDirection("North");

    }
    public void SwapWorld()
    { 
        inUnderworld = !inUnderworld;
        Overworld.SetActive(!Overworld.activeSelf);
        Underworld.SetActive(!Underworld.activeSelf);
        windScript.WindChange();
    }


    // Update is called once per frame
    public bool LandCheck()
    {       
        // check tile data for ground
        Vector3Int gridPosition = map.WorldToCell(body.transform.position);
        TileBase CurrentTile = map.GetTile(gridPosition);

        //print("At position " + gridPosition + " tile type: "+ CurrentTile);
        bool land = false;
        if (CurrentTile == null)
        {
            return false;
        }
        else
        {
           
            return true;
        }

        if (land == true)
        {
            
            return true;
        }
        return false;
    }

    public void ToggleboatExtras()
    {
        if (onLand)
        {
            head.SetActive(false);
            sail.SetActive(false);
        }
        else
        {
            head.SetActive(true);
            sail.SetActive(true);
        }
    }

    void Update()
    {
        
       
        if (inUnderworld || triggerLand)
        {
            onLand = true;
        }
        else
        {
            onLand = LandCheck();
        }
        


        if (Input.GetButtonDown("Fire1"))
        {
            
            setWindDirection(WindDirect);
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        sailAnimator.SetFloat("windSpeed", windSpeed);
        animator.SetBool("onLand", onLand);
        animator.SetFloat("speedX", body.velocity.x);
        animator.SetFloat("speedY", body.velocity.y);
        if(horizontal !=0)
            animator.SetTrigger("boatHorizontal");
        if(vertical > 0)
            animator.SetTrigger("boatUp");
        if (vertical < 0)
            animator.SetTrigger("boatDown");


        

        if (onLand)
        {
            //head.SetActive(false);
           // sail.SetActive(false);
            if (body.velocity.x > .1f)
            {
                sRenderer.flipX = false;
            }
            else if (body.velocity.x < -.1f)
            {
                sRenderer.flipX = true;
            }
        }
        else
        {
            //head.SetActive(true);
            //sail.SetActive(true);
            if (prevX < 0 && prevY == 0)
            {
                canFlip = false;
                if (body.velocity.x < -.1f)
                {
                    sRenderer.flipX = true;              
                    sailAnimator.SetBool("FlipShift", true);
                }
            }

            if(body.velocity.x>= 0)
            {
                canFlip = true;
            }

            if (body.velocity.x > .1f && vertical == 0 &&canFlip)
            {
                sRenderer.flipX = false;
                sailAnimator.SetBool("FlipShift", false);
            }
             if (body.velocity.x < -.1f&&canFlip)
            {
                sRenderer.flipX = true;
                sailAnimator.SetBool("FlipShift", true);
            }
            if (body.velocity.y != 0&&canFlip)
            {
                sRenderer.flipX = false;
                sailAnimator.SetBool("FlipShift", false);
            }

            hRenderer.flipX = sRenderer.flipX;
      

            prevX = body.velocity.x;
            prevY = body.velocity.y;
        }

        if (!canMove)
        {
            playerDirection = new Vector2(0, 0);
            body.velocity = playerDirection;
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            if (onLand)
            {
                landMove();
            }
            else
            {
                boatMove();
            }
        }
        
        
    }

    void landMove()
    {
        if (horizontal != 0 || vertical != 0)
        {
            body.velocity = new Vector2(horizontal * speed, vertical * speed).normalized * speed;

            body.velocity = new Vector2(Mathf.RoundToInt(body.velocity.x * 8), Mathf.RoundToInt(body.velocity.y * 8));
            body.velocity = body.velocity / 8;
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
            body.velocity = Vector2.zero;
        }
    }

    void boatMove()
    {
        if (horizontal != 0 || vertical != 0)
        {
            playerDirection = new Vector2(horizontal * boatSpeed, vertical * boatSpeed).normalized;
            float windDot = Vector2.Dot(playerDirection, windDirection);

            windDot = AgainstwindSpeed + (windDot + 1f) * (WithWindSpeed - AgainstwindSpeed)/ (1f - -1f);

            playerDirection = new Vector2 (playerDirection.x*(boatSpeed+ windSpeed*(windDot*windDot)), playerDirection.y * (boatSpeed + windSpeed * (windDot * windDot)));

            playerDirection = new Vector2(Mathf.RoundToInt(playerDirection.x * 8), Mathf.RoundToInt(playerDirection.y* 8));


            body.velocity = playerDirection/8;
            animator.SetBool("moving", true);
        }
        else
        {
            playerDirection = Vector2.zero;
            body.velocity = playerDirection;
            animator.SetBool("moving", false);
        }
    }



    public Vector2 setWindDirection(string direction)
    {
        if (direction == "North")
        {
            windDirection.x = 0;
            windDirection.y = 1;
        }
        if (direction == "NorthEast")
        {
            windDirection.x = 1;
            windDirection.y = 1;
        }
        if (direction == "East")
        {
            windDirection.x = 1;
            windDirection.y = 0;
        }
        if(direction == "SouthEast")
        {
            windDirection.x = 1;
            windDirection.y = -1;
        }
        if (direction == "South")
        {
            windDirection.x = 0;
            windDirection.y = -1;
        }
        if (direction == "SouthWest")
        {
            windDirection.x = -1;
            windDirection.y = -1;
        }
        if (direction == "West")
        {
            windDirection.x = -1;
            windDirection.y = 0;
        }
        if (direction == "NorthWest")
        {
            windDirection.x = -1;
            windDirection.y = 1;
        }
        //Update wind visuals here
        sailAnimator.SetTrigger(direction);

        windScript.WindChange();
        return windDirection.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "land")
        {
           
            triggerLand = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "land")
        {
            triggerLand = false;
        }
    }

}
