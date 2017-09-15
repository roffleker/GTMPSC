using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealifeGM.de.jojoa.methods
{
    class stringMethods
    {
        public static string console_prefix = "German Realife - ";
        public static string main_prefix = "~g~German Realife ~w~- ";

        public static string console_ressource_start = console_prefix + "Das Script wurde erfolgreich gestartet.";
        public static string console_login = console_prefix + "%name%, hat sich eingeloggt";
        public static string console_register = console_prefix + "%name%, hat sich registriert";


        public static string[] login_msg = { main_prefix + "Herzlich Willkommen, %name%.", main_prefix + "Wir konnten dich in unserer Datenbank finden!", main_prefix + "Bitte logge dich ein, indem du das Formular befolgst." };
        public static string[] register_msg = { main_prefix + "Herzlich Willkommen, %name%.", main_prefix + "Wir konnten dich nicht in unserer Datenbank finden!", main_prefix + "Bitte registrire dich nun, indem du das Formular befolgst." };

        public static string server_success_getted_data = main_prefix + "Der Server hat erfolgreich die Daten empfangen.";
        public static string success_login = main_prefix + "Du hast dich erfolgreich eingeloggt.";
        public static string success_register = main_prefix + "Du hast dich erfolgreich registriert.";
        public static string success_skin = main_prefix + "Du hast dir erfogreich einen Skin ausgesucht.";

        public static string[] choose_skin = {main_prefix + "Du hast noch keinen Skin gewählt.", main_prefix + "Bitte wähle nun einen aus"};



    }
}
