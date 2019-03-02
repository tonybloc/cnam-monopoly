namespace Monopoly.Settings
{
    /// <summary>
    /// Global settings 
    /// </summary>
    public static class Config
    {
        // XML files path
        public static readonly string filePath_XmlBoard = "../../Resources/XML/XMLBoardCell.xml";
        
        // Setting game
        public static int NUMBER_MAX_OF_HOTEL = 16;
        public static int NUMBER_MAX_OF_HOUSE = 32;
        public static int INITIAL_BANK_BALANCE = 15140;
        public static int INITIAL_PLAYER_BANK_BALANCE = 1500;
        public static int INITIAL_PARKING_BALANCE = 1500;
        public static int GRATIFICATION = 200;

    }
}