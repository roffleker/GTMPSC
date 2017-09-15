"use strict";
/// <reference path="types-gt-mp/index.d.ts" />
API.onServerEventTrigger.connect(function (eventName, args) {
    switch (eventName) {
        case 'cefconnect':
            connectHandler(args[0]);
            break;
        case 'wrong':
            wrongHandler(args[0]);
            break;
        case 'BankMenu':
            bankcef(0);
            break;
    }
});
var browser = null;
var money = 0;
var banknum = 0;
function bankcef(state) {
    if (state == 0) {
        API.setCanOpenChat(false);
        API.showCursor(true);
        var res = API.getScreenResolution();
        browser = API.createCefBrowser(500, 600);
        API.waitUntilCefBrowserInit(browser);
        API.setCefBrowserPosition(browser, (res.Width / 2) - (500 / 2), (res.Height / 2) - (499 / 2));
        API.loadPageCefBrowser(browser, "clientside/bank/menu.html");
        // API.sendChatMessage("BROWSER (" + res.Width + "x" + res.Height + ") IS LOADING?: " + API.isCefBrowserLoading(browser));
        API.waitUntilCefBrowserLoaded(browser);
        browser.call("setnum", banknum);
        // API.sendChatMessage("BROWSER LOADED")
    }
}
function connectHandler(state) {
    if (state == 0) {
        API.setCanOpenChat(false);
        API.showCursor(true);
        var res = API.getScreenResolution();
        browser = API.createCefBrowser(500, 600);
        API.waitUntilCefBrowserInit(browser);
        API.setCefBrowserPosition(browser, (res.Width / 2) - (500 / 2), (res.Height / 2) - (499 / 2));
        API.loadPageCefBrowser(browser, "clientside/connect/connect.html");
        API.sendChatMessage("BROWSER (" + res.Width + "x" + res.Height + ") IS LOADING?: " + API.isCefBrowserLoading(browser));
        API.waitUntilCefBrowserLoaded(browser);
        API.sendChatMessage("BROWSER LOADED");
    }
    else if (state == 1) {
        API.setCanOpenChat(false);
        API.showCursor(true);
        var res = API.getScreenResolution();
        browser = API.createCefBrowser(500, 600);
        API.waitUntilCefBrowserInit(browser);
        API.setCefBrowserPosition(browser, (res.Width / 2) - (500 / 2), (res.Height / 2) - (499 / 2));
        API.loadPageCefBrowser(browser, "clientside/connect/forceRegister.html");
        API.sendChatMessage("BROWSER (" + res.Width + "x" + res.Height + ") IS LOADING?: " + API.isCefBrowserLoading(browser));
        API.waitUntilCefBrowserLoaded(browser);
        API.sendChatMessage("BROWSER LOADED");
    }
}
function wrongHandler(state) {
    if (state == 0) {
        API.setCanOpenChat(false);
        API.showCursor(true);
        var res = API.getScreenResolution();
        browser = API.createCefBrowser(500, 600);
        API.waitUntilCefBrowserInit(browser);
        API.setCefBrowserPosition(browser, (res.Width / 2) - (500 / 2), (res.Height / 2) - (499 / 2));
        API.loadPageCefBrowser(browser, "clientside/connect/wrongLogin.html");
        API.sendChatMessage("BROWSER (" + res.Width + "x" + res.Height + ") IS LOADING?: " + API.isCefBrowserLoading(browser));
        API.waitUntilCefBrowserLoaded(browser);
        API.sendChatMessage("BROWSER LOADED");
    }
    else if (state == 1) {
        API.setCanOpenChat(false);
        API.showCursor(true);
        var res = API.getScreenResolution();
        browser = API.createCefBrowser(500, 600);
        API.waitUntilCefBrowserInit(browser);
        API.setCefBrowserPosition(browser, (res.Width / 2) - (500 / 2), (res.Height / 2) - (499 / 2));
        API.loadPageCefBrowser(browser, "clientside/connect/wrongRegister.html");
        API.sendChatMessage("BROWSER (" + res.Width + "x" + res.Height + ") IS LOADING?: " + API.isCefBrowserLoading(browser));
        API.waitUntilCefBrowserLoaded(browser);
        API.sendChatMessage("BROWSER LOADED");
    }
}
function login(pw) {
    API.showCursor(false);
    API.destroyCefBrowser(browser);
    API.setCanOpenChat(true);
    API.triggerServerEvent("checkLogin", pw);
}
function register(pw1, pw2) {
    API.showCursor(false);
    API.destroyCefBrowser(browser);
    API.setCanOpenChat(true);
    API.triggerServerEvent("checkRegister", pw1, pw2);
}
function einzahl(betrag) {
    API.triggerServerEvent("Einzahlen", banknum, betrag);
}
function auszahl(betrag) {
    API.triggerServerEvent("Auszahlen", banknum, betrag);
}
function uberweis(betrag, empfanger) {
    API.triggerServerEvent("Ueberweisen", banknum, betrag, empfanger);
}
