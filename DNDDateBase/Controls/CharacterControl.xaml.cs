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
            List<Character> chars = Application.Current.FindResource("Characters") as List<Character>;
            for (int i = 0; i < chars.Count; i++)
            {
                lbChars.Items.Add(chars[i]);
            }
        }

        private void btnSaveChar_Click(object sender, RoutedEventArgs e)
        {
            Character selectedChar = (Character)lbChars.SelectedItem;

            // Should update existing characters if exist
            List<Character> chars = Application.Current.FindResource("Characters") as List<Character>;
            if (selectedChar != null)
            {
                Character existingChar = chars.First(c => c.ID == selectedChar.ID);
                existingChar.Name = txtName.Text;
                existingChar.Notes = txtNotes.Text;
            } else
            {
                Character newChar = new Character()
                {
                    Name = txtName.Text,
                    Notes = txtNotes.Text
                };
                chars.Add(newChar);
                lbChars.Items.Add(newChar);
            }

            txtName.Clear();
            txtNotes.Clear();
            lbChars.SelectedItem = null;
        }

        private void lbChars_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Character selChar = (Character)lbChars.SelectedItem;
            txtName.Text = selChar.Name;
            txtNotes.Text = selChar.Notes;
        }

        private void btnDelChar_Click(object sender, RoutedEventArgs e)
        {
            int index = lbChars.SelectedIndex;
            if (index != -1)
            {
                MessageBoxResult res = MessageBox.Show("Are you sure you want to delete " + lbChars.SelectedItem, "Delete Character", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                    List<Character> chars = Application.Current.FindResource("Characters") as List<Character>;
                    Character character = chars.First(c => c.Name == ((Character)lbChars.Items[index]).Name);
                    chars.Remove(character);
                    lbChars.Items.RemoveAt(index);
                }
            }
        }
    }
}
