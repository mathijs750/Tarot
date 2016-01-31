using UnityEngine;
using System.Collections;

public class StartAnimation : MonoBehaviour {

    bool dissapear = true;

	// Update is called once per frame
	void Update () {
        Renderer render = gameObject.GetComponent<Renderer>();
        if (dissapear)
            render.material.color = new Color(1, 1, 1, render.material.color.a - (1 * Time.deltaTime));
        else
            render.material.color = new Color(1, 1, 1, render.material.color.a + (1 * Time.deltaTime));

        if (render.material.color.a < .2f)
            dissapear = false;
        else if (render.material.color.a > .9f)
            dissapear = true;
    }
}
