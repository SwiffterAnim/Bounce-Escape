using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricWallStarter : MonoBehaviour
{
    [SerializeField]
    private GameObject topElectricStarter;

    [SerializeField]
    private GameObject bottomElectricStarter;

    [SerializeField]
    private GameObject electricBeam;

    [SerializeField]
    private float beamFrequency = 2f;

    [SerializeField]
    private float minSize = 2f;

    [SerializeField]
    private float maxmSize = 4f;

    [SerializeField]
    private float delayToDestroy = 17f;

    // Start is called before the first frame update
    void Start()
    {
        float randomSize = Random.Range(minSize, maxmSize);

        topElectricStarter.transform.localPosition = new Vector3(0, randomSize, 0);

        bottomElectricStarter.transform.localPosition = new Vector3(0, -randomSize, 0);

        Vector3 electricBeamSize = electricBeam.transform.localScale;
        electricBeam.transform.localScale = new Vector3(
            electricBeamSize.x,
            randomSize * 2,
            electricBeamSize.z
        );

        StartCoroutine(ToggleElectricBeam());
        StartCoroutine(DestroyElectricWall());
    }

    private IEnumerator ToggleElectricBeam()
    {
        while (true)
        {
            electricBeam.SetActive(!electricBeam.activeSelf);
            yield return new WaitForSeconds(beamFrequency);
        }
    }

    private IEnumerator DestroyElectricWall()
    {
        yield return new WaitForSeconds(delayToDestroy);
        Destroy(gameObject);
    }
}
