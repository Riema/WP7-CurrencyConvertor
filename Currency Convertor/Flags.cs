using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Currency_Convertor
{
    public class Flags : List<Flag>
    {
        public Flags()
        {
            Add(new Flag() { Image = "/images/cz.png" });
            Add(new Flag() { Image = "/images/au.png" });
            Add(new Flag() { Image = "/images/br.png" });
            Add(new Flag() { Image = "/images/bg.png" });
            Add(new Flag() { Image = "/images/cn.png" });
            Add(new Flag() { Image = "/images/dk.png" });
            Add(new Flag() { Image = "/images/eu.png" });
            Add(new Flag() { Image = "/images/ph.png" });
            Add(new Flag() { Image = "/images/hk.png" });
            Add(new Flag() { Image = "/images/hr.png" });
            Add(new Flag() { Image = "/images/in.png" });
            Add(new Flag() { Image = "/images/id.png" });
            Add(new Flag() { Image = "/images/il.png" });
            Add(new Flag() { Image = "/images/jp.png" });
            Add(new Flag() { Image = "/images/za.png" });
            Add(new Flag() { Image = "/images/kr.png" });
            Add(new Flag() { Image = "/images/ca.png" });
            Add(new Flag() { Image = "/images/lt.png" });
            Add(new Flag() { Image = "/images/lv.png" });
            Add(new Flag() { Image = "/images/hu.png" });
            Add(new Flag() { Image = "/images/my.png" });
            Add(new Flag() { Image = "/images/mx.png" });
            Add(new Flag() { Image = "/images/no.png" });
            Add(new Flag() { Image = "/images/nz.png" });
            Add(new Flag() { Image = "/images/pl.png" });
            Add(new Flag() { Image = "/images/ro.png" });
            Add(new Flag() { Image = "/images/ru.png" });
            Add(new Flag() { Image = "/images/sg.png" });
            Add(new Flag() { Image = "/images/se.png" });
            Add(new Flag() { Image = "/images/ch.png" });
            Add(new Flag() { Image = "/images/th.png" });
            Add(new Flag() { Image = "/images/tr.png" });
            Add(new Flag() { Image = "/images/us.png" });
            Add(new Flag() { Image = "/images/gb.png" });
        }
    }
}
