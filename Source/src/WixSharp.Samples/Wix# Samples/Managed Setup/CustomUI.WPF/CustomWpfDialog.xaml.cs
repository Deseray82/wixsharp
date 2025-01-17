﻿using Caliburn.Micro;
using System;
using System.Windows.Media.Imaging;
using WixSharp;
using WixSharp.UI.Forms;
using WixSharp.UI.WPF;

namespace MyProduct
{
    public partial class CustomDialogView : WpfDialog, IWpfDialog
    {
        public CustomDialogView()
        {
            InitializeComponent();
        }

        public void Init()
        {
            // resolve and translate all elements with translatable content ("[<localization_key>]")
            base.Localize();

            var viewModel = new CustomDialogModel { Host = this.ManagedFormHost };

            ViewModelBinder.Bind(viewModel, this, null);
        }
    }

    public class CustomDialogModel : Caliburn.Micro.Screen
    {
        public ManagedForm Host { get; set; }

        public BitmapImage Banner => Host?.Runtime.Session.GetResourceBitmap("WixUI_Bmp_Banner").ToImageSource();

        bool canProceed;

        public bool CanProceedIsChecked
        {
            get { return canProceed; }
            set
            {
                canProceed = value;
                NotifyOfPropertyChange(() => CanProceedIsChecked);
                NotifyOfPropertyChange(() => CanGoNext);
            }
        }

        public string User { get; set; } = Environment.UserName;

        public bool CanGoNext
            => CanProceedIsChecked;

        public void GoPrev()
            => Host?.Shell.GoPrev();

        public void GoNext()
            => Host?.Shell.GoNext();

        public void Cancel()
            => Host?.Shell.Cancel();
    }
}