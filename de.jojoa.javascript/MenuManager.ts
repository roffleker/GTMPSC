/// <reference path="types-gt-mp/index.d.ts" />
API.onServerEventTrigger.connect(function (eventName, args) {
    switch (eventName) {
        case 'skinMenu':
            skinMenuHandler();
            break;
        case 'ShopMenu':
            vehicles = args[0].split(";");
            ShopMenuHandler();
       


    }
});

API.onUpdate.connect(function () {
    if (menus != null) {
        menus.ProcessMenus();
    }
});

let menus = null;
var vehicles = []
var skins = ["Abigail", "Abner", "Agent14", "AirHostess01SFY", "AirWorkerSMY", "AlDiNapoli", "AmmuCity01SMY", "Andreas", "AntonB", "ArmyMech01SMY", "Armoured01", "AviSchwartzman", "Bankman", "Barry", "Beach01AMY", "BeachVesp01AMY", "BlackOps02SMY", "Car3Guy1",
    "CIASec01SMM", "Cop01SMY", "DaveNorton", "Doorman01SMY", "FatLatin01AMM", "FIBSec01", "HighSec01SMM", "Hunter", "JewelThief", "Magenta", "Michelle", "MrsThornhill", "PrisGuard01SMM", "RampGang", "SCDressy01AFY", "SWAT01SMY", "Tonya", "Vinewood03AFY", "Zimbor"];
var num = 0;
var bankvalues = ["500,1000,1500,2000,2500,3000,3500,4000,4500,5000,6000,7000,8000,10000"]
var bank;

function skinMenuHandler() {
    menus = API.getMenuPool();
    let menu = API.createMenu("Skin-Auswahl", "Wähle deinen Skin aus.", 0, 0, 6);

    var list = new List(String);
    skins.forEach(function (skin) {
        list.Add(skin);
    });
    var list_item = API.createListItem("Skin", "Auswählen: Enter oder Mausklick", list, 0);
    menu.AddItem(list_item);

    list_item.OnListChanged.connect(function (sender, new_index) {
        num = new_index;
        var model = API.pedNameToModel(skins[num]);
        API.setPlayerSkin(model);

        API.sendChatMessage("Index: " + new_index + " - var:" + num);


    });

    list_item.Activated.connect(function (menu, item) {
        API.triggerServerEvent("setSkin",skins[num])
        API.showCursor(false);
        menu.Visible = false;
    });

    menus.Add(menu);
    menu.Visible = true;
}

function ShopMenuHandler() {
    menus = API.getMenuPool();
    let menu = API.createMenu("Shop", "Wähle ein Fahrzeug aus",0, 0, 6);

    var list = new List(String);
    vehicles.forEach(function (value) {
        list.Add(value.split(":")[0]);
    });


    var list_item_einz = API.createListItem("Fahrzeug", "Auswählen: Enter", list, 0);
    menu.AddItem(list_item_einz);

    var price_item = API.createMenuItem("Preis:","0");
    menu.AddItem(price_item);

    list_item_einz.OnListChanged.connect(function (sender, new_index) {
        num = new_index;
        price_item.Description = list[num].split(":")[1];

        //API.sendChatMessage("Index: " + new_index + " - var:" + num);


    });

   

    list_item_einz.Activated.connect(function (menu, item) {
        API.triggerServerEvent("VehicleBuy", vehicles[num]);
        API.showCursor(false);
        menu.Visible = false;
    });

   

    menus.Add(menu);
    menu.Visible = true;
}
