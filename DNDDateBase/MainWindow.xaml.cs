using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DNDDateBase.DNDDateBase.AppObjects;
using DNDDateBase.Utility;
using Microsoft.Win32;

namespace DNDDateBase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public static RoutedCommand SaveFile = new RoutedCommand();

        public MainWindow()
        {
            // auto method
            InitializeComponent();

            // Initialize tab panes
            charPane.Content = new CharacterControl();
            locationPane.Content = new LocationControl();
            cityPane.Content = new CityControl();
            itemPane.Content = new ItemControl();
        }

        #region MenuItems
        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = Application.Current.Resources[Constants.FileFilterName] as string
            };
            if (openFileDialog.ShowDialog() == true)
            {
                ClearHostData();
                bool success = ((App)Application.Current).LoadDataFromFile(openFileDialog.FileName);
                SetHostData();
                if (!success)
                {
                    // TODO Handle file error handling
                }
            }
        }
        private void MenuItemNew_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = Application.Current.Resources[Constants.FileFilterName] as string
            };
            if (saveDialog.ShowDialog() == true)
            {
                ((App)Application.Current).NewFile(saveDialog.FileName);
                ClearHostData();
                SetHostData();
            }
        }
        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                AddExtension = true,
                Filter = Application.Current.Resources[Constants.FileFilterName] as string
            };
            if (saveDialog.ShowDialog() == true)
            {
                ((App)Application.Current).SaveFile(saveDialog.FileName);
                ClearHostData();
                SetHostData();
            }
        }
        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion
        private void ClearHostData()
        {
            ((CharacterControl)charPane.Content).lbChars.Items.Clear();
            ((LocationControl)locationPane.Content).lbLocs.Items.Clear();
            ((CityControl)cityPane.Content).lbCitys.Items.Clear();
            ((ItemControl)itemPane.Content).lbItems.Items.Clear();
        }
        private void SetHostData()
        {
            foreach (Character character in Application.Current.FindResource("Characters") as List<Character>)
            {
                ((CharacterControl)charPane.Content).lbChars.Items.Add(character);
            }

            foreach (Location loc in Application.Current.FindResource("Locations") as List<Location>)
            {
                ((LocationControl)locationPane.Content).lbLocs.Items.Add(loc);
            }

            foreach (City city in Application.Current.FindResource("Cities") as List<City>)
            {
                ((CityControl)cityPane.Content).lbCitys.Items.Add(city);
            }

            foreach (Item item in Application.Current.FindResource("Items") as List<Item>)
            {
                ((ItemControl)itemPane.Content).lbItems.Items.Add(item);
            }
        }

        private void SaveFileShortCut(object sender, ExecutedRoutedEventArgs e)
        {
            ((App)Application.Current).SaveFile();
            this.Dispatcher.Invoke(() =>
            {
                saveLbl.Visibility = Visibility.Visible;
            }
            );

            Timer delayTimer = new Timer
            {
                Interval = 5000
            };
            delayTimer.Elapsed += (o, eventT) => hideSavelbl();
            delayTimer.Start();
        }

        private void hideSavelbl()
        {
            this.Dispatcher.Invoke(() =>
            {
                saveLbl.Visibility = Visibility.Hidden;
            }
            );
        }
    }
}
