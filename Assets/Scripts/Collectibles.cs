using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    [SerializeField]
    bool isCoin;
    [SerializeField]
    GameObject fuelParticles, coinParticles;

    float rotationalSpeed = 120f;
    float time;
    float floatingSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;
        if (time <= 1)
        {
            transform.Translate(Vector3.up * floatingSpeed * Time.deltaTime, Space.World);
        }
        else if(time > 1)
        {
            transform.Translate(Vector3.up * -floatingSpeed * Time.deltaTime, Space.World);
            if(time > 2)
                time = 0;
        }

        if (isCoin)
        {
            transform.Rotate(Vector3.up * rotationalSpeed * Time.deltaTime,Space.World);
        }
    }

    public IEnumerator CollectibleParticles()
    {
        transform.GetComponent<Renderer>().enabled = false;
        GameObject particles;
        if (isCoin)
        {
            particles = Instantiate(coinParticles, transform.position, Quaternion.identity);
        }
        else
        {
            particles = Instantiate(fuelParticles, transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(2f);

        Destroy(particles);

    }
}
