

using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace ПрактическаяРабота5.Classes
{
    public class Bishop
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Select = false;
        public bool Black = false;

        public Grid Figure { get; set; }
        public Bishop(int X, int Y, bool Black)
        {
            this.X = X;
            this.Y = Y;
            this.Black = Black;
        }
        public void SelectFigure(object sender, MouseButtonEventArgs e)
        {
            bool atack = false;

            Bishop SelectBishop = MainWindow.mainWindow.Bishops.Find(X => X.Select == true);

            if (SelectBishop != null)
            {
                if (this.Black && this.Y - 1 == SelectBishop.Y &&
                    (X == SelectBishop.X - 1 || X == SelectBishop.X - 2 || X == SelectBishop.X - 3 || X == SelectBishop.X - 4 || X == SelectBishop.X - 5 || X == SelectBishop.X - 6 || X == SelectBishop.X - 7 ||
                     X == SelectBishop.X + 1 || X == SelectBishop.X + 2 || X == SelectBishop.X + 3 || X == SelectBishop.X + 4 || X == SelectBishop.X + 5 || X == SelectBishop.X + 6 || X == SelectBishop.X + 7) ||
                    !this.Black && this.Y - 1 == SelectBishop.Y &&
                    (X == SelectBishop.X - 1 || X == SelectBishop.X - 2 || X == SelectBishop.X - 3 || X == SelectBishop.X - 4 || X == SelectBishop.X - 5 || X == SelectBishop.X - 6 || X == SelectBishop.X - 7 ||
                     X == SelectBishop.X + 1 || X == SelectBishop.X + 2 || X == SelectBishop.X + 3 || X == SelectBishop.X + 4 || X == SelectBishop.X + 5 || X == SelectBishop.X + 6 || X == SelectBishop.X + 7))
                {
                    MainWindow.mainWindow.gameBoard.Children.Remove(this.Figure);

                    // Перемещаем атакующую пешку на координаты атакованной пешки
                    Grid.SetColumn(SelectBishop.Figure, this.X);
                    Grid.SetRow(SelectBishop.Figure, this.Y);

                    // Присваиваем обновлённые координаты пешке
                    SelectBishop.X = this.X;
                    SelectBishop.Y = this.Y;

                    // Вызываем выделение пешки, которое снимет с неё выделение
                    SelectBishop.SelectFigure(null, null);
                    return;
                    // Запоминаем что была произведена атака
                    atack = true;
                }
            }
            if (!atack)
            {
                // Вызываем метод снятия выделения со всех пешек которые находятся на доске
                MainWindow.mainWindow.OnSelectBishop(this);

                // Если мы уже были выделены
                if (this.Select)
                {
                    // В зависимости от нашего цвета Белого/Чёрного, отображаем иконку
                    if (this.Black)
                        this.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Pawn(black).png")));
                    else
                        this.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Pawn.png")));

                    // Запоминаем что пешка более не является выделенной
                    this.Select = false;
                }
                else
                {
                    // Если пешка не выделена, изменяем иконку на выделенную
                    this.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Pawn(select).png")));

                    // Запоминаем что пешка является выделенной
                    this.Select = true;
                }

            }



        }
        public void Transform(int X, int Y)
        {
            // 1. Проверяем, что фигура вообще сдвинулась с места
            if (X == this.X && Y == this.Y)
            {
                SelectFigure(null, null);
                return;
            }

            // 2. Проверяем математическое правило хода слона:
            // Разница между текущей и целевой координатой по X должна быть равна разнице по Y.
            // Используем Math.Abs для получения абсолютного значения (модуля числа).
            if (Math.Abs(X - this.X) == Math.Abs(Y - this.Y))
            {

                // ВАЖНО: Здесь должна быть проверка, нет ли других фигур на пути слона.
                // Если у вас есть массив/матрица доски, нужно пустить цикл от this.X/this.Y до X/Y.
                // Если путь свободен:

                // Изменяем положение фигуры на WPF-сетке (Grid)
                Grid.SetColumn(this.Figure, X);
                Grid.SetRow(this.Figure, Y);

                // Запоминаем новые координаты слона
                this.X = X;
                this.Y = Y;
            }

            // Снимаем выделение со слона в любом случае
            SelectFigure(null, null);
        }

    }
}
