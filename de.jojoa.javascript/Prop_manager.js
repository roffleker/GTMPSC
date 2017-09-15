"use strict";
/// <reference path="types-gt-mp/index.d.ts" />
API.onServerEventTrigger.connect(function (eventName, args) {
    switch (eventName) {
        case 'getLoc':
            var pos = args[0];
            var id = args[1];
            var zone = API.getZoneName(pos);
            var street = API.getStreetName(pos);
            API.triggerServerEvent("getLoc_rt", street, zone, id);
            break;
    }
});
API.onUpdate.connect(function () {
    if (API.isControlJustPressed(51)) {
        API.triggerServerEvent("bProp");
    }
});
