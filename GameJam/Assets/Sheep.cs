using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public Transform _target;
    
    public float _sheepSightRadius = 20.0f;
    public float _sheepSpeed = 2.0f;
    public float _sheepDamageOutput = 5.0f;

    private float _changeDirectionTime = 0.0f;
    private float _timeBeforeDirectionChange = 6.0f;
    private Vector3 _wanderDirection;
    private Vector3 _foodDirection;
    private SphereCollider _sheepDetectionSphere;

    private enum SheepState
    {
        AFRAID,
        WANDERING,
        CHASE_FOOD
    }

    private SheepState _currentState = SheepState.WANDERING;
  

    Sheep(Transform targetTransformParam, float speedParam, float sightRadiusParam)
    {
        _target = targetTransformParam;
        _sheepSpeed = speedParam;
        _sheepSightRadius = sightRadiusParam;
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentState = SheepState.WANDERING; 
        _wanderDirection = pickRandomDirection();

        _timeBeforeDirectionChange = UnityEngine.Random.Range(6.0f, 12.0f);
        
        _sheepDetectionSphere = (SphereCollider)this.gameObject.AddComponent(typeof(SphereCollider));
        _sheepDetectionSphere.radius = _sheepSightRadius;
        _sheepDetectionSphere.isTrigger = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        moveBasedOnCurrentState();
    }

    void OnTriggerEnter(Collider other)
    {
        //Object needs to have a rigidbody for collisions.
        if (other.gameObject.CompareTag("Player"))
        {
            _currentState = SheepState.AFRAID;
        }

        if (other.gameObject.CompareTag("Crop") && _currentState != SheepState.CHASE_FOOD)
        {
            _currentState = SheepState.CHASE_FOOD;
            _foodDirection = (other.gameObject.transform.position - this.transform.position).normalized;
            _foodDirection.y = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _currentState = SheepState.AFRAID;
            return;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Object needs to have a rigidbody for collisions.
        if (other.gameObject.CompareTag("Player"))
        {
            _currentState = SheepState.WANDERING;
        }

        if (other.gameObject.CompareTag("Crop"))
        {
            _currentState = SheepState.WANDERING;
        }
    }


    //Move the sheep based on it's current state.
    void moveBasedOnCurrentState()
    {
        Debug.Log("My current state: " + _currentState);

        switch (_currentState)
        {
            case SheepState.AFRAID:
                runFromTarget();
                break;
            case SheepState.CHASE_FOOD:
                runToFood();
                break;
            case SheepState.WANDERING:
                wander();
                break;
        }
    }

    //Travels in a random direction. Changes direction after a certain amount of time.
    private void wander()
    {
        if (_changeDirectionTime > _timeBeforeDirectionChange)
        {
            _wanderDirection = pickRandomDirection();
            _changeDirectionTime = 0.0f;
        }
        

        this.transform.Translate(_wanderDirection * _sheepSpeed * Time.deltaTime);
        _changeDirectionTime += Time.smoothDeltaTime;
    }


    //Pick a random direction to travel in.
    private Vector3 pickRandomDirection()
    {
       Vector3 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
       randomDirection.y = 0;
       return randomDirection;
    }

    //Run towards the last seen food.
    private void runToFood()
    {
        this.transform.Translate(_foodDirection.normalized * _sheepSpeed * Time.deltaTime);
    }

    //Run away from the _target parameter (the player).
    private void runFromTarget()
    {
        Vector3 moveDirection = this.transform.position - _target.transform.position;
        moveDirection.y = 0;

        this.transform.Translate(moveDirection.normalized * _sheepSpeed * Time.deltaTime);
    }
}
