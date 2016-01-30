using UnityEngine;
using System.Collections;

public class TransitionTest : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            TransitionManager.Instance.StartTransition(TransitionManager.TransitionType.GameToGlobe);

        if (Input.GetKeyDown(KeyCode.Backspace))
            TransitionManager.Instance.StartTransition(TransitionManager.TransitionType.GlobeToGame);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            TransitionManager.Instance.StartTransition(TransitionManager.TransitionType.GlobeGameToCam);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            TransitionManager.Instance.StartTransition(TransitionManager.TransitionType.GlobeCamToGame);
	}
}
