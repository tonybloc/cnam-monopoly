using Monopoly.Models.Components;
using Monopoly.Models.Components.Cells;
using Monopoly.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Bank
{
    /// <summary>
    /// Main bank of the game
    /// </summary>
    public class Bank
    {
        #region Constantes
        public static int GLOBAL_BANK_ACCOUNT = 0;
        public static int PARKING_BANK_ACCOUNT = 1;
        #endregion

        #region Variables
        /// <summary>
        /// Instance of the bank
        /// </summary>
        private static Bank _instance = null;

        /// <summary>
        /// Players bank account
        /// </summary>
        private Dictionary<int, BankAccount> DictionayOfBankAcount;
        
        /// <summary>
        /// Number of houses
        /// </summary>
        public int NbHouse { get; private set; }

        /// <summary>
        /// Number of hotels
        /// </summary>
        public int NbHotel { get; private set; }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Creation of the bank
        /// </summary>
        private Bank()
        {
            NbHotel = Config.NUMBER_MAX_OF_HOUSE;
            NbHouse = Config.NUMBER_MAX_OF_HOTEL;
            DictionayOfBankAcount = new Dictionary<int, BankAccount>();
            DictionayOfBankAcount.Add(GLOBAL_BANK_ACCOUNT, new BankAccount(Config.INITIAL_BANK_BALANCE));
            DictionayOfBankAcount.Add(PARKING_BANK_ACCOUNT, new BankAccount(Config.INITIAL_PARKING_BALANCE));
        }

        /// <summary>
        /// Instance of the bank
        /// </summary>
        public static Bank Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Bank();
                }
                return _instance;
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Get user's bank account 
        /// </summary>
        /// <param name="p">Player</param>
        /// <returns>Payer' bank account</returns>
        public BankAccount GetBankAccount(Player player)
        {
            return DictionayOfBankAcount[player.Id];
        }

        /// <summary>
        /// Get main bank account 
        /// </summary>
        /// <returns>Bank account</returns>
        public BankAccount GetBankAccount()
        {
            return DictionayOfBankAcount[GLOBAL_BANK_ACCOUNT];
        }

        /// <summary>
        /// Create a specific bank account for a player 
        /// </summary>
        /// <param name="p">Joueur</param>
        public void CreateBankAccount(Player p)
        {
            if (!this.DictionayOfBankAcount.ContainsKey(p.Id))
            {
                this.DictionayOfBankAcount.Add(p.Id, new BankAccount(Config.INITIAL_PLAYER_BANK_BALANCE));
            }
            else
            {
                Console.WriteLine("Le joueur possède déjà un compte en banque");
            }

        }

        /// <summary>
        /// Payment of the gratification when the user stay or pass on the start point
        /// </summary>
        /// <param name="p">Player at the start point </param>
        public void PaymentGratification(Player p)
        {
            BankAccount bankAccount = GetBankAccount();
            BankAccount playerAccount = GetBankAccount(p);
            bankAccount.BankTransfer(playerAccount, Config.GRATIFICATION);
        }

        public void SellHouse(Player p , Land l)
        {
            if (NbHouse > 0)
            {
                NbHouse--;
                l.AddHouse();

                BankAccount bankAccount = GetBankAccount();
                BankAccount playerAccount = GetBankAccount(p);
                playerAccount.BankTransfer(bankAccount, l.LandGroup.HousePrice);
            }
            else
                throw new Exception();
        }


        public void SellHotel(Player p, Land l)
        {
            if (NbHotel > 0)
            {
                NbHotel--;
                l.AddHotel();

                BankAccount bankAccount = GetBankAccount();
                BankAccount playerAccount = GetBankAccount(p);
                playerAccount.BankTransfer(bankAccount, l.LandGroup.HotelPrice);
            }
        }

        #endregion


    }
}
