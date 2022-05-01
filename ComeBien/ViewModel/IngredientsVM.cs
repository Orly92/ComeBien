using ComeBien.DataAccess.Repositories;
using ComeBien.Models.Database;
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
    public class IngredientsVM : NotificationClass
    {
        private readonly IIngredientsRepository _ingredientsRepository;
        private ICommand _saveCommand;
        private ICommand _deleteCommand;
        private ICommand _selectCommand;
        private ICommand _resetCommand;
        private ObservableCollection<Ingredients> ingredientsCollection;
        private Ingredients _ingredient;
        public IngredientsVM()
        {
            _ingredientsRepository = new IngredientsRepository();
            _ingredient = new Ingredients();
            InitializeCollection();
        }

        private async Task InitializeCollection()
        {
            var ingredients = await _ingredientsRepository.GetAll();
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
                   // _selectCommand = new RelayCommand(async param => await Select((int)param), true);
                    _selectCommand = new RelayCommand(param => Select((int)param), true);

                return _selectCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new RelayCommand(async param => await DeleteStudent((int)param), true);

                return _deleteCommand;
            }
        }

        public async Task DeleteStudent(int id)
        {
            if (MessageBox.Show("Confirm delete of this record?", "Student", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                try
                {
                    await _ingredientsRepository.Delete(id);
                    MessageBox.Show("Record successfully deleted.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {
                    await InitializeCollection();
                }
            }
        }

        public async Task SaveData()
        {
            if (SelectedIngredient != null)
            {
                _ingredient.EsName = SelectedIngredient.EsName;
                _ingredient.FrName = SelectedIngredient.FrName;
                _ingredient.EnName = SelectedIngredient.EnName;
                _ingredient.Price = SelectedIngredient.Price;

                try
                {
                    if (SelectedIngredient.Id <= 0)
                    {
                        await _ingredientsRepository.Add(_ingredient);
                        MessageBox.Show("New record successfully saved.");
                    }
                    else
                    {
                        _ingredient.Id = SelectedIngredient.Id;
                        await _ingredientsRepository.Update(_ingredient);
                        MessageBox.Show("Record successfully updated.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {
                    await InitializeCollection();
                }
            }
        }


        public async void Select(int id)
        {
            SelectedIngredient = await _ingredientsRepository.FindById(id);
        }
        public void ResetData()
        {
            SelectedIngredient = new Ingredients();
        }
    }
}
