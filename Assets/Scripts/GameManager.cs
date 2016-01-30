using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameObject("GameManager").AddComponent<GameManager>();

            return instance;
        }
    }

    private GameManager() { }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            CardManager.Instance.ReadCards();
	}
}
