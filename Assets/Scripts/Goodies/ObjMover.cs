using System.Collections;
using UnityEngine;

public class ObjMover : MonoBehaviour
{
    [SerializeField] private AnimationCurve chargeCurve;
    [SerializeField] private float fireRate;
    [SerializeField] private float shotForce;
    [SerializeField] private float maxForce;
    [SerializeField] private float minForce;
    [SerializeField] private float chargeTime;
    public BoxTarget2D box;
    [SerializeField] AnimationCurve fallCurve;
    public Transform GrabTarget;

    bool canFire = true;

    IEnumerator routine;
    IEnumerator chargeRoutine;

    //float waitTime = 0;

    public void StartShotCooldown()
    {
        if (routine == null)
        {
            routine = FireRate();
            StartCoroutine(routine);
        }
    }

    public void StopShotCooldown()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
    }

    public void StartCharge(Input p)
    {
        if (chargeRoutine == null)
        {
            chargeRoutine = BuildForce(p);
            StartCoroutine(chargeRoutine);
        }
    }

    public void StopCharge()
    {
        if (chargeRoutine != null)
        {
            StopCoroutine(chargeRoutine);
            chargeRoutine = null;
        }
    }

    public IEnumerator BuildForce(Input p)
    {
        shotForce = minForce;
        float time = 0;
        while (time < chargeTime)
        {
            float percent = time / chargeTime;
            time += Time.deltaTime;
            shotForce = Mathf.LerpUnclamped(minForce, maxForce, chargeCurve.Evaluate(percent));
            Debug.Log(shotForce);
            yield return null;
        }
     
    }

    IEnumerator FireRate()
    {
        canFire = false;
        float timeBetweenShots = 1f / fireRate;
        yield return new WaitForSeconds(timeBetweenShots);
        canFire = true;
    }

    public void FireBullet(Transform obj,Transform start,Transform end)
    {
        if (canFire)
        {

            GrabTarget = obj;
            GrabTarget.GetComponent<BoxCollider2D>().isTrigger = true;
            StartCoroutine(BulletTravel(obj, start.position, end.position));
            StartShotCooldown();
        }        
    }

    IEnumerator BulletTravel(Transform obj, Vector3 start, Vector3 end)
    {
        float time = 0;
        while(time <= shotForce)
        {
            float percent = time / shotForce;
            time += Time.deltaTime;
            end.y = Mathf.Lerp(start.y, obj.position.y, fallCurve.Evaluate(percent));
            obj.position = Vector3.Lerp(start, end, percent);
            if (percent > .9)
            {
                GrabTarget.GetComponent<BoxCollider2D>().isTrigger = false;
                GrabTarget = null;
            }

            yield return null;
        }
    }
}
