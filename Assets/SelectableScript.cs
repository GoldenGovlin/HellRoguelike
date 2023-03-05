using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableScript : MonoBehaviour
{
    [SerializeField]
    [HideInInspector]
    GameObject UI;

    [SerializeField]
    [HideInInspector]
    GameObject SelectionSprite;

    [SerializeField]
    [HideInInspector]
    Animator SelectionAnimator;


    [SerializeField]
    [HideInInspector]
    GameObject GameManager;


    private void Awake()
    {
        UI = GameObject.FindWithTag("UI");
        SelectionSprite = GameObject.Find("SelectionSprite");
        SelectionAnimator = SelectionSprite.GetComponent<Animator>();
        GameManager = GameObject.FindWithTag("GameManager");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        print($"Selected: {gameObject.name}");
        GameManager.GetComponent<ClickSelector>().OnDeselect.AddListener(Deselect);

        SelectionAnimator.transform.position = gameObject.transform.position;
        SelectionAnimator.SetBool("IsSelected", true);




    }

    public void Deselect()
    {
        SelectionAnimator.SetBool("IsSelected", false);
        print("Deselected");
    }
}
