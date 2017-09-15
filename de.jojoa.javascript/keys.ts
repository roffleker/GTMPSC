/// <reference path="types-gt-mp/index.d.ts" />
API.onKeyDown.connect(function (sender, e) {
    if (e.KeyCode === Keys.L) {
        API.triggerServerEvent("KeyL");
    }
})