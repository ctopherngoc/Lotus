using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterCreationCanvas : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;

    [Header("Character Creation Buttons")]
    //public Button genderButton;

    [SerializeField] private GameObject character;
    private CharacterSprite spriteScript;


    // Start is called before the first frame update
    void Start()
    {
        EnableMainMenu();
        spriteScript = character.GetComponent<CharacterSprite>();
        //Hook events
        //genderButton.onClick.AddListener(changeGender);

    }

    public void ChangeGender()
    {
        Debug.Log("inside change gender button");
        spriteScript.ChangeGender();
    }

    public void NextTone()
    {
        Debug.Log("inside next tone button");
        spriteScript.ChangeSkinTone(true);
    }
    public void PreviousTone()
    {
        Debug.Log("inside previous tone button");
        spriteScript.ChangeSkinTone(false);
    }

    public void NextHair()
    {
        Debug.Log("inside next hair button");
        spriteScript.ChangeHair(true);
    }

    public void PreviousHair()
    {
        Debug.Log("inside previous hair button");
        spriteScript.ChangeHair(false);
    }

    public void NextAnimation()
    {
        Debug.Log("inside next anim button");
        spriteScript.ChangeAnimation(true);
    }

    public void PreviousAnimation()
    {
        Debug.Log("inside previous anim button");
        spriteScript.ChangeAnimation(false);
    }

    private void EnableMainMenu()
    {
        mainMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
