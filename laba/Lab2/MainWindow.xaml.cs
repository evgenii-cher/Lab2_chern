using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Lab2
{
    public class CEnemyTemplate
    {
        //Название противника
        [JsonInclude]
        string name;
        
        //Название иконки
        [JsonInclude]
        string iconName;
        //Атрибуты здоровья
        [JsonInclude]
        int baseLife;
        [JsonInclude]
        double lifeModifier;

        //Атрибуты золота за победу над противником
        [JsonInclude]
        int baseGold;
        [JsonInclude]
        double goldModifier;
        //Шанс на появление
        [JsonInclude]
        double spawnChance;

        public CEnemyTemplate(string name, string iconName, int baseLife, double lifeModifier, int baseGold, double goldModifier, double spawnChance)
        {
            this.name = name;
            this.iconName = iconName;
            this.baseLife = baseLife;
            this.lifeModifier = lifeModifier;
            this.baseGold = baseGold;
            this.goldModifier = goldModifier;
            this.spawnChance = spawnChance;
        }

        public string Name()
        {
            return name;
        }
        public string IconName()
        {
            return iconName;
        }
        public int BaseLife()
        {
            return baseLife;
        }
        public double LifeModifier()
        {
            return lifeModifier;
        }
        public int BaseGold()
        {
            return baseGold;
        }
        public double GoldModifier()
        {
            return goldModifier;
        }
        public double SpawnChance()
        {
            return spawnChance;
        }

    }

    public class CEnemyTemplateList
    {

        //Список противников из класса CEnemyTemplate
       List<CEnemyTemplate> listenemies = new List<CEnemyTemplate>();
        // List<CEnemyTemplate> enemiesList2;

        public void AddEnemy()
        {
            string name = (Application.Current.MainWindow as MainWindow).TextBoxName.Text.ToString();
            string iconName = (Application.Current.MainWindow as MainWindow).TextBoxEnemyIcon.Text.ToString();
            int baseLife = Convert.ToInt32((Application.Current.MainWindow as MainWindow).TextBoxBaseLife.Text);
            double lifeModifier = Convert.ToDouble((Application.Current.MainWindow as MainWindow).TextBoxLifeModifier.Text);
            int baseGold = Convert.ToInt32((Application.Current.MainWindow as MainWindow).TextBoxBaseGold.Text);
            double goldModifier = Convert.ToDouble((Application.Current.MainWindow as MainWindow).TextBoxGoldModifier.Text);
            double spawnChance = Convert.ToDouble((Application.Current.MainWindow as MainWindow).TextBoxSpawnChance.Text);

            listenemies.Add(new CEnemyTemplate(name, iconName, baseLife, lifeModifier, baseGold, goldModifier, spawnChance));

        }
        public void CEnemyTemplateListSave()
        {
            // Сериализация списка в JSON
            string jsonString = JsonSerializer.Serialize(listenemies);
            // Сохранение JSON в файл
            File.WriteAllText("tee1st.json", jsonString);
        }
        public void CleanJson()
        {
            listenemies.Clear();
            string jsonString = JsonSerializer.Serialize(listenemies);
            // Сохранение JSON в файл
            File.WriteAllText("tee1st.json", jsonString);
        }
        public void CEnemyTemplateListLoadandShow()
        { 
            // Чтение JSON из файла
            string jsonFromFile = File.ReadAllText("tee1st.json");
           // enemiesList2 = new List<CEnemyTemplate>();

            // Парсинг JSON
            JsonDocument doc = JsonDocument.Parse(jsonFromFile);
            //Добавление новой записи в список класса из json
            foreach (JsonElement element in doc.RootElement.EnumerateArray())
            {
                
                string name = element.GetProperty("name").GetString();
                string iconName = element.GetProperty("iconName").GetString();
                int baseLife = element.GetProperty("baseLife").GetInt32();
                double lifeModifier = element.GetProperty("lifeModifier").GetDouble();
                int baseGold = element.GetProperty("baseGold").GetInt32();
                double goldModifier = element.GetProperty("goldModifier").GetDouble();
                double spawnChance = element.GetProperty("spawnChance").GetDouble();

                if (name == (Application.Current.MainWindow as MainWindow).TextBoxNameDel.Text.ToString())
                    continue;
              

                // Создание нового экземпляра класса Person с помощью конструктора
                CEnemyTemplate cenemyTemplate = new CEnemyTemplate(name, iconName, baseLife, lifeModifier, baseGold, goldModifier, spawnChance);
                // Добавление объекта в список
               
                listenemies.Add(cenemyTemplate);
                // enemies.Add(enemiesTest2);

            }
          
           
            foreach (var cenemyTemplate in listenemies)
            {
                (Application.Current.MainWindow as MainWindow).TextBoxEnemies.Text += cenemyTemplate.Name() + " " + cenemyTemplate.IconName() + " " + cenemyTemplate.BaseLife() + " " + cenemyTemplate.GoldModifier() + " " + cenemyTemplate.SpawnChance() +  "\n";
              
            }

        }
      
}

    /* public class CIcon
     {
        public CIcon(int iconWidth, int iconHeight, string imagePath)
        {
          Point  position = new Point(0, 0);
          string  name = System.IO.Path.GetFileNameWithoutExtension(imagePath);
           Rectangle icon = new Rectangle();
            //установка цвета линии обводки и цвета заливки при помощи коллекции кистей
            icon.Stroke = Brushes.Black;
            ImageBrush ib = new ImageBrush();
            //позиция изображения будет указана как координаты  левого верхнего угла
            //изображение будет растянуто по размерам прямоугольника, описанного вокруг фигуры
            ib.AlignmentX = AlignmentX.Left;
            ib.AlignmentY = AlignmentY.Top;
            //загрузка изображения и назначение кисти
            ib.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            icon.RenderTransform = new TranslateTransform(position.X, position.Y);
            icon.Fill = ib;
            //параметры выравнивания
            icon.HorizontalAlignment = HorizontalAlignment.Left;
            icon.VerticalAlignment = VerticalAlignment.Center;
            //размеры прямоугольника
            icon.Height = iconHeight;
            icon.Width = iconWidth;

            


        }


    }*/

    public partial class MainWindow : Window
    {

      
        public MainWindow()
        {
            InitializeComponent();
            CEnemyTemplateList enemieslist = new CEnemyTemplateList();
            enemieslist.CEnemyTemplateListLoadandShow();



        }




        public void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            CEnemyTemplateList enemieslist = new CEnemyTemplateList();
            TextBoxEnemies.Clear();
            
            enemieslist.AddEnemy();
            enemieslist.CEnemyTemplateListLoadandShow();
            enemieslist.CEnemyTemplateListSave();
           
          
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
           
            CEnemyTemplateList enemieslist = new CEnemyTemplateList();
            enemieslist.CleanJson();
            TextBoxEnemies.Clear();


        }

        private void DelByName_Click(object sender, RoutedEventArgs e)
        {
            CEnemyTemplateList enemieslist = new CEnemyTemplateList();
            TextBoxEnemies.Clear();
            enemieslist.CEnemyTemplateListLoadandShow();
          
            enemieslist.CEnemyTemplateListSave();
           

            TextBoxNameDel.Clear();


        }

        private void Button1ico_Click(object sender, RoutedEventArgs e)
        {
            image1.Visibility = Visibility.Visible;
            image2.Visibility = Visibility.Hidden;
            image3.Visibility = Visibility.Hidden;
        }

        private void Button2ico_Click(object sender, RoutedEventArgs e)
        {
            image2.Visibility = Visibility.Visible;
            image1.Visibility = Visibility.Hidden;
            image3.Visibility = Visibility.Hidden;
        }

        private void Button3ico_Click(object sender, RoutedEventArgs e)
        {
            image1.Visibility = Visibility.Hidden;
            image2.Visibility = Visibility.Hidden;
            image3.Visibility = Visibility.Visible;
        }
    }
}
