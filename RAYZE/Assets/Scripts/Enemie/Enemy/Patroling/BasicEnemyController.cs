using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
  

    // Code arbeitet mit einer state machine!!
    private enum state
     {
        idle,
        walking,
        knockback,
        dead
     }

    private state currentState;


    private void Update()
    {
        switch (currentState) 
        {
            case state.walking:
                updateWalkingState();
            break;
            case state.knockback:
                updateKnockbackState();
            break;
            case state.dead:
                updateDeadState();
            break;
        }
    }


    //------Walking/running-- State------------------------------------------

    private void enterWalkingState() 
    { 
    
    }

    private void updateWalkingState() 
    {
    
    }

    private void exitWalkingState() 
    {
    
    }

    //---Knockback/hurt---State-----------------------------------------------
    private void enterKnockbackState() 
    { 
    
    }

    private void updateKnockbackState() 
    {
    
    }
    
    private void exitKnockbackstate() 
    {
    
    }

    private void enterDeadState() 
    {
    
    }

    private void updateDeadState()
    {

    }

  
}
