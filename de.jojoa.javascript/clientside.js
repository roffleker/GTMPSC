"use strict";
/// <reference path="types-gt-mp/index.d.ts" />
var money = 0;
API.onServerEventTrigger.connect(function (eventName, args) {
    switch (eventName) {
        case 'resetCamera':
            API.setActiveCamera(null);
            break;
        case 'interpolateCamera':
            var pos1 = args[0];
            var pos2 = args[1];
            var time = args[2];
            var cam1 = API.createCamera(pos1, new Vector3());
            var cam2 = API.createCamera(pos2, new Vector3());
            API.interpolateCameras(cam1, cam2, time, false, false);
            break;
        case 'setCamera':
            var pos = args[0];
            var cam = API.createCamera(pos, new Vector3());
            API.setActiveCamera(cam);
            break;
        case 'cBlip':
            var b = API.createBlip(args[0]);
            API.setBlipColor(b, args[2]);
            API.setBlipSprite(b, args[1]);
            API.setBlipShortRange(b, args[3]);
            API.setBlipName(b, args[4]);
            break;
        case 'acc_draw':
            money = args[0];
            break;
    }
});
API.onUpdate.connect(function () {
    if (money != 0) {
        var res = API.getScreenResolution();
        API.drawText(money.toString(), res.Width - 200, res.Height - 100, 36, 255, 255, 0, 150, 7, 1, false, true, 15);
    }
});
