using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FakePaint
{
    public partial class Form1 : Form
    {
        IFactory FigureFactory { get; set; }
        List<IFigure> figures = new List<IFigure>();
        public Color figureColor { get; set; }
        public Point tempPoint = new Point();

        public Form1()
        {
            InitializeComponent();
            List<string> figures = new List<string> { "Rectangle", "Circle", "Triangle" };
            guna2ComboBox1.Items.AddRange(figures.ToArray());
            guna2ComboBox1.SelectedIndex = 0;
        }
        private void SetFigure(IFigure figure, Size size)
        {
            figure.Color = figureColor;
            figure.Size = size;
            figure.IsFilled = guna2RadioButton2.Checked;
        }
        private void SetFigureWitdhHeight(bool flag)
        {
            guna2TextBox1.Enabled = flag;
            guna2TextBox2.Enabled = flag;
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            var item = guna2ComboBox1.SelectedItem.ToString();

            if (item == "Rectangle")
            {
                FigureFactory = new RectangleFactory();
                SetFigureWitdhHeight(true);
            }
            else if (item == "Circle")
            {
                FigureFactory = new CircleFactory();
                SetFigureWitdhHeight(true);
            }
            else if (item == "Triangle")
            {
                FigureFactory = new TriangleFactory();
                SetFigureWitdhHeight(true);
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            ColorDialog colordialog = new ColorDialog();
            var res = colordialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                figureColor = colordialog.Color;
                guna2CircleButton1.FillColor = colordialog.Color;
            }
        }//color

        private void guna2RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            SetFigureWitdhHeight(false);
            guna2TextBox1.Text = default;
            guna2TextBox2.Text = default;

        }//custom

        private void guna2RadioButton4_CheckedChanged(object sender, EventArgs e)//auto
        {
            SetFigureWitdhHeight(true);
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            var figure = FigureFactory.GetFigure();
            int.TryParse(guna2TextBox1.Text, out int widht);
            int.TryParse(guna2TextBox2.Text, out int height);
            if (widht > 0 && height > 0)
            {
                if (figure is Rectangle)
                {
                    SetFigure(figure, new Size(widht, height));
                    if (guna2RadioButton4.Checked)
                    {
                        figure.Points[0] = e.Location;
                    }
                }
                else if (figure is Circle)
                    {
                        SetFigure(figure, new Size(widht, height));
                        figure.Points[0] = e.Location;
                    }
                else if (figure is Triangle)
                    {
                        SetFigure(figure, new Size(widht, height));
                        figure.Points[0] = e.Location;
                        figure.Points[1].X = e.Location.X + figure.Size.Width/2;
                        figure.Points[1].Y = e.Location.Y + figure.Size.Height;

                        figure.Points[2].X = e.Location.X - figure.Size.Width/2;
                        figure.Points[2].Y = e.Location.Y + figure.Size.Height;
                    }
                figures.Add(figure);
                this.Refresh();
            }
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            using (var graphics = e.Graphics)
            {
                foreach (var figure in figures)
                {
                    SolidBrush solidBrush = new SolidBrush(figure.Color);
                    Pen pen = new Pen(figure.Color, 3);
                    if (figure is Rectangle rectangle && rectangle.IsCustom == false)
                    {
                        if (rectangle.IsFilled)
                        {
                            graphics.FillRectangle(solidBrush, rectangle.Points[0].X, rectangle.Points[0].Y, rectangle.Size.Width, rectangle.Size.Height);
                        }
                        else
                        {
                            graphics.DrawRectangle(pen, rectangle.Points[0].X, rectangle.Points[0].Y, rectangle.Size.Width, rectangle.Size.Height);
                        }
                    }
                    else if (figure is Rectangle rectangle1 && rectangle1.IsCustom == true)
                    {
                        if (rectangle1.IsFilled)
                        {
                            graphics.FillPolygon(solidBrush, rectangle1.Points);
                        }
                        else
                        {
                            graphics.DrawPolygon(pen, rectangle1.Points);
                        }
                    }
                    else if (figure is Circle circle)
                    {
                        if (circle.IsFilled)
                        {
                            graphics.FillEllipse(solidBrush, circle.Points[0].X, circle.Points[0].Y, circle.Size.Width, circle.Size.Height);
                        }
                        else
                        {
                            graphics.DrawEllipse(pen, circle.Points[0].X, circle.Points[0].Y, circle.Size.Width, circle.Size.Height);
                        }
                    }
                    else if (figure is Triangle triangle)
                    {
                        if (triangle.IsFilled)
                        {
                            graphics.FillPolygon(solidBrush, triangle.Points);
                        }
                        else
                        {
                            graphics.DrawPolygon(pen, triangle.Points);
                        }
                    }

                }
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            var figure = FigureFactory.GetFigure();

            if (figure is Rectangle && guna2RadioButton3.Checked)
            {
                tempPoint.X = e.Location.X;
                tempPoint.Y = e.Location.Y;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            var figure = FigureFactory.GetFigure();

            if (figure is Rectangle && guna2RadioButton3.Checked)
            {
                figure.Color = figureColor;
                figure.IsCustom = true;
                figure.IsFilled = guna2RadioButton2.Checked;


                figure.Points[0].X = tempPoint.X;
                figure.Points[0].Y = tempPoint.Y;

                figure.Points[1].X = e.Location.X;
                figure.Points[1].Y = figure.Points[0].Y;

                figure.Points[2].X = e.Location.X;
                figure.Points[2].Y = e.Location.Y;

                figure.Points[3].X = figure.Points[0].X;
                figure.Points[3].Y = e.Location.Y;

                figures.Add(figure);
                this.Refresh();
            }
           //else if (figure is Circle && guna2RadioButton3.Checked)
           // {
           //     figure.Color = figureColor;
           //     figure.IsCustom = true;
           //     figure.IsFilled = guna2RadioButton2.Checked;


           //     figure.Points[0].X = tempPoint.X;
           //     figure.Points[0].Y = tempPoint.Y;

                

              

                

           //     figures.Add(figure);
           //     this.Refresh();
           // }
        }
    }
}
