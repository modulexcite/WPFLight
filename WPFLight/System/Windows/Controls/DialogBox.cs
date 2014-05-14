using System.Windows.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WPFLight.Helpers;

namespace System.Windows.Controls {
	public abstract class DialogBox : Window {
		public DialogBox ( ) {
			this.Width = 200;
			this.Height = 200;
			this.Background = new SolidColorBrush (System.Windows.Media.Color.FromArgb (100, 100, 100, 100 ));
			this.BorderBrush = new SolidColorBrush (System.Windows.Media.Color.FromArgb (200, 200, 200, 200 ));
			this.BorderThickness = new Thickness (1);

			gridRoot = new Grid ();
			gridRoot.RowDefinitions.Add (new RowDefinition (new GridLength (45, GridUnitType.Pixel)));
			gridRoot.RowDefinitions.Add (RowDefinition.Star);
			gridRoot.RowDefinitions.Add (new RowDefinition (new GridLength (65, GridUnitType.Pixel)));
            gridRoot.Parent = this;

			base.Content = gridRoot;
		}

		#region Properties

		public new object Content {
			get { 
				return dialogContent;
			}
			set { 
				if (dialogContent != value) {
					var oldElement = dialogContent as UIElement;
					if (oldElement != null)
						gridRoot.Children.Remove(oldElement);

					var newElement = value as UIElement;
					if (newElement != null) {
						gridRoot.Children.Add(newElement);
						Grid.SetRow(newElement, 1);
					}

					dialogContent = value;
				}
			}
		}

		#endregion


        public override void Invalidate () {
            base.Invalidate();
            gridRoot.Invalidate();
        }

		public override void Initialize () {

			lblTitle = new Label ();
			lblTitle.Text = this.Title;
			lblTitle.FontScale = .4f;
			lblTitle.Margin = new Thickness (8, 4, 0, 0);

			gridRoot.Children.Add (lblTitle);

			cmdCancel = new Button ();
			cmdCancel.Content = "CANCEL";
            cmdCancel.IsEnabled = true;
			cmdCancel.Margin = new Thickness (15, 5, 15, 10);
			cmdCancel.Width = 120;
			cmdCancel.Foreground = Brushes.White;
			cmdCancel.HorizontalAlignment = HorizontalAlignment.Right;
			cmdCancel.Click += delegate {
				this.DialogResult = false;
				this.Close ();
			};

			gridRoot.Children.Add (cmdCancel);
			Grid.SetRow ( cmdCancel, 2);

			cmdOkay = new Button ();
			cmdOkay.Content = "OK";
            cmdOkay.IsEnabled = true;
			cmdOkay.Margin = new Thickness (15, 5, 145, 10);
			cmdOkay.Width = 80;
			cmdOkay.Foreground = Brushes.White;
			cmdOkay.HorizontalAlignment = HorizontalAlignment.Right;
			cmdOkay.Click += delegate {
				this.DialogResult = true;
				this.Close ();
			};

			gridRoot.Children.Add (cmdOkay);
			Grid.SetRow (cmdOkay, 2);

			if (this.WindowStartUpLocation == WindowStartUpLocation.CenterScreen) {
				var left = ((float)ScreenHelper.ORIGINAL_WIDTH / 2f) - this.ActualWidth / 2f;
				var top = ((float)ScreenHelper.ORIGINAL_HEIGHT / 2f) - this.ActualHeight / 2f;

				this.Left = left;
				this.Top = top;
			}

            gridRoot.Initialize();
			base.Initialize ();
		}

		/*
        public override void OnTouchDown (Microsoft.Xna.Framework.Input.Touch.TouchLocation state) {
            base.OnTouchDown(state);
            gridRoot.OnTouchDown(state);
        }

        public override void OnTouchUp (Microsoft.Xna.Framework.Input.Touch.TouchLocation state) {
            base.OnTouchUp(state);
            gridRoot.OnTouchUp(state);
        }

        public override void Update (GameTime gameTime) {
            base.Update(gameTime);
            gridRoot.Update(gameTime);
        }
        */
			
		#region Eigenschaften

		public string Title { get; set; }

		#endregion

		private Button cmdCancel;
		private Button cmdOkay;
		private Label lblTitle;
		private Grid gridRoot;

		private object dialogContent;
	}
}
