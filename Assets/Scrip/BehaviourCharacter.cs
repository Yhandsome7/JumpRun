using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourCharacter : MonoBehaviour
{
    
    [SerializeField] private float movespeed = 7f;
    [SerializeField] private float jumpspeed = 14f;
    private float addgravitation = 1f;
    private Rigidbody2D Gravitation;
    private int maxjump = 1;
    private bool grouded = true;
    // Update is called once per frame
    private int countjump;
    private void Start() {
        Gravitation = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
           Run();
        //   Jump();
        if(Input.GetKeyDown(KeyCode.Space)){
            this.Dont_Jump_Twice();
        }
    }
    private void Dont_Jump_Twice(){
        if(countjump > 0 ){
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,movespeed),ForceMode2D.Impulse);
            grouded = false;
            countjump = countjump - 1;
        }
        if(countjump == 0){
            return;
        }
    }
     private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "backgroud"){
            countjump = maxjump;
            grouded = true;
            movespeed = 7.5f;
        }    
    }
    private void Run(){
        float move  = Input.GetAxis("Horizontal")*movespeed*Time.deltaTime;
        transform.Translate(move,0,0); 
    }
    private void Jump(){
        float jump = Input.GetAxis("Jump")*jumpspeed*Time.deltaTime;
        transform.Translate(0,jump,0);
    }
}
