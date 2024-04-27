using FishNet.Example.ColliderRollbacks;
using GameKit.Utilities;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEditor;
using UnityEngine;


public class CharacterSprite : MonoBehaviour
{
    struct spriteObject
    {
        [SerializeField] public int count;
        [SerializeField] public int max;
        [SerializeField] public List<Transform> list;

        public spriteObject(int count, int max, List<Transform> list)
        {
            this.count = count;
            this.max = max;
            this.list = list;
        }
    }

    [SerializeField] public GameObject malePart;
    [SerializeField] public GameObject femalePart;
    [SerializeField] public List<GameObject> maleSprite;
    [SerializeField] public List<GameObject> femaleSprite;
    [SerializeField] public List<GameObject> attachmentSprite;
    [SerializeField] public GameObject genderText;
    [SerializeField] public GameObject hairText;
    [SerializeField] public GameObject hairColorText;
    [SerializeField] public GameObject headText;
    [SerializeField] public GameObject toneText;
    [SerializeField] public GameObject animText;

    [SerializeField] public Animator anim;
    [SerializeField] public int anim_index;
    [SerializeField] private bool gender;

    private Dictionary<string, int> genderSpriteHash = new Dictionary<string, int>()
    {
        { "headObj", 0 },{"headType", 1}, {"headGear", 2}, {"eyeBrow", 3},
        {"torso", 4}, {"upperRightArm", 5}, {"upperLeftArm",6 }, {"lowerRightArm", 7}, {"lowerLeftArm", 8},
        {"rightHand", 9}, {"leftHand", 10}, {"hips", 11}, {"rightLeg", 12}, {"leftLeg", 13}
    };

    private Dictionary<string, int> attachmentSpriteHash = new Dictionary<string, int>()
    {
        { "headObj", 0 }, { "hatHair", 1 },{"mask", 2}, {"hatNoHair", 3},{ "hair", 4}, {"headAttachObj", 5}, {"hairAttach", 6 },
        {"helmetAttach", 7}, {"chest", 8}, {"back", 9}, {"rightShoulder",10}, {"leftShoulder", 11}, {"rightElbow", 12},
        {"leftEblow", 13}, {"hips", 14}, {"rightKnee", 15}, {"leftKnee", 16}, {"ear", 17 }, {"facialHair", 18},
    };

    [SerializeField] public List<Material> materials = new List<Material>();
    [SerializeField] private int tone_index = 0;

    // genderless
    spriteObject hair = new spriteObject(-1, 0, new List<Transform>());
    spriteObject ear = new spriteObject(0, 0, new List<Transform>());
    spriteObject facialHair = new spriteObject(0, 0, new List<Transform>());

    // gender-locked
    spriteObject headType = new spriteObject(0, 0, new List<Transform>());
    spriteObject torso = new spriteObject(0, 0, new List<Transform>());
    spriteObject eyeBrow = new spriteObject(0, 0, new List<Transform>());
    spriteObject upperRightArm = new spriteObject(0, 0, new List<Transform>());
    spriteObject upperLeftArm = new spriteObject(0, 0, new List<Transform>());
    spriteObject lowerRightArm = new spriteObject(0, 0, new List<Transform>());
    spriteObject lowerLeftArm = new spriteObject(0, 0, new List<Transform>());
    spriteObject rightHand = new spriteObject(0, 0, new List<Transform>());
    spriteObject leftHand = new spriteObject(0, 0, new List<Transform>());
    spriteObject hips = new spriteObject(0, 0, new List<Transform>());
    spriteObject rightLeg = new spriteObject(0, 0, new List<Transform>());
    spriteObject leftLeg = new spriteObject(0, 0, new List<Transform>());

    [SerializeField]
    List<spriteObject> spriteList = new List<spriteObject>();

    // Start is called before the first frame update
    void Start()
    {
        //ChangeGender();
        anim = gameObject.GetComponent<Animator>();
        GetCount(attachmentSprite[attachmentSpriteHash["hair"]], hair.list, ref hair.max);
        GetCount(attachmentSprite[attachmentSpriteHash["ear"]], ear.list, ref ear.max);
        GetCount(attachmentSprite[attachmentSpriteHash["facialHair"]], facialHair.list, ref facialHair.max);
        GetGenderSprite(gender);

        spriteList.Add(headType);
        spriteList.Add(torso);
        spriteList.Add(upperRightArm);
        spriteList.Add(upperLeftArm);
        spriteList.Add(lowerRightArm);
        spriteList.Add(lowerLeftArm);
        spriteList.Add(rightHand);
        spriteList.Add(leftHand);
        spriteList.Add(hips);
        spriteList.Add(rightLeg);
        spriteList.Add(leftLeg);
        //changeGender();
        GetGenderSprite(gender);
        anim_index = 0;
        gender = true;
    }

    public void ChangeGender()
    {
        if (gender)
        {
            Debug.Log("gender bool false -> female");
            gender = false;
            GetGenderSprite(gender);
            malePart.SetActive(false);
            femalePart.SetActive(true);
            genderText.GetComponent<TMPro.TextMeshProUGUI>().text = "Female";
        }
        else
        {
            Debug.Log("gender bool true -> male");
            gender = true;
            GetGenderSprite(gender);
            malePart.SetActive(true);
            femalePart.SetActive(false);
            genderText.GetComponent<TMPro.TextMeshProUGUI>().text = "Male";
        }
    }

    public void ChangeHair(bool next)
    {   
        if (next)
        { 
            Debug.Log("changeHair " + hair.count);
            if (hair.count == hair.max - 1)
            {
                Debug.Log("bald");
                hair.list[hair.count].gameObject.SetActive(false);
                hair.count = -1;
            }
            else
            {
                Debug.Log("next hair");
                if (hair.count < 0)
                {
                    hair.count += 1;
                    hair.list[hair.count].gameObject.SetActive(true);
                }
                else
                {
                    hair.list[hair.count].gameObject.SetActive(false);
                    hair.count += 1;
                    hair.list[hair.count].gameObject.SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log("changeHair " + hair.count);
            if (hair.count == 0)
            {
                Debug.Log("last hair on list");
                hair.list[hair.count].gameObject.SetActive(false);
                hair.count = -1;

            }
            else
            {
                Debug.Log("back one hair");
                if (hair.count == -1)
                {
                    hair.count = hair.max - 1;
                    hair.list[hair.count].gameObject.SetActive(true);
                }
                else
                {
                    hair.list[hair.count].gameObject.SetActive(false);
                    hair.count -= 1;
                    hair.list[hair.count].gameObject.SetActive(true);
                }
            }

        }

        hairText.GetComponent<TMPro.TextMeshProUGUI>().text = "Hair " + (hair.count + 2);
    }
    
    public void ChangeAnimation(bool next)
    {   if (next)
        {   
            if (anim_index == 2)
            {
                anim_index = 0;
            }
            else
            {
                anim_index += 1;
            }
        }
        else
        {
            if (anim_index == 0)
            {
                anim_index = 2;
            }
            else
            {
                anim_index -= 1;
            }
        }
        if (anim_index == 0)
        {
            anim.Play("idle");
            animText.GetComponent<TMPro.TextMeshProUGUI>().text = "Idle";
        }
        else if (anim_index == 1)
        {
            anim.Play("walk");
            animText.GetComponent<TMPro.TextMeshProUGUI>().text = "Walk";
        }
        else
        {
            anim.Play("run_0");
            animText.GetComponent<TMPro.TextMeshProUGUI>().text = "Run";
        }
    }


    public void ChangeSkinTone(bool next)
    {   if (next)
        { 
            // 3 skin tones
            if(tone_index == 2)
            {
                tone_index = 0;
            }
            else
            {
                tone_index += 1;
            }
        } else
        {
            // 3 skin tones
            if (tone_index == 0)
            {
                tone_index = 2;
            }
            else
            {
                tone_index -= 1;
            }
        }
        Material[] mats = new Material[] { materials[tone_index] };

        foreach (var spritePart in spriteList)
        {
            foreach (var sprite in spritePart.list)
            {
                var mesh = sprite.GetComponent<SkinnedMeshRenderer>();
                var ren = mesh.GetComponent<Renderer>();
                ren.materials = mats;
            }
        }
        toneText.GetComponent<TMPro.TextMeshProUGUI>().text = "Tone " + (tone_index + 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetCount(GameObject spriteObject, List<Transform> list, ref int max)
    {
        max = spriteObject.transform.childCount;

        for (int i = 0; i < spriteObject.transform.childCount; i++)
        {
            list.Add(spriteObject.transform.GetChild(i));
        }

    }

    public void GetGenderSprite(bool gender)
    {
        if (gender)
        {
            headType.count = torso.count = eyeBrow.count = upperRightArm.count = upperLeftArm.count = 0;
            lowerRightArm.count = lowerLeftArm.count = rightHand.count = leftHand.count = hips.count = leftLeg.count = rightLeg.count = 0;
            GetCount(femaleSprite[genderSpriteHash["headType"]], headType.list, ref headType.max);
            GetCount(femaleSprite[genderSpriteHash["torso"]], torso.list, ref torso.max);
            GetCount(femaleSprite[genderSpriteHash["eyeBrow"]], eyeBrow.list, ref eyeBrow.max);
            GetCount(femaleSprite[genderSpriteHash["upperRightArm"]], upperRightArm.list, ref upperRightArm.max);
            GetCount(femaleSprite[genderSpriteHash["upperLeftArm"]], upperLeftArm.list, ref upperLeftArm.max);
            GetCount(femaleSprite[genderSpriteHash["lowerRightArm"]], lowerRightArm.list, ref lowerRightArm.max);
            GetCount(femaleSprite[genderSpriteHash["lowerLeftArm"]], lowerLeftArm.list, ref lowerLeftArm.max);
            GetCount(femaleSprite[genderSpriteHash["rightHand"]], rightHand.list, ref rightHand.max);
            GetCount(femaleSprite[genderSpriteHash["leftHand"]], leftHand.list, ref leftHand.max);
            GetCount(femaleSprite[genderSpriteHash["hips"]], hips.list, ref hips.max);
            GetCount(femaleSprite[genderSpriteHash["rightLeg"]], rightLeg.list, ref rightLeg.max);
            GetCount(femaleSprite[genderSpriteHash["leftLeg"]], leftLeg.list, ref leftLeg.max);
        }

        else
        {
            headType.count = torso.count = eyeBrow.count = upperRightArm.count = upperLeftArm.count = 0;
            lowerRightArm.count = lowerLeftArm.count = rightHand.count = leftHand.count = hips.count = leftLeg.count = rightLeg.count = 0;
            GetCount(maleSprite[genderSpriteHash["headType"]], headType.list, ref headType.max);
            GetCount(maleSprite[genderSpriteHash["torso"]], torso.list, ref torso.max);
            GetCount(maleSprite[genderSpriteHash["eyeBrow"]], eyeBrow.list, ref eyeBrow.max);
            GetCount(maleSprite[genderSpriteHash["upperRightArm"]], upperRightArm.list, ref upperRightArm.max);
            GetCount(maleSprite[genderSpriteHash["upperLeftArm"]], upperLeftArm.list, ref upperLeftArm.max);
            GetCount(maleSprite[genderSpriteHash["lowerRightArm"]], lowerRightArm.list, ref lowerRightArm.max);
            GetCount(maleSprite[genderSpriteHash["lowerLeftArm"]], lowerLeftArm.list, ref lowerLeftArm.max);
            GetCount(maleSprite[genderSpriteHash["rightHand"]], rightHand.list, ref rightHand.max);
            GetCount(maleSprite[genderSpriteHash["leftHand"]], leftHand.list, ref leftHand.max);
            GetCount(maleSprite[genderSpriteHash["hips"]], hips.list, ref hips.max);
            GetCount(maleSprite[genderSpriteHash["rightLeg"]], rightLeg.list, ref rightLeg.max);
            GetCount(maleSprite[genderSpriteHash["leftLeg"]], leftLeg.list, ref leftLeg.max);
        }
    }
}

