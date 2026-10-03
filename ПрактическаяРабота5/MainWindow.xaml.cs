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
using ПрактическаяРабота5.Classes;

namespace ПрактическаяРабота5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Pawn> Pawns = new List<Pawn>();
        public List<Bishop> Bishops = new List<Bishop>();
        public static MainWindow mainWindow;
        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
            Pawns.Add(new Classes.Pawn(0, 1, false));
            Pawns.Add(new Classes.Pawn(1, 1, false));
            Pawns.Add(new Classes.Pawn(2, 1, false));
            Pawns.Add(new Classes.Pawn(3, 1, false));
            Pawns.Add(new Classes.Pawn(4, 1, false));
            Pawns.Add(new Classes.Pawn(5, 1, false));
            Pawns.Add(new Classes.Pawn(6, 1, false));
            Pawns.Add(new Classes.Pawn(7, 1, false));
            // Добавление чёрных пешек
            Pawns.Add(new Classes.Pawn(0, 6, true));
            Pawns.Add(new Classes.Pawn(1, 6, true));
            Pawns.Add(new Classes.Pawn(2, 6, true));
            Pawns.Add(new Classes.Pawn(3, 6, true));
            Pawns.Add(new Classes.Pawn(4, 6, true));
            Pawns.Add(new Classes.Pawn(5, 6, true));
            Pawns.Add(new Classes.Pawn(6, 6, true));
            Pawns.Add(new Classes.Pawn(7, 6, true));
            CreateFigure();


            Bishops.Add(new Classes.Bishop(6, 0, false));
            Bishops.Add(new Classes.Bishop(1, 0, false));
            Bishops.Add(new Classes.Bishop(6, 7, true));
            Bishops.Add(new Classes.Bishop(1, 7, true));
            CreateFigureBishop();
        }

        //public void SelectTitle(object sender, MouseButtonEventArgs e)
        //{
        //    Grid Tile = sender as Grid;

        //    // Получаем координаты выбранного тайла
        //    int X = Grid.GetColumn(Tile);
        //    int Y = Grid.GetRow(Tile);

        //    // Получаем выбранную пешку
        //    Classes.Pawn SelectPawn = Pawns.Find(x => x.Select == true);

        //    // Если выбранная пешка присутствует
        //    if (SelectPawn != null)
        //    {
        //        // Перемещаем пешку на выбранный тайл
        //        SelectPawn.Transform(X, Y);
        //    }
        //}

        public void OnSelect(Classes.Pawn SelectPawn)
        {
            foreach (Classes.Pawn Pawn in Pawns)
            {
                if (Pawn != SelectPawn)
                    if (Pawn.Select)
                        Pawn.SelectFigure(null, null);
            }
        }
        public void OnSelectBishop(Classes.Bishop SelectBishop)
        {
            foreach (Classes.Bishop Bishop in Bishops)
            {
                if (Bishop != SelectBishop)
                    if (Bishop.Select)
                        Bishop.SelectFigure(null, null);
            }
        }

        public void CreateFigureBishop()
        {
            // Перебираем коллекцию пешек
            foreach (Classes.Bishop Bishop in Bishops)
            {
                // Создаём элемент Grid, с размерами тайла
                Bishop.Figure = new Grid()
                {
                    Width = 50,
                    Height = 50
                };
                // В зависимости от цвета пешки, указываем ей изображение
                if (Bishop.Black)
                    Bishop.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Pawn(black).png")));
                else
                    Bishop.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Pawn.png")));
                // Перемещаем пешку на указанную позицию/стартовую позицию
                Grid.SetColumn(Bishop.Figure, Bishop.X);
                Grid.SetRow(Bishop.Figure, Bishop.Y);
                // Подписываемся на событие нажатия на пешку
                Bishop.Figure.MouseDown += Bishop.SelectFigure;
                // Добавляем на интерфейс созданную пешку
                gameBoard.Children.Add(Bishop.Figure);
            }
        }

        public void CreateFigure()
        {
            // Перебираем коллекцию пешек
            foreach (Classes.Pawn Pawn in Pawns)
            {
                // Создаём элемент Grid, с размерами тайла
                Pawn.Figure = new Grid()
                {
                    Width = 50,
                    Height = 50
                };
                // В зависимости от цвета пешки, указываем ей изображение
                if (Pawn.Black)
                    Pawn.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Pawn(black).png")));
                else
                    Pawn.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/Pawn.png")));
                // Перемещаем пешку на указанную позицию/стартовую позицию
                Grid.SetColumn(Pawn.Figure, Pawn.X);
                Grid.SetRow(Pawn.Figure, Pawn.Y);
                // Подписываемся на событие нажатия на пешку
                Pawn.Figure.MouseDown += Pawn.SelectFigure;
                // Добавляем на интерфейс созданную пешку
                gameBoard.Children.Add(Pawn.Figure);
            }
        }

        private void SelectTile(object sender, MouseButtonEventArgs e)
        {
            Grid Tile = sender as Grid;

            // Получаем координаты выбранного тайла
            int X = Grid.GetColumn(Tile);
            int Y = Grid.GetRow(Tile);

            // Получаем выбранную пешку
            Classes.Pawn SelectPawn = Pawns.Find(x => x.Select == true);

            // Если выбранная пешка присутствует
            if (SelectPawn != null)
            {
                // Перемещаем пешку на выбранный тайл
                SelectPawn.Transform(X, Y);
            }
        }
    }
}
