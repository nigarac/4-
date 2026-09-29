using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace ПрактическаяРабота5.Classes
{
    public class Pawn
    {
        public int X {  get; set; }
        public int Y { get; set; }
        public bool Select = false;
        public bool Black= false;
        public Grid Figure {  get; set; }
        public Pawn(int X, int Y, bool Black)
        {
            this.X = X;
            this.Y = Y;
            this.Black = Black;
        }
        /// <summary> Метод выбора фигуры

        public void SelectFigure(object sender, MouseButtonEventArgs e)
        {
            // Переменная отвечающая за то, атакуют ли нашу фигуру
            bool atack = false;

            // Среди всех пешек ищем пешку, которая является выделенной
            Pawn SelectPawn = MainWindow.mainWindow.Pawns.Find(x => x.Select == true);

            // Если выделенная пешка существует
            if (SelectPawn != null)
            {
                // Проверяем атакует ли нас эта пешка, если это чёрная пешка, проверяем находится ли она ниже нас, и входит ли в диапазон атаки
                if (this.Black && this.Y - 1 == SelectPawn.Y && (this.X - 1 == SelectPawn.X || this.X == SelectPawn.X || this.X + 1 == SelectPawn.X) ||
                    // Если пешка является чёрной, проверяем находится ли она ниже нас, и входит ли в диапазон атаки
                    !this.Black && this.Y + 1 == SelectPawn.Y && (this.X - 1 == SelectPawn.X || this.X == SelectPawn.X || this.X + 1 == SelectPawn.X))
                {
                    // Обращаемся к доске, и удаляем пешку с доски
                    MainWindow.mainWindow.gameBoard.Children.Remove(this.Figure);

                    // Перемещаем атакующую пешку на координаты атакованной пешки
                    Grid.SetColumn(SelectPawn.Figure, this.X);
                    Grid.SetRow(SelectPawn.Figure, this.Y);

                    // Присваиваем обновлённые координаты пешке
                    SelectPawn.X = this.X;
                    SelectPawn.Y = this.Y;

                    // Вызываем выделение пешки, которое снимет с неё выделение
                    SelectPawn.SelectFigure(null, null);
                    return;
                    // Запоминаем что была произведена атака
                    atack = true;
                }
            }

            // Если атака не была произведена, значит нам необходимо выделить пешку
            if (!atack)
            {
                // Вызываем метод снятия выделения со всех пешек которые находятся на доске
                MainWindow.mainWindow.OnSelect(this);

                // Если мы уже были выделены
                if (this.Select)
                {
                    // В зависимости от нашего цвета Белого/Чёрного, отображаем иконку
                    if (this.Black)
                        this.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Pawn (black).png")));
                    else
                        this.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Pawn.png")));

                    // Запоминаем что пешка более не является выделенной
                    this.Select = false;
                }
                else
                {
                    // Если пешка не выделена, изменяем иконку на выделенную
                    this.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Pawn (select).png")));

                    // Запоминаем что пешка является выделенной
                    this.Select = true;
                }
            }
        }


        public void Transform(int X, int Y)
        {
            if (X != this.X)
            {
                // Снимаем выделение пешки
                SelectFigure(null, null);
                // Заканчиваем выполнение метода
                return;
            }

            // Проверяем координату по Y
            // Если пешка является чёрной, и если клетка на которой она стоит 1, и пользователь хочет переместить нашу фигуру на 2 клетки или на 1 клетку
            if (!Black && ((this.Y == 1 && this.Y + 2 == Y) || this.Y + 1 == Y) ||
                // Если пешка является белой, и если клетка на которой она стоит 1,
                // и пользователь хочет переместить нашу фигуру на 2 клетки или на 1 клетку
                Black && ((this.Y == 6 && this.Y - 2 == Y) || this.Y - 1 == Y))
            {
                // Изменяем положение фигуры на доске по X и Y
                Grid.SetColumn(this.Figure, X);
                Grid.SetRow(this.Figure, Y);
                // Запоминаем координаты на которые переместили пешку
                this.X = X;
                this.Y = Y;
            }
            // Снимаем выделение с пешки
            SelectFigure(null, null);
        }
    }


}
