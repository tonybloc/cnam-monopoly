using System.Collections.Generic;

namespace Monopoly.Settings
{
    /// <summary>
    /// Global settings 
    /// </summary>
    public static class Config
    {
        // XML files path
        public static readonly string filePath_XmlBoard = "./Resources/XML/XMLBoardCell.xml";
        public static readonly string filePath_XmlCommunityCard = "./Resources/XML/XMLCommunityCard.xml";
        public static readonly string filePath_XmlChanceCard = "./Resources/XML/XMLChanceCard.xml";

        // Setting game
        public static int NUMBER_MAX_OF_HOTEL = 16;
        public static int NUMBER_MAX_OF_HOUSE = 32;
        public static int INITIAL_BANK_BALANCE = 10000000;
        public static int INITIAL_PLAYER_BANK_BALANCE = 400;
        public static int INITIAL_PARKING_BALANCE = 0;
        public static int GRATIFICATION = 200;
        public static int NB_MIN_PLAYER_IN_GAME = 2;
        public static int NB_MAX_PLAYER_IN_GAME = 8;
        public static int NB_MAX_LAUNCH_DICES = 3;
        public static int JAIL_POSITION = 10;

        public static List<string> BotNames = new List<string> { "BOT-Patrick", "BOT-Charles", "BOT-Camille", "BOT-Jean", "BOT-Sarah", "BOT-Lucile", "BOT-François", "BOT-Clara", "BOT-Paul" };
    }
}