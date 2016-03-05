using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class ConversationSend
    {
        
        public static void SendConversation()
        {
            int enter_count = PlayerPrefs.GetInt("enter_count", 0);
            Debug.Log("EnterCount" + PlayerPrefs.GetInt("enter_count", 0));
            enter_count++;           

            if (enter_count == 3)
            {
                Debug.Log("Send_conversation");
                AdSDK.AdSDK.SendConversion();
                enter_count = 0;
            }
            PlayerPrefs.SetInt("enter_count", enter_count);
            PlayerPrefs.Save();
            Debug.Log("EnterCount" + PlayerPrefs.GetInt("enter_count", 0));
        }
    }
}
