using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 // Source https://www.youtube.com/watch?v=Sls8zlONwRk SidMakesGames
public class AiMove360 : MonoBehaviour
{   
       public float moveSpeed;             //Bewegungsgeschwindigkeit des Objektes
        public GameObject[] wayPoints;     // Array der Punkte erstellt, die abgelaufen werden sollen!

        int nextWaypoint = 1;
        float distToPoint;

        void FixedUpdate()
        {
           
        }


        void Update()
        {
            Move();
        }

        void Move()
        {

            distToPoint = Vector2.Distance(transform.position, wayPoints[nextWaypoint].transform.position);
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[nextWaypoint].transform.position, moveSpeed * Time.deltaTime);

            if (distToPoint < 0.2f)
            {
                TakeTurn();
            }

        }

        void TakeTurn()
        {
            Vector3 currRot = transform.eulerAngles;
            currRot.z += wayPoints[nextWaypoint].transform.eulerAngles.z;
            transform.eulerAngles = currRot;
            ChooseNextWaypoint();
        }

        void ChooseNextWaypoint()
        {
            nextWaypoint++;
            if (nextWaypoint == wayPoints.Length)
            {

                nextWaypoint = 0;

            }
        }

}



