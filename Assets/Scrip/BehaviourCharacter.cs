using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BehaviourCharacter : MonoBehaviour
{
    
    [SerializeField] private float movespeed = 7f;
    [SerializeField] private float jumpspeed = 7f;
    private Rigidbody2D player;
    private Animator playerAnimation;
    public int maxjump;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool grouded = true;
    // Update is called once per frame
    private int countjump=0;

    private Vector3 respawnPoint;
    public GameObject fallDetector;
    private void Start() {
        player = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        respawnPoint= transform.position;
    }
    private void Update()
    {   
        grouded=Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        Run();
        if(Input.GetKeyDown(KeyCode.Space)){
            if(grouded==true){
                countjump=0;   
            }
            this.Dont_Jump_Max_Times();
        }
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
        playerAnimation.SetFloat("speed", Mathf.Abs(player.velocity.x));
        playerAnimation.SetBool("onGround", grouded);
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag=="FallDetector"){
            transform.position=respawnPoint;
        }
        if(collision.tag=="Portal"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            respawnPoint = transform.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D other) { 
    }
    private void Dont_Jump_Max_Times(){
        if(grouded==true){
            Jump();
            countjump = countjump + 1;
        }
        else if(countjump < maxjump && grouded==false){
            Jump();
            countjump = countjump + 1;
        }
        if(countjump == maxjump){
            return;
        }
    }
     
    private void Run(){
        float move  = Input.GetAxis("Horizontal")*movespeed;
        if(move > 0f){
            player.velocity=new Vector2(move, player.velocity.y);
            player.transform.localScale = new Vector2(0.3006162f, 0.2518356f);
        }
        else if(move <0f){
            player.velocity=new Vector2(move, player.velocity.y);
            player.transform.localScale = new Vector2(-0.3006162f, 0.2518356f);
        }
        else{
            player.velocity=new Vector2(0f, player.velocity.y);
        }
    }
    private void Jump(){
        player.velocity= new Vector2(player.velocity.x, jumpspeed);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
