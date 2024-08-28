using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TGame;
using TGame.UI;
using TGame.Asset;




namespace Koakuma.Game
{ 
    public class GameManager : MonoBehaviour
    {
        

        public static AssetModule Asset { get => TGameFramework.Instance.GetModule<AssetModule>(); }
    }

}

