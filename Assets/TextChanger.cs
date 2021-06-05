using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextChanger : MonoBehaviour
{
    private TextMeshProUGUI gui;
    private int txtBox;
    private TextMeshProUGUI[] steps;
    // Start is called before the first frame update
    void Start()
    {
        steps = this.GetComponentsInChildren<TextMeshProUGUI>();
        txtBox = 0;
        gui = this.GetComponent<TextMeshProUGUI>();
        gui.text = steps[txtBox].text;
        for (int i = 1; i < steps.Length; i++)
        {
            steps[i].gameObject.SetActive(false);
        }
        steps[1].gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(0))
        if(Input.GetMouseButtonUp(0))
        {
            if (txtBox >=  10)
            {
                txtBox = 0;
                steps[10].gameObject.SetActive(false);

            }
            if(txtBox > 0)
                steps[txtBox].gameObject.SetActive(false);
            
            txtBox++;
            steps[txtBox].gameObject.SetActive(true);

        }
    }
}
