using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public Transform transformCamera;
    public Transform target;
    public float timeStartStop;
    public float speedMove;

    void Start()
    {
        StartCoroutine(FollowTargetProcess());
    }

    private IEnumerator FollowTargetProcess()
    {
        yield return new WaitForSeconds(timeStartStop);

        while (target != null)
        {
            transformCamera.transform.Translate(new Vector3(target.transform.position.x - transformCamera.transform.position.x, target.transform.position.y - transformCamera.transform.position.y, 0) * Time.deltaTime * speedMove);
            yield return null;
        }
    }
}
