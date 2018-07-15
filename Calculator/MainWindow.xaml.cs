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

namespace Calculator
{
    enum Operation { None = 0, Add, Sub, Multi, Div }
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        //계산기의 텍스트 바인딩
        public static DataSet ViewModel = new DataSet();

        private double Source = 0;
        private double Target = 0;

      

        Operation CurrentOperation = Operation.None;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = ViewModel;
        }

        private void TiteBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Minimize_click(object sender, RoutedEventArgs e)
        {
            Window.WindowState = WindowState.Minimized;
        }

        private void OnClickNumButton(object sender, RoutedEventArgs e)
        {

            if(ViewModel.DisplayText.Length < 17)
            {
                ViewModel.DisplayText = ViewModel.DisplayText +
                (((Button)sender).Content).ToString();
            }
        }


        private void OnClickEqulButton(object sender, RoutedEventArgs e)
        {
            bool success = double.TryParse(ViewModel.DisplayText , out Target);
            if(!success)
            {
                MessageBox.Show("잘못된 값 입니다.");
                return;
            }

            double resultVal = 0;

            try
            {
                if (CurrentOperation == Operation.Add)
                {
                    resultVal = Source + Target;
                }
                else if (CurrentOperation == Operation.Sub)
                {
                    resultVal = Source - Target;
                }
                else if (CurrentOperation == Operation.Multi)
                {
                    resultVal = Source * Target;
                }
                else if (CurrentOperation == Operation.Div)
                {
                    resultVal = Source / Target;
                }

            }
            catch(Exception)
            {
                MessageBox.Show("연산오류가 발생했습니다.");
            }


            ViewModel.SubDisplayText = "";
            CurrentOperation = Operation.None;
            ViewModel.DisplayText = resultVal.ToString();

        }

        private void OnClickDelButton(object sender, RoutedEventArgs e)
        {
            if (ViewModel.DisplayText.Length != 0)
            {
                ViewModel.DisplayText = ViewModel.DisplayText.Remove(ViewModel.DisplayText.Length - 1);
            }
        }

        private void OnOper(object sender, RoutedEventArgs e)
        {

        }


        private void OnOperatorButtonClick(object sender, RoutedEventArgs e)
        {

            Button OperationButton = (Button)sender;
            try
            {
                if (OperationButton == Add)
                { 
                    //이미 서브 디스플레이에 어떤 식이 입력되어 있는 경우라면
                    if(ViewModel.SubDisplayText != "" )
                    {
                        double val = double.Parse(ViewModel.DisplayText);

                        //식에 따를 적절한 처리를 한 다음
                        if(CurrentOperation == Operation.Add)
                        {
                            Source = Source + val;
                        }
                        else if(CurrentOperation == Operation.Sub)
                        {
                            Source = Source + val;
                        }
                        else if(CurrentOperation == Operation.Multi)
                        {
                            Source = Source * val;
                        }
                        else if(CurrentOperation == Operation.Div)
                        {
                            Source = Source / val;
                        }

                        //현재 연산을 덧셈으로 한 다음 리턴
                        CurrentOperation = Operation.Add;
                        ViewModel.SubDisplayText = Source.ToString() + " +";
                        ViewModel.DisplayText = "";

                    }
                    else //아니면
                    {
                        //현재 시행중인 연산을 덧셈으로 한 다음
                        CurrentOperation = Operation.Add;
                        //전항에 메인 디스플레이에 표시된 값 파싱해서 넣고
                        Source = double.Parse(ViewModel.DisplayText);
                        //서브 디스플레이에는 메인 디스플레이에 표시된 값과 " +"를 결합한다.
                        ViewModel.SubDisplayText = ViewModel.DisplayText + " +";
                        ViewModel.DisplayText = "";

                    }

                }
                else if (OperationButton == Sub)
                {
                    //파싱된 값
                    double val;
                    bool success = double.TryParse(ViewModel.DisplayText, out val);
                    
                    if(success == false)
                    {
                        ViewModel.DisplayText = "-";
                        return;
                    }

                    if (ViewModel.SubDisplayText != "")
                    {

                        if (CurrentOperation == Operation.Add)
                        {
                            Source = Source + val;
                        }
                        else if (CurrentOperation == Operation.Sub)
                        {
                            Source = Source + val;
                        }
                        else if (CurrentOperation == Operation.Multi)
                        {
                            Source = Source * val;
                        }
                        else if (CurrentOperation == Operation.Div)
                        {
                            Source = Source / val;
                        }

                        CurrentOperation = Operation.Sub;
                        ViewModel.SubDisplayText = Source.ToString() + " -";
                        ViewModel.DisplayText = "";
                    }
                    else
                    {
                        //상세는 위의 덧셈의 경우와 동일
                        CurrentOperation = Operation.Sub;
                        Source = val;
                        ViewModel.SubDisplayText = ViewModel.DisplayText + " -";
                        ViewModel.DisplayText = "";

                    }

                }
                else if (OperationButton == Multi)
                {
                    if (ViewModel.SubDisplayText != "")
                    {
                        double val = double.Parse(ViewModel.DisplayText);

                        //식에 따를 적절한 처리를 한 다음
                        if (CurrentOperation == Operation.Add)
                        {
                            Source = Source + val;
                        }
                        else if (CurrentOperation == Operation.Sub)
                        {
                            Source = Source + val;
                        }
                        else if (CurrentOperation == Operation.Multi)
                        {
                            Source = Source * val;
                        }
                        else if (CurrentOperation == Operation.Div)
                        {
                            Source = Source / val;
                        }

                        //현재 연산을 덧셈으로 한 다음 리턴
                        CurrentOperation = Operation.Multi;
                        ViewModel.SubDisplayText = Source.ToString() + " *";
                        ViewModel.DisplayText = "";

                    }
                    else
                    {
                        CurrentOperation = Operation.Multi;
                        Source = double.Parse(ViewModel.DisplayText);
                        ViewModel.SubDisplayText = ViewModel.DisplayText + " *";
                        ViewModel.DisplayText = "";
                    }

                }
                else if (OperationButton == Div)
                {

                    if (ViewModel.SubDisplayText != "")
                    {
                        double val = double.Parse(ViewModel.DisplayText);

                        //식에 따를 적절한 처리를 한 다음
                        if (CurrentOperation == Operation.Add)
                        {
                            Source = Source + val;
                        }
                        else if (CurrentOperation == Operation.Sub)
                        {
                            Source = Source + val;
                        }
                        else if (CurrentOperation == Operation.Multi)
                        {
                            Source = Source * val;
                        }
                        else if (CurrentOperation == Operation.Div)
                        {
                            Source = Source / val;
                        }

                        //현재 연산을 덧셈으로 한 다음 리턴
                        CurrentOperation = Operation.Div;
                        ViewModel.SubDisplayText = Source.ToString() + " /";
                        ViewModel.DisplayText = "";

                    }
                    else
                    {
                        CurrentOperation = Operation.Div;
                        Source = double.Parse(ViewModel.DisplayText);
                        ViewModel.SubDisplayText = ViewModel.DisplayText + " /";
                        ViewModel.DisplayText = "";
                    }

                }
            }
            catch (Exception) { }

        }

        private void OnClickCButton(object sender, RoutedEventArgs e)
        {
            ViewModel.DisplayText = "";
        }

        private void OnClickCEButton(object sender, RoutedEventArgs e)
        {
            Target = 0;
            Source = 0;
            ViewModel.DisplayText = "";
            ViewModel.SubDisplayText = "";

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.D0:
                case Key.NumPad0:
                    OnClickNumButton(num0, new RoutedEventArgs());
                    break;

                case Key.D1:
                case Key.NumPad1:
                    OnClickNumButton(num1, new RoutedEventArgs());
                    break;

                case Key.D2:
                case Key.NumPad2:
                    OnClickNumButton(num2, new RoutedEventArgs());
                    break;

                case Key.D3:
                case Key.NumPad3:
                    OnClickNumButton(num3, new RoutedEventArgs());
                    break;

                case Key.D4:
                case Key.NumPad4:
                    OnClickNumButton(num4, new RoutedEventArgs());
                    break;

                case Key.D5:
                case Key.NumPad5:
                    OnClickNumButton(num5, new RoutedEventArgs());
                    break;

                case Key.D6:
                case Key.NumPad6:
                    OnClickNumButton(num6, new RoutedEventArgs());
                    break;

                case Key.D7:
                case Key.NumPad7:
                    OnClickNumButton(num7, new RoutedEventArgs());
                    break;

                case Key.D8:
                case Key.NumPad8:
                    OnClickNumButton(num8, new RoutedEventArgs());
                    break;

                case Key.D9:
                case Key.NumPad9:
                    OnClickNumButton(num9, new RoutedEventArgs());
                    break;

                case Key.Return:
                    OnClickEqulButton(equl, new RoutedEventArgs());
                    break;


                case Key.Add:
                    OnOperatorButtonClick(Add, new RoutedEventArgs());
                    break;

                case Key.Subtract:
                    OnOperatorButtonClick(Sub, new RoutedEventArgs());
                    break;

                case Key.Multiply:
                    OnOperatorButtonClick(Multi, new RoutedEventArgs());
                    break;

                case Key.Divide:
                case Key.Oem2:
                    OnOperatorButtonClick(Div, new RoutedEventArgs());
                    break;

                case Key.Back:
                    OnClickDelButton(Del, new RoutedEventArgs());
                    break;

                case Key.C:
                    OnClickCButton(c, new RoutedEventArgs());
                    break;

                case Key.Decimal:
                    OnClickNumButton(point, new RoutedEventArgs());
                    break;
            }
        }
    }
}
