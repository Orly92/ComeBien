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
        public EventHandler ShowMessageBox = delegate { };
        private ICommand _saveCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        public IngredientsVM()
        {
            _ingredientsRepository = new IngredientsRepository();
            InitializeCollection();
        }

        private async Task InitializeCollection()
        {
            var ingredients = await _ingredientsRepository.GetAll();
            IngredientsCollection = new ObservableCollection<Ingredients>(ingredients);
        }

        private ObservableCollection<Ingredients> ingredientsCollection;
        private Ingredients _ingredient;

        public ObservableCollection<Ingredients> IngredientsCollection
        {
            get { return ingredientsCollection; }
            set
            {
                ingredientsCollection = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new RelayCommand(param => SaveData(), null);

                return _saveCommand;
            }
        }

        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                    _editCommand = new RelayCommand(param => EditData((int)param), null);

                return _editCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new RelayCommand(async param => await DeleteStudent((int)param), null);

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

        public void SaveData()
        {
            //if (StudentRecord != null)
            //{
            //    _studentEntity.Name = StudentRecord.Name;
            //    _studentEntity.Age = StudentRecord.Age;
            //    _studentEntity.Address = StudentRecord.Address;
            //    _studentEntity.Contact = StudentRecord.Contact;

            //    try
            //    {
            //        if (StudentRecord.Id <= 0)
            //        {
            //            _repository.AddStudent(_studentEntity);
            //            MessageBox.Show("New record successfully saved.");
            //        }
            //        else
            //        {
            //            _studentEntity.ID = StudentRecord.Id;
            //            _repository.UpdateStudent(_studentEntity);
            //            MessageBox.Show("Record successfully updated.");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Error occured while saving. " + ex.InnerException);
            //    }
            //    finally
            //    {
            //        GetAll();
            //        ResetData();
            //    }
            //}
        }

        public void EditData(int id)
        {
            //var model = _repository.Get(id);
            //StudentRecord.Id = model.ID;
            //StudentRecord.Name = model.Name;
            //StudentRecord.Age = (int)model.Age;
            //StudentRecord.Address = model.Address;
            //StudentRecord.Contact = model.Contact;
        }

    }
}
