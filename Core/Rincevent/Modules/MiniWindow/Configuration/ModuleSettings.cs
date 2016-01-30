using System;
using System.Runtime.Serialization;
using System.Reflection;
using Meow.FR.Rincevent.Core.Data;
using System.Drawing;
using System.ComponentModel;
using Meow.FR.Rincevent.Core.Extensibility;
using Meow.FR.Rincevent.Core.Gui.Properties;

namespace Meow.FR.Rincevent.Display.MiniWindow
{
    [GlobalizedObject("Display.MiniWindow")]
    public class ModuleSettings : DisplayModuleSettings
    {
        [GlobalizedCategory("1. Basics")]
        public Point Position
        {
            get { return MiniWindowSettings.Default.Position; }
            set { MiniWindowSettings.Default.Position = value; MiniWindowSettings.Default.Save(); }
        }

        [GlobalizedCategory("1. Basics")]
        public int Timer
        {
            get { return MiniWindowSettings.Default.Timer; }
            set { MiniWindowSettings.Default.Timer = value; MiniWindowSettings.Default.Save(); }
        }

        [GlobalizedCategory("2. Appearance")]
        public double Opacity
        {
            get { return MiniWindowSettings.Default.Opacity; }
            set { MiniWindowSettings.Default.Opacity = value; MiniWindowSettings.Default.Save(); }
        }

        [GlobalizedCategory("2. Appearance")]
        public bool Discreet
        {
            get { return MiniWindowSettings.Default.Discreet; }
            set { MiniWindowSettings.Default.Discreet = value; MiniWindowSettings.Default.Save(); }
        }

        [GlobalizedCategory("2. Appearance")]
        public bool FontAutoSize
        {
            get { return MiniWindowSettings.Default.FontAutoSize; }
            set { MiniWindowSettings.Default.FontAutoSize = value; MiniWindowSettings.Default.Save(); }
        }

        [GlobalizedCategory("2. Appearance")]
        public Size Size
        {
            get { return MiniWindowSettings.Default.Size; }
            set { MiniWindowSettings.Default.Size = value; MiniWindowSettings.Default.Save(); }
        }

        [GlobalizedCategory("3. Advanced")]
        public bool AutoScroll
        {
            get { return MiniWindowSettings.Default.AutoScroll; }
            set { MiniWindowSettings.Default.AutoScroll = value; MiniWindowSettings.Default.Save(); }
        }

        [GlobalizedCategory("3. Advanced")]
        public int AutoScrollTimer
        {
            get { return MiniWindowSettings.Default.AutoScrollTimer; }
            set { MiniWindowSettings.Default.AutoScrollTimer = value; MiniWindowSettings.Default.Save(); }
        }

        [GlobalizedCategory("3. Advanced")]
        public bool TimerRandom
        {
            get { return MiniWindowSettings.Default.TimerRandom; }
            set { MiniWindowSettings.Default.TimerRandom = value; MiniWindowSettings.Default.Save(); }
        }

        [GlobalizedCategory("3. Advanced")]
        public int TimerMinimum
        {
            get { return MiniWindowSettings.Default.TimerMinimum; }
            set
            {
                if (value < MiniWindowSettings.Default.TimerMaximum)
                {
                    MiniWindowSettings.Default.TimerMinimum = value;
                    MiniWindowSettings.Default.Save();
                }
            }
        }

        [GlobalizedCategory("3. Advanced")]
        public int TimerMaximum
        {
            get { return MiniWindowSettings.Default.TimerMaximum; }
            set
            {
                if (value > MiniWindowSettings.Default.TimerMinimum)
                {
                    MiniWindowSettings.Default.TimerMaximum = value;
                    MiniWindowSettings.Default.Save();
                }
            }
        }
    }
}
