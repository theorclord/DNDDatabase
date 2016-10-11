using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DNDDateBase
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {

    private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
      // A catch all exceptions function to deal with unexcepted errors
      MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Main Exception Catcher", MessageBoxButton.OK, MessageBoxImage.Warning);
      e.Handled = true;
    }

    private void Application_Exit(object sender, ExitEventArgs e)
    {
      // Save the data stored in the different object list
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
      // Create a list of characters to display in the character tab.

      // enable in app character creation

      // enable to save and load data 
      // enable serialization

      // enable creation of multiple defined classes 
      // Load data from files
      // TODO

      // Load main window
      MainWindow win = new MainWindow();
      win.Show();
    }
  }
}
