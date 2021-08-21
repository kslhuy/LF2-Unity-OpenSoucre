using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dennis : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animation_denis;
    [SerializeField] private int health; 
    [SerializeField] public Mouvement_Player1 david;
    private void Awake() {
        animation_denis = GetComponent<Animator>();
    }
    private void Start() {
        health = 3;     
    }

    public void Damage(Vector3 attackerPositon){
        health -=1;
        Vector3 dirToAttacker = (attackerPositon - GetPosition_Dennis()).normalized;
        animation_denis.Play("Hurt1_dennis_anim");

        if (IsDead()){
            Destroy(gameObject);
        }
    }

    public bool IsDead(){
        return health <= 0;
    }



    public Vector3 GetPosition_Dennis(){
        return transform.position;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "HitBox"){
            Damage(david.GetPosition());
            return;
        }
    }
    
}
    // Update is called once per frame


