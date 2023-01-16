using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates and stores champions available, XP and LVL purchase
/// </summary>
public class ChampionShop : MonoBehaviour
{
    public UIController uIController;
    public GamePlayController gamePlayController;
    public GameData gameData;

    ///Array to store available champions to purchase
    private Champion[] availableChampionArray;


    /// Start is called before the first frame update
    void Start()
    {
        RefreshShop(true);
    }

    /// Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Gives a level up the player
    /// </summary>
    public void BuyLvl()
    {
        gamePlayController.Buylvl();
    }

    /// <summary>
    /// Refreshes shop with new random champions
    /// </summary>
    public void RefreshShop(bool isFree)
    {
        //return if we dont have enough gold
        if (gamePlayController.currentGold < 2 && isFree == false)
            return;


        //init array
        availableChampionArray = new Champion[5];

        //fill up shop
        for (int i = 0; i < availableChampionArray.Length; i++)
        {
            //get a random champion
            Champion champion = GetRandomChampionInfo();

            //store champion in array
            availableChampionArray[i] = champion;

            //load champion to ui
            uIController.LoadShopItem(champion, i);

            //show shop items
            uIController.ShowShopItems();
        }

        //decrase gold
        if(isFree == false)
            gamePlayController.currentGold -= 2;

        //update ui
        uIController.UpdateUI();
    }

    /// <summary>
    /// Called when ui champion frame clicked
    /// </summary>
    /// <param name="index"></param>
    public void OnChampionFrameClicked(int index)
    {    
        bool isSucces = gamePlayController.BuyChampionFromShop(availableChampionArray[index]);

        if(isSucces)
            uIController.HideChampionFrame(index);
    }

    /// <summary>
    /// Returns a random champion
    /// </summary>
    public Champion GetRandomChampionInfo()
    {
        //randomise a number
        int rand = Random.Range(0, gameData.championsArray.Length);

        //return from array
        return gameData.championsArray[rand];
    }


}
