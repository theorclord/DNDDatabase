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
  /// Interaction logic for ObjectOfInterestControl.xaml
  /// </summary>
  public partial class ObjectOfInterestControl : UserControl
  {
    public ObjectOfInterestControl()
    {
      InitializeComponent();
      List<Item> items = Application.Current.FindResource("Items") as List<Item>;
      for (int i = 0; i < items.Count; i++)
      {
        lbItems.Items.Add(items[i]);
      }
    }

    private void btnSaveItem_Click(object sender, RoutedEventArgs e)
    {
      // Should update existing Locations
      Item saveItem = new Item();
      saveItem.Name = txtName.Text;
      saveItem.Notes = txtNotes.Text;

      List<Item> items = Application.Current.FindResource("Items") as List<Item>;
      if (items.Contains((DNDAppObj)saveItem))
      {
        ((Item)items[items.FindIndex(i => i.Name == saveItem.Name)]).Notes = saveItem.Notes;
      }
      else
      {
        lbItems.Items.Add(saveItem);
        items.Add(saveItem);
      }
      txtName.Clear();
      txtNotes.Clear();
    }

    private void lbItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      Item selObj = (Item)lbItems.SelectedItem;
      txtName.Text = selObj.Name;
      txtNotes.Text = selObj.Notes;
    }

    private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
    {
      int index = lbItems.SelectedIndex;
      if (index != -1)
      {
        MessageBoxResult res = MessageBox.Show("Are you sure you want to delete " + lbItems.SelectedItem, "Delete Item", MessageBoxButton.YesNo);
        if (res == MessageBoxResult.Yes)
        {
          List<Item> items = Application.Current.FindResource("Items") as List<Item>;
          Item item = items.First(c => c.Name == ((Item)lbItems.Items[index]).Name);
          items.Remove(item);
          lbItems.Items.RemoveAt(index);
        }
      }
    }
  }
}
