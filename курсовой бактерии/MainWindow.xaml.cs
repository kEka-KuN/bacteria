using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace курсовой_бактерии
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private static Canvas nCanvas = new Canvas();
        private static Label _Count = new Label();
        public class GenCode
        {
            public int rgb1, rgb2, rgb3;
            public int vector;
            public int distance;
            public double seconds;
            public Point position;
            public int NumChild;
        }
        protected class VectorMove
        {
            private TranslateTransform TTMove = new TranslateTransform();
            private DoubleAnimation AniMove = new DoubleAnimation();
            private double distance;
            private Point position;

            public VectorMove(double second, double _distance, Point _position)
            {
                AniMove.Duration = TimeSpan.FromSeconds(second);
                distance = _distance;
                position = _position;
            }
            public Point RightMove(UIElement element)
            {
                if (distance + position.X > 1872)
                {
                    distance = 1872 - position.X;
                }

                element.RenderTransform = TTMove;
                AniMove.To = distance;
                TTMove.BeginAnimation(TranslateTransform.XProperty, AniMove);
                return new Point(distance + position.X, position.Y);
            }
            public Point LeftMove(UIElement element)
            {
                if (distance > position.X)
                {
                    distance = position.X;
                }

                element.RenderTransform = TTMove;
                AniMove.To = -distance;
                TTMove.BeginAnimation(TranslateTransform.XProperty, AniMove);
                return new Point(position.X - distance, position.Y);
            }
            public Point DownMove(UIElement element)
            {
                if (distance + position.Y > 964)
                {
                    distance = 964 - position.Y;
                }

                element.RenderTransform = TTMove;
                AniMove.To = distance;
                TTMove.BeginAnimation(TranslateTransform.YProperty, AniMove);
                return new Point(position.X, position.Y + distance);
            }
            public Point UpMove(UIElement element)
            {
                if (distance > position.Y)
                {
                    distance = position.Y;
                }

                element.RenderTransform = TTMove;
                AniMove.To = -distance;
                TTMove.BeginAnimation(TranslateTransform.YProperty, AniMove);
                return new Point(position.X, position.Y - distance);
            }
            public Point UpLeftMove(UIElement element)
            {
                if (distance > position.X)
                {
                    distance = position.X;
                }
                if (distance > position.Y)
                {
                    distance = position.Y;
                }

                element.RenderTransform = TTMove;
                AniMove.To = -distance;
                TTMove.BeginAnimation(TranslateTransform.YProperty, AniMove);
                TTMove.BeginAnimation(TranslateTransform.XProperty, AniMove);
                return new Point(position.X - distance, position.Y - distance);
            }
            public Point DownRightMove(UIElement element)
            {
                if (distance + position.X > 1872)
                {
                    distance = 1872 - position.X;
                }
                if (distance + position.Y > 964)
                {
                    distance = 964 - position.Y;
                }

                element.RenderTransform = TTMove;
                AniMove.To = distance;
                TTMove.BeginAnimation(TranslateTransform.YProperty, AniMove);
                TTMove.BeginAnimation(TranslateTransform.XProperty, AniMove);
                return new Point(position.X + distance, position.Y + distance);
            }
            public Point UpRightMove(UIElement element)
            {
                if (distance + position.X > 1872)
                {
                    distance = 1872 - position.X;
                }
                if (distance > position.Y)
                {
                    distance = position.Y;
                }

                element.RenderTransform = TTMove;
                AniMove.To = -distance;
                TTMove.BeginAnimation(TranslateTransform.YProperty, AniMove);
                AniMove.To = distance;
                TTMove.BeginAnimation(TranslateTransform.XProperty, AniMove);
                return new Point(position.X + distance, position.Y - distance);
            }
            public Point DownLeftMove(UIElement element)
            {
                if (distance + position.Y > 964)
                {
                    distance = 964 - position.Y;
                }
                if (distance > position.X)
                {
                    distance = position.X;
                }

                element.RenderTransform = TTMove;
                AniMove.To = distance;
                TTMove.BeginAnimation(TranslateTransform.YProperty, AniMove);
                AniMove.To = -distance;
                TTMove.BeginAnimation(TranslateTransform.XProperty, AniMove);
                return new Point(position.X - distance, position.Y + distance);
            }
        }
        public class Bacterium
        {
            private GenCode gen;
            private Ellipse Visual;
            private DispatcherTimer Existence;

            public Bacterium()
            {
                Random random = new Random();
                gen = new GenCode();
                Visual = new Ellipse();

                gen.rgb1 = random.Next(0, 240);
                gen.rgb2 = random.Next(0, 240);
                gen.rgb3 = random.Next(0, 240);
                gen.vector = random.Next(0, 8);
                gen.distance = random.Next(0, 1000);
                gen.seconds = random.Next(1, 3);
                gen.NumChild = 2;

                Existence = new DispatcherTimer();
                Existence.Interval = TimeSpan.FromSeconds(gen.seconds);
                Existence.Tick += death;

                Visual.Width = 45;
                Visual.Height = 45;
                Visual.Fill = new SolidColorBrush(Color.FromRgb(Convert.ToByte(gen.rgb1), Convert.ToByte(gen.rgb2), Convert.ToByte(gen.rgb3)));
                Canvas.SetLeft(Visual, 900);
                Canvas.SetTop(Visual, 500);
            }
            public Bacterium(int non)
            {
                Random random = new Random();
                gen = new GenCode();

                gen.rgb1 = random.Next(0, 240);
                gen.rgb2 = random.Next(0, 240);
                gen.rgb3 = random.Next(0, 240);
                gen.vector = random.Next(0, 8);
                gen.distance = random.Next(0, 1000);
                gen.seconds = random.Next(1, 3);
                gen.NumChild = 2;

                Existence = new DispatcherTimer();
                Existence.Interval = TimeSpan.FromSeconds(gen.seconds);
                Existence.Tick += death;

            }
            private Bacterium(GenCode _gen)
            {
                Random random = new Random();
                gen = new GenCode();
                Visual = new Ellipse();

                gen.rgb1 = _gen.rgb1 + random.Next(-45, 45);
                gen.rgb2 = _gen.rgb2 + random.Next(-45, 45);
                gen.rgb3 = _gen.rgb3 + random.Next(-45, 45);

                if (gen.rgb1 < 0) { gen.rgb1 = 0; }
                if (gen.rgb1 > 240) { gen.rgb1 = 240; }

                if (gen.rgb2 < 0) { gen.rgb2 = 0; }
                if (gen.rgb2 > 240) { gen.rgb2 = 240; }

                if (gen.rgb3 < 0) { gen.rgb3 = 0; }
                if (gen.rgb3 > 240) { gen.rgb3 = 240; }

                gen.distance = random.Next(0, 1000);
                gen.vector = random.Next(0, 8);

                gen.seconds = _gen.seconds + random.NextDouble() * random.Next(-1, 1);
                if (gen.seconds < 1) { gen.seconds = 1; }
                if (gen.seconds > 4) { gen.seconds = 4; }

                if (random.Next(0, 100) == 0) { gen.NumChild = 3; }
                else if (random.Next(0,60) == 0) { gen.NumChild = 0; }
                else if (nCanvas.Children.Count>700) { gen.NumChild = 0; }
                else if (random.Next(0, 10) == 0) { gen.NumChild = 2; }
                else { gen.NumChild = _gen.NumChild; }

                Existence = new DispatcherTimer();
                Existence.Interval = TimeSpan.FromSeconds(gen.seconds);
                Existence.Tick += death;

                Visual.Width = 45;
                Visual.Height = 45;
                Visual.Fill = new SolidColorBrush(Color.FromRgb(Convert.ToByte(gen.rgb1), Convert.ToByte(gen.rgb2), Convert.ToByte(gen.rgb3)));
                Canvas.SetLeft(Visual, _gen.position.X);
                Canvas.SetTop(Visual, _gen.position.Y);
            }
            public Bacterium(GenCode _gen, int non)
            {
                Random random = new Random();
                gen = new GenCode();

                gen.rgb1 = _gen.rgb1 + random.Next(-45, 45);
                gen.rgb2 = _gen.rgb2 + random.Next(-45, 45);
                gen.rgb3 = _gen.rgb3 + random.Next(-45, 45);

                if (gen.rgb1 < 0) { gen.rgb1 = 0; }
                if (gen.rgb1 > 240) { gen.rgb1 = 240; }

                if (gen.rgb2 < 0) { gen.rgb2 = 0; }
                if (gen.rgb2 > 240) { gen.rgb2 = 240; }

                if (gen.rgb3 < 0) { gen.rgb3 = 0; }
                if (gen.rgb3 > 240) { gen.rgb3 = 240; }

                gen.distance = random.Next(0, 1000);
                gen.vector = random.Next(0, 8);

                gen.seconds = _gen.seconds + random.NextDouble() * random.Next(-1, 1);
                if (gen.seconds < 1) { gen.seconds = 1; }
                if (gen.seconds > 4) { gen.seconds = 4; }

                if (random.Next(0, 100) == 0) { gen.NumChild = 3; }
                else if (random.Next(0, 60) == 0) { gen.NumChild = 0; }
                else if (random.Next(0, 10) == 0) { gen.NumChild = 2; }
                else { gen.NumChild = _gen.NumChild; }

                Existence = new DispatcherTimer();
                Existence.Interval = TimeSpan.FromSeconds(gen.seconds);
                Existence.Tick += death;
            }

            private void death(object sender, EventArgs e)
            {
                Existence.Stop();
                nCanvas.Children.Remove(Visual);

                for (int i = 0; i < gen.NumChild; i++)
                {
                    Bacterium check = new Bacterium(gen);
                    nCanvas.Children.Add(check.GetVisual);
                    check.Life();
                }

                _Count.Content = Convert.ToString(nCanvas.Children.Count - 2);
            }
            /*private void death(object sender, ElapsedEventArgs e)
            {
                Dispatcher dispatcher = Application.Current.MainWindow.Dispatcher;
                dispatcher.BeginInvoke(() =>
                {
                    var mc = new MainWindow();
                    var mc1 = new Bacterium();
                    mc._Canvas.Children.Remove(mc1.Visual);
                });

                Application.Current.MainWindow.Dispatcher.BeginInvoke(() =>
                {
                    var mc = new MainWindow();
                    var mc1 = new Bacterium();
                    mc._Canvas.Children.Remove(mc1.Visual);
                });

                //mc.Dispatcher.Invoke(() => { mc._Canvas.Children.Remove(mc1.Visual); });

                var mc = new MainWindow();
                mc._Canvas.Children.Remove(Visual);
            }*/

            public Ellipse GetVisual { get => Visual; }
            public GenCode GetGenForTesting { get => gen; }
            public void Life()
            {
                Point position = new Point(Canvas.GetLeft(Visual), Canvas.GetTop(Visual));
                VectorMove vector = new VectorMove(gen.seconds, gen.distance, position);
                switch (gen.vector)
                {
                    case 0:
                        gen.position = vector.DownLeftMove(Visual);
                        break;
                    case 1:
                        gen.position = vector.DownRightMove(Visual);
                        break;
                    case 2:
                        gen.position = vector.DownMove(Visual);
                        break;
                    case 3:
                        gen.position = vector.UpLeftMove(Visual);
                        break;
                    case 4:
                        gen.position = vector.UpRightMove(Visual);
                        break;
                    case 5:
                        gen.position = vector.UpMove(Visual);
                        break;
                    case 6:
                        gen.position = vector.LeftMove(Visual);
                        break;
                    case 7:
                        gen.position = vector.RightMove(Visual);
                        break;
                }
                Existence.Start();
            }

        }

        public MainWindow()
        {
            InitializeComponent();
            nCanvas = _Canvas;
            _Count = Count;
            StartButton.Click += Start;
            Count.Content = Convert.ToString(_Canvas.Children.Count - 2);
        }
        private void Start(object sender, RoutedEventArgs e)
        {
            Bacterium check = new Bacterium();
            _Canvas.Children.Add(check.GetVisual);
            Count.Content = Convert.ToString(_Canvas.Children.Count - 2);
            check.Life();
        }
    }
}
