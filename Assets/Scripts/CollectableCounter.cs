using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableCounter : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        //text = gameObject.GetComponent<TextMeshPro>();
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Player.Instance.GetCollected().ToString() + " / Maximum";
    }
}
