using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class oscProcess : MonoBehaviour
{

    public OSCReceiver oscReceiver;
    public OSCTransmitter oscTransmitter;
    public Unlock unlock;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        oscReceiver.Bind("/tourner", SetAngle);
        oscReceiver.Bind("/valider", CheckSolution);

        if ( unlock.isUnlocked() ) {
        var oscMessage = new OSCMessage("/lumiere"); 
        oscMessage.AddValue( OSCValue.Int(1) ); // Ajoute l’entier 1
        oscTransmitter.Send( oscMessage ) // Envoyer le message 
        } else {
        var oscMessage = new OSCMessage("/lumiere"); 
        oscMessage.AddValue( OSCValue.Int(0) ); // Ajoute l’entier 0
        oscTransmitter.Send( oscMessage ); // Envoyer le message 
        }

    }
}
