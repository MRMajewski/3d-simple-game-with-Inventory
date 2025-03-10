using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CharacterModelEquipmentChanger : MonoBehaviour
{
    public Animator bodyAnimator;

    public List<Animator> Helmets;
    public List<Animator> Necklaces;
    public List<Animator> Armors;
    public List<Animator> Rings;
    public List<Animator> Boots;
    public List<Animator> Weapons;


    public void ChangeEquipment(string category, int index)
    {
        switch (category)
        {
            case "Helmet":
                ChangeHelmet(index);
                break;
            case "Necklace":
                ChangeNecklace(index);
                break;
            case "Armor":
                ChangeArmor(index);
                break;
            case "Ring":
                ChangeRing(index);
                break;
            case "Boots":
                ChangeBoots(index);
                break;
            case "Weapon":
                ChangeWeapon(index);
                break;
            default:
                Debug.Log("No correct category found");
                break;
        }


        void ChangeHelmet(int index)
            {
            int newIndex = index;
            if (index!=-1)
            {
                float newIndexFloat = index / 5f;
                newIndex = Mathf.CeilToInt(Mathf.Clamp(newIndexFloat * (Helmets.Count - 1), 0, Helmets.Count - 1));
            }

            for (int i = 0; i < Helmets.Count; i++)
            {
                if (i == newIndex)
                {
                    Helmets[i].gameObject.SetActive(true);
                    SyncAnimation(Helmets[i]);
                }
                 

                else
                    Helmets[i].gameObject.SetActive(false);
            }

        }

        void ChangeNecklace(int index)
        {
            int newIndex = index;
            if (index != -1)
            {
                 newIndex = Mathf.CeilToInt(Mathf.Clamp((index / 5f) * (Necklaces.Count - 1), 0, Necklaces.Count - 1));
            }
            for (int i = 0; i < Necklaces.Count; i++)
            {
                if (i == newIndex)
                {
                    Necklaces[i].gameObject.SetActive(true);
                    SyncAnimation(Necklaces[i]);
                }
                  
                else
                    Necklaces[i].gameObject.SetActive(false);
            }
        }

        void ChangeArmor(int index)
        {
            int newIndex = index;
            if (index != -1)
            {
                 newIndex = Mathf.CeilToInt(Mathf.Clamp((index / 5f) * (Armors.Count - 1), 0, Armors.Count - 1));
            }
            for (int i = 0; i < Armors.Count; i++)
            {
                if (i == newIndex)
                {     
                    Armors[i].gameObject.SetActive(true);
                    SyncAnimation(Armors[i]);
                }
                else
                    Armors[i].gameObject.SetActive(false);
            }
        }

        void ChangeRing(int index)
        {
            int newIndex = index;
            if (index != -1)
            {
                 newIndex = Mathf.CeilToInt(Mathf.Clamp((index / 5f) * (Rings.Count - 1), 0, Rings.Count - 1));
            }

            for (int i = 0; i < Rings.Count; i++)
            {
                if (i == newIndex)
                {
                    Rings[i].gameObject.SetActive(true);
                    SyncAnimation(Rings[i]);
                }
                   
                else
                    Rings[i].gameObject.SetActive(false);
            }
        }

        void ChangeBoots(int index)
        {
            int newIndex = index;
            if (index != -1)
            {
                 newIndex = Mathf.CeilToInt(Mathf.Clamp((index / 5f) * (Boots.Count - 1), 0, Boots.Count - 1));
            }

            for (int i = 0; i < Boots.Count; i++)
            {
                if (i == newIndex)
                {
                    Boots[i].gameObject.SetActive(true);
                    SyncAnimation(Boots[i]);
                }
                   
                else
                    Boots[i].gameObject.SetActive(false);
            }
        }

        void ChangeWeapon(int index)
        {
            int newIndex = index;
            if (index != -1)
            {
                 newIndex = Mathf.CeilToInt(Mathf.Clamp((index / 5f) * (Weapons.Count - 1), 0, Weapons.Count - 1));
            }
            for (int i = 0; i < Weapons.Count; i++)
            {
                if (i == newIndex)
                {
                    Weapons[i].gameObject.SetActive(true);

                }
                   
                else
                    Weapons[i].gameObject.SetActive(false);
            }
        }


        void SyncAnimation(Animator newAnimator)
        {

            if (bodyAnimator == null || newAnimator == null) return; 

            AnimatorStateInfo stateInfo = bodyAnimator.GetCurrentAnimatorStateInfo(0);
            newAnimator.Play(stateInfo.shortNameHash, 0, stateInfo.normalizedTime);
        }

    }
}
