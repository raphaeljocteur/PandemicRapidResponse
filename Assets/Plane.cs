using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{

    [SerializeField] SO_Plane data;

    Vector3 destination;
    bool m_hasReached = true;
    [SerializeField] float m_time = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if(m_hasReached && destination != data.destination.position)
        {
            destination = data.destination.position;
            StartCoroutine(Travel(m_time));
        }
    }

    private IEnumerator Travel(float time)
    {

        Vector3 start = transform.position;
        float timer = 0;
        m_hasReached = false;

        while (timer < time)
        {
            transform.position = Vector3.Lerp(start, destination, timer / time);
            timer += Time.deltaTime;
            yield return null;
        }

        m_hasReached = true;
        yield return null;
    }
}
