using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealifeGM.de.jojoa.data
{
    class Property
    {
        public Vector3 pos;
        public string ID;
        public int price;
        public int type;
        public string typeName;
        public Account owner;
        public string street;
        public string zone;
        public Marker mark;
        public TextLabel lbl1;
        public TextLabel lbl2;
        public TextLabel lbl3;
        public TextLabel lbl4;



        public Property(Vector3 posI,int priceI,int typeI)
        {
            pos = posI;
            price = priceI;
            type = typeI;
            getTypeName();
            owner = methods.getMethods.getAccountByName("IM");

            
        }

        public void getTypeName()
        {
            switch (type)
            {
                case 1:
                    typeName = "Kleine Wohnung";
                    break;
                case 2:
                    typeName = "Große Wohnung";
                    break;
                case 3:
                    typeName = "Kleines Haus";
                    break;
                case 4:
                    typeName = "Großes Haus";
                    break;
                case 5:
                    typeName = "Kleine Villa";
                    break;
                case 6:
                    typeName = "Große Villa";
                    break;
            }
        }

       

        public void Save()
        {
            mysql.MySQL_PropertyData.saveProperty(this);
        }
        
        public void Remove()
        {
            mysql.MySQL_PropertyData.RemoveProp(this);
        }
    }
}
