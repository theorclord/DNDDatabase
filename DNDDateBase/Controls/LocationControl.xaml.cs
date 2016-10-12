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
  /// Interaction logic for LocationControl.xaml
  /// </summary>
  public partial class LocationControl : UserControl
  {
    public LocationControl()
    {
      InitializeComponent();
      List<Location> locs = Application.Current.FindResource("Locations") as List<Location>;
      for (int i = 0; i < locs.Count; i++)
      {
        lbLocs.Items.Add(locs[i]);
      }
    }

    private void btnSaveLoc_Click(object sender, RoutedEventArgs e)
    {
      // Should update existing Locations
      Location saveLoc = new Location();
      saveLoc.Name = txtName.Text;
      saveLoc.Notes = txtNotes.Text;

      List<Location> locs = Application.Current.FindResource("Locations") as List<Location>;
      if (locs.Contains((DNDAppObj)saveLoc))
      {
        ((Location)locs[locs.FindIndex(i => i.Name == saveLoc.Name)]).Notes = saveLoc.Notes;
      }
      else
      {
        lbLocs.Items.Add(saveLoc);
        locs.Add(saveLoc);
      }
      txtName.Clear();
      txtNotes.Clear();
    }

    private void lbLocs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      Location selLoc = (Location)lbLocs.SelectedItem;
      txtName.Text = selLoc.Name;
      txtNotes.Text = selLoc.Notes;
    }

    private void btnDeleteLoc_Click(object sender, RoutedEventArgs e)
    {
      int index = lbLocs.SelectedIndex;
      if (index != -1)
      {
        MessageBoxResult res = MessageBox.Show("Are you sure you want to delete " + lbLocs.SelectedItem, "Delete Location", MessageBoxButton.YesNo);
        if (res == MessageBoxResult.Yes)
        {
          List<Location> locs = Application.Current.FindResource("Locations") as List<Location>;
          Location character = locs.First(c => c.Name == ((Location)lbLocs.Items[index]).Name);
          locs.Remove(character);
          lbLocs.Items.RemoveAt(index);
        }
      }
    }
  }
}
