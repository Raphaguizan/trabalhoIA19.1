using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public GameObject cameraLook;
    public float speed;
    public bool active;

    private GameManager gm;
    private SelectPiece select;

    // Use this for initialization
    void Start () {
        gm = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        select = GameObject.FindObjectOfType(typeof(SelectPiece)) as SelectPiece;

        transform.rotation = Quaternion.Euler(0, 280, 0);
    }

    public void setPlayer2(){
        transform.eulerAngles = new Vector3(0, 80, 0);
    }
	
	// Update is called once per frame
	void Update () {
        cameraLook.transform.LookAt(transform);
     
        if(active){
            if (gm.getTurno() == 2)
        {
            Vector3 to = new Vector3(0, 80, 0);
            if (Vector3.Distance(to, transform.eulerAngles) > 0.01f)
            {
                transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, speed);
            }
        }
        else
        {
            Vector3 to = new Vector3(0, 280, 0);
            if (Vector3.Distance(to, transform.eulerAngles) > 0.01f)
            {
                transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, speed);
            }
        }
        }
    }
}
