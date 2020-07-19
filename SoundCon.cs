using System.Net;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class SoundCon : MonoBehaviour
{
    public AudioClip[] aClips;
    public AudioSource myAudioSource;
    string btnName;
    string API_Key = "229FXQ1IFY6SJYU8";
    // Start is called before the first frame update
    TextMesh textMesh;

    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        textMesh = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit)){
                btnName = Hit.transform.name;
                switch (btnName)
                {
                    case "LightOn":
                        myAudioSource.clip = aClips[0];
                        myAudioSource.Play();
                        Light("On");
                        break;

                    case "LightOff":
                        myAudioSource.clip = aClips[1];
                        myAudioSource.Play();
                        Light("Off");
                        break;

                    case "FanOn":
                        myAudioSource.clip = aClips[0];
                        myAudioSource.Play();
                        Fan("On");
                        break;
                    case "FanOff":
                        myAudioSource.clip = aClips[1];
                        myAudioSource.Play();
                        Fan("Off");
                        break;
                    default:
                        break;


                }
            }

        }

        void Fan(string x) {
            if (x == "On"){
                StartCoroutine(GetRequest("https://api.thingspeak.com/update?api_key=229FXQ1IFY6SJYU8&field2=1"));
            }

            else if (x == "Off")
            {
                StartCoroutine(GetRequest("https://api.thingspeak.com/update?api_key=229FXQ1IFY6SJYU8&field2=2"));
            }
        }


        void Light(string x) {
            if (x == "On"){
                StartCoroutine(GetRequest("https://api.thingspeak.com/update?api_key=229FXQ1IFY6SJYU8&field1=1"));
            }

            else if (x == "Off") {
                StartCoroutine(GetRequest("https://api.thingspeak.com/update?api_key=229FXQ1IFY6SJYU8&field1=2"));
            }
        }

        

    }
   

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }


}
