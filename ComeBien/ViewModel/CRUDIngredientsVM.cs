using ComeBien.DataAccess.Repositories;
using ComeBien.Models.Database;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComeBien.ViewModel
{
    public class CRUDIngredientsVM : NotificationClass
    {
        private readonly IIngredientsRepository _ingredientsRepository;
        private ICommand _saveCommand;
        private ICommand _deleteCommand;
        private ICommand _selectCommand;
        private ICommand _resetCommand;
        private ObservableCollection<Ingredients> ingredientsCollection;
        private Ingredients _ingredient;
        public CRUDIngredientsVM()
        {
            _ingredientsRepository = new IngredientsRepository();
            _ingredient = new Ingredients();
            InitializeCollection();
        }

        private void InitializeCollection()
        {
            var ingredients = _ingredientsRepository.GetAllSync();
            IngredientsCollection = new ObservableCollection<Ingredients>(ingredients);
        }

        public ObservableCollection<Ingredients> IngredientsCollection
        {
            get { return ingredientsCollection; }
            set
            {
                ingredientsCollection = value;
                OnPropertyChanged("IngredientsCollection");
            }
        }

        public Ingredients SelectedIngredient
        {
            get
            {
                return _ingredient;
            }
            set
            {
                _ingredient = value;
                OnPropertyChanged("SelectedIngredient");
            }
        }

        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                    _resetCommand = new RelayCommand(param => ResetData(), true);

                return _resetCommand;
            }
        }
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new RelayCommand(async param => await SaveData(), true);

                return _saveCommand;
            }
        }

        public ICommand SelectCommand
        {
            get
            {
                if (_selectCommand == null)
                    _selectCommand = new RelayCommand(async param => await Select((int)param), true);
                
                return _selectCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new RelayCommand(async param => await DeleteIngredient((int)param), true);

                return _deleteCommand;
            }
        }

        public async Task DeleteIngredient(int id)
        {
            if (MessageBox.Show(
                ComeBien.Resources.Resources.ResourceManager.GetString("ConfirmDelete"),
                ComeBien.Resources.Resources.ResourceManager.GetString("Ingredient")
                , MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                Log.Information($"Intento de borrado del ingrediente {id}");
                try
                {
                    await _ingredientsRepository.Delete(id);
                    Log.Information($"Borrado satisfactoriamente el ingrediente {id}");
                    MessageBox.Show(ComeBien.Resources.Resources.ResourceManager.GetString("SuccessDelete"));
                }
                catch (Exception ex)
                {
                    Log.Error($"Error borrando el ingrediente {id}",ex);
                    MessageBox.Show($"{ComeBien.Resources.Resources.ResourceManager.GetString("ErrorSaving")}. {ex.InnerException}",
                        "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    InitializeCollection();
                }
            }
        }

        public async Task SaveData()
        {
            if (SelectedIngredient != null 
                && !string.IsNullOrEmpty(SelectedIngredient.FrName)
                && !string.IsNullOrEmpty(SelectedIngredient.EsName)
                && !string.IsNullOrEmpty(SelectedIngredient.EnName)
                && SelectedIngredient.Price > 0
                )
            {
                _ingredient.EsName = SelectedIngredient.EsName;
                _ingredient.FrName = SelectedIngredient.FrName;
                _ingredient.EnName = SelectedIngredient.EnName;
                _ingredient.Price = SelectedIngredient.Price;

                try
                {
                    if (SelectedIngredient.Id <= 0)
                    {
                        Log.Information($"Creando un nuevo ingrediente, de nombre => {SelectedIngredient.EsName}");
                        await _ingredientsRepository.Add(_ingredient);
                        MessageBox.Show(ComeBien.Resources.Resources.ResourceManager.GetString("SuccessAdd"));
                    }
                    else
                    {
                        Log.Information($"Actualizando el ingrediente, de nombre = {SelectedIngredient.EsName} y id = {SelectedIngredient.Id}");

                        _ingredient.Id = SelectedIngredient.Id;
                        await _ingredientsRepository.Update(_ingredient);
                        MessageBox.Show(ComeBien.Resources.Resources.ResourceManager.GetString("SuccessUpdate"));
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("Error salvando el ingrediente", ex);
                    MessageBox.Show($"{ComeBien.Resources.Resources.ResourceManager.GetString("ErrorSaving")}. {ex.InnerException}",
                        "",MessageBoxButton.OK,MessageBoxImage.Error);
                }
                finally
                {
                    InitializeCollection();
                }
            }
            else
            {
                MessageBox.Show($"{ComeBien.Resources.Resources.ResourceManager.GetString("InvalidData")}",
                    "", MessageBoxButton.OK);
            }
        }


        public async Task Select(int id)
        {
            Log.Information($"Ingrediente {id} seleccionado");
            SelectedIngredient = await _ingredientsRepository.FindById(id);
        }
        public void ResetData()
        {
            SelectedIngredient = new Ingredients();
        }
    }
}
