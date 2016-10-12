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
using DNDDateBase.Utility;

namespace DNDDateBase
{
  /// <summary>
  /// Interaction logic for CityControl.xaml
  /// </summary>
  public partial class CityControl : UserControl
  {
    public CityControl()
    {
      InitializeComponent();
      List<City> cities = Application.Current.FindResource("Cities") as List<City>;
      for (int i = 0; i < cities.Count; i++)
      {
        lbCitys.Items.Add(cities[i]);
      }
    }

    private void btnSaveCity_Click(object sender, RoutedEventArgs e)
    {
      // Should update existing cities
      City saveCity = new City();
      saveCity.Name = txtName.Text;
      saveCity.Notes = txtNotes.Text;

      List<City> cities = Application.Current.FindResource("Cities") as List<City>;
      if (cities.Contains(saveCity))
      {
        cities[cities.FindIndex(i => i.Name == saveCity.Name)].Notes = saveCity.Notes;
      }
      else
      {
        lbCitys.Items.Add(saveCity);
        cities.Add(saveCity);
      }
      txtName.Clear();
      txtNotes.Clear();
    }

    private void lbCitys_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      City selCity = (City)lbCitys.SelectedItem;
      txtName.Text = selCity.Name;
      txtNotes.Text = selCity.Notes;
    }

    private void btnDeleteCity_Click(object sender, RoutedEventArgs e)
    {
      int index = lbCitys.SelectedIndex;
      if (index != -1)
      {
        MessageBoxResult res = MessageBox.Show("Are you sure you want to delete " + lbCitys.SelectedItem, "Delete City", MessageBoxButton.YesNo);
        if (res == MessageBoxResult.Yes)
        {
          List<City> cities = Application.Current.FindResource("Cities") as List<City>;
          City city = cities.First(c => c.Name == ((City)lbCitys.Items[index]).Name);
          cities.Remove(city);
          lbCitys.Items.RemoveAt(index);
        }
      }
    }
  }
}
