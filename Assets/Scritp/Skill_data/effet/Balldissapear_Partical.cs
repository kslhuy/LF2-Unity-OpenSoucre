using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Balldissapear_Partical {
    public static void Init(){
        DavidBall.OnBallHitTarget += DavidBall_OnBallHitTarget;
    }

    private static void DavidBall_OnBallHitTarget(object sender, System.EventArgs e){
        return;
    }
}
