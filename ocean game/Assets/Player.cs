using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public bool ResetPlayerToStart = false;
    public Vector2 ResetPosition;
    public bool loaded = false;
    public bool shrineWindSet = false;// set by shrine when wind changed right before nightfall
    public float offset;

    [SerializeField]
    private Tilemap map;
    [SerializeField]
    private Tilemap decorationMap;
    [SerializeField]
    private Tilemap objectMap;
    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    public bool WinterShrine = false;
    public bool DrownedShrine = false;
    public GameObject WestShrineDoor;
    public GameObject WestShrineOpener;
    
    public GameObject fireN, fireS, fireE, fireW;

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

    public Vector3 doorStore;

    public int sfxTerrainType;

    private bool triggerLand = false;
    public bool triggerCaveDecor;
    public bool triggerOcean = false;

    bool triggerCaveWater;

    private float triggerLandCount = 0;

    Animator animator;
    SpriteRenderer sRenderer;
    public windEffect windScript;

    public Animator sailAnimator;
    public GameObject sail;
    public GameObject head;
    SpriteRenderer hRenderer;

    Rigidbody2D body;
    public bool moving;
    float horizontal;
    float vertical;
    public float speed = 20.0f;
    public float boatSpeed = 4;
    public bool onLand = true;
    Vector3 prevVel = new Vector3 (0,0,0);
    public bool iceMove = false;
 


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
 
    public int northShrine = 0;
    public int southShrine = 0;
    public int eastShrine = 0;
    public int westShrine = 0;

    public int gatorState = 0;

    public int age = 1;
    public int lastShrine =99; 
    public bool snow;

    public wispManager wispManagerScript;
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.piss";
        if (System.IO.File.Exists(path))
        {
            Debug.Log("loadddd");
            PlayerData data = SaveSystem.LoadPlayer();
            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            transform.position = position;

            northShrine = data.northShrine;
            southShrine = data.southShrine;
            eastShrine = data.eastShrine;
            westShrine = data.westShrine;

            fireSet();

            gatorState = data.gatorState;
            age = data.age;
            lastShrine = data.lastShrine;

            wispManagerScript.wispCheck(age, lastShrine);

            loaded = true;
        }
    }

    public AnimatorOverrideController override1;
    public AnimatorOverrideController override2;
    public AnimatorOverrideController override3;

    void fireSet()
    {
        if (northShrine == 1)
            fireN.SetActive(true);
        else
            fireN.SetActive(false);
        if (southShrine == 1)
            fireS.SetActive(true);
        else
            fireS.SetActive(false);
        if (eastShrine == 1)
            fireE.SetActive(true);
        else
            fireE.SetActive(false);
        if (westShrine == 1)
            fireW.SetActive(true);
        else
            fireE.SetActive(false);
    }
    public void IncreaseAge(int id)
    {
        Debug.Log("id " + id);

        // 1= north, 2 = east, 3 = south, 4= west;
        if(id == 1)
        {
            if(northShrine != 1)
            {
                if(age == 4)
                {
                    lastShrine = id;
                    
                }

                age++;
                northShrine = 1;
                swapAnimator();
            }           
        }
        if (id == 2)
        {
            if (eastShrine != 1)
            {
                if (age == 4)
                {
                    lastShrine = id;
                }
                age++;
                eastShrine = 1;
                swapAnimator();
            }
        }
        if (id == 3)
        {
            if (southShrine != 1)
            {
                if (age == 4)
                {
                    lastShrine = id;
                }
                age++;
                southShrine = 1;
                swapAnimator();
            }
        }
        if (id == 4)
        {
            if (westShrine != 1)
            {
                if (age == 4)
                {
                    lastShrine = id;
                }
                age++;
                westShrine = 1;
                swapAnimator();
            }
        }
        wispManagerScript.wispCheck(age, lastShrine);
    }



    public void swapAnimator()
    {
        Debug.Log("age " + age);
        if (age ==3)
            animator.runtimeAnimatorController = override1;
        if (age == 4)
            animator.runtimeAnimatorController = override2;
        if (age>=5)
            animator.runtimeAnimatorController = override3;       
    }




    public TreasureManager treasureManagerScript;
    plantWave plantAndWaterScript;

    void Start()
    {
        plantAndWaterScript = GetComponent<plantWave>();

        hRenderer = head.GetComponent<SpriteRenderer>();
        sailAnimator = sail.GetComponent<Animator>();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
        setWindDirection(WindDirect);
        // treasure manager needs underworld and overworld active at same time to initialize
        if (inUnderworld)
        {
            Debug.Log("under");
            Overworld.SetActive(true);
            treasureManagerScript.TreasureManagerStart();
            Overworld.SetActive(false);
        }
        else
        {
            Debug.Log("Over");
            Underworld.SetActive(true);
            treasureManagerScript.TreasureManagerStart();
            Underworld.SetActive(false);
        }




        LoadPlayer(); // Load Player here on start!!

        if(westShrine == 1)
        {
            WestShrineDoor.SetActive(true);
            WestShrineOpener.SetActive(false);
        }
        if(northShrine == 1)
        {
            WinterShrine = true;
            
        }
        if(southShrine == 1)
        {
            DrownedShrine = true;
        }
        
        if (ResetPlayerToStart) 
        {
            Vector3 posRes = new Vector3(ResetPosition.x, ResetPosition.y, transform.position.z);
            transform.position = posRes;

            northShrine = 0;
            southShrine = 0;
            eastShrine = 0;
            westShrine = 0;
            age = 1;

        }

        swapAnimator();
    }






    private void OnApplicationQuit()
    {
        Debug.Log("quit");
        SavePlayer(); // Save Player on Quit!!
        if (inUnderworld) // also save treaure
        {
            Overworld.SetActive(true);
            treasureManagerScript.SaveChests();//
            Overworld.SetActive(false);
        }
        else
        {
            Underworld.SetActive(true);
            treasureManagerScript.SaveChests();//
            Underworld.SetActive(false);
        }
        
    }
    private void OnApplicationPause(bool pause)
    {        
        if (pause    == true)
        {
            Debug.Log("pause");
            SavePlayer(); // Also Save Player on Pause for Mobile!!
            if (inUnderworld) // also save treasure
            {
                Overworld.SetActive(true);
                treasureManagerScript.SaveChests();//
                Overworld.SetActive(false);
            }
            else
            {
                Underworld.SetActive(true);
                treasureManagerScript.SaveChests();//
                Underworld.SetActive(false);
            }

        }
        
    }

    

    public void SwapWorld()
    { 
        inUnderworld = !inUnderworld;
        Overworld.SetActive(!Overworld.activeSelf);
        Underworld.SetActive(!Underworld.activeSelf);
        windScript.WindChange();
        endTriggerLandIce();
        triggerCaveWater = false;
        plantAndWaterScript.endWater();

    }

    void endTriggerLandIce()
    {
        triggerLand = false;
        triggerLandCount = 0;
        iceMove = false;
        Vector3 roundTrans = new Vector3(Mathf.RoundToInt(transform.position.x * 8), Mathf.RoundToInt(transform.position.y * 8), transform.position.z * 8); // round position to nearest pixel cus ice move isnt pixel perfect
        transform.position = roundTrans / 8;
    }

    public int TerrainSoundType()
    {
        Vector3 footPos = new Vector3(body.transform.position.x, body.transform.position.y + offset, 0); // number added to y is player offset value from ysort
        Vector3Int gridPosition = objectMap.WorldToCell(footPos);
        TileBase CurrentTile = objectMap.GetTile(gridPosition);
        if(CurrentTile != null)
        {

            if (dataFromTiles.ContainsKey(CurrentTile))
            {
                //Debug.Log(dataFromTiles[CurrentTile].terrainSoundType);
                return dataFromTiles[CurrentTile].terrainSoundType;
            }
            
        }
        else
        {
             gridPosition = decorationMap.WorldToCell(footPos);
             CurrentTile = decorationMap.GetTile(gridPosition);
            if (CurrentTile != null)
            {

                if (dataFromTiles.ContainsKey(CurrentTile))
                {
                    
                    return dataFromTiles[CurrentTile].terrainSoundType;
                }

            }
        }


        return 1;
    }


    // Update is called once per frame
    public bool LandCheck()
    {
        

        // check tile data for ground
        Vector3 transOff = new Vector3(body.transform.position.x, body.transform.position.y+offset, body.transform.position.z);

        Vector3Int gridPosition = map.WorldToCell(transOff);
        TileBase CurrentTile = map.GetTile(gridPosition);




        //print("At position " + gridPosition + " tile type: "+ CurrentTile);
        bool land = false;
        if (inUnderworld && triggerOcean)
        {
            return false;
        }

        if (CurrentTile == null)
        {
            return false;
        }
        else
        {
            //Debug.Log(dataFromTiles[CurrentTile].isLand);  
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
       if (body.velocity != new Vector2(0,0))
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
       
        if (inUnderworld || triggerLandCount >=1)
        {
            onLand = true;
            if (triggerLandCount>=1 && !iceMove)
            {
                sfxTerrainType = 4;
            }
            if (inUnderworld && !triggerCaveDecor)
            {
                sfxTerrainType = 3;
            }
            if (triggerCaveDecor)
            {
                sfxTerrainType = 1;
            }           
            if (iceMove)
            {
                sfxTerrainType = 6;
            }
            if (triggerLand && !iceMove)
            {
                sfxTerrainType = 4;
            }
            if (triggerCaveWater)
            {
                sfxTerrainType = 5;
            }
        }
        else
        {
            onLand = LandCheck();
            sfxTerrainType = TerrainSoundType();
        }
        if (!inUnderworld)
        {
            triggerOcean = false;
        }
        if (triggerOcean)
        {
            onLand = false;
        }
        /* 
        if (Input.GetButtonDown("Fire1"))
        {
            
            setWindDirection(WindDirect);
        }
        */
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        sailAnimator.SetFloat("windSpeed", windSpeed);
        sailAnimator.SetTrigger(WindDirect);
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
            prevVel = playerDirection;
            velstore = playerDirection;
            velstore = playerDirection;
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            if (onLand)
            {
                if (triggerOcean)
                {
                    boatMove();
                }
                else
                {
                    landMove();
                }
                
            }
            else
            {
                
                boatMove();
            }

            

        }
        
        
    }

    Vector2 velstore = new Vector2(0f, 0f);
    void landMove()
    {
       
        if (horizontal != 0 || vertical != 0)
        {
            body.velocity = new Vector2(horizontal * speed, vertical * speed).normalized * speed;

            body.velocity = new Vector2(Mathf.RoundToInt(body.velocity.x * 8), Mathf.RoundToInt(body.velocity.y * 8));
            body.velocity = body.velocity / 8;
            velstore = body.velocity;
            animator.SetBool("moving", true);
            if (iceMove)
            {
                iceLerp();
            }

        }
        else
        {
            animator.SetBool("moving", false);

            if (!iceMove)
            {
                body.velocity = Vector2.zero;
            }
            else
            {
                velstore = Vector2.zero;
                iceLerp();
            } 
        } 
        prevVel = body.velocity;
    }

    public float iceLerpValMult = 5;
    void iceLerp()
    {
        float dotP = Vector2.Dot(prevVel.normalized, velstore.normalized)*2 ;
       
        body.velocity = Vector2.Lerp(prevVel, velstore, (dotP+iceLerpValMult)*Time.deltaTime);       
    }


    void boatMove()
    {
        if (horizontal != 0 || vertical != 0)
        {
            playerDirection = new Vector2(horizontal * boatSpeed, vertical * boatSpeed).normalized;
            float windDot = Vector2.Dot(playerDirection, windDirection);
            if (triggerOcean)
            {
                windDot = Vector2.Dot(playerDirection, new Vector2(playerDirection.x*-1f,playerDirection.y*-1f));
            }

            windDot = AgainstwindSpeed + (windDot + 1f) * (WithWindSpeed - AgainstwindSpeed)/ (1f - -1f);

            playerDirection = new Vector2 (playerDirection.x*(boatSpeed+ windSpeed*(windDot*windDot)), playerDirection.y * (boatSpeed + windSpeed * (windDot * windDot)));
            playerDirection = new Vector2(Mathf.RoundToInt(playerDirection.x * 8), Mathf.RoundToInt(playerDirection.y* 8));


            body.velocity = playerDirection/8;
            animator.SetBool("moving", true);
            velstore = Vector2.zero;
            if (iceMove)
            {
                iceLerp();
            }
        }
        else
        {
            if (iceMove)
            {
                velstore = Vector2.zero;
                iceLerp();
            }
            playerDirection = Vector2.zero;
            body.velocity = playerDirection;
            animator.SetBool("moving", false);
        }
        prevVel = new Vector2 (Mathf.Clamp(body.velocity.x,-10f,10f), Mathf.Clamp(body.velocity.y, -10f, 10f));
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
        print(direction + " wind blows!");
        WindDirect = direction;
        windScript.WindChange();
        return windDirection.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "land")
        {
           
            triggerLand = true;
            triggerLandCount += 1;
        }
        if(other.tag == "caveDecor")
        {
            triggerCaveDecor = true;
        }
        if (other.tag == "caveWater")
        {
            triggerCaveWater = true;
        }
        if(other.tag == "ice")
        {

            triggerLand = true;
            triggerLandCount += 1;
            iceMove = true;
        }
        if(other.tag == "ocean")
        {
            triggerOcean = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "land")
        {
            triggerLand = false;
            triggerLandCount -= 1;
        }
        if (other.tag == "caveDecor")
        {
            triggerCaveDecor = false;
        }
        if (other.tag == "caveWater")
        {
            triggerCaveWater = false;
        }
        if (other.tag == "ice")
        {
            triggerLand = false;
            triggerLandCount -= 1;
            iceMove = false;
            Vector3 roundTrans = new Vector3(Mathf.RoundToInt(transform.position.x * 8), Mathf.RoundToInt(transform.position.y * 8), transform.position.z * 8); // round position to nearest pixel cus ice move isnt pixel perfect
            transform.position = roundTrans / 8;

        }
        if (other.tag == "ocean")
        {
          //  triggerOcean = false;
        }

    }

}
