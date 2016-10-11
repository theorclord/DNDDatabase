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
  /// Interaction logic for CharacterControl.xaml
  /// </summary>
  public partial class CharacterControl : UserControl
  {
    public CharacterControl()
    {
      InitializeComponent();
    }
    private void btnAddChar_Click(object sender, RoutedEventArgs e)
    {
      // TODO
      //lbChars.Items.Add(App.Current.FindResource("strChar").ToString());
    }

    private void btnSaveChar_Click(object sender, RoutedEventArgs e)
    {
      Character saveChar = new Character();
      saveChar.Name = txtName.Text;
      saveChar.Notes = txtNotes.Text;

      lbChars.Items.Add(saveChar);
      txtName.Clear();
      txtNotes.Clear();
    }

    private void lbChars_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      Character selChar = (Character)lbChars.SelectedItem;
      txtName.Text = selChar.Name;
      txtNotes.Text = selChar.Notes;
    }
  }
}
