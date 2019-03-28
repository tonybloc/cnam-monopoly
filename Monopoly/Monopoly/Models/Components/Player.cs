using Monopoly.Handlers;
using Monopoly.Models.Bank;
using Monopoly.Models.Components.Cards;
using Monopoly.Models.Components.Cells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components
{
    public class Player : INotifyPropertyChanged
    {
        #region Constantes & Enumeration

        public enum TypeOfPlayer : int {USER = 0,BOT = 1}
        public enum StatusOfPlayer : int { WAITING = 0, PLAYING = 1}
        public enum TypeOfBulding : int { BUILD_HOTEL = 0, BUILD_HOUSE = 2}
        private static int NextID = 0;
        #endregion

        #region Variables
        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if(_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if(_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _position;
        public int Position
        {
            get
            {
                return _position;
            }
            set
            {
                if(_position != value)
                {
                    _position = value;
                    OnPropertyChanged();
                }
            }
        }
        private Pawn _pawn;
        public Pawn Pawn
        {
            get
            {
                return _pawn;
            }
            set
            {
                if(_pawn != value)
                {
                    _pawn = value;
                    OnPropertyChanged();
                }
            }
        }
        private StatusOfPlayer _status;
        public StatusOfPlayer Status
        {
            get
            {
                return _status;
            }
            set
            {
                if(_status != value)
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _inJail;
        public bool InJail
        {
            get { return _inJail; }
            set
            {
                if(_inJail != value)
                {
                    _inJail = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _isWinner;
        public bool IsWinner
        {
            get { return _isWinner; }
            set
            {
                if (_isWinner != value)
                {
                    _isWinner = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _hasLost;
        public bool HasLost
        {
            get { return _hasLost; }
            set
            {
                if (_hasLost != value)
                {
                    _hasLost = value;
                    OnPropertyChanged();
                }
            }
        }

        private TypeOfPlayer _playerType;
        public TypeOfPlayer PlayerType
        {
            get
            {
                return _playerType;
            }
            set
            {
                if(_playerType != value)
                {
                    _playerType = value;
                    OnPropertyChanged();
                }
            }
        }
       
        public BankAccount Amount { get; set; }

        public ObservableCollection<Property> ListOfProperties { get; private set; }
        public ObservableCollection<Card> ListOfCards { get; private set; }
        
        public event PropertyChangedEventHandler PropertyChanged;        
        #endregion  

        #region Constructeurs
        /// <summary>
        /// Create new instance of class
        /// </summary>
        public Player()
        {
            NextID += 1;
            this.Id = NextID;
            this.Name = "";
            this.Pawn = new Pawn();
            this.Position = 0;
            this.Status = StatusOfPlayer.WAITING;
            this.InJail = false;
            this.HasLost = false;
            this.IsWinner = false;
            this.PlayerType = TypeOfPlayer.USER;
            this.ListOfProperties = new ObservableCollection<Property>();
            this.ListOfCards = new ObservableCollection<Card>();
            this.Amount = null;
        }

        /// <summary>
        /// Create a new instance of class
        /// </summary>
        /// <param name="name">name of player</param>
        /// <param name="pawn">pwan of player</param>
        public Player(string name, Pawn pawn, TypeOfPlayer type)
        {
            NextID += 1;
            this.Id = NextID;
            this.Name = name;
            this.Pawn = pawn;
            this.Position = 0;
            this.Status = StatusOfPlayer.WAITING;
            this.InJail = false;
            this.HasLost = false;
            this.IsWinner = false;
            this.PlayerType = type;
            this.ListOfProperties = new ObservableCollection<Property>();            
            this.ListOfCards = new ObservableCollection<Card>();
            this.Amount = null;
        }

        #endregion

        #region NotifyPropertyChanged

        /// <summary>
        /// Notify Property Changed
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Methodes publique

        /// <summary>
        /// Deplace le joueurs à la position cible
        /// </summary>
        /// <param name="nb">Nombre de deplacement</param>
        public void MoveTo(int position)
        {
            this.Position = position;
        }

        /// <summary>
        /// Deplace le joueur sur une case cible
        /// </summary>
        /// <param name="cell">Case du plateau</param>
        public void MoveTo(Cell cell)
        {
            this.Position = cell.Id;      
        }
        
        /// <summary>
        /// Add new Property in list
        /// </summary>
        /// <param name="property">Property</param>
        public void AddPorperty(Property p)
        {
            this.ListOfProperties.Add(p);
        }

        /// <summary>
        /// Remove property in list
        /// </summary>
        /// <param name="p">Property</param>
        public void RemoveProperty(Property p)
        {
            this.ListOfProperties.Remove(p);
        }

        /// <summary>
        /// Add new card in list
        /// </summary>
        /// <param name="c">Card</param>
        public void AddCard(Card c)
        {
            this.ListOfCards.Add(c);
        }

        /// <summary>
        /// Remove Card in list
        /// </summary>
        /// <param name="c">Card</param>
        public void RemoveCard(Card c)
        {
            this.ListOfCards.Remove(c);
        }

        #endregion
    }
}
