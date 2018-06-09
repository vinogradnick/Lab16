using System;

namespace Products
{
    public  class VirtualTimer
    {
        public static int Time = 9999;

        public static int StartTime = DateTime.Now.Minute;

        public static void IncreaseTime()
        {
            DateTime date = new DateTime();

            if (date.Minute < DateTime.Now.Minute)
                Time--;
        }
       
    }
}