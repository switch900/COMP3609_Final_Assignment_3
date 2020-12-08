using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ImdbClient.ImdbServiceReference;
using System.Collections.ObjectModel;

namespace ImdbClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       /* public ObservableCollection<Movies> ObserveMovieList { get; set; }
        public static List<Movies> movieList;
        public int ienumerableCount;
        public string tConst = "tt0000014";
        public string originalTitle = null;

        private ImdbServiceReference.ImdbServiceClient dataServiceClient;*/
        //  private System.Data.Services.Client.DataServiceQuery<AdventureWorksService.SalesOrderHeader> salesQuery;
        // private CollectionViewSource ordersViewSource;

        public MainWindow()
        {
            InitializeComponent();

            List<User> items = new List<User>();
            items.Add(new User() { Id = "AB1000002", Name = "Northgate Chevrolet Buick GMC Ltd" });
            items.Add(new User() { Id = "AB1000003", Name = "Smyl Chevrolet Ltd" });
            items.Add(new User() { Id = "AB1000004", Name = "Weldner Motors Limited" });
            items.Add(new User() { Id = "AB1000005", Name = "Griffiths Motors (Hinton)" });
            items.Add(new User() { Id = "AB1000006", Name = "Chinook Chevrolet Buick GMC Ltd" });
            items.Add(new User() { Id = "AB1000007", Name = "Thomas Homes & R.V. Center (1997) Inc." });
            items.Add(new User() { Id = "AB1000008", Name = "Silverhill Acura" });
            items.Add(new User() { Id = "AB1000009", Name = "Alberta Indian Investment Corp." });
            items.Add(new User() { Id = "AB1000010", Name = "Jaman Mazda" });
            items.Add(new User() { Id = "AB1000011", Name = "Ninth Avenue Auto Mart" });
            lvUsers.ItemsSource = items;

            List<Product> products = new List<Product>();
            products.Add(new Product() { ProductID = "CR", StartDate = "12-Dec-2014", EndDate = "31-Dec-9999", ProductCategory = "RTM", Description = "Doe, FirstSalesStaff Hi(Executive Vice President)" });
            products.Add(new Product() { ProductID = "UN", StartDate = "12-Dec-2014", EndDate = "31-Dec-9999", ProductCategory = "FOM", Description = "Yay, SecondSalesStaff Hello" });
            lvProducts.ItemsSource = products;

            List<Comments> comments = new List<Comments>();
            comments.Add(new Comments() { FirstLine = "Personnel (4) comment by Slic\\Arra on 15-Dec-2014", AlertExpires = "Alert expires 18-Dec-2014", Comment = "This is a new comment" });
            comments.Add(new Comments() { FirstLine = "Other Products (5) comment by Slic\\Arra on 15-Dec-2014", AlertExpires = "Alert expires 18-Dec-2014", Comment = "This is a new test" });
            comments.Add(new Comments() { FirstLine = "Legal (1) comment by Slic\\Arra on 15-Dec-2014", AlertExpires = "Alert expires 10-Dec-2014", Comment = "My latest comment." });
            comments.Add(new Comments() { FirstLine = "General (3) comment by Slic\\Arra on 12-Dec-2014", AlertExpires = "Alert expires 13-Dec-2014", Comment = "This is my test." });
            lvComments.ItemsSource = comments;
        }

       /* private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO: Modify the port number in the following URI as required.
            dataServiceClient = new ImdbServiceReference.ImdbServiceClient();
            GetCount(tconst);

            // salesQuery = dataServiceClient.SalesOrderHeaders;
            *//*
                        ordersViewSource = ((CollectionViewSource)(this.FindResource("salesOrderHeadersViewSource")));
                        ordersViewSource.Source = salesQuery.Execute();
                        ordersViewSource.View.MoveCurrentToFirst();*//*
        }*/


        private void GetCount(string id)
        {
            ImdbServiceReference.ImdbServiceClient movies = new ImdbServiceReference.ImdbServiceClient();
            MessageBox.Show(dataServiceClient.GetAllMoviesById(id).originalTitle);

            // TotalCount.Content = temp.Count;
        }

    }
    public class User
        {
            public string Id { get; set; }

            public string Name { get; set; }

        }

        public class Product
        {
            public string ProductID { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string ProductCategory { get; set; }
            public string Description { get; set; }
        }

        public class Comments
        {
            public string FirstLine { get; set; }
            public string AlertExpires { get; set; }
            public string Comment { get; set; }
        }


    }

