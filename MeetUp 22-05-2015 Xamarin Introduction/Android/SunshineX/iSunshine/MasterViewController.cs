using System;
using System.Collections.Generic;

using System.Linq;
using UIKit;
using Foundation;
using CoreGraphics;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Json;
using Newtonsoft.Json;


namespace iSunshine
{
	public partial class MasterViewController : UITableViewController
	{
		public DetailViewController DetailViewController { get; set; }

		DataSource dataSource;

		public MasterViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("Master", "Master");
			
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad) {
				PreferredContentSize = new CGSize (320f, 600f);
				ClearsSelectionOnViewWillAppear = false;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Perform any additional setup after loading the view, typically from a nib.
			NavigationItem.LeftBarButtonItem = EditButtonItem;

			var addButton = new UIBarButtonItem (UIBarButtonSystemItem.Refresh, RefreshData);
			addButton.AccessibilityLabel = "Refresh";
			NavigationItem.RightBarButtonItem = addButton;

			DetailViewController = (DetailViewController)((UINavigationController)SplitViewController.ViewControllers [1]).TopViewController;

			TableView.Source = dataSource = new DataSource (this);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		async void RefreshData (object sender, EventArgs args)
		{
		//	;

			string url = "http://api.openweathermap.org/data/2.5/forecast/daily?q=Thessaloniki&mode=json&units=metric&cnt=7";

			// Fetch the weather information asynchronously, 
			// parse the results, then update the screen:
			JsonValue json = await FetchWeatherAsync(url);
			//var ans = json.ToString();
			Forecast forecast = JsonConvert.DeserializeObject<Forecast>(json.ToString());
			var forecastList = forecast.list.Select(i => i.weather.First().description).ToList();
			if (forecastList.Any ()) 
			{
				
				//dataSource.Objects.Clear ();
				int k = 0;
				forecastList.ForEach((i) => { 
					dataSource.Objects[k] = i;
					using (var indexPath = NSIndexPath.FromRowSection (k++, 0))
					{

						TableView.ReloadRows (new [] { indexPath }, UITableViewRowAnimation.Automatic);


					}
				});

			}

		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "showDetail") {
				var indexPath = TableView.IndexPathForSelectedRow;
				var item = dataSource.Objects [indexPath.Row];
				var controller = (DetailViewController)((UINavigationController)segue.DestinationViewController).TopViewController;
				controller.SetDetailItem (item);
				controller.NavigationItem.LeftBarButtonItem = SplitViewController.DisplayModeButtonItem;
				controller.NavigationItem.LeftItemsSupplementBackButton = true;
			}
		}

		private async Task<JsonValue> FetchWeatherAsync(string url)
		{
			// Create an HTTP web request using the URL:
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
			request.ContentType = "application/json";
			request.Method = "GET";

			// Send the request to the server and wait for the response:
			using (WebResponse response = await request.GetResponseAsync())
			{
				// Get a stream representation of the HTTP web response:
				using (Stream stream = response.GetResponseStream())
				{
					// Use this stream to build a JSON document object:
					JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
					Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

					// Return the JSON document:
					return jsonDoc;
				}
			}
		}

		class DataSource : UITableViewSource
		{
			static readonly NSString CellIdentifier = new NSString ("Cell");
			public List<string> Objects = null;
				
			readonly MasterViewController controller;
			private String[] default_data = {
				"Mon 6/23 - Sunny - 31/17",
				"Tue 6/24 - Foggy - 21/8",
				"Wed 6/25 - Cloudy - 22/17",
				"Thurs 6/26 - Rainy - 18/11",
				"Fri 6/27 - Foggy - 21/10",
				"Sat 6/28 - TRAPPED IN WEATHERSTATION - 23/18",
				"Sun 6/29 - Sunny - 20/7"
			};

			public DataSource (MasterViewController controller)
			{
				this.controller = controller;
				Objects = new List<string>()
				{
					"Mon 6/23 - Sunny - 31/17",
					"Tue 6/24 - Foggy - 21/8",
					"Wed 6/25 - Cloudy - 22/17",
					"Thurs 6/26 - Rainy - 18/11",
					"Fri 6/27 - Foggy - 21/10",
					"Sat 6/28 - TRAPPED IN WEATHERSTATION - 23/18",
					"Sun 6/29 - Sunny - 20/7"
				};

			}


			// Customize the number of sections in the table view.
			public override nint NumberOfSections (UITableView tableView)
			{
				return 1;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return Objects.Count;
			}

			// Customize the appearance of table view cells.
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (CellIdentifier, indexPath);

				cell.TextLabel.Text = Objects [indexPath.Row].ToString ();

				return cell;
			}

			public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
			{
				// Return false if you do not want the specified item to be editable.
				return true;
			}

			public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
			{
				if (editingStyle == UITableViewCellEditingStyle.Delete) {
					// Delete the row from the data source.
					Objects.RemoveAt (indexPath.Row);
					controller.TableView.DeleteRows (new [] { indexPath }, UITableViewRowAnimation.Fade);
				} else if (editingStyle == UITableViewCellEditingStyle.Insert) {
					// Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
				}
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
					controller.DetailViewController.SetDetailItem (Objects [indexPath.Row]);
			}
		}
	}

	public class Forecast
	{
		public string cod { get; set; }
		public float message { get; set; }
		public City city { get; set; }
		public int cnt { get; set; }
		public List[] list { get; set; }
	}

	public class City
	{
		public int id { get; set; }
		public string name { get; set; }
		public string country { get; set; }
		public Coord coord { get; set; }
	}

	public class Coord
	{
		public float lat { get; set; }
		public float lon { get; set; }
	}

	public class List
	{
		public int dt { get; set; }
		public Temp temp { get; set; }
		public float pressure { get; set; }
		public int humidity { get; set; }
		public Weather[] weather { get; set; }
		public float speed { get; set; }
		public int deg { get; set; }
		public int clouds { get; set; }
		public float rain { get; set; }
	}

	public class Temp
	{
		public float day { get; set; }
		public float min { get; set; }
		public float max { get; set; }
		public float night { get; set; }
		public float eve { get; set; }
		public float morn { get; set; }
	}

	public class Weather
	{
		public int id { get; set; }
		public string main { get; set; }
		public string description { get; set; }
		public string icon { get; set; }
	}

}


