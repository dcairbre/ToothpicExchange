﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class MapAreaRoomControl:DraggableControl {

		#region Member not available in designer.

		public MapArea MapAreaItem=new MapArea();

		#endregion

		#region Properties available in designer.

		[Category("Employee Info")]
		[Description("Primary Key From employee Table")]
		public long EmployeeNum { get; set; }

		[Category("Employee Info")]
		[Description("Employee's Name")]
		public string EmployeeName { get; set; }

		[Category("Employee Info")]
		[Description("Employee's Phone Extension #")]
		public string Extension { get; set; }

		[Category("Employee Info")]
		[Description("Elapsed Time Since Last Status Change")]
		public string Elapsed { get; set; }

		[Category("Employee Info")]
		[Description("Current Employee Status")]
		public string Status { get; set; }

		[Category("Employee Info")]
		[Description("Image Indicating Employee's Current Phone Status")]
		public Image PhoneImage { get; set; }

		[Category("Employee Info")]
		[Description("Image Indicating Employee's Current Phone Status")]
		public Image ChatImage { get; set; }

		[Category("Employee Info")]
		[Description("Image Indicating Employee's Current Proximity Status")]
		public Image ProxImage { get; set; }

		[Category("Appearance")]
		[Description("Overrides the drawing of the control and just makes it look like a label with a custom border")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Bindable(true)]
		public override string Text {
			get {
				return base.Text;
			}
			set {
				base.Text = value;
				Invalidate();
			}
		}

		private int _borderThickness=4;
		[Category("Appearance")]
		[Description("Thickness of the border drawn around the control")]
		public int BorderThickness {
			get {
				return _borderThickness;
			}
			set {
				_borderThickness=value;
				Invalidate();
			}
		}

		///<summary>Set when flashing starts so we know what inner color to go back to.</summary>
		private Color _innerColorRestore=Color.FromArgb(128,Color.Red);
		private Color DefaultOuterColor=Color.Red;
		[Category("Appearance")]
		[Description("Exterior Border Color")]
		public Color OuterColor {
			get {
				return DefaultOuterColor;
			}
			set {
				DefaultOuterColor=value;
				Invalidate();
			}
		}

		///<summary>Set when flashing starts so we know what outer color to go back to.</summary>
		private Color _outerColorRestore=Color.Red;
		private Color DefaultInnerColor=Color.FromArgb(128,Color.Red);
		[Category("Appearance")]
		[Description("Interior Fill Color")]
		public Color InnerColor {
			get {
				return DefaultInnerColor;
			}
			set {
				DefaultInnerColor=value;
				Invalidate();
			}
		}

		private bool IsEmpty=false;
		[Category("Appearance")]
		[Description("No Extension Assigned")]
		public bool Empty {
			get {
				return IsEmpty;
			}
			set {
				IsEmpty=value;
				Invalidate();
			}
		}

		private bool _allowEdit=false;
		[Category("Behavior")]
		[Description("Double-click will open editor")]
		public bool AllowEdit {
			get {
				return _allowEdit;
			}
			set {
				_allowEdit=value;
			}
		}

		private Font _fontHeader=SystemFonts.DefaultFont;
		[Category("Behavior")]
		[Description("Font used for the top row. Generally reserved for the name of the MapAreaRoom.")]
		public Font FontHeader {
			get {
				return _fontHeader;
			}
			set {
				_fontHeader=value;
				Invalidate();
			}
		}

		public bool IsFlashing {
			get {
				return timerFlash.Enabled;
			}
		}

		#endregion

		#region Events

		public event EventHandler MapAreaRoomChanged;
		public event EventHandler RoomControlClicked;

		#endregion

		#region Ctor

		///<summary>Default. Must be called by all other ctors as we will call InitializeComponent here.</summary>
		public MapAreaRoomControl() {
			InitializeComponent();
		}

		///<summary>Takes all required fields as input. Suggest using this version when adding a cubicle to a ClinicMapPanel.</summary>
		public MapAreaRoomControl(MapArea cubicle,string elapsed,string employeeName,long employeeNum,string extension,string status,Font font,
			Font fontHeader,Color innerColor,Color outerColor,Color backColor,Point location,Size size,Image phoneImage,Image chatImage,bool allowDragging,bool allowEdit)
			: this() {
			cubicle.ItemType=MapItemType.Room;
			MapAreaItem=cubicle;
			Elapsed = elapsed;
			EmployeeName = employeeName;
			EmployeeNum = employeeNum;
			Extension = extension;
			Status = status;
			Font = font;
			FontHeader=fontHeader;
			Location = location;
			Size=size;
			InnerColor = innerColor;
			OuterColor = outerColor;
			BackColor=backColor;
			PhoneImage = phoneImage;
			ChatImage = chatImage;
			AllowDragging=allowDragging;
			AllowEdit=allowEdit;
			Name=MapAreaItem.MapAreaNum.ToString();
		}

		#endregion

		#region Drawing

		public void StartFlashing() {
			if(IsFlashing) { //already on
				return;
			}
			//save the colors
			_outerColorRestore=OuterColor;
			_innerColorRestore=InnerColor;
			timerFlash.Start();
		}

		public void StopFlashing() {
			if(!IsFlashing) { //already off
				return;
			}
			timerFlash.Stop();
			OuterColor=_outerColorRestore;
			InnerColor=_innerColorRestore;
		}

		public void SetNormalColors() {
			SetColors(Color.Black,Color.Black,Color.White);
		}

		public void SetWarnColors() {
			SetColors(Color.Black,Color.Black,Color.FromArgb(255,237,102));
		}

		public void SetAlertColors() {
			SetColors(Color.White,Color.Black,Color.Red);
		}

		public void SetTriageColors() {
			SetColors(Color.Black,Phones.PhoneColorScheme.COLOR_DUAL_OuterTriage,Phones.PhoneColorScheme.COLOR_DUAL_InnerTriageHere);
		}

		public void SetColors(Color foreColor,Color outerColor,Color innerColor) {
			ForeColor=foreColor;
			OuterColor=outerColor;
			InnerColor=innerColor;
		}

		private void timerFlash_Tick(object sender,EventArgs e) {
			//flip inner and outer colors
			if(OuterColor==_outerColorRestore) {
				OuterColor=_innerColorRestore;
				InnerColor=_outerColorRestore;
			}
			else {
				OuterColor=_outerColorRestore;
				InnerColor=_innerColorRestore;
			}
		}

		private void MapAreaRoomControl_Paint(object sender,PaintEventArgs e) {
			Brush brushInner=new SolidBrush(Empty?Color.FromArgb(20,Color.Gray):InnerColor);
			Brush brushText=new SolidBrush(Empty?Color.FromArgb(128,Color.Gray):ForeColor);
			Pen penOuter=new Pen(Empty?Color.FromArgb(128,Color.Gray):OuterColor,BorderThickness);
			try {
				RectangleF rcOuter=this.ClientRectangle;
				//clear control canvas
				e.Graphics.Clear(this.BackColor);
				float halfPenThickness=BorderThickness/(float)2;
				//deflate for border
				rcOuter.Inflate(-halfPenThickness,-halfPenThickness);
				//draw border
				e.Graphics.DrawRectangle(penOuter,rcOuter.X,rcOuter.Y,rcOuter.Width,rcOuter.Height);
				//deflate to drawable region
				rcOuter.Inflate(-halfPenThickness,-halfPenThickness);
				//fill interior
				e.Graphics.FillRectangle(brushInner,rcOuter);
				StringFormat stringFormat=new StringFormat(StringFormatFlags.NoWrap);
				stringFormat.Alignment=StringAlignment.Center;
				stringFormat.LineAlignment=StringAlignment.Center;
				if(this.Empty) { //empty room so gray out and return
					e.Graphics.DrawString("EMPTY",Font,brushText,rcOuter,stringFormat);
					return;
				}
				else if(this.Text!="") { //using as a label so just draw the string					
					FitText(this.Text,Font,brushText,new RectangleF(rcOuter.Left,rcOuter.Top+2,rcOuter.Width,rcOuter.Height),stringFormat,e.Graphics);
					return;
				}
				//3 rows of data
				int rowsLowestCommonDenominator=6;
				float typicalRowHeight=rcOuter.Height/(float)rowsLowestCommonDenominator;
				//==================== row 1 - EMPLOYEE NAME ====================
				float rowHeight=typicalRowHeight*2; //row 1 is 2/6 tall
				FitText(EmployeeName,FontHeader,brushText,new RectangleF(rcOuter.X,rcOuter.Y,rcOuter.Width,rowHeight),stringFormat,e.Graphics);
				float yPosBottom=rowHeight;
				//e.Graphics.DrawRectangle(Pens.LimeGreen,rcOuter.X,rcOuter.Y,rcOuter.Width,rowHeight);
				//==================== row 2 - ELAPSED TIME ====================
				rowHeight=typicalRowHeight*2; //row 2 is 2/6 tall
				FitText(Elapsed,Font,brushText,new RectangleF(rcOuter.X,rcOuter.Y+yPosBottom-5,rcOuter.Width,rowHeight),stringFormat,e.Graphics);
				//e.Graphics.DrawRectangle(Pens.Red,rcOuter.X,rcOuter.Y+yPosBottom,rcOuter.Width,rowHeight);
				yPosBottom+=rowHeight;
				//==================== row 3 (Middle) - EMPLOYEE STATUS ====================
				//left-most 3/4 of row 3 is the status text
				FitText(Status,Font,brushText,new RectangleF(rcOuter.X+(rcOuter.Width/6)-4,rcOuter.Y+yPosBottom-14,((rcOuter.Width/6)*4)+4,rowHeight),stringFormat,e.Graphics);
				//FitText(Status,Font,brushText,new RectangleF(rcOuter.X+(rcOuter.Width/6)-2,rcOuter.Y+yPosBottom+1,((rcOuter.Width/6)*4)+4,rowHeight),stringFormat,e.Graphics);
				//==================== row 4 (Left) - PROXIMITY STATUS ====================
				int iconShiftCenter = 8;
				rowHeight =typicalRowHeight*2; //row 3 is 2/6 tall
				if(ProxImage!=null) {
					using(Bitmap bitmap = new Bitmap(ProxImage)) {//right-most 1/4 of row 3 is the phone icon
						RectangleF rectImage = new RectangleF(rcOuter.X-2+iconShiftCenter,rcOuter.Y+yPosBottom+4,ProxImage.Width,rowHeight);
						//Scale the image.
						if(bitmap.Height<rectImage.Height) {
							rectImage.Y+=(rectImage.Height-bitmap.Height)/2;
							rectImage.Height=bitmap.Height;
						}
						if(bitmap.Width<rectImage.Width) {
							rectImage.X-=(rectImage.Width-bitmap.Width)/2;
							rectImage.Width=bitmap.Width;
						}
						e.Graphics.DrawImage(
							ProxImage,
							rectImage,
							new RectangleF(0,0,bitmap.Width,bitmap.Height),
							GraphicsUnit.Pixel);
						//e.Graphics.DrawRectangle(Pens.Orange,rectImage.X,rectImage.Y,rectImage.Width,rectImage.Height);
					}
					//using(Font fnt = new Font("Arial",19,FontStyle.Regular)) {
					//	TextRenderer.DrawText(e.Graphics,"👤",fnt,new Point((int)rcOuter.X-6,(int)rcOuter.Y+(int)yPosBottom+3),Color.FromArgb(96,96,96));
					//	//FitText uses g.DrawString() which does not handle unicode characters well.
					//	//FitText("👤",fnt,Brushes.Gray,new RectangleF(rcOuter.X-2,rcOuter.Y+yPosBottom+1,((rcOuter.Width/6))+1,rowHeight),stringFormat,e.Graphics,true);
					//}
				}
				//==================== row 4 (middle) - CHAT ICON - ONLY SHOWS IF PHONE IS NOT SHOWING ====================
				if(PhoneImage==null && ChatImage != null) {
					using(Bitmap bitmap = new Bitmap(ChatImage)) {//right-most 1/4 of row 3 is the phone icon
						RectangleF rectImage=new RectangleF((float)(rcOuter.X+(rcOuter.Width/2))-9,rcOuter.Y+yPosBottom+4,ChatImage.Width,rowHeight);
						//Scale the image.
						if(bitmap.Height<rectImage.Height) {
							rectImage.Y+=(rectImage.Height-bitmap.Height)/2;
							rectImage.Height=bitmap.Height;
						}
						if(bitmap.Width<rectImage.Width) {
							rectImage.X-=(rectImage.Width-bitmap.Width)/2;
							rectImage.Width=bitmap.Width;
						}
						e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
						e.Graphics.DrawImage(
							ChatImage,
							rectImage,
							new RectangleF(0,0,bitmap.Width,bitmap.Height),
							GraphicsUnit.Pixel);
					}
				}				
				//==================== row 4 (right) - PHONE ICON ====================
				if(PhoneImage!=null) {
					using(Bitmap bitmap=new Bitmap(PhoneImage)) {//right-most 1/4 of row 3 is the phone icon
						RectangleF rectImage=new RectangleF((rcOuter.X+(rcOuter.Width/6)*5)-BorderThickness-2-iconShiftCenter,rcOuter.Y+yPosBottom+4,PhoneImage.Width,rowHeight);
						//Scale the image.
						if(bitmap.Height<rectImage.Height) {
							rectImage.Y+=(rectImage.Height-bitmap.Height)/2;
							rectImage.Height=bitmap.Height;
						}
						if(bitmap.Width<rectImage.Width) {
							rectImage.X-=(rectImage.Width-bitmap.Width)/2;
							rectImage.Width=bitmap.Width;
						}
						e.Graphics.DrawImage(
							PhoneImage,
							rectImage,
							new RectangleF(0,0,bitmap.Width,bitmap.Height),
							GraphicsUnit.Pixel);
						//e.Graphics.DrawRectangle(Pens.Orange,rectImage.X,rectImage.Y,rectImage.Width,rectImage.Height);
					}
				}
				//e.Graphics.DrawRectangle(Pens.Blue,rcOuter.X,rcOuter.Y+yPosBottom,rcOuter.Width,rowHeight);
				yPosBottom+=rowHeight;
			}
			catch (Exception){ }
			finally {
				brushInner.Dispose();
				brushText.Dispose();
				penOuter.Dispose();
			}
		}

		///<summary>Replaces Graphics.DrawString. Finds a suitable font size to fit the text to the bounding rectangle.</summary>
		public static void FitText(string text,Font font,Brush brush,RectangleF rectF,StringFormat stringFormat,Graphics graphics) {
			float emSize=font.Size;
			while(true) {
				using(Font newFont=new Font(font.FontFamily,emSize,font.Style)) {
					Size size=TextRenderer.MeasureText(text,newFont);
					if(size.Width<rectF.Width || emSize<2) { //does our new font fit? only allow smallest of 2 point font.
						graphics.DrawString(text,newFont,brush,rectF,stringFormat);
						return;
					}
				}
				//text didn't fit so decrement the font size and try again
				emSize-=.1F;
			}
		}

		#endregion

		#region Mouse events

		private void MapAreaRoomControl_DoubleClick(object sender,EventArgs e) {
			if(!AllowEdit) {
				return;
			}
			//edit this room
			FormMapAreaEdit FormEP=new FormMapAreaEdit();
			FormEP.MapItem=this.MapAreaItem;
			if(FormEP.ShowDialog(this)!=DialogResult.OK) {
				return;
			}
			if(MapAreaRoomChanged!=null) { //let anyone interested know that this cubicle was edited
				MapAreaRoomChanged(this,new EventArgs());
			}
		}

		private void MapAreaRoomControl_Click(object sender,EventArgs e) {
			if(AllowEdit) {
				return; //they're editing the room, don't change anyone's statuses.
			}
			if(!IsFlashing && Status != "OnWay") { //If OnWay we're setting them to available, dont return.
				return; //why did they click me? I don't know, just return.
			}
			//they want to change the room's status to 'HelpOnTheWay' if they're needing help.			
			RoomControlClicked?.Invoke(this, e);
		}

		#endregion

	}
}