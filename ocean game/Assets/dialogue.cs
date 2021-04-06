using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public GameObject bubble;
    private int index;
    float typingSpeed = 0.02f;
    public bool talking = false;
    private string sentences;
    private bool mirrored;
    private Vector3 offset;
    private bool inTrigger = false;
    public Player playerScript;
    public Vector3 textOffset;

    private npcDialogue npcScript = null;

    private AudioSource sor;
    [SerializeField]
    private AudioClip[] clips;
    Vector2 pitchRange;
    void Start()
    {
        sor = GetComponent<AudioSource>();
    }


    IEnumerator Type()
    {
        foreach (char letter in sentences)
        {
            sor.pitch = (Random.Range(pitchRange.x, pitchRange.y));
            sor.PlayOneShot(clips[UnityEngine.Random.Range(0, clips.Length)]);
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
            wait();



        }
        talking = false;
        playerScript.canMove = true;
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        talking = false;
        
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
            
        }
        else
        {
            textDisplay.text = "";
            index = 0;
            
            
        }
    }

    private void EndText()
    {
        
        bubble.SetActive(false);
        textDisplay.text = "";
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "npc")
        {
            inTrigger = true;
            playerScript.canMove = false;
            playerScript.playerDirection = new Vector2(0, 0); ;
            npcScript = collision.GetComponent<npcDialogue>();
            sentences =npcScript.getSentences();
            mirrored = npcScript.getMirrored();
            typingSpeed = npcScript.getTalkSpeed();
            pitchRange = npcScript.getPitchRange();
            offset = npcScript.getOffset();
            bubble.transform.position = collision.transform.position;
            bubble.transform.position = bubble.transform.position + offset;
            textDisplay.transform.position = bubble.transform.position + textOffset;

            bubble.SetActive(true);
            if (mirrored)
            {
                bubble.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                bubble.GetComponent<SpriteRenderer>().flipX = false;
            }


            StartCoroutine(Type());
            talking = true;
            
        }
    }

    private void OnTriggerExit2D( Collider2D collision)
    {
        if( collision.tag == "npc")
        {
            inTrigger = false;
            
        }

    }

    
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if(!inTrigger && !talking)
        {
            EndText();

        }
    }
}
