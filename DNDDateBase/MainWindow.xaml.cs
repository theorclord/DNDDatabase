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
using DNDDateBase.Utility;
using Microsoft.Win32;

namespace DNDDateBase
{
  public static class CustomCommands
  {
    public static readonly RoutedUICommand Exit = new RoutedUICommand(
      "Exit",
      "Exit",
      typeof(CustomCommands),
      new InputGestureCollection()
      {
        new KeyGesture(Key.F4, ModifierKeys.Alt)
      }
    );
  }

  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    //public static RoutedCommand SaveFile = new RoutedCommand();

    public MainWindow()
    {
      InitializeComponent();

      //SaveFile.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));

      // Initialize tab panes
      charPane.Content = new CharacterControl();
      locationPane.Content = new LocationControl();
      cityPane.Content = new CityControl();
      itemPane.Content = new ItemControl();
    }

    #region MenuItems
    private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = Application.Current.Resources["filter"] as string;
      if (openFileDialog.ShowDialog() == true)
      {
        clearHostData();
        bool success = ((App)Application.Current).LoadDataFromFile(openFileDialog.FileName);
        setHostData();
        if (!success)
        {
          // Handle file error handling
        }
      }
    }
    private void MenuItemNew_Click(object sender, RoutedEventArgs e)
    {
      SaveFileDialog saveDialog = new SaveFileDialog();
      saveDialog.Filter = Application.Current.Resources["filter"] as string;
      if (saveDialog.ShowDialog() == true)
      {
        ((App)Application.Current).NewFile(saveDialog.FileName);
        clearHostData();
        setHostData();
      }
    }
    private void MenuItemSave_Click(object sender, RoutedEventArgs e)
    {
      SaveFileDialog saveDialog = new SaveFileDialog();
      saveDialog.AddExtension = true;
      saveDialog.Filter = Application.Current.Resources["filter"] as string;
      if (saveDialog.ShowDialog() == true)
      {
        ((App)Application.Current).SaveFile(saveDialog.FileName);
        clearHostData();
        setHostData();
      }
    }
    private void MenuItemExit_Click(object sender, RoutedEventArgs e)
    {
      Application.Current.Shutdown();
    }
    #endregion
    private void clearHostData()
    {
      ((CharacterControl)charPane.Content).lbChars.Items.Clear();
      ((LocationControl)locationPane.Content).lbLocs.Items.Clear();
      ((CityControl)cityPane.Content).lbCitys.Items.Clear();
      ((ItemControl)itemPane.Content).lbItems.Items.Clear();
    }
    private void setHostData()
    {
      foreach(Character character in Application.Current.FindResource("Characters") as List<Character>)
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
      
      Timer delayTimer = new Timer();
      delayTimer.Interval = 5000;
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
