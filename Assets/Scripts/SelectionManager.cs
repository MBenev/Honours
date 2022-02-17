using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Color defaultColor = new Color(1.0f, 1.0f, 1.0f);

    private Transform _selection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_selection != null)
        {
            var selectionRenerer = _selection.GetComponent<Renderer>();
            selectionRenerer.material.color = defaultColor;
            _selection = null;
        }

        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    defaultColor = selectionRenderer.material.color;
                    selectionRenderer.material.color = new Color(0.2f, 0.2f, 0.2f);
                    //selectionRenderer.material.color = Color.yellow;
                }
                _selection = selection;
            }

        }
    }
}
