using System;
using System.Collections.Generic;
using System.IO;
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


namespace PhoHiCreat
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            source_path = "";
            highlight_path = "";
            image_loader = new ImageControl.ImageLoader();
            image_container = new ImageControl.ImageContainer();

            displayed_image_index = 0;
        }

        public string source_path;
        public string highlight_path;

        public int displayed_image_index;

        public ImageControl.ImageLoader image_loader;
        public ImageControl.ImageContainer image_container;

        private void Previous_image_btn_Click(object sender, RoutedEventArgs e)
        {
            // Show previous image
            // Also works with left button
            previous_image();
        }

        private void Next_image_btn_Click(object sender, RoutedEventArgs e)
        {
            // Show next image
            // Also works with right button
            next_image();
        }

        private void Add_to_highlight_btn_Click(object sender, RoutedEventArgs e)
        {
            // Add image to highlight directory
            // Also works with enter
            add_to_highlights();
        }

        private void Source_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox source_textbox = (TextBox)sender;

            // Check if directory exists
            bool textbox_status = check_if_directory_exists(source_textbox.Text);
            if (textbox_status)
            {
                source_path = source_textbox.Text;
                image_container.initialize_ImageContainer(image_loader.GetFilesFrom(source_path));
                updateImageSource();
            }
            set_textbox_background(source_textbox, textbox_status);
        }

        
        private void Target_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // Check if directory exists 
            // Create directory if it doesnt exist
            TextBox source_textbox = (TextBox)sender;
            string target_textbox_text = source_textbox.Text;
            // Check if directory exists
            bool textbox_status = check_if_directory_exists(target_textbox_text);
            set_textbox_background(source_textbox, textbox_status);
            if (textbox_status)
            {
                highlight_path = target_textbox_text;
            }
        }


        private void next_image()
        {
            displayed_image_index++;
            updateImageSource();
        }


        private void previous_image()
        {
            if(displayed_image_index != 0 )
            displayed_image_index--;
            updateImageSource();
        }

        private void updateImageSource()
        {
            try
            {
                ImageViewer.Source = image_container.image_list[displayed_image_index];
            }
            catch(Exception e)
            {
                displayed_image_index = 0;
                updateImageSource();
            }
            
        }

        private void add_to_highlights()
        {
            string fileToCopy = image_container.image_list[displayed_image_index].UriSource.ToString();
            fileToCopy = fileToCopy.Replace("file:///", "");
            string filename = System.IO.Path.GetFileName(fileToCopy);
            string target_path = highlight_path+"\\" + filename;
        File.Copy(fileToCopy, target_path);
        }


        private bool check_if_directory_exists(string path)
        {
            if (!Directory.Exists(path))
            {
                return false;
            }
            else return true;
        }


        private void create_directory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                
            }
        }


        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    next_image();
                    break;
                case Key.Left:
                    previous_image();
                    break;
                case Key.Enter:
                    add_to_highlights();
                    break;

            }
        }


        private void set_textbox_background(TextBox textbox, bool status)
        {
            if (status == true)
            {
                textbox.Background = Brushes.LightGreen;
            }
            else
            {
                textbox.Background = Brushes.Red;
            }
        }

        private void Target_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox source_textbox = (TextBox)sender;
            string target_textbox_text = source_textbox.Text;

            if (e.Key == Key.Enter)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to create this directory", "Create Directory", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    create_directory(target_textbox_text);
                    set_textbox_background(source_textbox, true);
                    highlight_path = target_textbox_text;
                }

            }
        }
    }
}
