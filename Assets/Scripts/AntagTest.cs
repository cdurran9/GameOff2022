using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntagTest : MonoBehaviour
{
    public string[] dispositions;
    public int chosenDisposition;
    public GameObject protag;
    [Range(0f, 0.005f)]
    public float moveSpeed;
    private bool cooldown = false;
    [Range(0f, 1f)]
    public float attackSpeed;
    [Range(0.2f, 1f)]
    public float jukeInterval;
    public float preferredDistance = 7f;
    private float dist = 0f;

    IEnumerator Cooldown(){
        attackSpeed = Random.Range(0.02f, 0.75f);
        Debug.Log("New attackSpeed is: " + attackSpeed);
        yield return new WaitForSeconds(attackSpeed);
        cooldown = false;
    }

    IEnumerator Jukes(){
        yield return new WaitForSeconds(jukeInterval);
        chosenDisposition = 3;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (chosenDisposition){
            case 0:
                Pursue("aggressive");
                break;
            case 1:
                Pursue("coward");
                break;
            case 2:
                Pursue("boo");
                break;
            case 3: 
                Pursue("jukes");
                break;
            default:
                Debug.Log("ChosenDisposition switch is borked");
                break;
        }

        dist = Vector3.Distance(protag.transform.position, transform.position);
        if(dist < 3 && !cooldown){
            Attack();
        }

    }

    private void Pursue(string type){
        switch (type){
            case "aggressive":
                moveSpeed = Mathf.Abs(moveSpeed);
                Chase();
                break;

            case "coward":
                KeepDistance();
                break;

            case "jukes":
                jukeInterval = Random.Range(0.2f, 1f);
                StartCoroutine(Jukes());
                chosenDisposition = Random.Range(0, 2);
                break;

            case "boo":
                moveSpeed = Mathf.Abs(moveSpeed);
                if((protag.GetComponent<ProtagTest>().turnDir == "left" && this.transform.position.x > protag.transform.position.x) ||
                    (protag.GetComponent<ProtagTest>().turnDir == "right" && this.transform.position.x < protag.transform.position.x)){
                        Chase();
                    }
                    break;

            default:
                Debug.Log("Not movin");
                break;
        }
    }

    private void Chase(){
        if(protag.transform.position.x > this.transform.position.x){
            this.transform.Translate(moveSpeed, 0, 0);
        }
        else if(protag.transform.position.x < this.transform.position.x){
            this.transform.Translate(-moveSpeed, 0, 0);
        }
        if(protag.transform.position.y > this.transform.position.y){
            this.transform.Translate(0, moveSpeed, 0);
        }
        else if(protag.transform.position.y < this.transform.position.y){
            this.transform.Translate(0, -moveSpeed, 0);
        }
    }

    private void Attack(){
        cooldown = true;
        Debug.Log("Swing!");
        StartCoroutine(Cooldown());
    }

    private void KeepDistance(){
        if(dist < preferredDistance){
            moveSpeed = (Mathf.Abs(moveSpeed) * -1);
            Chase();
        }
    }
}
