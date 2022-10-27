using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThangMayBehaviour : MonoBehaviour
{
    public GameObject player;
    public float moveSpeedX=0;
    public float moveSpeedY=0;
    private int frame=1;
    public int TimeStop=100;
    public int TimeMove=900;
    private int endTime;
    public Transform PlayerOnCheck;
    public Vector2 PlayerOnCheckSize;
    public LayerMask PlayerOnCheckLayer;
    private bool PlayerIsOn = false;
    private Collider2D so_Collider;
    // Start is called before the first frame update
    void Start()
    {
        endTime=TimeMove*2+TimeStop;   
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(so_Collider);
        so_Collider= Physics2D.OverlapBox(PlayerOnCheck.position, PlayerOnCheckSize, 0, PlayerOnCheckLayer);
        PlayerIsOn = Physics2D.OverlapBox(PlayerOnCheck.position, PlayerOnCheckSize, 0, PlayerOnCheckLayer);
        move();
    }
    private void move(){
        if(frame<=TimeMove && frame>0){
            if(PlayerIsOn) playerMove(1);
            transform.position = new Vector2(transform.position.x+moveSpeedX/100, transform.position.y+moveSpeedY/100);
        }
        else if(frame<=endTime && frame>TimeMove+TimeStop){
            transform.position = new Vector2(transform.position.x-moveSpeedX/100, transform.position.y-moveSpeedY/100);
            if(PlayerIsOn) playerMove(2);
        }
        else if(frame==endTime+1) frame=-TimeStop;
        frame++;
    }
    private void playerMove(int trangThai){
        switch(trangThai){
            case 1:
                player.transform.position=new Vector2(player.transform.position.x+moveSpeedX/100, player.transform.position.y+moveSpeedY/100);
                break;
            case 2:
                player.transform.position=new Vector2(player.transform.position.x-moveSpeedX/100, player.transform.position.y-moveSpeedY/100);
                break;
            default:
                break;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(PlayerOnCheck.position, PlayerOnCheckSize);
    }
}
