using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class DavidBall : MonoBehaviour
{
    public static event EventHandler OnBallHitTarget;
    public static DavidBall Create(Vector3 positon , Vector3 direction){
        // Transform davidBallTransform =  Instantiate(GameAsset.instance.pfDavidBall,positon , Quaternion.identity);
        Transform davidBallTransform =  Instantiate(GameAsset.instance.pfDeepDLA,positon , Quaternion.identity);

        davidBallTransform.eulerAngles = new Vector3(0,0,UtilsClass.GetAngleFromVectorFloat(direction));
        DavidBall davidBall = davidBallTransform.GetComponent<DavidBall>();
        davidBall.Setup(direction);
        return davidBall;
    }
    private float SPEED = 1f;
    private const float GainIncreasBallSpeed = 4f;
    private const float Distance_travelMAX = 30f;
    private float distanceTravalled;
    private Vector3 dir;

    SpriteRenderer spriteRenderer;
    Animator anim;

    private void Awake() {
        // Setup(new Vector3(-1 ,0,0));
        anim = GetComponent<Animator>();
    }

    private void Setup(Vector3 dir){

        this.dir = dir; 
        
    }
    private void Update() {
        SPEED += SPEED * Time.deltaTime * GainIncreasBallSpeed;
        transform.position += dir*SPEED*Time.deltaTime;
        distanceTravalled += SPEED*Time.deltaTime;
        if (SPEED >= 2f){
            SPEED = 2f;
        }
        
        if (distanceTravalled > Distance_travelMAX){
            // Ball travlled too much , destroy this
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider collider) {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable != null){
            Debug.Log(damageable);
            // Destroy(gameObject);
        }
        // if (other.gameObject.tag == "Hittable" || other.gameObject.tag == "Player2"){
        //     // if (OnBallHitTarget !=null) OnBallHitTarget(this , EventArgs.Empty);
                // anim.Play("ball_hit");
                // Invoke("Destroythis",0.33f);
                
            // Debug.Log("True");
        }
}


