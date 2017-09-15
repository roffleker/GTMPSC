/// <reference path="types-gt-mp/index.d.ts" />
var listm = [];
var listb = [];
var listtemp = [];
var listres = [];
var x;
var y;
API.onUpdate.connect(function () {
    if (listres != []) {
        var res = API.getScreenResolution();
        var x = (res.Width - 150) / 2;
        var y = (res.Height - (40 + listres.length * 20)) / 2
        API.drawRectangle(x, y, 150, (40 + listres.length * 20), 255, 255, 255, 150);
        API.drawRectangle(x + 5, y + 30, 140, 1, 255, 255, 255, 150);
        API.drawRectangle(x + 100, y + 45, 1, (listres.length * 20) - 10, 255, 255, 255, 150);
        API.drawText("ERGEBNISSE", x + 2, y + 2, 36, 255, 255, 255, 150, 7, 1, false, false, 20);
        
        listres.forEach(draw)
    }
});

function draw(item,index) {
    var arr = item.split(":")
    var time = arr[0];
    var name = arr[1];
    var xp = x + 2;
    var yp = y + 32;
    API.drawText(time + " Sek.", xp + 100, yp + index * 20, 18, 255, 255, 255, 150, 6, 1, false, false, 15);
    API.drawText(name, xp, yp + index * 20, 18, 255, 255, 255, 150, 6, 1, false, false, 15);
}

API.onServerEventTrigger.connect(function (eventName, args) {
    switch (eventName) {

        case 'racep':
            var i = args[0];
            var t = args[1];
            var m = listm[i];
            var m2 = listm[i + 1];
            var color = new System.Drawing.Color
            color.FromArgb(255, 255, 255, 0);

            if (API.getMarkerColor(m) == color) {
                API.setMarkerColor(m, 255, 0, 255, 0);
                API.setMarkerColor(m2, 255, 255, 255, 0);

                var b = listm[i];
                var b2 = listm[i + 1];
                API.setBlipColor(b, 24);
                API.setBlipScale(b, 0.8);
                API.setBlipColor(b2, 46);
                API.setBlipScale(b2, 1);

                API.sendChatMessage("Checkpoint: " + i + "Zeit:" + t / 100);
            }
            break;
        case 'racecp':
            var pos = API.getEntityPosition(API.getLocalPlayer());
            listm.push(API.createMarker(1, pos, new Vector3(), new Vector3(), new Vector3(1, 1, 1), 255, 0, 0, 255));
            listb.push(API.createBlip(pos));
            break;
        case 'racerp':
            var ind = args[0];
            


            API.deleteEntity(listb[ind]);
            API.deleteEntity(listm[ind]);
            listb.splice(ind, 1);
            listm.splice(ind, 1);

            break;
        case 'racesp':
            var pos2 = args[0];
            listm.push(API.createMarker(1, pos2, new Vector3(), new Vector3(), new Vector3(1, 1, 1), 255, 0, 0, 255));
            listb.push(API.createBlip(pos2));
            break;
        case 'racest':
            var m3 = listm[0];
            var m4 = listm[listm.length - 1];
            API.setMarkerColor(m3, 255, 255, 255, 0);
            API.setMarkerColor(m4, 255, 0, 0, 255);

            break;
        case "ergebniss":
            var inn = args[0];
            listres = inn;
           
           
            

            break;


    }
});