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

namespace WPFSistemaDeLavagemAutomotiva.View.Components
{
    public class NavButton : ListBoxItem
    {
        static NavButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavButton), new FrameworkPropertyMetadata(typeof(NavButton)));
        }


        //Permite definir o link de navegação do botão
        public Uri NavLink 
        {
            get { return (Uri)GetValue(NavLinkProperty); }
            set { SetValue(NavLinkProperty, value); }
        }

        public static readonly DependencyProperty NavLinkProperty =
            DependencyProperty.Register("NavLink", typeof(Uri), typeof(NavButton), new PropertyMetadata(null));


        //Permite definir o ícone do botão de navegação
        public Geometry Icon
        {
            get { return (Geometry)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(Geometry), typeof(NavButton), new PropertyMetadata(null));


        //Permite definir a cor do ícone do botão de navegação
        public Brush IconFill
        {
            get { return (Brush)GetValue(IconFillProperty); }
            set { SetValue(IconFillProperty, value); }
        }

        public static readonly DependencyProperty IconFillProperty =
            DependencyProperty.Register("IconFill", typeof(Brush), typeof(NavButton), new PropertyMetadata(Brushes.Black));


        // Permite definir o texto exibido no botão de navegação
        public string NavText
        {
            get { return (string)GetValue(NavTextProperty); }
            set { SetValue(NavTextProperty, value); }
        }

        public static readonly DependencyProperty NavTextProperty =
            DependencyProperty.Register("NavText", typeof(string), typeof(NavButton), new PropertyMetadata("Botão"));

        // Permite definir a cor de fundo do botão ao passar o mouse sobre ele
        public Brush HoverFill
        {
            get => (Brush)GetValue(HoverFillProperty);
            set => SetValue(HoverFillProperty, value);
        }

        public static readonly DependencyProperty HoverFillProperty = DependencyProperty.Register(
                                                                          nameof(HoverFill),
                                                                          typeof(Brush),
                                                                          typeof(NavButton),
                                                                          new PropertyMetadata(new SolidColorBrush(Color.FromRgb(42, 132, 241))) // #2a84f1
                                                                      );
        // Permite definir a cor de fundo do botão quando selecionado
        public Brush SelectedFill
        {
            get => (Brush)GetValue(SelectedFillProperty);
            set => SetValue(SelectedFillProperty, value);
        }

        public static readonly DependencyProperty SelectedFillProperty = DependencyProperty.Register(
                                                                          nameof(SelectedFill),
                                                                          typeof(Brush),
                                                                          typeof(NavButton),
                                                                          new PropertyMetadata(new SolidColorBrush(Color.FromRgb(42, 132, 241))) // #2a84f1
                                                                      );
    }
}
