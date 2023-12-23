using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {

    float speed;
    bool turning = false;

    void Start() {

        speed = Random.Range(FlockManager.FM.minSpeed, FlockManager.FM.maxSpeed); //set speed random
    }


    void Update() {
        // make a collision box to contain the fish so it didn't fly out of map , gonna try to make it random later
        Bounds b = new Bounds(FlockManager.FM.transform.position, FlockManager.FM.swimLimits * 2.0f);

        if (!b.Contains(transform.position)) {

            turning = true; // quay dau nka

        } else {

            turning = false;
        }

        if (turning) {

            Vector3 direction = FlockManager.FM.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                FlockManager.FM.rotationSpeed * Time.deltaTime);
        } else {


            if (Random.Range(0, 100) < 10) {

                speed = Random.Range(FlockManager.FM.minSpeed, FlockManager.FM.maxSpeed); // random reset speed
            }


            if (Random.Range(0, 100) < 10) {
                ApplyRules(); //turn the fish around towarnd the direction it need to be travel in
            }
        }

        this.transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
    }

    private void ApplyRules() {

        List<GameObject> gos;
        gos = FlockManager.FM.allFish;

        Vector3 vCentre = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;

        float gSpeed = 0.01f;
        float mDistance;
        int groupSize = 0;

        foreach (GameObject go in gos) {

            if (go != this.gameObject) {

                mDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if (mDistance <= FlockManager.FM.neighbourDistance) {

                    vCentre += go.transform.position;
                    groupSize++;

                    if (mDistance < 1.0f) {

                        vAvoid = vAvoid + (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0) {

            vCentre = vCentre / groupSize + (FlockManager.FM.goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            if (speed > FlockManager.FM.maxSpeed) {

                speed = FlockManager.FM.maxSpeed;
            }

            Vector3 direction = (vCentre + vAvoid) - transform.position;
            if (direction != Vector3.zero) {

                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(direction),
                    FlockManager.FM.rotationSpeed * Time.deltaTime);
            }
        }
    }
}