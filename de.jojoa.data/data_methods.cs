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
    class data_methods : Script
    {
       
        public data_methods()
        {

        }
        #region Account
        public static void redraw_acc(Account a)
        {
            //money
            API.shared.triggerClientEvent(a.p, "acc_draw", a.money);
            //Property
            foreach(Property prop in a.props)
            {
                API.shared.triggerClientEvent(a.p, "cBlip", prop.pos, 40,2,false,"Haus");
            }
           
        }
        #endregion Account
        #region Property
        public static void show_prop(Property prop)
        {
            if (prop.mark != null)
            {
                prop.mark.delete();
                prop.lbl1.delete();
                prop.lbl2.delete();
                prop.lbl3.delete();
                prop.lbl4.delete();
            }
            int r = 255;
            int g = 0;
            string lbl3_text = "Besitzer: " + prop.owner.p.name;
            if (prop.owner == de.jojoa.methods.getMethods.getAccountByName("ImmobilienManagement"))
            {
                lbl3_text = prop.price + "$";
                g = 255;
            }
            int b = 0;

            prop.mark = API.shared.createMarker(0, prop.pos, new Vector3(), new Vector3(), new Vector3(1, 1, 1), 150, r, g, b);
            prop.lbl1 = API.shared.createTextLabel(mysql.MySQL_PropertyData.getString(prop, "street"), prop.pos.Add(new Vector3(0, -1.5, 0)), 8, 10);
            prop.lbl2 = API.shared.createTextLabel(mysql.MySQL_PropertyData.getString(prop, "zone"), prop.pos.Add(new Vector3(0, -2.5, 0)), 8, 10);
            prop.lbl3 = API.shared.createTextLabel(lbl3_text, prop.pos.Add(new Vector3(0, -4.5, 0)), 8, 10);
            prop.lbl4 = API.shared.createTextLabel(prop.typeName, prop.pos.Add(new Vector3(0, -3.5, 0)), 8, 1);

        }
        #endregion Property
    }
}
