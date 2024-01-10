using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Bericht[] berichten;
    public Persoon[] personen;

    // gesprek starter
    public void StartGesprek()
    {
        FindObjectOfType<DialogueManager>().OpenDialoog(berichten, personen);
    }
}

[System.Serializable]
public class Bericht{
    public int KarakterID;
    public string bericht;
}

[System.Serializable]
public class Persoon
{
    public string naam;
    public Sprite sprite;

}