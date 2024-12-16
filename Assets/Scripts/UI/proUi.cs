using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Profiling;

public class proUi : MonoBehaviour
{
    public TextMeshProUGUI value;
    public Image bar;
    public Proficiency_ui proficiency_ui;

    public Color red;
    public Color blue;
    public Color green;
    // Start is called before the first frame update
    void Start()
    {
        proficiency_ui = Shared.gameMgr.GetComponent<Proficiency_ui>();
    }

    // Update is called once per frame
    void Update()
    {
        value.text = (proficiency_ui.Profill.fillAmount * 100).ToString("F0") + "%";
        bar.fillAmount = proficiency_ui.Profill.fillAmount;
        if (bar.fillAmount < 0.34)
        {
            bar.color = green;
        }
        else if (bar.fillAmount > 0.34 && bar.fillAmount < 0.67)
        {
            bar.color = blue;
        }
        else
        {
            bar.color = red;
        }
    }
}
