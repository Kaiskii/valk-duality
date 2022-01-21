using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour {
    
    //Singleton Implementation
    private static FadeInOut _instance;

    public static FadeInOut Instance { get { return _instance; } }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    //Variable Decl
    public bool isFadeOut = true;
    public bool isFadeIn = true;
    public bool loadScene = false;

    [SerializeField] float fadeStrength = 0.02f;
    float t = 0;
    public int sceneInt = 0;

    private void Start() {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    private void Update() {
        if(isFadeOut)
            FadeOutFromStart();

        if (isFadeIn)
            FadeIn();

    }

    void FadeIn() {
        SpriteRenderer sprR = GetComponent<SpriteRenderer>();

        if(sprR.color.a <= 1.0f) {
            t += Time.deltaTime * fadeStrength;

            sprR.color = new Color(0, 0, 0, t);
        }

        if(sprR.color.a >= 1.0f) {
            isFadeIn = false;
            t = 0;

            if(loadScene)
                SceneManager.LoadScene(sceneInt);
        }
    }


    void FadeOutFromStart() {
        SpriteRenderer sprR = GetComponent<SpriteRenderer>();

        if (sprR.color.a >= 0) {
            t += Time.deltaTime * fadeStrength;

            sprR.color = new Color(0, 0, 0, 1.0f - t);
        }

        if(sprR.color.a <= 0) {
            isFadeOut = false;
            t = 0;
        }
    }

}
