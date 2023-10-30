using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private float Force = 0f;
    [SerializeField] private Vector3 Offset = Vector3.zero;

    private Quaternion originRot;

    private void Start()
    {
        originRot = transform.rotation;
    }

    public void StartShake()
    {
        StartCoroutine(ShakeCorutine());
    }
    public void ResetShake()
    {
        StopAllCoroutines();
        StartCoroutine(ResetCamera());
    }
    IEnumerator ShakeCorutine()
    {
        Vector3 originEuler = transform.eulerAngles;
        while (true)
        {
            float rotX = Random.Range(-Offset.x, Offset.x);
            float rotY = Random.Range(-Offset.y, Offset.y);

            Vector3 randomRot = originEuler + new Vector3(rotX, rotY, 0);
            Quaternion rot = Quaternion.Euler(randomRot);

            while(Quaternion.Angle(transform.rotation, rot) > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, Force * Time.deltaTime);
                yield return null;
            }
            yield return null;
        }
    }
    IEnumerator ResetCamera()
    {
        while(Quaternion.Angle(transform.rotation, originRot) > 0f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, originRot,Force*Time.deltaTime);
            yield return null;
        }
    }
}
