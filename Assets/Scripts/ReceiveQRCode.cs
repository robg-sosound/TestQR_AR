using System.Collections;
using System.Collections.Generic;
using QRFoundation;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class ReceiveQRCode : MonoBehaviour
{
    //public MyIntEvent m_MyEvent;
    public AudioSource p_audiosource;
    public AudioClip aclip;
    AsyncOperationHandle<AudioClip> opHandle;
    public QRCodeTracker e_QRCodeTracker;
    public string rec_qr_code;
    public string addr_str;
    public Text changeText;

    public bool _isplaying;

    public string _lastcard;
    public bool _isSameCard;
    // Start is called before the first frame update

    public float QRGetWidthDelegate(string content){
        if (_isplaying){
            return 0.01f;
        }
        //changeText.text = "ONCODEDETECTED";
        changeText.text = content;
        rec_qr_code = content;
        if ( content == _lastcard){
            return(0.01f);
        }
        addr_str = "Assets/Audio/pack0/" + content;
        //if (content == "P000000.mp3"){
            changeText.text = addr_str;
            //LoadAndPlayAdddressable(addr_str);
            StartCoroutine(LoadAndPlayAdddressable(addr_str));
             _lastcard = content;
        //}
        Debug.Log("OnCodeDetected Called"); 
        return 0.05f;
    }

    public void onCodeLost(){
        changeText.text = "LOST";
        _lastcard = "";
    }
    public void OnCodeDetected(string content){

        //changeText.text = "ONCODEDETECTED";
        //changeText.text = content; 
        Debug.Log("OnCodeDetected Called"); 
        return;
    }
        public void OnCodeRegistered(string content, GameObject gameObject)
    {
        //changeText.text = "ONCODE REGISTERED";
        //changeText.text = content;
        Debug.Log("OnCodeRegistered Called"); 
    }

    public void changeTextClick(){
        changeText.text = "BUTTON CLICKED";
    }
    void Start()
    {

         Screen.sleepTimeout = SleepTimeout.NeverSleep;
        _isplaying = false;;
        p_audiosource.PlayOneShot(aclip,1.0f);
        //LoadAndPlayAdddressable("P000000.mp3");
        if (this.e_QRCodeTracker != null)
		{
            this.e_QRCodeTracker.objText = changeText;
			//this.e_QRCodeTracker.onCodeDetected  += new GetWidthDelegate(this.QRGetWidthDelegate);
            this.e_QRCodeTracker.getWidthDelegate  += new GetWidthDelegate(this.QRGetWidthDelegate);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  IEnumerator LoadAndPlayAdddressable(string key){

         opHandle = Addressables.LoadAssetAsync<AudioClip>(key);
        yield return opHandle;

        if (opHandle.Status == AsyncOperationStatus.Succeeded) {
            changeText.text = "PLAY_SUCC";
            _isplaying = true;
            var newAudioClip = opHandle.Result;
            p_audiosource.clip = newAudioClip;
            //AudioClip obj = opHandle.Result;
            //Instantiate(obj, transform);
            p_audiosource.Play();
            //p_audiosource.PlayOneShot(obj,1.0f);

            yield return new WaitUntil(() => p_audiosource.isPlaying == false);
            _isplaying = false;
            p_audiosource.clip = null;
            Addressables.Release(opHandle);

        } else {
            changeText.text = "FAIL_LOAD";
        }
    }

    //_lobbyClipReference.Instantiate<AudioClip>().Completed += (loaded) => {
    // _lobbyClip = loaded.Result;
    //  PlayLobby();
    //};

    //LoadAndPlayAdddressable
}
