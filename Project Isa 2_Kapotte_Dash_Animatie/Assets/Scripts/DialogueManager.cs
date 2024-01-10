using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // Variablen voor de indeling van het textvak
    public Image persoonAfbeelding;
    public Text persoonNaam;
    public Text berichtTekst;
    public RectTransform achtergrondVak;

    Bericht[] huidigeBerichten;
    Persoon[] huidigePersonen;
    int ActiefBericht = 0;
    public static bool isActief = false;

    public void OpenDialoog(Bericht[] berichten, Persoon[] personen)
    {
        huidigeBerichten = berichten;
        huidigePersonen = personen;
        ActiefBericht = 0;
        isActief = true;

        // Checken of gesprek gestart is

        Debug.Log("Gesprek gestart! Geladen berichten: " + berichten.Length);
        DisplayBericht();
        achtergrondVak.LeanScale(Vector3.one, 0.5f);
    }

    // Berichten zichtbaar maken
    void DisplayBericht()
    {
        Bericht berichtNaarDisplay = huidigeBerichten[ActiefBericht];
        berichtTekst.text = berichtNaarDisplay.bericht;

        // persoon die op dit moment spreekt
        Persoon persoonNaarDisplay = huidigePersonen[berichtNaarDisplay.KarakterID];
        persoonNaam.text = persoonNaarDisplay.naam;
        persoonAfbeelding.sprite = persoonNaarDisplay.sprite;

        AnimateTextColor();
    }

    // Volgende berichten inladen
    public void VolgendeBericht()
    {
        ActiefBericht++;
        if (ActiefBericht < huidigeBerichten.Length)
        {
            DisplayBericht();
        }
        else
        {
            Debug.Log("Gesprek beëindigd!");
            achtergrondVak.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
            isActief = false;
        }
    }
    // animaties voor de text
    void AnimateTextColor() {
        LeanTween.textAlpha(berichtTekst.rectTransform, 0, 0);
        LeanTween.textAlpha(berichtTekst.rectTransform, 1, 0.5f);
    }
    // Start is called before the first frame update

    // het tekstvak begint ontzichtbaar
    void Start()
    {
        achtergrondVak.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //spatie om naar volgende bericht te gaan
        if (Input.GetKeyDown(KeyCode.Space) && isActief == true)
        {
            VolgendeBericht();
        }
    }
}
