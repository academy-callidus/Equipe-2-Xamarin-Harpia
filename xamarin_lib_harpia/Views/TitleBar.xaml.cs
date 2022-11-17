﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xamarin_lib_harpia.Models.Services;

namespace xamarin_lib_harpia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TitleBar : StackLayout
    {
        public string Subtitle { get; }
        /// <summary>
        /// Cria uma propriedade própria do elemento Titlebar que pode ser acessada no arquivo xaml
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(TitleText),
            typeof(string),
            typeof(TitleBar),
            defaultValue:"Título",
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: TitleTextPropertyChanged);
        /// <summary>
        /// Função que realiza as alterações da nova propriedade no elemento alvo
        /// </summary>
        /// <param name="bindable">Elemento que receberá as alterações da nova propriedade</param>
        /// <param name="newValue">Novo texto que irá ser exibido</param>
        private static void TitleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (TitleBar)bindable;
            control.NavTitle.Text = newValue?.ToString();
        }

        public string TitleText
        {
            get => GetValue(TitleProperty)?.ToString();

            set => SetValue(TitleProperty, value);
        }
        public TitleBar()
        {
            InitializeComponent();
            IPrinterConnection connection = DependencyService.Get<IPrinterConnection>();
            ConnectionStatusService Status = new ConnectionStatusService(connection);

            Subtitle = Status.ConnectionStatus();
            NavSubtitle.SetBinding(Label.TextProperty, new Binding("Subtitle", source: this));
        }
    }
}