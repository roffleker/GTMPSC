using GrandTheftMultiplayer.Server.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealifeGM.de.jojoa.data
{
    class Bank_Account
    {
        public int money = 0;
        public int number = 0;
        public Account owner;
       
        

       
        public Bank_Account(Account a, int kntNR)
        {
            owner = a;
            number = kntNR;
            this.money = mysql.MySQL_Bank.getInt(number, "money");
            methods.getMethods.lba.Add(this);
            
        }
        public void Save()
        {
            mysql.MySQL_Bank.Save(this);
        }
    }
}
