using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class LoseCounter
    {
        private static LoseCounter instance;
        public int lose_count;

        public void IncrementCount()
        {
            lose_count++;
        }

        public static LoseCounter Instance
        {
            
            get
            {
                if (instance == null)
                {
                    instance = new LoseCounter();
                }
                return instance;
            }
        }
    }
}
