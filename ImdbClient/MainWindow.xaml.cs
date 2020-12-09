using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using ImdbServiceApp;
using ImdbClient.ImdbServiceReference;
using System.ComponentModel;
using System;

namespace ImdbClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string predicate = null;
        private readonly int pageSize = 20;
        private int pageIndex = 0;
        private int maxPageIndex = 1;
        public int recordsCount = 0;
        public int _moviesCount = 0;
        public IEnumerable<Movies> pageList;
        private readonly ImdbRepositoryClient client;
        public Movies selectedMovie;
#pragma warning disable IDE1006 // Naming Styles
        private ObservableCollection<Movies> _movies { get; set; }
#pragma warning restore IDE1006 // Naming Styles
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isLocked = true;
        private bool saveEqualsCreate = false;
        private bool saveEqualsUpdate = false;
#pragma warning disable IDE1006 // Naming Styles
        public ObservableCollection<Movies> _Movies
#pragma warning restore IDE1006 // Naming Styles
        {
            get
            {
                return _movies;
            }
            set
            {
                _movies = value;
                RaisePropertyChanged("Movies");
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            client = new ImdbRepositoryClient();
            lvUsers.ItemsSource = null;
            lvComments.ItemsSource = null;
        }

        private void LoadData()
        {
            predicate = searchTextBox.Text.Trim();
            _movies = client.GetAllMoviesPaged(predicate, pageSize, pageIndex).ToObservableCollection();
            lvUsers.ItemsSource = _movies;
            moviesDataGrid.ItemsSource = _movies;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LockEditor();
            
        }

        private void SearchTextBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (searchTextBox.Text == "")
            {
                searchTextBox.Text = "Search";
            }
        }

        private void SearchTextBox_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (searchTextBox.Text.Equals("Search"))
            {
                searchTextBox.Text = "";
            }
        }

        private void SearchTextBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LoadData();
            GetCount();
        }

        private void SearchTextBox_MouseDoubleClickAsync(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            LoadData();
            GetCount();
        }

        private void SearchTextBoxt_KeyDownAsync(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key == System.Windows.Input.Key.Return)
            {
                LoadData();
                GetCount();
            }
        }

        private void UnlockEditor()
        {
            tconsttxtbx.IsEnabled = true;
            genrestxtbx.IsEnabled = true;
            originalTitletxtbx.IsEnabled = true;
            primaryTitletxtbx.IsEnabled = true;
            startYeartxtbx.IsEnabled = true;
            endYeartxtbx.IsEnabled = true;
            titleTypetxtbx.IsEnabled = true;
            runtimeMinutestxtbx.IsEnabled = true;
            isAdultchkbx.IsEnabled = true;
        }
        private void LockEditor()
        {
            tconsttxtbx.IsEnabled = false;
            genrestxtbx.IsEnabled = false;
            originalTitletxtbx.IsEnabled = false;
            primaryTitletxtbx.IsEnabled = false;
            startYeartxtbx.IsEnabled = false;
            endYeartxtbx.IsEnabled = false;
            titleTypetxtbx.IsEnabled = false;
            runtimeMinutestxtbx.IsEnabled = false;
            isAdultchkbx.IsEnabled = false;
        }
        private void SaveChangeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Movie Saved");
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Close Program");
            Reset();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Movie Deleted");
         //   client.DeleteMovieById(selectedMovie);
        }

        private void Reset()
        {
            this.DataContext = null;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            GetCount();
        }

        public Movies SelectedMovie
        {
            get
            {
                return selectedMovie;
            }
            set
            {
                selectedMovie = value;
                RaisePropertyChanged("SelectedMovie");
            }
        }

        private async void GetCount()
        {
            RecordsCountTxtbx.Text = "";
            var temp = await client.GetAllMoviesByPredicateCountAsync(predicate);
            RecordsCountTxtbx.Text = temp.ToString();
            maxPageIndex = temp / pageSize;
        }

        private void Get_movieCount()
        {
            _moviesCount = _movies.Count;
        }

        private void LvUsers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedMovie = (Movies)lvUsers.SelectedItem;
            DataContext = selectedMovie;
            UnlockEditor();
        }

        private void LvUsers_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            selectedMovie = (Movies)lvUsers.SelectedItem;
            DataContext = selectedMovie;
            UnlockEditor();
        }

        private void AddMovieButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = null;

            if (isLocked.Equals(true))
            {
                MessageBox.Show("You may now add a new movie");
                isLocked = false;
                saveEqualsUpdate = false;
                saveEqualsCreate = true;
                UnlockEditor();
            }
            else
            {
                isLocked = true;
                saveEqualsUpdate = false;
                saveEqualsCreate = false;
                MessageBox.Show("Editor is now locked");
                LockEditor();
            }
        }

        private void SaveMovieButton_Click(object sender, RoutedEventArgs e)
        {
            if (saveEqualsUpdate.Equals(true))
            {
                client.UpdateMovie(selectedMovie);
                MessageBox.Show("Movie is Updated");
                isLocked = true;
                saveEqualsUpdate = false;
                saveEqualsCreate = false;
                MessageBox.Show("Editor is now locked");
                LockEditor();
            }
            else if (saveEqualsCreate.Equals(true))
            {
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                bool ifSuccessStartYear = short.TryParse(startYeartxtbx.Text, out short tempStartYear);
#pragma warning restore IDE0059 // Unnecessary assignment of a value
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                bool ifSuccessEndYear = short.TryParse(endYeartxtbx.Text, out short tempEndYear);
#pragma warning restore IDE0059 // Unnecessary assignment of a value
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                bool ifSuccessRunTimeMinutes = short.TryParse(runtimeMinutestxtbx.Text, out short tempRunTimeMinutes);
#pragma warning restore IDE0059 // Unnecessary assignment of a value

                Movies newMovie = new Movies()
                {
                    tconst = tconsttxtbx.Text,
                    titleType = titleTypetxtbx.Text,
                    primaryTitle = primaryTitletxtbx.Text,
                    originalTitle = originalTitletxtbx.Text,
                    isAdult = Convert.ToBoolean(isAdultchkbx.IsChecked),
                    startYear = tempStartYear,
                    endYear = tempEndYear,
                    runtimeMinutes = tempRunTimeMinutes,
                    genres = genrestxtbx.Text
                };

                client.AddMovie(newMovie);
                MessageBox.Show("New Movie is Saved");
                isLocked = true;
                saveEqualsUpdate = false;
                saveEqualsCreate = false;
                MessageBox.Show("Editor is now locked");
                LockEditor();
                LoadData();
            }
            else
            {
                MessageBox.Show("Click Add or Edit movie before saving");
            }
            isLocked = true;
        }

        private void EditMovieButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (isLocked.Equals(true) && DataContext != null)
            {
                MessageBox.Show("You may now edit a new movie");
                isLocked = false;
                saveEqualsUpdate = true;
                saveEqualsCreate = false;
                UnlockEditor();
            }
            else
            {
                isLocked = true;
                saveEqualsUpdate = false;
                saveEqualsCreate = false;
                MessageBox.Show("Choose a movie to edit");
                LockEditor();
            }
        }

        private void CloseMovieButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = null;
            _movies = null;
            lvUsers.ItemsSource = _movies;
            moviesDataGrid.ItemsSource = _movies;
        }

        private void DeleteMovieButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext == null)
            {
                MessageBox.Show("Choose a movie to delete");
            }
            else
            {
                if (MessageBox.Show("Delete Movie?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    MessageBox.Show("Movie wasn't deleted");
                }
                else
                {
                    client.DeleteMovieById(selectedMovie.tconst);
                    MessageBox.Show("Movie deleted");
                    LoadData();
                }
            }
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (pageIndex > 0)
            {
                pageIndex--;
            }
            LoadData();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (pageIndex <= maxPageIndex)
            {
                pageIndex++;
                LoadData();
            }
            else
            {
                MessageBox.Show("That is the end of the records");
            }
        }

        private void DatagridAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Datagrid Add Disabled");
        }

        private void DatagridEdit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Datagrid Edit Disabled");
        }

        private void DatagridSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Datagrid Save Disabled");
        }

        private void DatagridClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Datagrid Close Disabled");
        }

        private void DatagridDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Datagrid Delete Disabled");
        }

        private void ReviewsAddButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Reviews Add Button Disabled");
        }

        private void ReviewsEditButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Reviews Edit Button Disabled");
        }

        private void ReviewsSaveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Reviews Save Button Disabled");
        }

        private void ReviewsCloseButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Reviews Close Button Disabled");
        }
    }

    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> col)
        {
            return new ObservableCollection<T>(col);
        }
    }
}

