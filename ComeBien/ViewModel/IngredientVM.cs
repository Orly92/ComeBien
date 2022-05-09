using ComeBien.Models.Database;
using ComeBien.Models.Globals;
using ComeBien.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeBien.ViewModel
{
    internal class IngredientVM : NotificationClass
    {
        private ICommand _addCommand;
        private ICommand _removeCommand;
        public IngredientVM()
        {

        }
        public IngredientVM(Ingredients x)
        {
            Id = x.Id;
            Price = Math.Round(x.Price,2);
            switch (ConfigService.lang)
            {
                case Languages.France:
                    Name = x.FrName;
                    break;
                case Languages.English:
                    Name = x.EnName;
                    break;
                default:
                    Name = x.EsName;
                    break;
            }
            Quantity = 0;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                    _addCommand = new RelayCommand(param => AddIng(), true);

                return _addCommand;
            }
        }

        public ICommand RemoveCommand
        {
            get
            {
                if (_removeCommand == null)
                    _removeCommand = new RelayCommand(param => RemIng(), true);

                return _removeCommand;
            }
        }

        private void AddIng()
        {
            Log.Information($"Adicionando un ingrediente {Id}:{Name}");
            Quantity += 1;
            OnPropertyChanged("Quantity");
        }

        private void RemIng()
        {
            if (Quantity > 0)
            {
                Log.Information($"Disminuyendo un ingrediente {Id}:{Name}");
                Quantity -= 1;
                OnPropertyChanged("Quantity");
            }
        }

        public void ResetQuantity()
        {
            Log.Information($"Reseteando un ingrediente {Id}:{Name}");
            Quantity = 0;
            OnPropertyChanged("Quantity");
        }
    }
}
