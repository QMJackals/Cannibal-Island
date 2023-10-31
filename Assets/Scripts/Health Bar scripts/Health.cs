using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int health;
    public int numOfHeats; //heart container. If we need 3 hearts, so we need 3 heart containers

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    public int UpdateHealth(int delta)
    {
        health += delta;
        return health;
    }

    //https://www.youtube.com/watch?v=3uyolYVsiWc&t=178s
    private void Update()
    {

        //condition to make player does not have health more than the number of heart
        if(health > numOfHeats)
        {
            health = numOfHeats;
        }



        //hearts.Length is number of hearts in the hearts
        for (int i = 0; i < hearts.Length; i++)
        {
            /*
             * health = 6, hearts = 3
             * health = 5, hearts = 2.5
             * health = 4, hearts = 2
             * health = 3, hearts = 1.5
             * health = 2, hearts = 1
             * health = 1, hearts = 0.5
             * heallth = 0, hearts = 0
             */
            int nbOfHalfHeart = health % 2;
            int nbOfFullHeart = health / 2;

            if (i < nbOfFullHeart)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (i == nbOfFullHeart && nbOfHalfHeart == 1)
            {
                hearts[i].sprite = halfHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            //check whether heart i should be visibled
            if (i < numOfHeats)
            {
                hearts[i].enabled = true; 
            } 
            else
            {
                hearts[i].enabled = false;
            }


        }
    }

}
