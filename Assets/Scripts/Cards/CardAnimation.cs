using UnityEngine;
using System.Collections;

public class CardAnimation : MonoBehaviour {

    Vector3 startPosition = new Vector3(0, -235, -20);
    Vector3 midDestination = new Vector3(0, -201, -35);
    Vector3[] destinations = new Vector3[] { new Vector3(-28.5f, -191, -20), new Vector3(-28.5f, -211, -20), new Vector3(28.5f, -191, -20), new Vector3(28.5f, -211, -20) };
    Vector3 destination = new Vector3(-30, -230, -2);

    float originalUnit = 40;
    float unit = 40;
    float freq = .7f;


    public void StartTransition(int final)
    {
        StartCoroutine(TransitionCard(final));
    }

	public IEnumerator TransitionCard(int finalDestination)
    {
        Vector3 finalDestiny = destinations[finalDestination];

        float counter = 0;
        
        while (counter < 1)
        {
            transform.position = Vector3.Lerp(startPosition, midDestination, counter);

            counter += Time.deltaTime / .6f;

            yield return null;
        }

        transform.position = midDestination;

        yield return new WaitForSeconds(1);
        counter = 0;

        while (counter < 1)
        {
            transform.position = Vector3.Lerp(midDestination, finalDestiny, counter);

            counter += Time.deltaTime / .6f;

            yield return null;
        }

        transform.position = finalDestiny;
    }

    public void StartSpiral()
    {
        StartCoroutine(spiralAnimation());
    }

    private IEnumerator spiralAnimation()
    {
        unit = originalUnit;

        while (unit > 0)
        {
            float x = unit * Mathf.Cos(unit * freq);
            float y = -208 + (unit * Mathf.Sin(unit * freq));
            transform.position = new Vector3(x, y, -2);
            unit -= Time.deltaTime * 10;

            float newScaleX = 1f / originalUnit * unit;
            float newScaleZ = 2f / originalUnit * unit;
            transform.localScale = new Vector3(newScaleX, 1, newScaleZ);

            yield return null;
        }

        Destroy(gameObject);
    }
}
