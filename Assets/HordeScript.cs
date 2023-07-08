using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HordeScript : MonoBehaviour
{
    public Button plusButton;
    public Button minusButton;
    public Button plusButton1;
    public Button minusButton1;
    public Button plusButton2;
    public Button minusButton2;
    public TMP_Text titleText;
    public TMP_Text titleText1;
    public TMP_Text titleText2;

    public int summary = 0;
    public int summary1 = 0;
    public int summary2 = 0;

    // Start is called before the first frame update
    private void Start()
    {

        plusButton.onClick.AddListener(() =>
        {
            summary = summary + 1;
        });

        minusButton.onClick.AddListener(() =>
        {
            if (summary > 0)
            {
                summary = summary - 1;
            }
        });
        
        plusButton1.onClick.AddListener(() =>
        {
            summary1 = summary1 + 1;
        });

        minusButton1.onClick.AddListener(() =>
        {
            if (summary1 > 0)
            {
                summary1 = summary1 - 1;
            }
        });
        
        plusButton2.onClick.AddListener(() =>
        {
            summary2 = summary2 + 1;
        });

        minusButton2.onClick.AddListener(() =>
        {
            if (summary2 > 0)
            {
                summary2 = summary2 - 1;
            }
        });
    }


    // Update is called once per frame
    void Update()
    {
        titleText.text = summary.ToString();
        titleText1.text = summary1.ToString();
        titleText2.text = summary2.ToString();
    }
}
