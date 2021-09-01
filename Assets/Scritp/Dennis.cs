using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dennis : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animation_denis;
    [SerializeField] private int health; 
    [SerializeField] public DavidMovement david;

    // private NavMeshAgent navMeshAgent;
    private enum State{
        Idle,
        Hurt,

    }
    private void Awake() {
        animation_denis = GetComponent<Animator>();
        // navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Start() {
        health = 10;     
    }
    private void Update() {
        // navMeshAgent.destination = david.GetPosition();
    }

    public void Damage(Vector3 attackerPositon){
        health -=1;
        Vector3 dirToAttacker = (attackerPositon - GetPosition_Dennis()).normalized;
        

        if (IsDead()){
            Destroy(gameObject);
        }
        else{
            // float knockBackDistance = 2f;
            // transform.position += dirToAttacker*1f *knockBackDistance;
            animation_denis.Play("Hurt1_dennis_anim");
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
        // Debug.Log("damage");

        }
    }
    
}
    // Update is called once per frame


