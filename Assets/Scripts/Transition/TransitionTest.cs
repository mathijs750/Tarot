using UnityEngine;
using System.Collections;

public class TransitionTest : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            CardManager.Instance.ReadCards();

        //if (Input.GetKeyDown(KeyCode.Space))
        //    TransitionManager.Instance.StartTransition(TransitionManager.TransitionType.GameToGlobe);

        //if (Input.GetKeyDown(KeyCode.Backspace))
        //    TransitionManager.Instance.StartTransition(TransitionManager.TransitionType.GlobeToGame);

        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //    TransitionManager.Instance.StartTransition(TransitionManager.TransitionType.GlobeGameToCam);

        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //    TransitionManager.Instance.StartTransition(TransitionManager.TransitionType.GlobeCamToGame);

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    CardAnimation anim = Instantiate(Resources.Load<GameObject>("Prefabs/Cards/Card_01")).AddComponent<CardAnimation>();
        //    anim.StartTransition(0);
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    CardAnimation anim = Instantiate(Resources.Load<GameObject>("Prefabs/Cards/Card_02")).AddComponent<CardAnimation>();
        //    anim.StartTransition(1);
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    CardAnimation anim = Instantiate(Resources.Load<GameObject>("Prefabs/Cards/Card_03")).AddComponent<CardAnimation>();
        //    anim.StartTransition(2);
        //}
    }
}
