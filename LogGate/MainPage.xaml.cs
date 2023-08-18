﻿using LogGate.ViewModel;


namespace LogGate;

public partial class MainPage : ContentPage
{

    private MainViewModel mainPageVM;
    public MainPage(MainViewModel mpvm)
    {

        InitializeComponent();
        mainPageVM = mpvm;

    }

    private void btnAdd_Clicked(object sender, EventArgs e)
    {
        mainPageVM.Add();
    }
}

