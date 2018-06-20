using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactAudrey : MonoBehaviour {

    public Animator auderyAnim;

	public void AngryOn(){

        auderyAnim.SetBool("angry", true);
        return;

    }

    public void AngryOff() {

        auderyAnim.SetBool("angry", false);
        return;

    }

    public void AckOn() {

        auderyAnim.SetBool("ack", true);
        return;

    }

    public void AckOff() {

        auderyAnim.SetBool("ack", false);
        return;

    }
}
