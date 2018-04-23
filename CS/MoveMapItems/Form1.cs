using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraMap;
using DevExpress.Map;

namespace MoveMapItems {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            InitMap();
        }

        MapPushpin pin;

        private void InitMap() {
            VectorItemsLayer vlayer = (VectorItemsLayer)this.mapControl1.Layers["ShapefileLayer"];
            ShapefileDataAdapter data = new ShapefileDataAdapter();

            data.FileUri = new Uri(string.Format("file:///{0}/Countries.shp", Environment.CurrentDirectory));
            vlayer.Data = data;
        }

        private void mapControl1_MouseDown(object sender, MouseEventArgs e) {
            MapHitInfo info = this.mapControl1.CalcHitInfo(e.Location);
            if (info.InMapPushpin) {
                this.mapControl1.EnableScrolling = false;
                foreach (object obj in info.HitObjects) {
                    if (obj.GetType() == typeof(MapPushpin))
                    pin = (MapPushpin)obj;
                }
            }
        }

        private void mapControl1_MouseMove(object sender, MouseEventArgs e) {
            if (pin != null) {
                CoordPoint point = this.mapControl1.ScreenPointToCoordPoint(new MapPoint(e.X, e.Y));
                pin.Location = point;
            }
        }

        private void mapControl1_MouseUp(object sender, MouseEventArgs e) {
            if (pin != null) {
                CoordPoint point = this.mapControl1.ScreenPointToCoordPoint(new MapPoint(e.X, e.Y));
                pin.Location = point;
                this.mapControl1.EnableScrolling = true;
                pin = null;
            }
        }
    }
}
