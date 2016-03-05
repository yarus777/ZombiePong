using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class SendStatistics : MonoBehaviour
    {
        private static bool IsConversationSend = false;
        private void Start()
        {
            Debug.Log("IsSend" + IsConversationSend);
            if (!IsConversationSend)
            {
                ConversationSend.SendConversation();
                IsConversationSend = true;
            }
        }
    }
}
