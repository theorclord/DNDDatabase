using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using DNDDateBase.Utility;

namespace DNDDateBase
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    
    [Serializable()]
    public class SerializedDataContainer
    {
      public List<Character> chars { get; set; }
      public List<City> cities { get; set; }
      public List<Location> locs { get; set; }
      public List<Item> items { get; set; }

    }

    private const string saveFilePath = @"c:\MySerializedStuff.xml";

    private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
      // A catch all exceptions function to deal with unexcepted errors
      MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Main Exception Catcher", MessageBoxButton.OK, MessageBoxImage.Warning);
      e.Handled = true;
    }

    private void Application_Exit(object sender, ExitEventArgs e)
    {
      // Save the data stored in the different object list
      SerializedDataContainer data = new SerializedDataContainer();
      data.chars = (List<Character>)Application.Current.FindResource("Characters");
      data.cities = (List<City>)Application.Current.FindResource("Cities");
      data.locs = (List<Location>)Application.Current.FindResource("Locations");
      data.items = (List<Item>)Application.Current.FindResource("Items");

      XmlSerializer serializer = new XmlSerializer(typeof(SerializedDataContainer));
      TextWriter writer = new StreamWriter(saveFilePath);
      serializer.Serialize(writer, data);
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
      XmlSerializer deserializer = new XmlSerializer(typeof(SerializedDataContainer));
      StreamReader dataReader = new StreamReader(saveFilePath);
      XmlReader xmlDataReader = new XmlTextReader(dataReader);

      SerializedDataContainer appData;
      if (deserializer.CanDeserialize(xmlDataReader))
      {
        appData = (SerializedDataContainer)deserializer.Deserialize(xmlDataReader);
      }
      else
      {
        appData = new SerializedDataContainer();
      }
      
      List<Character> chars = appData.chars;
      // Ensures list for application if none exist
      if (chars == null)
      {
        chars = new List<Character>();
      }
      Application.Current.Resources.Add("Characters", chars);


      List<City> cities = appData.cities;
      if(cities == null)
      {
        cities = new List<City>();
      }
      Application.Current.Resources.Add("Cities", cities);


      List<Location> locs = appData.locs;
      if(locs == null)
      {
        locs = new List<Location>();
      }
      Application.Current.Resources.Add("Locations", locs);


      List<Item> items = appData.items;
      if(items == null)
      {
        items = new List<Item>();
      }
      Application.Current.Resources.Add("Items", items);

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
