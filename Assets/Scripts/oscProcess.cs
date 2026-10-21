using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class oscProcess : MonoBehaviour
{

    public OSCReceiver oscReceiver;

    public OSCTransmitter oscTransmitter;

    public Unlock unlock;
    
    public int potInMin = 0; // Valeur minimale du potentiomètre

    public int potInMax = 1023; // Valeur maximale du potentiomètre

    public float potOutMin = -4.4f; // Valeur minimale de sortie (position de la palette)

    public float potOutMax = 4.4f; // Valeur maximale de sortie (position

    // Start is called before the first frame update
    void Start()
    {

        oscReceiver.Bind("/tourner", SetAngle);
        oscReceiver.Bind("/valider", CheckSolution);
    }

    // Update is called once per frame
    void Update()
    {
    if ( unlock.isUnlocked() ) {
        var oscMessage = new OSCMessage("/lumiere"); 
        oscMessage.AddValue( OSCValue.Int(1) ); // Ajoute l’entier 1
        oscTransmitter.Send( oscMessage ); // Envoyer le message 
        } else {
        var oscMessage = new OSCMessage("/lumiere"); 
        oscMessage.AddValue( OSCValue.Int(0) ); // Ajoute l’entier 0
        oscTransmitter.Send( oscMessage ); // Envoyer le message 
        }
    }

     void CheckSolution(OSCMessage message)
    {
        // Validez qu’il y a bien le nombre attendu d’arguments (1 dans l’exemple) :
        if (message.Values.Count != 1)
        {
            Debug.Log("Le message " + message.Address + " n’a pas le bon nombre d’arguments");
            return; // Quitte la fonction sans exécuter la suite
        }

        // Vérifiez que l’argument est du type attendu (`int` dans l’exemple) :
        if (message.Values[0].Type != OSCValueType.Int)
        {
            Debug.Log("Le premier argument du message " + message.Address + "n’est pas un entier");
            return; // Quitte la fonction sans exécuter la suite
        }

        // Récupérer la valeur de l’argument :
        int valeur = message.Values[0].IntValue;

        // Deboguer
        // Debug.Log("Reçu : " + message.Address + " " + valeur);

        // TRAITER LA VALEUR ICI !
        if (valeur == 1)
        {
            unlock.CheckSolution();
        }
        else
        {

        }


    }

    void SetAngle(OSCMessage message)
    {
        // Validez qu’il y a bien le nombre attendu d’arguments (1 dans l’exemple) :
        if (message.Values.Count != 1)
        {
            Debug.Log("Le message " + message.Address + " n’a pas le bon nombre d’arguments");
            return; // Quitte la fonction sans exécuter la suite
        }

        // Vérifiez que l’argument est du type attendu (`int` dans l’exemple) :
        if (message.Values[0].Type != OSCValueType.Int)
        {
            Debug.Log("Le premier argument du message " + message.Address + "n’est pas un entier");
            return; // Quitte la fonction sans exécuter la suite
        }

        // Récupérer la valeur de l’argument :
        int valeur = message.Values[0].IntValue;

        // Deboguer
         Debug.Log("Reçu : " + message.Address + " " + valeur);

        // TRAITER LA VALEUR ICI !

        float ajuste = (((float)valeur - potInMin) / (potInMax - potInMin) * (potOutMax - potOutMin) + potOutMin);

        // AJOUTER À LA LIGNE SUIVANTE LE CODE POUR APPLIQUER LA VARIABLE ajuste AU DÉPLACEMENT DE LA PALETTE ICI !
        
        Slider.SetPosition(ajuste);

    }

}
