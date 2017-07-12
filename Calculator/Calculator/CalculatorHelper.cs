using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Calculator
{
    public class CalculatorHelper
    {
        double num1, num2, result;
        string str1;
        bool judge = true;  //数字输入辅助判断
        bool judge2 = false; //运算辅助判断
        
        //加减乘除方法
        public void FucClick(TextBox textBox1,string name)
        {                          
            try
            {
                if (judge2 == true)
                {
                    switch (str1)
                    {
                        case "Add": num1 += Double.Parse(textBox1.Text);
                            break;
                        case "Sub": if (num1 != 0) num1 -= Double.Parse(textBox1.Text);
                            else num1 = Double.Parse(textBox1.Text);
                            break;
                        case "Multiplication": if (num1 != 0) num1 = num1 * Double.Parse(textBox1.Text);
                            break;
                        case "Division": if (num1 != 0) num1 = num1 / Double.Parse(textBox1.Text);
                            break;
                        case "remainder": if (num1 != 0) num1 = num1 % Double.Parse(textBox1.Text);
                            break;
                        default: num1 = Double.Parse(textBox1.Text);
                            break;
                    }
                    textBox1.Text = num1.ToString();
                    judge = true;
                    judge2 = false;                    
                }
                str1 = name;
            }
            catch (Exception)
            {
                textBox1.Text = null;
            }
        }

        //数字键方法
        public void Num(TextBox textBox1,string btntext )
        {            
            double myNumber = Double.Parse(btntext);
            if (judge == true)
            {                                 
                textBox1.Text = null;
                judge = false;
            }           
            textBox1.Text += myNumber.ToString();                       
            judge2 = true;          
        }

        //等于键方法
        public void Equal(TextBox textBox1)
        {
            num2 = Double.Parse(textBox1.Text);
            if (judge2 == true)
            try
            {               
                switch (str1)
                {
                    case "Add": result = num1 + num2;
                        break;
                    case "Sub": result = num1 - num2;
                        break;
                    case "Multiplication": result = num1 * num2;
                        break;
                    case "Division": if (num2 != 0) result = num1 / num2;
                        else MessageBox.Show("除数不能为0");
                        break;                   
                    case null: result = num2;
                        break;
                    case "Square": result = num1 * num1;   //Square
                        break;
                    case "Reciprocal": result = 1 / num1;
                        break;
                    case "remainder": result = num1 % num2;
                        break;
                }
                textBox1.Text = result.ToString();
                judge = true;
                judge2 = true;
                num1 = num2 = 0;
                str1 = null;
            }
            catch (Exception)
            {
                textBox1.Text = null;
            }          
        }

        //小数点方法
        public void Point(TextBox textBox1)
        {
            string str = ".";
            if (textBox1.Text.IndexOf(str) == -1 && textBox1.Text !="" || textBox1.Text == "0")
            {
                textBox1.Text += str;
                judge = false;
            }
            else if (textBox1.Text.Contains('.'))
            {
                MessageBox.Show("数字没有两个小数点!");
            }
            
        }

        //清除键方法
        public void Clear(TextBox textBox1)
        {            
            num1 = 0;
            num2 = 0;
            result = 0;
            textBox1.Text = 0.ToString();
            judge = true;
            judge2 = true;
            str1 = null;
        }

        //单目运算
        public void MonocularOperation(TextBox textBox1,string name)
        {
            try
            {
                switch (str1)
                {
                    case "Add": num1 += Double.Parse(textBox1.Text);
                        break;
                    case "Sub": if (num1 != 0) num1 -= Double.Parse(textBox1.Text);
                        else num1 = Double.Parse(textBox1.Text);
                        break;
                    case "Multiplication": if (num1 != 0) num1 = num1 * Double.Parse(textBox1.Text);
                        break;
                    case "Division": if (num1 != 0) num1 = num1 / Double.Parse(textBox1.Text);
                        break;
                    case "remainder": if (num1 != 0) num1 = num1 % Double.Parse(textBox1.Text);
                        break;
                    default: num1 = Double.Parse(textBox1.Text);
                        break;
                }
            }
            catch(Exception)
            {
                textBox1.Text = null;
            }  
            str1 = name;
            switch (str1)
            {
                case "Square": textBox1.Text = (num1 * num1).ToString();
                    break;
                case "reciprocal": if (num1 != 0) textBox1.Text = (1 / num1).ToString();
                   break;
            }
            judge = true;
        }
       
    }
}
