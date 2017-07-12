using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Calculator
{
    public class CreateCalculator
    {
        [STAThread]
        public static void Main()
        {           
            Application.Run(new Form1());
            
        }
        public class Form1 : Form
        {                                 
            public Form1()
            {            
                CreateForm();                
                CreateNumButton();
                CreateFucButton("+", new int[] { 250, 60 }, "Add",1);
                CreateFucButton("-", new int[] { 250, 130 }, "Sub",1);
                CreateFucButton("*", new int[] { 250, 200 }, "Multiplication", 1);
                CreateFucButton("/", new int[] { 250, 270 }, "Division", 1);
                CreateFucButton("=", new int[] { 170, 270 }, "Equal", 2);
                CreateFucButton("C", new int[] { 10, 340 }, "Clear", 3);
                CreateFucButton(".", new int[] { 90, 270 }, "Point", 4);
                CreateFucButton("平方", new int[] { 90, 340 }, "Square", 5);
                CreateFucButton("1/x", new int[] { 250, 340 }, "reciprocal", 5);
                CreateFucButton("%", new int[] { 170, 340 }, "remainder", 1);
            }

            
            
            //创建功能按钮
            void CreateFucButton(string op,int[]location,string name,int typeofbutton) 
            {
                Button btn = new Button();
                btn.Text = op;
                btn.Location = new System.Drawing.Point(location[0], location[1]);
                btn.Size = new System.Drawing.Size(62,53);
                btn.Name = name;
                btn.Tag = typeofbutton;
                btn.Visible = true;
                this.Controls.Add(btn);
                btn.Click += button_Click;
            }
           
            
            //创建数字按钮
            void CreateNumButton()
            {
                int count = 1;
                for (int i = 0; i < 3; i++)
                {                   
                    for (int j = 0; j < 3; j++)
                    { 
                        Button btn = new Button();
                        btn.Text = count.ToString();
                        btn.Location = new System.Drawing.Point(10+80*j,60+70*i);
                        btn.Size = new System.Drawing.Size(62, 53);
                        btn.Name = count.ToString();
                        btn.Visible = true;
                        btn.Tag = 0;
                        this.Controls.Add(btn);
                        btn.Click += button_Click;
                        count++;
                    } 
                }
                Button btn0 = new Button();
                btn0.Text = 0.ToString();
                btn0.Location = new System.Drawing.Point(10 , 270);
                btn0.Size = new System.Drawing.Size(62, 53);
                btn0.Name = 0.ToString();
                btn0.Visible = true;
                btn0.Tag = 0;
                this.Controls.Add(btn0);
                btn0.Click += button_Click;
            }

           
            //创建窗体
            public System.Windows.Forms.TextBox textBox1; 
            void CreateForm()
            {
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(340, 421);
                this.HelpButton = true;
                this.Name = "Form1";
                this.Text = "calculator";
                this.ResumeLayout(false);
                this.PerformLayout();

                //textBox1
                this.textBox1 = new System.Windows.Forms.TextBox();
                this.textBox1.Location = new System.Drawing.Point(12, 12);
                this.textBox1.Name = "textBox1";
                this.textBox1.Size = new System.Drawing.Size(304, 21);
                this.textBox1.TabIndex = 2;
                this.Controls.Add(textBox1);                
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(340, 421);
            }            
            //按钮事件
            CalculatorHelper helper = new CalculatorHelper();
            void button_Click(object sender, EventArgs e)
            {
                Button btn = (Button)sender;
                string buttontype=btn.Tag.ToString();
                switch (buttontype)
                {
                    case "0": helper.Num(textBox1, btn.Text);
                        break;
                    case "1": helper.FucClick(textBox1, btn.Name);
                        break;
                    case "2": helper.Equal(textBox1);
                        break;
                    case "3": helper.Clear(textBox1);
                        break;
                    case "4": helper.Point(textBox1);
                        break;
                    case "5": helper.MonocularOperation(textBox1, btn.Name);
                        break;
                }
            }
        }

    }
}
